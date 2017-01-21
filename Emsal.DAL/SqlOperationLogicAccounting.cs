using Emsal.DAL.CustomObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Emsal.Utility.CustomObjects;
using System.Data;
using Emsal.DAL.SearchObject;

namespace Emsal.DAL
{
    public class SqlOperationLogicAccounting
    {

        public List<ProductionDetail> GetOfferProductionDetailistForMonitoringEVId_OP(OfferProductionDetailSearch opds)
        {

            var result = new List<ProductionDetail>();
            StringBuilder squery = new StringBuilder();
            StringBuilder squeryunion = new StringBuilder();
            var query1 = @" with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au
 ";


            squeryunion.Append(query1);

            var queryadminID = @" WHERE  Id=@addressID ";
            if (opds.adminID != 0)
            {
                squeryunion.Append(queryadminID);
            }
            var query2 = @"  

   UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
  --select ID from cte;
  , RESULTS AS
    (
        SELECT *
            , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn
            , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed
        FROM
		 (

  
		select  table2.*,pc.ProductName as  ProductParentName,adr.fullAddress as personAddress,adr.addressDesc as personAddressDesc,ev.name as userType
 ,fo.name as organizationName,fo.voen,ur.RoleId ,padr.Id as prodcutAdressId
  from(  
 select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id ,FirstTable.ProductCatalogParentID,FirstTable.Email
					  ,FirstTable.Name,FirstTable.Surname,FirstTable.FatherName,FirstTable.birtday,
					  FirstTable.gender,FirstTable.profilePicture,FirstTable.address_Id,
					  FirstTable.PinNumber,FirstTable.adrID,FirstTable.potentialProductsQuantity,
					  FirstTable.userType_eV_ID,FirstTable.personID,FirstTable.productAddress_Id
					   from (   
                     select distinct op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,us.Email,person.Name,person.Surname,
					person.FatherName,person.birtday,person.gender,person.profilePicture,person.address_Id,person.PinNumber
			,adr.Id as adrID,pp.quantity as potentialProductsQuantity,us.userType_eV_ID
			  ,person.Id as personID,op.productAddress_Id
                      from [dbo].[tblOffer_Production] op 
     
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                     left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
                     left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                     left join [dbo].[tblPerson] person on us.Id=person.UserId and person.Status=1 
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                     left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id and ev.Status=1
                     left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1  
					 left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
					 left join  [dbo].[tblPotential_Production] pp on us.Id=pp.user_Id and pp.Status=1
					 
                      where   
                op.monitoring_eV_Id= @monitoring_eV_Id and
        op.Status=1 
                    ) as FirstTable   
                     left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1  
					 left join [dbo].[tblPerson] person on FirstTable.address_Id=person.address_Id and person.Status=1
					 left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
      ) as table2
       left join [dbo].[tblProductCatalog] pc on pc.Id=table2.ProductCatalogParentID
	    left join [dbo].[tblPerson] person on person.address_Id=table2.adrID and person.Status=1
					 left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
					   left join [dbo].[tblEnumValue] ev on table2.userType_eV_ID=ev.Id and ev.Status=1  
					    left join  [dbo].[tblForeign_Organization] fo on table2.user_Id=fo.userId and fo.Status=1
               left join tblUserRole ur on table2.user_Id=ur.UserId and ur.Status=1
			 left  join tblProductAddress padr on table2.productAddress_Id=padr.Id and padr.Status=1
			   join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id 
	   
           
		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
           

            ) as tb where tb.EnumCategoryId=5

";
            var query3 = @"

)  SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ";
            squery.Append(squeryunion.ToString());


            squery.Append(query2);
            var queryRoleID = @" and RoleId=@RoleId";
            var queryName = @" and (Name like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')";
            var queryProductId = @" and product_Id=@product_Id";
            var queryContractId = @" and contractId=@contractId";
            var queryPersonId = @" and personID=@personID";
            if (opds.roleID != 0)
            {
                squery.Append(queryRoleID);
            }
            if (opds.contractID != 0)
            {
                squery.Append(queryContractId);
            }
            if (opds.productID != 0)
            {
                squery.Append(queryProductId);
            }
            if (!String.IsNullOrEmpty(opds.name))
            {
                squery.Append(queryName);
            }
            if (opds.personID != 0)
            {
                squery.Append(queryPersonId);
            }



            squery.Append(query3);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {


                    command.Parameters.AddWithValue("@monitoring_eV_Id", opds.monintoring_eV_Id);

                    command.Parameters.AddWithValue("@page_num", opds.page);

                    command.Parameters.AddWithValue("@page_size", opds.pageSize);

                    command.Parameters.AddWithValue("@Name", (opds.name ?? (object)DBNull.Value));


                    command.Parameters.AddWithValue("@product_Id", opds.productID);

                    // command.Parameters.AddWithValue("@userID", opds.userID);

                    command.Parameters.AddWithValue("@RoleId", opds.roleID);

                    command.Parameters.AddWithValue("@personID", opds.personID);

                    command.Parameters.AddWithValue("@contractId", opds.contractID);
                    command.Parameters.AddWithValue("@addressID", opds.adminID);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionDetail()
                        {

                            productionID = reader.GetInt64OrDefaultValue(0),
                            unitPrice = reader.GetDecimalOrDefaultValue(1),
                            quantity = reader.GetDecimalOrDefaultValue(2),
                            description = reader.GetStringOrEmpty(3),
                            productId = reader.GetInt64OrDefaultValue(4),
                            productName = reader.GetStringOrEmpty(5),
                            Status = reader.GetStringOrEmpty(6),
                            fullAddress = reader.GetStringOrEmpty(7),
                            addressDesc = reader.GetStringOrEmpty(8),
                            enumCategoryId = reader.GetInt64OrDefaultValue(9),
                            enumValueId = reader.GetInt64OrDefaultValue(10),
                            enumValueName = reader.GetStringOrEmpty(11),
                            userId = reader.GetInt64OrDefaultValue(12),
                            email = reader.GetStringOrEmpty(14),
                            name = reader.GetStringOrEmpty(15),
                            surname = reader.GetStringOrEmpty(16),
                            fatherName = reader.GetStringOrEmpty(17),

                            birtday = reader.GetInt64OrDefaultValue(18),
                            gender = reader.GetStringOrEmpty(19),
                            profilPicture = reader.GetStringOrEmpty(20),
                            adress_Id = reader.GetInt64OrDefaultValue(21),
                            pinNumber = reader.GetStringOrEmpty(22),
                            potentialProductQuantity = reader.GetDecimalOrDefaultValue(24),
                            userType_eV_ID = reader.GetInt64OrDefaultValue(25),

                            personID = reader.GetInt64OrDefaultValue(26),
                            productAddressID = reader.GetInt64OrDefaultValue(27),
                            productParentName = reader.GetStringOrEmpty(28),
                            personAdress = reader.GetStringOrEmpty(29),
                            personAdressDesc = reader.GetStringOrEmpty(30),
                            userType = reader.GetStringOrEmpty(31),
                            organizationName = reader.GetStringOrEmpty(32),
                            voen = reader.GetStringOrEmpty(33),







                        });
                    }

                }

                connection.Close();
            }

            return result;
        }
        public Int64 GetOfferProductionDetailistForMonitoringEVId_OPC(OfferProductionDetailSearch opds)
        {
            Int64 count = 0;

            StringBuilder squery = new StringBuilder();
            StringBuilder squeryID = new StringBuilder();
            var query1 = @" ;with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au

";
            var query2 = @"  UNION ALL
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
  select COUNT(*) as Count  from (
  
		select  table2.*,pc.ProductName as  ProductParentName,adr.fullAddress as personAddress,adr.addressDesc as personAddressDesc,ev.name as userType
 ,fo.name as organizationName,fo.voen,ur.RoleId ,padr.Id as prodcutAdressId,ev.enumCategory_enumCategoryId
  from(  
 select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,
				 
                     FirstTable.user_Id ,FirstTable.ProductCatalogParentID,FirstTable.Email
					  ,FirstTable.Name,FirstTable.Surname,FirstTable.FatherName,FirstTable.birtday,
					  FirstTable.gender,FirstTable.profilePicture,FirstTable.address_Id,
					  FirstTable.PinNumber,FirstTable.adrID,FirstTable.potentialProductsQuantity,
					  FirstTable.userType_eV_ID,FirstTable.personID,FirstTable.productAddress_Id,FirstTable.EnumCategoryId
					   from (   
                     select distinct op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,
				
                    pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,us.Email,person.Name,person.Surname,
					person.FatherName,person.birtday,person.gender,person.profilePicture,person.address_Id,person.PinNumber
			,adr.Id as adrID,pp.quantity as potentialProductsQuantity,us.userType_eV_ID
			  ,person.Id as personID,op.productAddress_Id,prc.EnumCategoryId
                      from [dbo].[tblOffer_Production] op 
     
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                     left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
                     left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                     left join [dbo].[tblPerson] person on us.Id=person.UserId and person.Status=1 
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                     left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id and ev.Status=1
                     left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1  
					 left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
					 left join  [dbo].[tblPotential_Production] pp on us.Id=pp.user_Id and pp.Status=1
					 
                      where   
                op.monitoring_eV_Id= @monitoring_eV_Id and
        op.Status=1 
                    ) as FirstTable   
					 left join [dbo].[tblPerson] person on FirstTable.address_Id=person.address_Id and person.Status=1
					 left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
      ) as table2
       left join [dbo].[tblProductCatalog] pc on pc.Id=table2.ProductCatalogParentID
	    left join [dbo].[tblPerson] person on person.address_Id=table2.adrID and person.Status=1
					 left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
					   left join [dbo].[tblEnumValue] ev on table2.userType_eV_ID=ev.Id and ev.Status=1  
					    left join  [dbo].[tblForeign_Organization] fo on table2.user_Id=fo.userId and fo.Status=1
               left join tblUserRole ur on table2.user_Id=ur.UserId and ur.Status=1
			 left  join tblProductAddress padr on table2.productAddress_Id=padr.Id and padr.Status=1
			  left join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id 
	   
           
		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
           

            ) as tb
  where tb.EnumCategoryId=5 ";

            var queryadminID = @"  WHERE Id=@addressID ";
            squeryID.Append(query1);
            if (opds.adminID != 0)
            {
                squeryID.Append(queryadminID);
            }
            squery.Append(squeryID.ToString());
            squery.Append(query2);
            var queryRoleID = @" and RoleId=@RoleId";
            var queryName = @" and (Name like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')";
            var queryProductId = @" and product_Id=@product_Id";
            var queryContractId = @" and contractId=@contractId";
            var queryPersonId = @" and personID=@personID";
            if (opds.roleID != 0)
            {
                squery.Append(queryRoleID);
            }
            if (opds.contractID != 0)
            {
                squery.Append(queryContractId);
            }
            if (opds.productID != 0)
            {
                squery.Append(queryProductId);
            }
            if (!String.IsNullOrEmpty(opds.name))
            {
                squery.Append(queryName);
            }
            if (opds.personID != 0)
            {
                squery.Append(queryPersonId);
            }





            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@monitoring_eV_Id", opds.monintoring_eV_Id);



                    command.Parameters.AddWithValue("@Name", (opds.name ?? (object)DBNull.Value));


                    command.Parameters.AddWithValue("@product_Id", opds.productID);

                    // command.Parameters.AddWithValue("@userID", opds.userID);

                    command.Parameters.AddWithValue("@RoleId", opds.roleID);

                    command.Parameters.AddWithValue("@personID", opds.personID);

                    command.Parameters.AddWithValue("@contractId", opds.contractID);
                    command.Parameters.AddWithValue("@addressID", opds.adminID);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader["Count"]);
                    }

                }
                connection.Close();
            }

            return count;
        }


        public List<ProductionDetail> GetOfferProductionDetailistForStateEVId_OP(OfferProductionDetailSearch ops)
        {
            var result = new List<ProductionDetail>();
            StringBuilder squery = new StringBuilder();
            var query1 = @"WITH RESULTS AS
    (
        SELECT *
            , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn
            , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed
        FROM (
		select table3.* ,pc.ProductName as potentialProduct,fo.name as organizationName,fo.voen from( select table2.*, person.Id as personID, adr.Id as adressId, adr.adminUnit_Id,pp.quantity as potentialProductQunatity,person.Name as personName,person.Surname as surName,person.FatherName
		,person.PinNumber from (select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id ,pc.ProductName as ProductParentName,FirstTable.potentialProduct_Id
					  ,FirstTable.roleId,FirstTable.roleName
					   from (   
                     select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,op.potentialProduct_Id,
					r.Id as roleId,r.Name as roleName

                      from [dbo].[tblOffer_Production] op  
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                     left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id and ev.Status=1
                       left join    tblUserRole ur on us.Id=ur.UserId and ur.Status=1
                  left join [dbo].[tblRole] r on ur.RoleId=r.Id and r.Status=1
                      where    op.Status=1 and r.Id in (11,15)

					  and op.state_eV_Id=@state_eV_Id
                    ) as FirstTable   
                   left   join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1
				   left join [dbo].[tblProductCatalog] pc on pc.Id=FirstTable.ProductCatalogParentID
				   left join [dbo].[tblPotential_Production] pp on pp.Id=FirstTable.potentialProduct_Id
				  )  as table2
				   left join [dbo].[tblPotential_Production] pp on pp.Id=table2.potentialProduct_Id  
					left  join [dbo].[tblPerson] person on table2.user_Id=person.UserId
					left  join [dbo].[tblAddress] adr on person.address_Id=adr.Id
					   
				 ) as table3
				    left join [dbo].[tblProductCatalog] pc  on pc.Id=table3.potentialProduct_Id   and pc.Status=1 
				  left join [dbo].[tblPotential_Production] pp on pp.Id=table3.potentialProduct_Id
				 left join tblForeign_Organization fo on table3.user_Id=fo.userId and fo.Status=1
				 where table3.EnumCategoryId=5
	) as tb
              where tb.EnumCategoryId=5";
            var query2 = @" ) SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC";
            squery.Append(query1);
            var queryroleID = @" and RoleId=@RoleId";
            var queryName = @"and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')";
            var queryProduct = @" and product_Id=@product_Id";
            if (ops.roleID != 0)
            {
                squery.Append(queryroleID);
            }
            if (!string.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);

            }
            if (ops.productID != 0)
            {
                squery.Append(queryProduct);

            }

            squery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {

                    /*  cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = name;*/
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@roleId", ops.roleID);
                    command.Parameters.AddWithValue("@Name", ops.name ?? (object)DBNull.Value);
                    //command.Parameters.AddWithValue("@Name", ops.name);
                    command.Parameters.AddWithValue("@product_Id", ops.productID);

                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionDetail()
                        {
                            productionID = reader.GetInt64OrDefaultValue(0),
                            unitPrice = reader.GetDecimalOrDefaultValue(1),
                            quantity = reader.GetDecimalOrDefaultValue(2),
                            description = reader.GetStringOrEmpty(3),
                            productId = reader.GetInt64OrDefaultValue(4),
                            productName = reader.GetStringOrEmpty(5),
                            Status = reader.GetStringOrEmpty(6),
                            fullAddress = reader.GetStringOrEmpty(7),
                            addressDesc = reader.GetStringOrEmpty(8),
                            enumCategoryId = reader.GetInt64OrDefaultValue(9),
                            enumValueId = reader.GetInt64OrDefaultValue(10),
                            enumValueName = reader.GetStringOrEmpty(11),
                            userId = reader.GetInt64OrDefaultValue(12),
                            productParentName = reader.GetStringOrEmpty(13),
                            roleID = reader.GetInt64OrDefaultValue(15),
                            roleName = reader.GetStringOrEmpty(16),
                            potentialProductQuantity = reader.GetDecimalOrDefaultValue(20),
                            potentialProduct = reader.GetStringOrEmpty(25),
                            organizationName = reader.GetStringOrEmpty(26),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public Int64 GetOfferProductionDetailistForStateEVId_OPC(OfferProductionDetailSearch ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query1 = @"
	select COUNT(*) as Count from(	select table3.* ,pc.ProductName as potentialProduct,fo.name as organizationName,fo.voen from( select table2.*, person.Id as personID, adr.Id as adressId, adr.adminUnit_Id,pp.quantity as potentialProductQunatity,person.Name as personName,person.Surname as surName,person.FatherName
		,person.PinNumber from (select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id ,pc.ProductName as ProductParentName,FirstTable.potentialProduct_Id
					  ,FirstTable.roleId,FirstTable.roleName
					   from (   
                     select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,op.potentialProduct_Id,
					r.Id as roleId,r.Name as roleName

                      from [dbo].[tblOffer_Production] op  
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                     left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id and ev.Status=1
                       left join    tblUserRole ur on us.Id=ur.UserId and ur.Status=1
                  left join [dbo].[tblRole] r on ur.RoleId=r.Id and r.Status=1
                      where    op.Status=1 and r.Id in (11,15)

					  and op.state_eV_Id=@state_eV_Id
					 
                    ) as FirstTable   
                   left   join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1
				   left join [dbo].[tblProductCatalog] pc on pc.Id=FirstTable.ProductCatalogParentID
				   left join [dbo].[tblPotential_Production] pp on pp.Id=FirstTable.potentialProduct_Id
				  )  as table2
				   left join [dbo].[tblPotential_Production] pp on pp.Id=table2.potentialProduct_Id  
					left  join [dbo].[tblPerson] person on table2.user_Id=person.UserId
					left  join [dbo].[tblAddress] adr on person.address_Id=adr.Id
					   
				 ) as table3
				    left join [dbo].[tblProductCatalog] pc  on pc.Id=table3.potentialProduct_Id   and pc.Status=1 
				  left join [dbo].[tblPotential_Production] pp on pp.Id=table3.potentialProduct_Id
				 left join tblForeign_Organization fo on table3.user_Id=fo.userId and fo.Status=1
				 where table3.EnumCategoryId=5
	
            ) as tb
			where tb.EnumCategoryId=5";
            squery.Append(query1);
            var queryroleID = @" and RoleId=@RoleId";
            var queryName = @"and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')";
            var queryProduct = @" and product_Id=@product_Id";
            if (ops.roleID != 0)
            {
                squery.Append(queryroleID);
            }
            if (!string.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);

            }
            if (ops.productID != 0)
            {
                squery.Append(queryProduct);

            }

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@userID", ops.userID);
                    command.Parameters.AddWithValue("@Name", ops.name ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@product_Id", ops.productID);
                    command.Parameters.AddWithValue("@roleId", ops.roleID);
                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader["Count"]);
                    }

                }
                connection.Close();
            }

            return count;
        }
        public List<AnnouncementDetail> GetAnnouncementDetails_Search(OfferProductionDetailSearch ops)
        {
            var result = new List<AnnouncementDetail>();

            var query1 = @"WITH RESULTS AS
    (
        SELECT *
            , ROW_NUMBER() OVER (ORDER BY tb.announcementId DESC) AS rn
            , ROW_NUMBER() OVER (ORDER BY tb.announcementId ASC) AS rn_reversed
        FROM (
		
		
		select distinct announcementId,table1.productID,pc.ProductName as productParentName,table1.ProductName,table1.description from( select an.Id as announcementId,pc.Id as productID,
pc.ProductName,ev.name,pc.ProductCatalogParentID,ev.description from tblAnnouncement an
left join tblProductCatalog pc on an.product_id=pc.Id and pc.Status=1
left join tblEnumValue ev on an.quantity_type_Name=ev.name and ev.Status=1
where an.Status=1) as table1
left join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1 and pc.Status=1
		
				  
				  
				  ) as tb
				  where ProductName like '%'+@Name+'%' or productParentName like '%'+@Name+'%'
            
        ) SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {

                    command.Parameters.AddWithValue("@Name", ops.name == "" ? (object)DBNull.Value : ops.name);
                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    //command.Parameters.AddWithValue("@Name", ops.name);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new AnnouncementDetail()
                        {
                            announcementID = reader.GetInt64OrDefaultValue(0),
                            productId = reader.GetInt64OrDefaultValue(1),
                            parentName = reader.GetStringOrEmpty(2),
                            productName = reader.GetStringOrEmpty(3),
                            description = reader.GetStringOrEmpty(4),



                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<GetOfferProductionDetailistForEValueId> GetOfferProductionDetailistForEValueId_OP1(OfferProductionDetailSearch ops)
        {
            var result = new List<GetOfferProductionDetailistForEValueId>();
            StringBuilder squery = new StringBuilder();
            StringBuilder squeryunion = new StringBuilder();
            var query1 = @" with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au
";

            var query2 = @" UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
  --select ID from cte;
  , RESULTS AS
    (
        SELECT *
            , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn
            , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed
        FROM
		 (
select distinct table1.*,pc.ProductName as productParentName,ev.name as kategoryName from (select op.Id ,op.unit_price,op.quantity,pc.ProductName,pc.ProductCatalogParentID
,pdocument.documentName,pdocument.documentRealName,pdocument.documentUrl,op.product_Id,pdocument.documentSize,
person.Name as personName,person.Surname,person.FatherName,person.gender,person.PinNumber,fo.name as organizationName,fo.voen
,pa.fullAddress,prc.EnumValueId,prc.EnumCategoryId,ur.RoleId,u.userType_eV_ID,op.productAddress_Id,u.Email,person.Id as personId
 from tblOffer_Production op
left join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
left join tblProduct_Document pdocument on pc.Id=pdocument.Product_catalog_Id and pdocument.Status=1
left join tblPerson person on person.UserId=op.user_Id and op.Status=1
left join tblForeign_Organization fo on op.user_Id=fo.userId and fo.Status=1
left join tblProductAddress pa on op.Id=pa.offer_production_id and pa.Status=1
left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
left join tblUserRole ur on op.user_Id=ur.UserId  and ur.Status=1
left join tblUser u on op.user_Id=u.Id and u.Status=1
--left join tblCommunication com on person.Id=com.PersonId and person.Status=1
where op.Status=1 and op.state_eV_Id=@state_eV_Id
)as table1
left join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
left join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1 
 left  join tblProductAddress padr on table1.productAddress_Id=padr.Id and padr.Status=1
			   join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id 
	   
           
		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
           

            ) as tb where tb.EnumCategoryId=5 ";
            var query3 = @"  )  SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";

            var queryadminID = @"  WHERE Id=@addressID ";



            var queryProductId = @" and product_Id=@productID  ";
            var queryRoleId = @" and RoleId=@roleID ";
            var queryName = @" and  (personName like '%'+@name+'%' or Surname like '%'+@name+'%'  or FatherName like '%'+@name+'%')  ";
            //var queryVoen = @" and voen like '%'+@voen+'%' ";//voen=@voen
            var queryPinNumber = @" and  (PinNumber like '%'+@pinNumber+'%' or voen like '%'+@voen+'%' ) ";
            var queryUserType = @" and  userType_eV_ID=@usertypeEvId      ";
            squeryunion.Append(query1);
            if (ops.adminID != 0)
            {
                squeryunion.Append(queryadminID);
            }
            squery.Append(squeryunion.ToString());
            squery.Append(query2);
            if (ops.name != null)
            {
                squery.Append(queryName);
            }
            if (ops.productID != 0)
            {
                squery.Append(queryProductId);
            }
            if (ops.roleID != 0)
            {
                squery.Append(queryRoleId);
            }
            if (ops.usertypeEvId != 0)
            {
                squery.Append(queryUserType);
            }
            if (ops.pinNumber != null || ops.voen != null)
            {
                squery.Append(queryPinNumber);
            }




            squery.Append(query3);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@roleID", ops.roleID);
                    command.Parameters.AddWithValue("@name", ops.name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@productID", ops.productID);
                    command.Parameters.AddWithValue("@voen", ops.voen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@pinNumber", ops.pinNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@usertypeEvId", ops.usertypeEvId);
                    command.Parameters.AddWithValue("@addressID", ops.adminID);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new GetOfferProductionDetailistForEValueId()
                        {
                            productionID = reader.GetInt64OrDefaultValue(0),
                            unitPrice = reader.GetDecimalOrDefaultValue(1),
                            quantity = reader.GetDecimalOrDefaultValue(2),
                            productName = reader.GetStringOrEmpty(3),
                            documentName = reader.GetStringOrEmpty(5),
                            documentRealName = reader.GetStringOrEmpty(6),
                            documentUrl = reader.GetStringOrEmpty(7),
                            productId = reader.GetInt64OrDefaultValue(8),
                            documentSize = reader.GetInt64OrDefaultValue(9),
                            name = reader.GetStringOrEmpty(10),
                            surname = reader.GetStringOrEmpty(11),
                            fatherName = reader.GetStringOrEmpty(12),
                            gender = reader.GetStringOrEmpty(13),
                            pinNumber = reader.GetStringOrEmpty(14),
                            organizationName = reader.GetStringOrEmpty(15),
                            voen = reader.GetStringOrEmpty(16),
                            fullAddress = reader.GetStringOrEmpty(17),
                            email = reader.GetStringOrEmpty(23),
                            personID = reader.GetInt64OrDefaultValue(24),
                            productParentName = reader.GetStringOrEmpty(25),
                            kategoryName = reader.GetStringOrEmpty(26),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public long GetOfferProductionDetailistForEValueId_OPC1(OfferProductionDetailSearch ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            StringBuilder squeryID = new StringBuilder();
            var query1 = @" ;with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au

";
            var query2 = @"  UNION ALL
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
  select COUNT(*) as Count  from (
  
		select distinct table1.*,pc.ProductName as productParentName,ev.name as kategoryName from (select op.Id ,op.unit_price,op.quantity,pc.ProductName,pc.ProductCatalogParentID
,pdocument.documentName,pdocument.documentRealName,pdocument.documentUrl,op.product_Id,pdocument.documentSize,
person.Name as personName,person.Surname,person.FatherName,person.gender,person.PinNumber,fo.name as organizationName,fo.voen
,pa.fullAddress,prc.EnumValueId,prc.EnumCategoryId,ur.RoleId,u.userType_eV_ID,op.productAddress_Id,u.Email,person.Id as personId
 from tblOffer_Production op
left join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
left join tblProduct_Document pdocument on pc.Id=pdocument.Product_catalog_Id and pdocument.Status=1
left join tblPerson person on person.UserId=op.user_Id and op.Status=1
left join tblForeign_Organization fo on op.user_Id=fo.userId and fo.Status=1
left join tblProductAddress pa on op.Id=pa.offer_production_id and pa.Status=1
left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
left join tblUserRole ur on op.user_Id=ur.UserId and ur.Status=1
left join tblUser u on op.user_Id=u.Id and u.Status=1
--left join tblCommunication com on person.Id=com.PersonId and person.Status=1
where op.Status=1 and op.state_eV_Id=@state_eV_Id
)as table1
left join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
left join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1 
 left  join tblProductAddress padr on table1.productAddress_Id=padr.Id and padr.Status=1
			   join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id 
	
	   
           
		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
           

            ) as tb
  where tb.EnumCategoryId=5 ";

            var queryadminID = @"  WHERE Id=@addressID ";
            squeryID.Append(query1);
            if (ops.adminID != 0)
            {
                squeryID.Append(queryadminID);
            }
            squery.Append(squeryID.ToString());
            squery.Append(query2);
            var queryProductId = @" and product_Id=@productID  ";
            var queryRoleId = @" and RoleId=@roleID ";
            var queryName = @" and  (personName like '%'+@name+'%' or Surname like '%'+@name+'%'  or FatherName like '%'+@name+'%')  ";
            //var queryVoen = @" and voen like '%'+@voen+'%' ";//voen=@voen
            var queryPinNumber = @" and  (PinNumber like '%'+@pinNumber+'%' or voen like '%'+@voen+'%' ) ";
            var queryUserType = @" and  userType_eV_ID=@usertypeEvId      ";
            if (ops.name != null)
            {
                squery.Append(queryName);
            }
            if (ops.productID != 0)
            {
                squery.Append(queryProductId);
            }
            if (ops.roleID != 0)
            {
                squery.Append(queryRoleId);
            }
            if (ops.usertypeEvId != 0)
            {
                squery.Append(queryUserType);
            }
            if (ops.pinNumber != null || ops.voen != null)
            {
                squery.Append(queryPinNumber);
            }

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);

                    command.Parameters.AddWithValue("@roleID", ops.roleID);
                    command.Parameters.AddWithValue("@name", ops.name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@productID", ops.productID);
                    command.Parameters.AddWithValue("@voen", ops.voen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@pinNumber", ops.pinNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@usertypeEvId", ops.usertypeEvId);
                    command.Parameters.AddWithValue("@addressID", ops.adminID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader["Count"]);
                    }
                }
                connection.Close();
            }

            return count;
        }
        public List<PersonDetail> GetUserDetailInfoForOffers_OP(GetDemandProductionDetailistForEValueIdSearch ops)
        {
            var result = new List<PersonDetail>();
            StringBuilder squery = new StringBuilder();
            var query1 = @"  with  RESULTS AS
    (
        SELECT *
            , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn
            , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed
        FROM
		 (

 select  person.Id,person.Name,person.Surname,person.FatherName,person.profilePicture,fo.UserId
 ,person.PinNumber,person.gender,fo.name as organizationName,fo.voen,ur.RoleId,op.contractId,op.monitoring_eV_Id,prc.EnumCategoryId
,cnt.Status as contractStatus
 from tblUser usr
 join tblForeign_Organization fo on usr.Id=fo.userId and fo.Status=1
 left join tblPerson person  on person.Id=fo.manager_Id
 left join tblUserRole ur on ur.UserId=usr.ID and ur.Status=1
 left join tblOffer_Production op on usr.Id=op.user_Id and op.Status=1
 left join tblProductionControl prc on prc.Offer_Production_Id=op.Id and prc.Status=1
left join tblContract cnt on person.UserId=cnt.SupplierUserID and cnt.Status=1
 left join tblAddress adr on fo.address_Id=adr.Id
 left  join tblPRM_AdminUnit au on adr.adminUnit_Id  =au.Id 
	        and  au.Id 
		   in (
				
				select table4.Id from 
				(select aunit.Id as userID, aunit.Id ,aunit.Name 
				from [dbo].[tblUser] us,
				[dbo].[tblPRM_ASCBranch] ascbranch,

				[dbo].[tblBranchResponsibility] branchResp,

				[dbo].[tblPRM_AdminUnit] aunit

				 where  us.ASC_ID=ascbranch.Id
				 and ascbranch.Id= branchResp.branchId
	              and branchResp.branchType_eVId=51
				 and (aunit.Id=branchResp.adminUnitId 
				 or aunit.ParentID= branchResp.adminUnitId)
				
                 and us.Id=@userID
			) as table4

			)
           

 where fo.Status=1 and usr.userType_eV_ID=50  and prc.EnumCategoryId=5
 
 union 
  select  person.Id,person.Name,person.Surname,person.FatherName,person.profilePicture,person.UserId
 ,person.PinNumber,person.gender,'' as organizationName, ''as voen,ur.RoleId,op.contractId,op.monitoring_eV_Id,prc.EnumCategoryId,
cnt.Status as contractStatus
 from tblUser usr
 join tblPerson person  on usr.Id=person.UserId
 left join tblUserRole ur on ur.UserId=usr.ID and ur.Status=1
 left join tblOffer_Production op on usr.Id=op.user_Id and op.Status=1
 left join tblProductionControl prc on prc.Offer_Production_Id=op.Id and prc.Status=1
left join tblContract cnt on person.UserId=cnt.SupplierUserID and cnt.Status=1
  left join tblAddress adr on person.address_Id=adr.Id
  left  join tblPRM_AdminUnit au on adr.adminUnit_Id  =au.Id 
	      and  au.Id 
			   in (
				select table4.Id from 
				(select aunit.Id as userID, aunit.Id ,aunit.Name 
				from [dbo].[tblUser] us,
				[dbo].[tblPRM_ASCBranch] ascbranch,

				[dbo].[tblBranchResponsibility] branchResp,

				[dbo].[tblPRM_AdminUnit] aunit

				 where  us.ASC_ID=ascbranch.Id
				 and ascbranch.Id= branchResp.branchId
	              and branchResp.branchType_eVId=51
				 and (aunit.Id=branchResp.adminUnitId 
				 or aunit.ParentID= branchResp.adminUnitId)
				
                and us.Id=@userID
			) as table4

				)
           

 
 where person.Status=1 and usr.Status=1 and usr.userType_eV_ID=26 and prc.EnumCategoryId=5 
   ) as tb where tb.EnumCategoryId=5
   
    ";
            var query2 = @" )  SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY Name,Surname,FatherName --rn ASC  
";
            squery.Append(query1);
            var queryRoleId = @" and RoleId=@RoleId";
            var queryContractID = @" and contractId=@contractId";
            var queryMonitoringId = @"  and monitoring_eV_Id=@monitoring_eV_Id";
            var queryName = @" and (Name like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%')";
            var queryVoen = @"  and (voen like '%'+@voen+'%' or PinNumber Like '%'+@PinNumber+'%')";
            //var queryUserID = @" and userId=@userId";
            //var queryPinNumber = @" and PinNumber=@PinNumber";
            if (ops.roleID != 0)
            {
                squery.Append(queryRoleId);
            }
            if (ops.contractId != 0)
            {
                squery.Append(queryContractID);
            }
            if (ops.Name != null)
            {
                squery.Append(queryName);
            }
            if (ops.monitoring_Ev_Id != 0)
            {
                squery.Append(queryMonitoringId);

            }
            if (ops.voen != null || ops.pinNumber != null)
            {
                squery.Append(queryVoen);
            }
            //if (ops.user_Id != 0)
            //{
            //    squery.Append(queryUserID);
            //}



            squery.Append(query2);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {

                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
                    command.Parameters.AddWithValue("@Name", ops.Name ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@voen", ops.voen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PinNumber", ops.pinNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@contractId", ops.contractId);
                    command.Parameters.AddWithValue("@monitoring_eV_Id", ops.monitoring_Ev_Id);
                    command.Parameters.AddWithValue("@userID", ops.user_Id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new PersonDetail()
                        {
                            personId = reader.GetInt64OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            SurName = reader.GetStringOrEmpty(2),
                            FatherName = reader.GetStringOrEmpty(3),
                            profilePicture = reader.GetStringOrEmpty(4),
                            userId = reader.GetInt64OrDefaultValue(5),
                            pinNumber = reader.GetStringOrEmpty(6),
                            gender = reader.GetStringOrEmpty(7),
                            organizationName = reader.GetStringOrEmpty(8),
                            voen = reader.GetStringOrEmpty(9),
                            contractID = reader.GetInt64OrDefaultValue(11),
                            contractStatus = reader.GetInt64OrDefaultValue(14),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public Int64 GetUserDetailInfoForOffers_OPC(GetDemandProductionDetailistForEValueIdSearch ops)
        {

            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query1 = @"select count(*) as count from
		 (
select  person.Id,person.Name,person.Surname,person.FatherName,person.profilePicture,fo.UserId
 ,person.PinNumber,person.gender,fo.name as organizationName,fo.voen,ur.RoleId,op.contractId,op.monitoring_eV_Id,prc.EnumCategoryId
 from tblUser usr
 join tblForeign_Organization fo on usr.Id=fo.userId and fo.Status=1
 left join tblPerson person  on person.Id=fo.manager_Id
 left join tblUserRole ur on ur.UserId=usr.ID and ur.Status=1
 left join tblOffer_Production op on usr.Id=op.user_Id and op.Status=1
 left join tblProductionControl prc on prc.Offer_Production_Id=op.Id and prc.Status=1

 left join tblAddress adr on fo.address_Id=adr.Id
 left  join tblPRM_AdminUnit au on adr.adminUnit_Id  =au.Id 
	        and  au.Id 
		   in (
				
				select table4.Id from 
				(select aunit.Id as userID, aunit.Id ,aunit.Name 
				from [dbo].[tblUser] us,
				[dbo].[tblPRM_ASCBranch] ascbranch,

				[dbo].[tblBranchResponsibility] branchResp,

				[dbo].[tblPRM_AdminUnit] aunit

				 where  us.ASC_ID=ascbranch.Id
				 and ascbranch.Id= branchResp.branchId
	              and branchResp.branchType_eVId=51
				 and (aunit.Id=branchResp.adminUnitId 
				 or aunit.ParentID= branchResp.adminUnitId)
				
                 and us.Id=@userID
			) as table4

			)
           

 where fo.Status=1 and usr.userType_eV_ID=50  and prc.EnumCategoryId=5
 
 union 
  select  person.Id,person.Name,person.Surname,person.FatherName,person.profilePicture,person.UserId
 ,person.PinNumber,person.gender,'' as organizationName, ''as voen,ur.RoleId,op.contractId,op.monitoring_eV_Id,prc.EnumCategoryId
 from tblUser usr
 join tblPerson person  on usr.Id=person.UserId
 left join tblUserRole ur on ur.UserId=usr.ID and ur.Status=1
 left join tblOffer_Production op on usr.Id=op.user_Id and op.Status=1
 left join tblProductionControl prc on prc.Offer_Production_Id=op.Id and prc.Status=1

  left join tblAddress adr on person.address_Id=adr.Id
  left  join tblPRM_AdminUnit au on adr.adminUnit_Id  =au.Id 
	      and  au.Id 
			   in (
				select table4.Id from 
				(select aunit.Id as userID, aunit.Id ,aunit.Name 
				from [dbo].[tblUser] us,
				[dbo].[tblPRM_ASCBranch] ascbranch,

				[dbo].[tblBranchResponsibility] branchResp,

				[dbo].[tblPRM_AdminUnit] aunit

				 where  us.ASC_ID=ascbranch.Id
				 and ascbranch.Id= branchResp.branchId
	              and branchResp.branchType_eVId=51
				 and (aunit.Id=branchResp.adminUnitId 
				 or aunit.ParentID= branchResp.adminUnitId)
				
                and us.Id=@userID
			) as table4

				)
           

 
 where person.Status=1 and usr.Status=1 and usr.userType_eV_ID=26 and prc.EnumCategoryId=5 
  ) as tb where tb.EnumCategoryId=5 ";


            squery.Append(query1);
            var queryRoleId = @" and RoleId=@RoleId";
            var queryContractID = @" and contractId=@contractId";
            var queryMonitoringId = @"  and monitoring_eV_Id=@monitoring_eV_Id";
            var queryName = @" and (Name like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%')";
            var queryVoen = @"  and (voen like '%'+@voen+'%' or PinNumber Like '%'+@PinNumber+'%')";
            //  var queryUserID = @" and userId=@userId";
            //var queryPinNumber = @" and PinNumber=@PinNumber";
            if (ops.roleID != 0)
            {
                squery.Append(queryRoleId);
            }
            if (ops.contractId != 0)
            {
                squery.Append(queryContractID);
            }
            if (ops.Name != null)
            {
                squery.Append(queryName);
            }
            if (ops.monitoring_Ev_Id != 0)
            {
                squery.Append(queryMonitoringId);

            }
            if (ops.voen != null || ops.pinNumber != null)
            {
                squery.Append(queryVoen);
            }
            //if (ops.user_Id != 0)
            //{
            //    squery.Append(queryUserID);
            //}




            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {

                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
                    command.Parameters.AddWithValue("@Name", ops.Name ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@voen", ops.voen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PinNumber", ops.pinNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@contractId", ops.contractId);
                    command.Parameters.AddWithValue("@monitoring_eV_Id", ops.monitoring_Ev_Id);
                    command.Parameters.AddWithValue("@userID", ops.user_Id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {


                        count = Convert.ToInt32(reader["Count"]);



                    }
                }
                connection.Close();
            }

            return count;
        }
    }
}
