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

  
		 
  select distinct table1.*,pc.ProductName as productParentName,ev.name as kategoryName,adr.fullAddress as personAddress,adr.addressDesc as personAdrDesc
,ev1.name as userType,con.[ContractNumber ],con.Id as contTempID from (select  op.Id ,op.unit_price,op.quantity,
 op.description,op.product_Id, pc.ProductName,pc.ProductCatalogParentID
,
person.Name as personName,person.Surname,person.FatherName,person.gender,person.birtday,person.profilePicture, person.PinNumber,fo.name as organizationName,fo.voen
,pa.fullAddress,pa.addressDesc, prc.EnumValueId,prc.EnumCategoryId,ur.RoleId,u.userType_eV_ID,op.productAddress_Id,u.Email,person.Id as personId,
pp.quantity as potentialQuantity,person.address_Id,person.UserId
 from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1  --and pc.Id in (select Id from cte)

left join tblPerson person on person.UserId=op.user_Id and op.Status=1
left join tblForeign_Organization fo on op.user_Id=fo.userId and fo.Status=1
left join tblProductAddress pa on pa.Id=op.productAddress_Id and pa.Status=1
left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
left join tblUserRole ur on op.user_Id=ur.UserId  and ur.Status=1
left join tblUser u on op.user_Id=u.Id and u.Status=1
 left join tblPotential_Production pp on pp.Id=op.potentialProduct_Id and pp.Status=1
 
where op.Status=1 and op.state_eV_Id=2 and op.monitoring_eV_Id=@monitoring_eV_Id
)as table1
left join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
left join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1 
 left  join tblProductAddress padr on table1.productAddress_Id=padr.Id and padr.Status=1
 left join tblAddress adr on adr.Id=table1.address_Id and adr.Status=1
 left join tblEnumValue ev1 on ev1.Id=table1.userType_eV_ID and ev1.Status=1 
 left join tblContractDetailTemp con on con.offerID=table1.Id and con.Status=1
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
            var queryName = @" and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')";
            var queryProductId = @" and product_Id=@product_Id";
            var queryContractId = @" and contractId=@contractId";
            var queryPersonId = @" and personID=@personID";
            if (opds.roleID != 0)
            {
                squery.Append(queryRoleID);
            }
            //if (opds.contractID != 0)
            //{
            //    squery.Append(queryContractId);
            //}
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

                    command.Parameters.AddWithValue("@Name", opds.name.GetStringOrEmptyData());


                    command.Parameters.AddWithValue("@product_Id", opds.productID);

                    // command.Parameters.AddWithValue("@userID", opds.userID);

                    command.Parameters.AddWithValue("@RoleId", opds.roleID);

                    command.Parameters.AddWithValue("@personID", opds.personID);

                    //command.Parameters.AddWithValue("@contractId", opds.contractID);
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

                            name = reader.GetStringOrEmpty(7),
                            surname = reader.GetStringOrEmpty(8),
                            fatherName = reader.GetStringOrEmpty(9),
                            gender = reader.GetStringOrEmpty(10),
                            birtday = reader.GetInt64OrDefaultValue(11),

                            profilPicture = reader.GetStringOrEmpty(12),
                            pinNumber = reader.GetStringOrEmpty(13),
                            organizationName = reader.GetStringOrEmpty(14),
                            voen = reader.GetStringOrEmpty(15),
                            fullAddress = reader.GetStringOrEmpty(16),
                            addressDesc = reader.GetStringOrEmpty(17),
                            enumValueId = reader.GetInt64OrDefaultValue(18),
                            enumCategoryId = reader.GetInt64OrDefaultValue(19),
                            roleID = reader.GetInt64OrDefaultValue(20),
                            userType_eV_ID = reader.GetInt64OrDefaultValue(21),
                            productAddressID = reader.GetInt64OrDefaultValue(22),
                            email = reader.GetStringOrEmpty(23),
                            personID = reader.GetInt64OrDefaultValue(24),
                            potentialProductQuantity = reader.GetDecimalOrDefaultValue(25),
                            adress_Id = reader.GetInt64OrDefaultValue(26),
                            userId = reader.GetInt64OrDefaultValue(27),
                            productParentName = reader.GetStringOrEmpty(28),
                            enumValueName = reader.GetStringOrEmpty(29),
                            personAdress = reader.GetStringOrEmpty(30),
                            personAdressDesc = reader.GetStringOrEmpty(31),
                            userType = reader.GetStringOrEmpty(32),
                            ContractNumber=reader.GetStringOrEmpty(33),
                            contTempID=reader.GetInt64OrDefaultValue(34),
                          
                        








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
  select COUNT(*) as Count from(
  select distinct table1.*,pc.ProductName as productParentName,ev.name as kategoryName,adr.fullAddress as personAddress,adr.addressDesc as personAdrDesc
,ev1.name as userType,con.[ContractNumber ],con.Id as contTempID from (select  op.Id ,op.unit_price,op.quantity,
 op.description,op.product_Id, pc.ProductName,pc.ProductCatalogParentID
,
person.Name as personName,person.Surname,person.FatherName,person.gender,person.birtday,person.profilePicture, person.PinNumber,fo.name as organizationName,fo.voen
,pa.fullAddress,pa.addressDesc, prc.EnumValueId,prc.EnumCategoryId,ur.RoleId,u.userType_eV_ID,op.productAddress_Id,u.Email,person.Id as personId,
pp.quantity as potentialQuantity,person.address_Id,person.UserId
 from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1  --and pc.Id in (select Id from cte)

left join tblPerson person on person.UserId=op.user_Id and op.Status=1
left join tblForeign_Organization fo on op.user_Id=fo.userId and fo.Status=1
left join tblProductAddress pa on pa.Id=op.productAddress_Id and pa.Status=1
left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
left join tblUserRole ur on op.user_Id=ur.UserId  and ur.Status=1
left join tblUser u on op.user_Id=u.Id and u.Status=1
 left join tblPotential_Production pp on pp.Id=op.potentialProduct_Id and pp.Status=1
 
where op.Status=1 and op.state_eV_Id=2 and op.monitoring_eV_Id=@monitoring_eV_Id
)as table1
left join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
left join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1 
 left  join tblProductAddress padr on table1.productAddress_Id=padr.Id and padr.Status=1
 left join tblAddress adr on adr.Id=table1.address_Id and adr.Status=1
 left join tblEnumValue ev1 on ev1.Id=table1.userType_eV_ID and ev1.Status=1 
 left join tblContractDetailTemp con on con.offerID=table1.Id and con.Status=1
			   join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id 
	   
		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
           
		   	 

            ) as tb where tb.EnumCategoryId=5 ";

            var queryadminID = @"  WHERE Id=@addressID ";
            squeryID.Append(query1);
            if (opds.adminID != 0)
            {
                squeryID.Append(queryadminID);
            }
            squery.Append(squeryID.ToString());
            squery.Append(query2);
            var queryRoleID = @" and RoleId=@RoleId";
            var queryName = @" and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')";
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



                    command.Parameters.AddWithValue("@Name", (opds.name.GetStringOrEmptyData()));


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
		select distinct table1.*,pc.ProductName as productParentName,ev.name as kategoryName,pc1.ProductName as potentialProduct,
pp.quantity as potentialQUnatity from (select op.Id ,op.unit_price,op.quantity,
op.description,op.product_Id, pc.ProductName,pc.ProductCatalogParentID
,ev.name as Status,
person.Name as personName,person.Surname,person.FatherName,person.gender,person.PinNumber,fo.name as organizationName,fo.voen
, pa.fullAddress,pa.addressDesc,prc.EnumValueId,prc.EnumCategoryId,ur.RoleId,u.userType_eV_ID,op.productAddress_Id,u.Email,person.Id as personId
,r.Name as roleName,op.potentialProduct_Id,op.user_Id,pa.adminUnit_Id
 from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1  --and pc.Id in (select Id from cte)

left join tblPerson person on person.UserId=op.user_Id and op.Status=1
left join tblForeign_Organization fo on op.user_Id=fo.userId and fo.Status=1
left join tblProductAddress pa on op.productAddress_Id=pa.Id  and pa.Status=1
left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
left join tblUserRole ur on op.user_Id=ur.UserId  and ur.Status=1
left join tblUser u on op.user_Id=u.Id and u.Status=1
  left join [dbo].[tblRole] r on ur.RoleId=r.Id and r.Status=1
   left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id and ev.Status=1
--left join tblCommunication com on person.Id=com.PersonId and person.Status=1
where op.Status=1 and op.state_eV_Id=@state_eV_Id --and r.Id in(11,15)
)as table1
left join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
left join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1 
 left  join tblProductAddress padr on table1.productAddress_Id=padr.Id and padr.Status=1
 left join [dbo].[tblProductCatalog] pc1  on pc.Id=table1.potentialProduct_Id   and pc1.Status=1 
	 left join [dbo].[tblPotential_Production] pp on pp.Id=table1.potentialProduct_Id	and pp.Status=1
	    join tblPRM_AdminUnit au on au.Id=table1.adminUnit_Id and au.Status=1

            ) as tb where tb.EnumCategoryId=5 ";
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
            var queryName = @" and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')";
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
                    command.Parameters.AddWithValue("@Name", ops.name.GetStringOrEmptyData());
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
                            Status = reader.GetStringOrEmpty(7),
                            organizationName = reader.GetStringOrEmpty(13),
                            voen=reader.GetStringOrEmpty(14),

                            fullAddress = reader.GetStringOrEmpty(15),
                            addressDesc = reader.GetStringOrEmpty(16),
                            enumValueId = reader.GetInt64OrDefaultValue(17),
                            enumCategoryId = reader.GetInt64OrDefaultValue(18),
                            roleID = reader.GetInt64OrDefaultValue(19),
                            roleName = reader.GetStringOrEmpty(24),
                            userId = reader.GetInt64OrDefaultValue(26),
                            productParentName = reader.GetStringOrEmpty(28),
                            enumValueName = reader.GetStringOrEmpty(29),
                            potentialProduct = reader.GetStringOrEmpty(30),
                          
                           
                           
                            potentialProductQuantity = reader.GetDecimalOrDefaultValue(31),
                          
                           


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
	select COUNT(*) as Count from(	select distinct table1.*,pc.ProductName as productParentName,ev.name as kategoryName,pc1.ProductName as potentialProduct,
pp.quantity as potentialQUnatity from (select op.Id ,op.unit_price,op.quantity,
op.description,op.product_Id, pc.ProductName,pc.ProductCatalogParentID
,ev.name as Status,
person.Name as personName,person.Surname,person.FatherName,person.gender,person.PinNumber,fo.name as organizationName,fo.voen
, pa.fullAddress,pa.addressDesc,prc.EnumValueId,prc.EnumCategoryId,ur.RoleId,u.userType_eV_ID,op.productAddress_Id,u.Email,person.Id as personId
,r.Name as roleName,op.potentialProduct_Id,op.user_Id,pa.adminUnit_Id
 from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1  --and pc.Id in (select Id from cte)

left join tblPerson person on person.UserId=op.user_Id and op.Status=1
left join tblForeign_Organization fo on op.user_Id=fo.userId and fo.Status=1
left join tblProductAddress pa on op.productAddress_Id=pa.Id  and pa.Status=1
left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
left join tblUserRole ur on op.user_Id=ur.UserId  and ur.Status=1
left join tblUser u on op.user_Id=u.Id and u.Status=1
  left join [dbo].[tblRole] r on ur.RoleId=r.Id and r.Status=1
   left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id and ev.Status=1
--left join tblCommunication com on person.Id=com.PersonId and person.Status=1
where op.Status=1 and op.state_eV_Id=@state_eV_Id --and r.Id in(11,15)
)as table1
left join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
left join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1 
 left  join tblProductAddress padr on table1.productAddress_Id=padr.Id and padr.Status=1
 left join [dbo].[tblProductCatalog] pc1  on pc.Id=table1.potentialProduct_Id   and pc1.Status=1 
	 left join [dbo].[tblPotential_Production] pp on pp.Id=table1.potentialProduct_Id	and pp.Status=1
	     join tblPRM_AdminUnit au on au.Id=table1.adminUnit_Id and au.Status=1

            ) as tb where tb.EnumCategoryId=5 ";
            squery.Append(query1);
            var queryroleID = @" and RoleId=@RoleId";
            var queryName = @" and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')";
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
                 //   command.Parameters.AddWithValue("@userID", ops.userID);
                 command.Parameters.AddWithValue("@Name", ops.name.GetStringOrEmptyData());

                    command.Parameters.AddWithValue("@product_Id", ops.productID);
                    command.Parameters.AddWithValue("@roleId", ops.roleID);
                    // command.Parameters.AddWithValue("@page_num", ops.page);
                    //command.Parameters.AddWithValue("@page_size", ops.pageSize);
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
            StringBuilder squery = new StringBuilder();
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
				 
            
        ";
            var product = @"  where ProductName like '%'+@Name+'%' or productParentName like '%'+@Name+'%'";
            squery.Append(query1);
            if (!String.IsNullOrEmpty(ops.productName))
            {
                squery.Append(product);
            }
            var query2 = @"  ) SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC";
            squery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {

                    command.Parameters.AddWithValue("@Name", ops.productName.GetStringOrEmptyData());
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
        public Int64 GetAnnouncementDetails_Search_OPC(OfferProductionDetailSearch ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query1 = @"select COUNT(*) as count
        FROM (
		
		
		select distinct announcementId,table1.productID,pc.ProductName as productParentName,table1.ProductName,table1.description from( select an.Id as announcementId,pc.Id as productID,
pc.ProductName,ev.name,pc.ProductCatalogParentID,ev.description from tblAnnouncement an
left join tblProductCatalog pc on an.product_id=pc.Id and pc.Status=1
left join tblEnumValue ev on an.quantity_type_Name=ev.name and ev.Status=1
where an.Status=1) as table1
left join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1 and pc.Status=1
		
				  
				  
				  ) as tb
				 
            
        ";
            var product = @"  where ProductName like '%'+@Name+'%' or productParentName like '%'+@Name+'%'";
            squery.Append(query1);
            if (!String.IsNullOrEmpty(ops.productName))
            {
                squery.Append(product);
            }

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@Name", ops.productName.GetStringOrEmptyData());
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
        
        public List<GetOfferProductionDetailistForEValueId> GetOfferProductionDetailistForEValueId_OP1(OfferProductionDetailSearch ops)
        {
            var result = new List<GetOfferProductionDetailistForEValueId>();
            StringBuilder squery = new StringBuilder();
            StringBuilder squeryunion = new StringBuilder();
            StringBuilder squeryproduct = new StringBuilder();
            var queryPro1 = @" with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblProductCatalog au
 

  
  
  ";
            var queryProduct = @" where Id=@productID";

            var queryPro2 = @" UNION ALL 
   SELECT au.Id

  FROM dbo.tblProductCatalog au  JOIN cte c ON au.ProductCatalogParentID = c.Id) ";
            var query1 = @" , cte1(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au
";

            var query2 = @" UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte1 c ON au.parentId = c.Id
  
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
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1  and pc.Id in (select Id from cte)
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
							select Id from cte1
							 		)
           

            ) as tb where tb.EnumCategoryId=5  ";
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



            //   var queryProductId = @" and product_Id=@productID  ";
            var queryRoleId = @" and RoleId=@roleID ";
            var queryName = @" and  (personName like '%'+@name+'%' or Surname like '%'+@name+'%'  or FatherName like '%'+@name+'%')  ";
            //var queryVoen = @" and voen like '%'+@voen+'%' ";//voen=@voen
            var queryPinNumber = @" and  (PinNumber like '%'+@pinNumber+'%' or voen like '%'+@voen+'%' ) ";
            var queryUserType = @" and  userType_eV_ID=@usertypeEvId      ";
            squeryunion.Append(query1);
            squeryproduct.Append(queryPro1);
            if (ops.productID != 0)
            {
                squeryproduct.Append(queryProduct);
            }
            squeryproduct.Append(queryPro2);
            squery.Append(squeryproduct.ToString());
            if (ops.adminID != 0)
            {
                squeryunion.Append(queryadminID);
            }
            squery.Append(squeryunion.ToString());
            squery.Append(query2);
            if (!String.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);
            }

            if (ops.roleID != 0)
            {
                squery.Append(queryRoleId);
            }
            if (ops.usertypeEvId != 0)
            {
                squery.Append(queryUserType);
            }
            if (!String.IsNullOrEmpty(ops.pinNumber) || !String.IsNullOrEmpty(ops.voen))
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
                    command.Parameters.AddWithValue("@name", ops.name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@productID", ops.productID);
                    command.Parameters.AddWithValue("@voen", ops.voen.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@pinNumber", ops.pinNumber.GetStringOrEmptyData());
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

        public Int64 GetOfferProductionDetailistForEValueId_OPC1(OfferProductionDetailSearch ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            StringBuilder squeryunion = new StringBuilder();
            StringBuilder squeryproduct = new StringBuilder();
            var queryPro1 = @" with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblProductCatalog au
 

  
  
  ";
            var queryProduct = @" where Id=@productID";

            var queryPro2 = @" UNION ALL 
   SELECT au.Id

  FROM dbo.tblProductCatalog au  JOIN cte c ON au.ProductCatalogParentID = c.Id) ";
            var query1 = @" , cte1(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au
";

            var query2 = @" UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte1 c ON au.parentId = c.Id
  
  )
   select COUNT(*) as Count  from (
  
		select distinct table1.*,pc.ProductName as productParentName,ev.name as kategoryName from (select op.Id ,op.unit_price,op.quantity,pc.ProductName,pc.ProductCatalogParentID
,pdocument.documentName,pdocument.documentRealName,pdocument.documentUrl,op.product_Id,pdocument.documentSize,
person.Name as personName,person.Surname,person.FatherName,person.gender,person.PinNumber,fo.name as organizationName,fo.voen
,pa.fullAddress,prc.EnumValueId,prc.EnumCategoryId,ur.RoleId,u.userType_eV_ID,op.productAddress_Id,u.Email,person.Id as personId
 from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1 and pc.Id in (select Id from cte)
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
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
left join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1 
 left  join tblProductAddress padr on table1.productAddress_Id=padr.Id and padr.Status=1
			   join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id 
	
	   
           
		   	   and  au.Id 
			   in (
							select Id from cte1
							 		)
           

            ) as tb
  where tb.EnumCategoryId=5  ";


            var queryadminID = @"  WHERE Id=@addressID ";




            var queryRoleId = @" and RoleId=@roleID ";
            var queryName = @" and  (personName like '%'+@name+'%' or Surname like '%'+@name+'%'  or FatherName like '%'+@name+'%')  ";
            //var queryVoen = @" and voen like '%'+@voen+'%' ";//voen=@voen
            var queryPinNumber = @" and  (PinNumber like '%'+@pinNumber+'%' or voen like '%'+@voen+'%' ) ";
            var queryUserType = @" and  userType_eV_ID=@usertypeEvId      ";

            squeryunion.Append(query1);
            squeryproduct.Append(queryPro1);
            if (ops.productID != 0)
            {
                squeryproduct.Append(queryProduct);
            }
            squeryproduct.Append(queryPro2);
            squery.Append(squeryproduct.ToString());
            if (ops.adminID != 0)
            {
                squeryunion.Append(queryadminID);
            }
            squery.Append(squeryunion.ToString());
            squery.Append(query2);
            if (!String.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);
            }

            if (ops.roleID != 0)
            {
                squery.Append(queryRoleId);
            }
            if (ops.usertypeEvId != 0)
            {
                squery.Append(queryUserType);
            }
            if (!String.IsNullOrEmpty(ops.pinNumber) || !String.IsNullOrEmpty(ops.voen))
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
                    command.Parameters.AddWithValue("@name", ops.name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@productID", ops.productID);
                    command.Parameters.AddWithValue("@voen", ops.voen.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@pinNumber", ops.pinNumber.GetStringOrEmptyData());
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
        public List<PersonDetail> GetUserDetailInfoForOffers_OP(GetDemandProductionDetailistForEValueIdSearch1 ops)
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
 from tblUser usr
left join tblForeign_Organization fo on usr.Id=fo.userId and fo.Status=1
 left join tblPerson person  on person.Id=fo.manager_Id
 left join tblUserRole ur on ur.UserId=usr.ID and ur.Status=1
 left join tblOffer_Production op on usr.Id=op.user_Id and op.Status=1
 left join tblProductionControl prc on prc.Offer_Production_Id=op.Id and prc.Status=1
left join tblContract cnt on person.UserId=cnt.SupplierUserID and cnt.Status=1
 left join tblAddress adr on fo.address_Id=adr.Id
 left  join tblPRM_AdminUnit au on adr.adminUnit_Id =au.Id 
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
left join tblPerson person  on usr.Id=person.UserId and  person.Status=1
 left join tblUserRole ur on ur.UserId=usr.ID and ur.Status=1
 left join tblOffer_Production op on usr.Id=op.user_Id and op.Status=1
 left join tblProductionControl prc on prc.Offer_Production_Id=op.Id and prc.Status=1
left join tblContract cnt on person.UserId=cnt.AgentUserID and cnt.Status=1
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
           

 
 where  usr.Status=1 and usr.userType_eV_ID=26 and prc.EnumCategoryId=5 
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
            var queryContractStatus = @" and tb.contractId>0";
            var queryContractStatus1 = @" and (contractId is null or contractId=0)";
            if (ops.contractStatus == false)
            {
                squery.Append(queryContractStatus1);
            }
            else  if (ops.contractStatus == true)
            {
                squery.Append(queryContractStatus);
            }
            // var queryCStatus = @"  and contractStatus=@contractStatus";
            //var queryUserID = @" and userId=@userId";
            //var queryPinNumber = @" and PinNumber=@PinNumber";
            if (ops.roleID != 0)
            {
                squery.Append(queryRoleId);
            }

            if (!String.IsNullOrEmpty(ops.Name))
            {
                squery.Append(queryName);
            }
            if (ops.monitoring_Ev_Id != 0)
            {
                squery.Append(queryMonitoringId);

            }
            if (!String.IsNullOrEmpty(ops.voen) || !String.IsNullOrEmpty(ops.pinNumber))
            {
                squery.Append(queryVoen);
            }

            if (ops.contractId != 0)
            {
                squery.Append(queryContractID);
            }

            squery.Append(query2);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {

                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
                    command.Parameters.AddWithValue("@Name", ops.Name.GetStringOrEmptyData());

                    command.Parameters.AddWithValue("@voen", ops.voen.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@PinNumber", ops.pinNumber.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@contractId", ops.contractId);
                    command.Parameters.AddWithValue("@monitoring_eV_Id", ops.monitoring_Ev_Id);
                    command.Parameters.AddWithValue("@userID", ops.user_Id);

                    // command.Parameters.Add(ops.contractStatus);

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
                            // contractStatus = reader.GetInt64OrDefaultValue(14),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public Int64 GetUserDetailInfoForOffers_OPC(GetDemandProductionDetailistForEValueIdSearch1 ops)
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
  left join tblPRM_AdminUnit au on adr.adminUnit_Id  =au.Id 
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
   left join tblPRM_AdminUnit au on adr.adminUnit_Id  =au.Id 
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
            var queryContractStatus = @" and tb.contractId>0";
            var queryContractStatus1 = @" and  (contractId is null or contractId=0) ";
            if (ops.contractStatus == true)
            {
                squery.Append(queryContractStatus);
            }
          else  if (ops.contractStatus == false)
            {
                squery.Append(queryContractStatus1);
            }
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
            if (!String.IsNullOrEmpty(ops.Name))
            {
                squery.Append(queryName);
            }
            if (ops.monitoring_Ev_Id != 0)
            {
                squery.Append(queryMonitoringId);

            }
            if (!String.IsNullOrEmpty(ops.voen) || !String.IsNullOrEmpty(ops.pinNumber))
            {
                squery.Append(queryVoen);
            }
            if (ops.contractId != 0)
            {
                squery.Append(queryContractID);
            }

            if (ops.contractId != 0)
            {
                squery.Append(queryContractID);
            }





            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {

                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
                    command.Parameters.AddWithValue("@Name", ops.Name.GetStringOrEmptyData());

                    command.Parameters.AddWithValue("@voen", ops.voen.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@PinNumber", ops.pinNumber.GetStringOrEmptyData());
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
        public List<ForeignOrganization> GetGovermentOrganisatinByAdminID(string adminIDList)
        {
            var result = new List<ForeignOrganization>();
            StringBuilder squery = new StringBuilder();
            StringBuilder squeryUnion = new StringBuilder();

            var query1 = @" 

DECLARE @list NVARCHAR(MAX)
SET @list =@adminUNItID;

DECLARE @pos INT
DECLARE @nextpos INT
DECLARE @valuelen INT
DECLARE @tbl TABLE (number int NOT NULL)

SELECT @pos = 0, @nextpos = 1;

WHILE @nextpos > 0
BEGIN
    SELECT @nextpos = charindex(',', @list, @pos + 1)
    SELECT @valuelen = CASE WHEN @nextpos > 0
                            THEN @nextpos
                            ELSE len(@list) + 1
                        END - @pos - 1
    INSERT @tbl (number)
        VALUES (convert(int, substring(@list, @pos + 1, @valuelen)))
    SELECT @pos = @nextpos;
END
;with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au  ";
            squeryUnion.Append(query1);
            var queryadmin = @" where au.Id in (SELECT number FROM @tbl)";
            if (!string.IsNullOrEmpty(adminIDList))
            {
                squeryUnion.Append(queryadmin);
            }
            var query2 = @" 
      UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )select fo.Id, fo.name as organizationName,pa.adminUnit_Id from tblForeign_Organization  fo
 join tblAddress pa on fo.address_Id=pa.Id and pa.Status=1

 join tblPRM_AdminUnit au on pa.adminUnit_Id=au.Id and au.Status=1

		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
where fo.Status=1";
            squery.Append(squeryUnion.ToString());

            //   var queryAdminUNItList = @" and  au.Id in (SELECT number FROM @tbl) ";
            squery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@adminUNItID", adminIDList.GetStringOrEmptyData());

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ForeignOrganization()
                        {
                            ID = reader.GetInt64OrDefaultValue(0),
                            organizationName = reader.GetStringOrEmpty(1),

                        });
                    }


                    connection.Close();
                }

                return result;
            }
        }
        public List<AdminUnitRegion> GetPRM_AdminUnitByAdminID(Int64 adminID)
        {
            var result = new List<AdminUnitRegion>();


            var query = @" with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au  
      UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )select fo.Id, fo.name as organizationName,pa.adminUnit_Id from tblForeign_Organization  fo
 join tblAddress pa on fo.address_Id=pa.Id and pa.Status=1

 join tblPRM_AdminUnit au on pa.adminUnit_Id=au.Id and au.Status=1

		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
where fo.Status=1";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@adminID", adminID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new AdminUnitRegion()
                        {

                            ID = reader.GetInt64OrDefaultValue(2)
                        });
                    }


                    connection.Close();
                }

                return result;
            }
        }


        public List<DemanProductionGroup> GetTotalDemandOffers(DemandOfferProductsSearch ops)
        {
            var result = new List<DemanProductionGroup>();
            StringBuilder squery = new StringBuilder();
            var query = @" select distinct table1.*,pc.ProductName as parentName,
pdocument.documentUrl,pdocument.documentName from (
select  
op.product_Id,pc.ProductCatalogParentID,pc.ProductName,
SUM(quantity) as toplam,ev.name as kategoryName
,prc.EnumValueId ,pc.productCode from tblDemand_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 left join tblProductPrice price on op.product_Id=price.productId and price.Status=1 and price.year=2016 and price.partOfYear=4
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 join tblEnumValue ev on prc.EnumValueId=ev.Id  and ev.Status=1 
where op.Status=1 --and op.product_Id=250 
and ev.enumCategory_enumCategoryId=5
and op.state_eV_Id=2
--and op.product_Id=334
group by op.product_Id,pc.ProductCatalogParentID,pc.ProductName,price.unit_price,
ev.name,pc.productCode
,prc.EnumValueId  
) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 left join tblProduct_Document pdocument on table1.product_Id=pdocument.Product_catalog_Id and pdocument.Status=1
";
            var queryproduct = @" where product_Id=@product_Id";
            var query2 = @" order by parentName, ProductName
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY";

            squery.Append(query);
            if (ops.productId != 0)
            {
                squery.Append(queryproduct);
            }
            squery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@product_Id", ops.productId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemanProductionGroup()
                        {
                            productId = reader.GetInt64OrDefaultValue(0),
                            productName = reader.GetStringOrEmpty(2),
                            totalQuantity = reader.GetDecimalOrDefaultValue(3),
                           
                            enumValueName = reader.GetStringOrEmpty(4),
                            productCode = reader.GetStringOrEmpty(6),
                            productParentName = reader.GetStringOrEmpty(7),
                           
                            documentUrl = reader.GetStringOrEmpty(8),
                            documentName = reader.GetStringOrEmpty(9),





                        });
                    }


                    connection.Close();
                }

                return result;
            }
        }

        public Int64 GetTotalDemandOffers_OPC(DemandOfferProductsSearch ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            //var query1 = @"";
            var query = @"select Count(*) as count from(select distinct table1.*,pc.ProductName as parentName,
pdocument.documentUrl,pdocument.documentName from (
select  
op.product_Id,pc.ProductCatalogParentID,pc.ProductName,
SUM(quantity) as toplam,ev.name as kategoryName
,prc.EnumValueId  from tblDemand_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 left join tblProductPrice price on op.product_Id=price.productId and price.Status=1 and price.year=2016 and price.partOfYear=4
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 join tblEnumValue ev on prc.EnumValueId=ev.Id  and ev.Status=1 
where op.Status=1 --and op.product_Id=250 
and ev.enumCategory_enumCategoryId=5
and op.state_eV_Id=2
--and op.product_Id=334
group by op.product_Id,pc.ProductCatalogParentID,pc.ProductName,price.unit_price,
ev.name
,prc.EnumValueId  

) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 left join tblProduct_Document pdocument on pc.Id=pdocument.Product_catalog_Id and pdocument.Status=1
--join tblOffer_Production op on op.product_Id=table1.product_Id and op.Status=1
) as tb
";
            squery.Append(query);
            var queryproduct = @" where tb.product_Id=@product_Id";

            if (ops.productId != 0)
            {
                squery.Append(queryproduct);
            }


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@product_Id", ops.productId);

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
        public List<DemanOfferProduction> GetTotalOffersbyProductID(Int64 productID, DemandOfferProductsSearch ops)
        {
            var result = new List<DemanOfferProduction>();
            StringBuilder squery = new StringBuilder();
            StringBuilder squery1 = new StringBuilder();
            var query = @"  select distinct * from(select distinct table1.Id,table1.product_Id,table1.ProductName,table1.quantity,table1.total_price,table1.personName,table1.Surname,
table1.FatherName,table1.organisationName,table1.PinNumber,table1.voen,table1.userType_eV_ID
,pc.ProductName as parentName,ev.name as kategoryName--,price.unit_price
,table1.roleDesc,table1.TaxexType, table1.contractID,table1.price/table1.count as unitPriceCalndar,table1.RoleId,table1.edvStatus
 from (
select op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName
,op.quantity,op.total_price
,r.Name as roleName,r.Description as roleDesc,ur.RoleId,u.userType_eV_ID,
person.Id as personID,
person.Name as personName, person.Surname,
' ' as voen,' ' as organisationName,
prc.EnumValueId ,
cntr.ContractStartDate,person.PinNumber,person.FatherName, op.contractId as contractID,
 (select CASE u.TaxexType
     WHEN 1 THEN N'ƏDV ödəyicisi'
	WHEN 0 THEN N'Sadələşmiş vergi ödəyicisi'
    WHEN 4 THEN N'6-8 % sadələşmiş vergi ödəyicisi'
    ELSE N'Məlumat Mövcud deyil'
	end ) as TaxexType,SUM(pcal.price) as price,COUNT(pcal.price) as count,pc.edvStatus
from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 join tblUserRole ur on op.user_Id=ur.UserId and ur.Status=1
 join tblRole r on r.Id=ur.RoleId and r.Status=1
 join tblUser u on u.Id=op.user_Id and u.Status=1
 join tblProductionCalendar pcal on pcal.offer_Id=op.Id and pcal.Status=1
 left join tblPerson person on person.UserId=op.user_Id and person.Status=1
left join tblContract cntr on op.user_Id=cntr.SupplierUserID and cntr.Status=1
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 -- left join tblCommunication com on com.PersonId=person.ID and com.Status=1 and com.comType=10120
where op.Status=1 and u.userType_eV_ID=26 and op.state_eV_Id=2
group by op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName,op.quantity,op.total_price,r.Name,
r.Description,ur.RoleId,u.userType_eV_ID,person.Id,person.Name,person.Surname,prc.EnumValueId,ContractStartDate,person.PinNumber,
person.FatherName,op.contractId,u.TaxexType,pc.edvStatus
) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1
--join tblProductionCalendar pcal on pcal.offer_Id=table1.Id and pcal.Status=1 
  where ev.enumCategory_enumCategoryId=5
 ";
            var queryUnion = @" union all
 select distinct table1.Id,table1.product_Id,table1.ProductName,table1.quantity,table1.total_price,table1.personName,table1.Surname,
table1.FatherName,table1.organisationName,table1.PinNumber,table1.voen,table1.userType_eV_ID,pc.ProductName as parentName
,ev.name as kategoryName,--price.unit_price,
table1.roleDesc
,table1.TaxexType, table1.contractID,table1.price/table1.count as unitPriceCalndar,table1.RoleId,table1.edvStatus
 -- , com.communication as phoneNumber
  from (
select op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName
,op.quantity,op.total_price,
r.Name as roleName,r.Description as roleDesc,ur.RoleId,u.userType_eV_ID,

person.Id as personID,
person.Name as personName, person.Surname,
fo.voen,fo.name as organisationName,
prc.EnumValueId  ,
cntr.ContractStartDate,person.PinNumber,person.FatherName, op.contractId as contractID,
(select CASE u.TaxexType
     WHEN 1 THEN N'ƏDV ödəyicisi'
	WHEN 0 THEN N'Sadələşmiş vergi ödəyicisi'
    WHEN 4 THEN N'6-8 % sadələşmiş vergi ödəyicisi'
    ELSE N'Məlumat Mövcud deyil'
	end ) as TaxexType,SUM(pcal.price) as price,COUNT(pcal.price) as count,pc.edvStatus
from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 join tblUserRole ur on op.user_Id=ur.UserId and ur.Status=1
 join tblRole r on r.Id=ur.RoleId and r.Status=1
 join tblUser u on u.Id=op.user_Id and u.Status=1

left join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
left join tblPerson person on person.Id=fo.manager_Id and person.Status=1
left join tblContract cntr on op.user_Id=cntr.SupplierUserID and cntr.Status=1
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 join tblProductionCalendar pcal on pcal.offer_Id=op.Id and pcal.Status=1
-- left join tblCommunication com on com.PersonId=person.id and com.Status=1 and com.comType=10120
where op.Status=1 and u.userType_eV_ID=50 and op.state_eV_Id=2
group by op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName,op.quantity,op.total_price,r.Name,
r.Description,ur.RoleId,u.userType_eV_ID,person.Id,person.Name,person.Surname,prc.EnumValueId,ContractStartDate,fo.voen,fo.name,person.PinNumber,
person.FatherName,op.contractId,u.TaxexType,pc.edvStatus
) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1
--join tblProductionCalendar pcal on pcal.offer_Id=table1.Id and pcal.Status=1 
 where ev.enumCategory_enumCategoryId=5
 
 
"; var query1Calen = @"select distinct * from(select distinct table1.Id,table1.product_Id,table1.ProductName,table1.quantity,table1.total_price,table1.personName,table1.Surname,
table1.FatherName,table1.organisationName,table1.PinNumber,table1.voen,table1.userType_eV_ID
,pc.ProductName as parentName,ev.name as kategoryName--,price.unit_price
,table1.roleDesc,table1.TaxexType, table1.contractID,table1.price/table1.count as unitPriceCalndar,table1.RoleId,
table1.edvStatus,table1.months_eV_Id
 from (
select op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName
,pcal.quantity,pcal.quantity*pcal.price as total_price
,r.Name as roleName,r.Description as roleDesc,ur.RoleId,u.userType_eV_ID,
person.Id as personID,
person.Name as personName, person.Surname,
' ' as voen,' ' as organisationName,
prc.EnumValueId ,
cntr.ContractStartDate,person.PinNumber,person.FatherName, op.contractId as contractID,
 (select CASE u.TaxexType
     WHEN 1 THEN N'ƏDV ödəyicisi'
	WHEN 0 THEN N'Sadələşmiş vergi ödəyicisi'
    WHEN 4 THEN N'6-8 % sadələşmiş vergi ödəyicisi'
    ELSE N'Məlumat Mövcud deyil'
	end ) as TaxexType,SUM(pcal.price) as price,COUNT(pcal.price) as count,pcal.months_eV_Id,pc.edvStatus
from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 join tblUserRole ur on op.user_Id=ur.UserId and ur.Status=1
 join tblRole r on r.Id=ur.RoleId and r.Status=1
 join tblUser u on u.Id=op.user_Id and u.Status=1
 join tblProductionCalendar pcal on pcal.offer_Id=op.Id and pcal.Status=1
 left join tblPerson person on person.UserId=op.user_Id and person.Status=1
left join tblContract cntr on op.user_Id=cntr.SupplierUserID and cntr.Status=1
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 -- left join tblCommunication com on com.PersonId=person.ID and com.Status=1 and com.comType=10120
where op.Status=1 and u.userType_eV_ID=26 and op.state_eV_Id=2
group by op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName,pcal.quantity,op.total_price,r.Name,
r.Description,ur.RoleId,u.userType_eV_ID,person.Id,person.Name,person.Surname,prc.EnumValueId,ContractStartDate,person.PinNumber,
person.FatherName,op.contractId,u.TaxexType,pcal.months_eV_Id,pcal.price,pc.edvStatus
) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1
--join tblProductionCalendar pcal on pcal.offer_Id=table1.Id and pcal.Status=1 
  where ev.enumCategory_enumCategoryId=5";
            var query2Calen = @" union all
 select distinct table1.Id,table1.product_Id,table1.ProductName,table1.quantity,table1.total_price,table1.personName,table1.Surname,
table1.FatherName,table1.organisationName,table1.PinNumber,table1.voen,table1.userType_eV_ID,pc.ProductName as parentName
,ev.name as kategoryName,--price.unit_price,
table1.roleDesc
,table1.TaxexType, table1.contractID,table1.price/table1.count as unitPriceCalndar,table1.RoleId,table1.edvStatus,table1.months_eV_Id
 -- , com.communication as phoneNumber
  from (
select op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName
,pcal.quantity,pcal.quantity*pcal.price as total_price,
r.Name as roleName,r.Description as roleDesc,ur.RoleId,u.userType_eV_ID,

person.Id as personID,
person.Name as personName, person.Surname,
fo.voen,fo.name as organisationName,
prc.EnumValueId  ,
cntr.ContractStartDate,person.PinNumber,person.FatherName, op.contractId as contractID,
(select CASE u.TaxexType
     WHEN 1 THEN N'ƏDV ödəyicisi'
	WHEN 0 THEN N'Sadələşmiş vergi ödəyicisi'
    WHEN 4 THEN N'6-8 % sadələşmiş vergi ödəyicisi'
    ELSE N'Məlumat Mövcud deyil'
	end ) as TaxexType,SUM(pcal.price) as price,COUNT(pcal.price) as count,pcal.months_eV_Id,pc.edvStatus
from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 join tblUserRole ur on op.user_Id=ur.UserId and ur.Status=1
 join tblRole r on r.Id=ur.RoleId and r.Status=1
 join tblUser u on u.Id=op.user_Id and u.Status=1

left join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
left join tblPerson person on person.Id=fo.manager_Id and person.Status=1
left join tblContract cntr on op.user_Id=cntr.SupplierUserID and cntr.Status=1
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 join tblProductionCalendar pcal on pcal.offer_Id=op.Id and pcal.Status=1
-- left join tblCommunication com on com.PersonId=person.id and com.Status=1 and com.comType=10120
where op.Status=1 and u.userType_eV_ID=50 and op.state_eV_Id=2
group by op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName,pcal.quantity,op.total_price,r.Name,
r.Description,ur.RoleId,u.userType_eV_ID,person.Id,person.Name,person.Surname,prc.EnumValueId,ContractStartDate,fo.voen,fo.name,person.PinNumber,
person.FatherName,op.contractId,u.TaxexType,pcal.months_eV_Id,pcal.price,pc.edvStatus
) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1
--join tblProductionCalendar pcal on pcal.offer_Id=table1.Id and pcal.Status=1 
 where ev.enumCategory_enumCategoryId=5";
            var queryProduct = @" ) as table2 
 where table2.product_Id= @productID";
            var query2 = @"    order by parentName,ProductName,unitPriceCalndar
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY";
            var queryRole = @" and RoleId=@RoleId";
            var queryPin = @" and PinNumber like '%'+@PinNumber+'%'";
            var queryVoen = @" and voen like '%'+@voen+'%'";
            var queryMonth = @"  and months_eV_Id=@months_eV_Id";
            if (ops.monthID != 0)
            {
                squery.Append(query1Calen);
                squery1.Append(query2Calen);
            }
            else
            {
                squery.Append(query);
                squery1.Append(queryUnion);
            }



            squery.Append(squery1.ToString());
            squery.Append(queryProduct);
            if (ops.roleID != 0)
            {
                squery.Append(queryRole);
            }
            if (!string.IsNullOrEmpty(ops.pinNumber))
            {
                squery.Append(queryPin);
            }
            if (!string.IsNullOrEmpty(ops.voen))
            {
                squery.Append(queryVoen);
            }
            if (ops.monthID != 0)
            {
                squery.Append(queryMonth);

            }
            squery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@productID", productID);
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@voen", ops.voen.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
                    command.Parameters.AddWithValue("@PinNumber", ops.pinNumber.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@months_eV_Id", ops.monthID);


                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemanOfferProduction()
                        {
                            productionID = reader.GetInt64OrDefaultValue(0),
                            productId = reader.GetInt64OrDefaultValue(1),
                            productName = reader.GetStringOrEmpty(2),
                            quantity = reader.GetDecimalOrDefaultValue(3),
                            totalQuantityPrice = reader.GetDecimalOrDefaultValue(4),
                            personName = reader.GetStringOrEmpty(5),
                            surname = reader.GetStringOrEmpty(6),
                            fatherName = reader.GetStringOrEmpty(7),
                            organizationName = reader.GetStringOrEmpty(8),
                            pinNumber = reader.GetStringOrEmpty(9),
                            voen = reader.GetStringOrEmpty(10),
                            usertype_Ev_ID = reader.GetInt64OrDefaultValue(11),
                            productParentName = reader.GetStringOrEmpty(12),
                            enumValueName = reader.GetStringOrEmpty(13),

                            roledesc = reader.GetStringOrEmpty(14),
                            // roledesc = reader.GetStringOrEmpty(16),
                            //roleId = reader.GetInt64OrDefaultValue(4),
                            //personID = reader.GetInt64OrDefaultValue(6),
                            taxesType = reader.GetStringOrEmpty(15),
                            contractID = reader.GetInt64OrDefaultValue(16),
                            unit_price = reader.GetDecimalOrDefaultValue(17),
                            edvStatus=reader.GetInt64OrDefaultValue(19),


                        });
                    }


                    connection.Close();
                }

                return result;
            }
        }
        public Int64 GetTotalOffersbyProductID_OPC(Int64 productID, DemandOfferProductsSearch ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            StringBuilder squery1 = new StringBuilder();

            //var query1 = @"";
            var query = @"select COUNT(*) as count from( select distinct table1.Id,table1.product_Id,table1.ProductName,table1.quantity,table1.total_price,table1.personName,table1.Surname,
table1.FatherName,table1.organisationName,table1.PinNumber,table1.voen,table1.userType_eV_ID
,pc.ProductName as parentName,ev.name as kategoryName--,price.unit_price
,table1.roleDesc,table1.TaxexType, table1.contractID,table1.price/table1.count as unitPriceCalndar,table1.RoleId,table1.edvStatus
 from (
select op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName
,op.quantity,op.total_price
,r.Name as roleName,r.Description as roleDesc,ur.RoleId,u.userType_eV_ID,
person.Id as personID,
person.Name as personName, person.Surname,
' ' as voen,' ' as organisationName,
prc.EnumValueId ,
cntr.ContractStartDate,person.PinNumber,person.FatherName, op.contractId as contractID,
 (select CASE u.TaxexType
     WHEN 1 THEN N'ƏDV ödəyicisi'
	WHEN 0 THEN N'Sadələşmiş vergi ödəyicisi'
    WHEN 4 THEN N'6-8 % sadələşmiş vergi ödəyicisi'
    ELSE N'Məlumat Mövcud deyil'
	end ) as TaxexType,SUM(pcal.price) as price,COUNT(pcal.price) as count,pc.edvStatus
from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 join tblUserRole ur on op.user_Id=ur.UserId and ur.Status=1
 join tblRole r on r.Id=ur.RoleId and r.Status=1
 join tblUser u on u.Id=op.user_Id and u.Status=1
 join tblProductionCalendar pcal on pcal.offer_Id=op.Id and pcal.Status=1
 left join tblPerson person on person.UserId=op.user_Id and person.Status=1
left join tblContract cntr on op.user_Id=cntr.SupplierUserID and cntr.Status=1
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 -- left join tblCommunication com on com.PersonId=person.ID and com.Status=1 and com.comType=10120
where op.Status=1 and u.userType_eV_ID=26 and op.state_eV_Id=2
group by op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName,op.quantity,op.total_price,r.Name,
r.Description,ur.RoleId,u.userType_eV_ID,person.Id,person.Name,person.Surname,prc.EnumValueId,ContractStartDate,person.PinNumber,
person.FatherName,op.contractId,u.TaxexType,pc.edvStatus
) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1
--join tblProductionCalendar pcal on pcal.offer_Id=table1.Id and pcal.Status=1 
  where ev.enumCategory_enumCategoryId=5
";
            var query2 = @" union all
 select distinct table1.Id,table1.product_Id,table1.ProductName,table1.quantity,table1.total_price,table1.personName,table1.Surname,
table1.FatherName,table1.organisationName,table1.PinNumber,table1.voen,table1.userType_eV_ID,pc.ProductName as parentName
,ev.name as kategoryName,--price.unit_price,
table1.roleDesc
,table1.TaxexType, table1.contractID,table1.price/table1.count as unitPriceCalndar,table1.RoleId,table1.edvStatus
 -- , com.communication as phoneNumber
  from (
select op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName
,op.quantity,op.total_price,
r.Name as roleName,r.Description as roleDesc,ur.RoleId,u.userType_eV_ID,

person.Id as personID,
person.Name as personName, person.Surname,
fo.voen,fo.name as organisationName,
prc.EnumValueId  ,
cntr.ContractStartDate,person.PinNumber,person.FatherName, op.contractId as contractID,
(select CASE u.TaxexType
     WHEN 1 THEN N'ƏDV ödəyicisi'
	WHEN 0 THEN N'Sadələşmiş vergi ödəyicisi'
    WHEN 4 THEN N'6-8 % sadələşmiş vergi ödəyicisi'
    ELSE N'Məlumat Mövcud deyil'
	end ) as TaxexType,SUM(pcal.price) as price,COUNT(pcal.price) as count,pc.edvStatus
from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 join tblUserRole ur on op.user_Id=ur.UserId and ur.Status=1
 join tblRole r on r.Id=ur.RoleId and r.Status=1
 join tblUser u on u.Id=op.user_Id and u.Status=1

left join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
left join tblPerson person on person.Id=fo.manager_Id and person.Status=1
left join tblContract cntr on op.user_Id=cntr.SupplierUserID and cntr.Status=1
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 join tblProductionCalendar pcal on pcal.offer_Id=op.Id and pcal.Status=1
-- left join tblCommunication com on com.PersonId=person.id and com.Status=1 and com.comType=10120
where op.Status=1 and u.userType_eV_ID=50 and op.state_eV_Id=2
group by op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName,op.quantity,op.total_price,r.Name,
r.Description,ur.RoleId,u.userType_eV_ID,person.Id,person.Name,person.Surname,prc.EnumValueId,ContractStartDate,fo.voen,fo.name,person.PinNumber,
person.FatherName,op.contractId,u.TaxexType,pc.edvStatus
) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1
--join tblProductionCalendar pcal on pcal.offer_Id=table1.Id and pcal.Status=1 
 where ev.enumCategory_enumCategoryId=5  )as tb
where product_Id= @productID ";
            var query1Calen = @"select COUNT(*) as Count from(select distinct table1.Id,table1.product_Id,table1.ProductName,table1.quantity,table1.total_price,table1.personName,table1.Surname,
table1.FatherName,table1.organisationName,table1.PinNumber,table1.voen,table1.userType_eV_ID
,pc.ProductName as parentName,ev.name as kategoryName--,price.unit_price
,table1.roleDesc,table1.TaxexType, table1.contractID,table1.price/table1.count as unitPriceCalndar,table1.RoleId,
table1.edvStatus,table1.months_eV_Id
 from (
select op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName
,pcal.quantity,pcal.quantity*pcal.price as total_price
,r.Name as roleName,r.Description as roleDesc,ur.RoleId,u.userType_eV_ID,
person.Id as personID,
person.Name as personName, person.Surname,
' ' as voen,' ' as organisationName,
prc.EnumValueId ,
cntr.ContractStartDate,person.PinNumber,person.FatherName, op.contractId as contractID,
 (select CASE u.TaxexType
     WHEN 1 THEN N'ƏDV ödəyicisi'
	WHEN 0 THEN N'Sadələşmiş vergi ödəyicisi'
    WHEN 4 THEN N'6-8 % sadələşmiş vergi ödəyicisi'
    ELSE N'Məlumat Mövcud deyil'
	end ) as TaxexType,SUM(pcal.price) as price,COUNT(pcal.price) as count,pcal.months_eV_Id,pc.edvStatus
from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 join tblUserRole ur on op.user_Id=ur.UserId and ur.Status=1
 join tblRole r on r.Id=ur.RoleId and r.Status=1
 join tblUser u on u.Id=op.user_Id and u.Status=1
 join tblProductionCalendar pcal on pcal.offer_Id=op.Id and pcal.Status=1
 left join tblPerson person on person.UserId=op.user_Id and person.Status=1
left join tblContract cntr on op.user_Id=cntr.SupplierUserID and cntr.Status=1
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 -- left join tblCommunication com on com.PersonId=person.ID and com.Status=1 and com.comType=10120
where op.Status=1 and u.userType_eV_ID=26 and op.state_eV_Id=2
group by op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName,pcal.quantity,op.total_price,r.Name,
r.Description,ur.RoleId,u.userType_eV_ID,person.Id,person.Name,person.Surname,prc.EnumValueId,ContractStartDate,person.PinNumber,
person.FatherName,op.contractId,u.TaxexType,pcal.months_eV_Id,pcal.price,pc.edvStatus
) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1
--join tblProductionCalendar pcal on pcal.offer_Id=table1.Id and pcal.Status=1 
  where ev.enumCategory_enumCategoryId=5";
            var query2Calen = @" union all
 select distinct table1.Id,table1.product_Id,table1.ProductName,table1.quantity,table1.total_price,table1.personName,table1.Surname,
table1.FatherName,table1.organisationName,table1.PinNumber,table1.voen,table1.userType_eV_ID,pc.ProductName as parentName
,ev.name as kategoryName,--price.unit_price,
table1.roleDesc
,table1.TaxexType, table1.contractID,table1.price/table1.count as unitPriceCalndar,table1.RoleId,table1.edvStatus,table1.months_eV_Id
 -- , com.communication as phoneNumber
  from (
select op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName
,pcal.quantity,pcal.quantity*pcal.price as total_price,
r.Name as roleName,r.Description as roleDesc,ur.RoleId,u.userType_eV_ID,

person.Id as personID,
person.Name as personName, person.Surname,
fo.voen,fo.name as organisationName,
prc.EnumValueId  ,
cntr.ContractStartDate,person.PinNumber,person.FatherName, op.contractId as contractID,
(select CASE u.TaxexType
     WHEN 1 THEN N'ƏDV ödəyicisi'
	WHEN 0 THEN N'Sadələşmiş vergi ödəyicisi'
    WHEN 4 THEN N'6-8 % sadələşmiş vergi ödəyicisi'
    ELSE N'Məlumat Mövcud deyil'
	end ) as TaxexType,SUM(pcal.price) as price,COUNT(pcal.price) as count,pcal.months_eV_Id,pc.edvStatus
from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 join tblUserRole ur on op.user_Id=ur.UserId and ur.Status=1
 join tblRole r on r.Id=ur.RoleId and r.Status=1
 join tblUser u on u.Id=op.user_Id and u.Status=1

left join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
left join tblPerson person on person.Id=fo.manager_Id and person.Status=1
left join tblContract cntr on op.user_Id=cntr.SupplierUserID and cntr.Status=1
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 join tblProductionCalendar pcal on pcal.offer_Id=op.Id and pcal.Status=1
-- left join tblCommunication com on com.PersonId=person.id and com.Status=1 and com.comType=10120
where op.Status=1 and u.userType_eV_ID=50 and op.state_eV_Id=2
group by op.Id,op.product_Id,pc.ProductCatalogParentID,pc.ProductName,pcal.quantity,op.total_price,r.Name,
r.Description,ur.RoleId,u.userType_eV_ID,person.Id,person.Name,person.Surname,prc.EnumValueId,ContractStartDate,fo.voen,fo.name,person.PinNumber,
person.FatherName,op.contractId,u.TaxexType,pcal.months_eV_Id,pcal.price,pc.edvStatus
) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 join tblEnumValue ev on table1.EnumValueId=ev.Id and ev.Status=1
--join tblProductionCalendar pcal on pcal.offer_Id=table1.Id and pcal.Status=1 
 where ev.enumCategory_enumCategoryId=5) as tb where product_Id= @productID ";
            var queryRole = @" and RoleId=@RoleId";
            var queryPin = @" and PinNumber like '%'+@PinNumber+'%'";
            var queryVoen = @" and voen like '%'+@voen+'%'";
            var queryMOnth = @" and months_eV_Id=@months_eV_Id";

            if (ops.monthID != 0)
            {
                squery.Append(query1Calen);
                squery.Append(query2Calen);
               
            }
            else
            {
                squery.Append(query);
                squery.Append(query2);
            }


            if (ops.monthID!=0)
            {
                squery.Append(queryMOnth);
            }
            if (ops.roleID != 0)
            {
                squery.Append(queryRole);
            }
            if (!string.IsNullOrEmpty(ops.pinNumber))
            {
                squery.Append(queryPin);
            }
            if (!string.IsNullOrEmpty(ops.voen))
            {
                squery.Append(queryVoen);
            }
         //   squery.Append(queryTb);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@productID", productID);
                    command.Parameters.AddWithValue("@voen", ops.voen.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
                    command.Parameters.AddWithValue("@PinNumber", ops.pinNumber.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@months_eV_Id", ops.monthID);

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
        ///dila
        public List<OfferProducts> GetTotalOffers(Int64 productID, Int64 monthID)
        {
            var result = new List<OfferProducts>();
            StringBuilder squery = new StringBuilder();
            var query = @" 
 select tb.product_Id ,SUM(tb.quantity)as totalOfferQuantity,
 tb.ProductName,pc.ProductName as parentName,SUM(tb.quantity) as totalOffer 
from(
select op.product_Id,pc.ProductName,pc.ProductCatalogParentID,op.quantity,
cal.months_eV_Id from tblOffer_Production op
 join tblProductCatalog pc on pc.Status=1 and op.product_Id=pc.Id
  join tblProductionCalendar cal on cal.offer_Id=op.Id and cal.Status=1
where op.Status=1
 product_Id=@productID  ";

            var query11 = @" 
) as tb
left join tblProductCatalog pc on pc.Id=tb.ProductCatalogParentID and pc.Status=1

";
            var query2 = @" group by pc.ProductName,tb.product_Id,tb.ProductName";
            var querymon = @" and months_eV_Id=@months_eV_Id";
            squery.Append(query);
            if (monthID != 0)
            {
                squery.Append(querymon);
            }
            squery.Append(query11);
            squery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@productID", productID);
                    command.Parameters.AddWithValue("@months_eV_Id", monthID);


                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OfferProducts()
                        {


                            productID = reader.GetInt64OrDefaultValue(0),
                            totalQuantity = reader.GetDecimalOrDefaultValue(1),
                            productName = reader.GetStringOrEmpty(2),
                            parentName = reader.GetStringOrEmpty(3),
                            totalQualityForMonth = reader.GetDecimalOrDefaultValue(4),
                            monthID = reader.GetInt64OrDefaultValue(5),




                        });
                    }


                    connection.Close();
                }

                return result;
            }
        }

        //huseyn
        public List<OfferProducts> GetTotalOffer(Int64 productID, Int64 monthID)
        {
            var result = new List<OfferProducts>();
            StringBuilder squery = new StringBuilder();
            var query = @"
select tb.product_Id, SUM(tb.quantity)as totalOfferQuantity,
 tb.ProductName,SUM(tb.total_price) as totalOfferPrice 
from(
select op.product_Id, pc.ProductName, pc.ProductCatalogParentID ";

            var query11 = @" 
) as tb
group by tb.product_Id, tb.ProductName

";
            var querymon = @" , pcal.quantity, op.total_price 
from tblOffer_Production op
 join tblProductCatalog pc on pc.Status=1 and op.product_Id=pc.Id
 join tblProductionCalendar pcal on pcal.offer_Id=op.Id and pcal.Status=1
where op.Status=1 and op.state_eV_Id=2
and product_Id=@productID  and months_eV_Id=@months_eV_Id";
            var query12 = @" , op.quantity, op.total_price 
from tblOffer_Production op
 join tblProductCatalog pc on pc.Status=1 and op.product_Id=pc.Id
where op.Status=1 and op.state_eV_Id=2
and product_Id=@productID ";
            squery.Append(query);
            if (monthID != 0)
            {
                squery.Append(querymon);
            }
            else
            {
                squery.Append(query12);
            }
            squery.Append(query11);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@productID", productID);
                    command.Parameters.AddWithValue("@months_eV_Id", monthID);


                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OfferProducts()
                               {


                                   productID = reader.GetInt64OrDefaultValue(0),
                                   totalQuantity = reader.GetDecimalOrDefaultValue(1),
                                   productName = reader.GetStringOrEmpty(2),
                                   totalPrice = reader.GetDecimalOrDefaultValue(3),



                               });
                    }


                    connection.Close();
                }

                return result;
            }
        }
        public List<OfferProducts> GetTotalOfferRole(Int64 productID)
        {
            var result = new List<OfferProducts>();
            StringBuilder squery = new StringBuilder();
            var query = @"   select tb.product_Id ,SUM(tb.quantity)as totalOfferQuantity,tb.ProductName,pc.ProductName as parentName,SUM(tb.quantity)*tb.unit_price as totalOffer,
 tb.RoleId ,tb.type
 from(select op.product_Id,pc.ProductName,pc.ProductCatalogParentID,op.quantity,
price.unit_price,ur.RoleId,'sellerPeson'as type from tblOffer_Production op
 join tblProductCatalog pc on pc.Status=1 and op.product_Id=pc.Id
 left join tblProductPrice price on op.product_Id=price.productId and price.Status=1
  join tblUserRole ur on ur.UserId=op.user_Id and ur.Status=1
where op.Status=1 and ur.RoleId=11

) as tb
left join tblProductCatalog pc on pc.Id=tb.ProductCatalogParentID and pc.Status=1
where product_Id=@productID
group by pc.ProductName,tb.product_Id,tb.ProductName,tb.unit_price,tb.RoleId,tb.type
union
select tb.product_Id ,SUM(tb.quantity)as totalOfferQuantity,tb.ProductName,pc.ProductName as parentName,SUM(tb.quantity)*tb.unit_price as totalOffer,
 tb.RoleId ,tb.type
 from(select op.product_Id,pc.ProductName,pc.ProductCatalogParentID,op.quantity,
price.unit_price,ur.RoleId,'producerPerson' as type from tblOffer_Production op
 join tblProductCatalog pc on pc.Status=1 and op.product_Id=pc.Id
 left join tblProductPrice price on op.product_Id=price.productId and price.Status=1
  join tblUserRole ur on ur.UserId=op.user_Id and ur.Status=1
where op.Status=1 and ur.RoleId=15

) as tb
left join tblProductCatalog pc on pc.Id=tb.ProductCatalogParentID and pc.Status=1
where product_Id=@productID
group by pc.ProductName,tb.product_Id,tb.ProductName,tb.unit_price,tb.RoleId,tb.type
";



            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productID", productID);


                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OfferProducts()
                        {


                            productID = reader.GetInt64OrDefaultValue(0),
                            totalQuantity = reader.GetDecimalOrDefaultValue(1),
                            productName = reader.GetStringOrEmpty(2),
                            parentName = reader.GetStringOrEmpty(3),
                            totalPrice = reader.GetDecimalOrDefaultValue(4),
                            roleId = reader.GetInt64OrDefaultValue(5),
                            type = reader.GetStringOrEmpty(6)




                        });
                    }


                    connection.Close();
                }

                return result;
            }
        }
        public List<DemandDetail> GetTotalDemandOffersRegion(DemandOfferProductsSearch ops)
        {
            var result = new List<DemandDetail>();
            StringBuilder squery = new StringBuilder();

            StringBuilder squeryunion = new StringBuilder();
            var query1 = @" with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au
 ";


            squeryunion.Append(query1);

            var queryadminID = @" WHERE  Id=@adminID ";
            if (ops.adminID != 0)
            {
                squeryunion.Append(queryadminID);
            }
            var query2 = @"  

  --select ID from cte;
   UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
  select table1.*,pc.ProductName as parentName,
table1.toplam *unit_price as totalDemandPrice,pa.Name as regionName from (
select  
op.product_Id,pc.ProductCatalogParentID,pc.ProductName,
SUM(quantity) as toplam,price.unit_price,ev.name as kategoryName
,prc.EnumValueId,au.ParentRegionID,op.Status from tblDemand_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 join tblProductPrice price on op.product_Id=price.productId and price.Status=1 --and price.year=2016 and price.partOfYear=4
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 join tblEnumValue ev on prc.EnumValueId=ev.Id  and ev.Status=1 
 join tblProductAddress pa on pa.Id=op.address_Id and pa.Status=1
 join tblPRM_AdminUnit au on au.Id=pa.adminUnit_Id and au.Status=1
  and  au.Id 
			   in (
							select Id from cte
							 		)
where op.Status=1
and ev.enumCategory_enumCategoryId=5

group by op.product_Id,pc.ProductCatalogParentID,pc.ProductName,price.unit_price,
ev.name
,prc.EnumValueId  ,au.ParentRegionID,op.Status

) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
  join tblPRM_AdminUnit pa on table1.ParentRegionID=pa.Id and pa.Status=1
 where table1.Status=1

";
            var query3 = @"

order by regionName,ProductName
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY
    ";
            squery.Append(squeryunion.ToString());
            squery.Append(query2);
            var queryProduct = @" and product_Id=@product_Id";
            if (ops.productId != 0)
            {
                squery.Append(queryProduct);
            }

            squery.Append(query3);


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@product_Id", ops.productId);
                    command.Parameters.AddWithValue("@adminID", ops.adminID);
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@RecordsPerPage", ops.page_size);


                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemandDetail()
                        {

                            productID = reader.GetInt64OrDefaultValue(0),
                            productName = reader.GetStringOrEmpty(2),
                            totalQuantity = reader.GetDecimalOrDefaultValue(3),
                            unit_price = reader.GetDecimalOrDefaultValue(4),
                            kategoryName = reader.GetStringOrEmpty(5),
                            parentName = reader.GetStringOrEmpty(9),
                            totalPrice = reader.GetDecimalOrDefaultValue(10),
                            regionName = reader.GetStringOrEmpty(11)







                        });
                    }


                    connection.Close();
                }

                return result;
            }
        }
        public Int64 GetTotalDemandOffersRegion_OPC(DemandOfferProductsSearch ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            //var query1 = @"";
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
  --select ID from cte;
  select COUNT(*) as count
         FROM ( select table1.*,pc.ProductName as parentName,
table1.toplam *unit_price as totalDemandPrice,pa.Name as regionName from (
select  
op.product_Id,pc.ProductCatalogParentID,pc.ProductName,
SUM(quantity) as toplam,price.unit_price,ev.name as kategoryName
,prc.EnumValueId,au.ParentRegionID,op.Status from tblDemand_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 join tblProductPrice price on op.product_Id=price.productId and price.Status=1 --and price.year=2016 and price.partOfYear=4
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 join tblEnumValue ev on prc.EnumValueId=ev.Id  and ev.Status=1 
 join tblProductAddress pa on pa.Id=op.address_Id and pa.Status=1
 join tblPRM_AdminUnit au on au.Id=pa.adminUnit_Id and au.Status=1
  and  au.Id 
			   in (
							select Id from cte
							 		)
where op.Status=1
and ev.enumCategory_enumCategoryId=5

group by op.product_Id,pc.ProductCatalogParentID,pc.ProductName,price.unit_price,
ev.name
,prc.EnumValueId  ,au.ParentRegionID,op.Status

) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
  join tblPRM_AdminUnit pa on table1.ParentRegionID=pa.Id and pa.Status=1
  ) as tb
  where tb.Status=1  --tb.product_Id=@product_Id
  ";

            var queryadminID = @"  WHERE Id=@adminID ";
            squeryID.Append(query1);
            if (ops.adminID != 0)
            {
                squeryID.Append(queryadminID);
            }
            squery.Append(squeryID.ToString());
            squery.Append(query2);
            var queryProduct = @" and product_Id=@product_Id";
            if (ops.productId != 0)
            {
                squery.Append(queryProduct);
            }


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {


                    command.Parameters.AddWithValue("@product_Id", ops.productId);
                    command.Parameters.AddWithValue("@adminID", ops.adminID);
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
        public decimal GetOfferPriceCount(Int64 offerID, Int64 productID)
        {
            decimal count = 0;
            var query = @" select tb.pricce/tb.count as unitPrice from(select SUm( pcc.price) as pricce,COUNT(*) as count from tblOffer_Production op
 join tblProductionCalendar pcc on op.Id =pcc.offer_Id and pcc.Production_type_eV_Id=3
 where pcc.offer_Id=@offer_Id and
  op.product_Id=@product_Id
  )as tb";



            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@offer_Id", offerID);
                    command.Parameters.AddWithValue("@product_Id", productID);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToDecimal(reader["unitPrice"]);
                    }

                }
                connection.Close();
            }

            return count;
        }
        public decimal GetOfferQuantity(Int64 productID)
        {
            decimal count = 0;
            var query = @" select an.quantity,an.product_id from tblAnnouncement an
where an.Status=1 and an.product_id=@product_Id ";



            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {



                    command.Parameters.AddWithValue("@product_Id", productID);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToDecimal(reader["quantity"]);
                    }

                }
                connection.Close();
            }

            return count;
        }

        public Int64 GetTotalOffer1_OPC(OfferProductionDetailSearch opds)
        {
            Int64 count = 0;
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
 select Count(*) as Count from(select tb.*,pc.ProductName as parentName from(
select op.Id,op.unit_price,op.quantity,op.total_price, op.description,op.product_Id, pc.ProductName,op.productOrigin,pc.ProductCatalogParentID,ev.name as status,
prc.EnumCategoryId,prc.EnumValueId,ev1.name as enumValueName,fo.name as orgName,person.Name as personName,
person.Surname,person.FatherName,adr.fullAddress as prsAddress,padr.fullAddress as productAddress,au1.Name as productOriginName,u.Email,
padr.fullAddress,padr.addressDesc from tblOffer_Production op
 join tblProductCatalog pc on pc.Id=op.product_Id and pc.Status=1
 left join tblEnumValue ev on ev.Id=op.state_eV_Id and ev.Status=1
 left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 left join tblEnumValue ev1 on ev1.Id=prc.EnumValueId and  ev1.Status=1
 left join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
 left join tblPerson person on person.UserId=op.user_Id and person.Status=1
 left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
  left  join tblProductAddress padr on op.productAddress_Id=padr.Id and padr.Status=1
   join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id and au.Status=1

		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
     
   left join tblPRM_AdminUnit au1 on au1.Id=op.productOrigin and au1.Status=1
   
		   	 
   left join tblUser u on u.Id=op.user_Id and u.Status=1
where op.Status=1 and op.state_eV_Id=2
) as tb
 join tblProductCatalog pc on pc.Id=tb.ProductCatalogParentID and pc.Status=1
left join tblAnnouncement an on an.product_id=tb.product_Id and an.Status=1
 ) as tb  where tb.EnumCategoryId=5


";

            squery.Append(squeryunion.ToString());


            squery.Append(query2);

            var queryProductId = @" and tb.product_Id=@product_Id";
            var queryCountryID = @" and productOrigin=@countryId ";


            if (opds.countryId != 0)
            {
                squery.Append(queryCountryID);
            }


            if (opds.productID != 0)
            {
                squery.Append(queryProductId);
            }


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {


                    command.Parameters.AddWithValue("@product_Id", opds.productID);
                    command.Parameters.AddWithValue("@addressID", opds.adminID);
                    command.Parameters.AddWithValue("@countryId", opds.countryId);

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
        public List<OfferPerson> GetOfferPersonByProductID(Int64 productId)
        {
            var result = new List<OfferPerson>();


            var query = @" 
select distinct tb.* from(select op.Id,op.product_Id,

person.Name as personName, person.Surname,
' ' as organisationName

,person.FatherName,adr.fullAddress as adress,pa.fullAddress as productAddres,op.productOrigin,person.Id as personID,
 u.Email
from tblOffer_Production op
 join tblUser u on u.Id=op.user_Id and u.Status=1
 join tblPerson person on person.UserId=op.user_Id and person.Status=1
 join tblAddress adr on adr.user_Id=op.user_Id and adr.Status=1
 join tblProductAddress pa on pa.Id=op.productAddress_Id and pa.Status=1

where op.Status=1 and op.state_eV_Id=2 and u.userType_eV_ID=26

union
select op.Id,op.product_Id,

'' as personName,'' as Surname,
person.name as organisationName

,'' as FatherName,adr.fullAddress as adress,pa.fullAddress as productAddres,op.productOrigin
,person.Id as personID,u.Email
 
from tblOffer_Production op
 join tblUser u on u.Id=op.user_Id and u.Status=1
 join tblForeign_Organization person on person.UserId=op.user_Id and person.Status=1
 join tblAddress adr on adr.user_Id=op.user_Id and adr.Status=1
  join tblProductAddress pa on pa.Id=op.productAddress_Id and pa.Status=1
where op.Status=1 and op.state_eV_Id=2 and u.userType_eV_ID=50
) as tb
--join tblProductCatalogControl prc on prc.ProductId=tb.product_Id and prc.Status=1
--join tblEnumValue ev on  ev.Id=prc.EnumValueId and ev.Status=1
where tb.product_Id=@product_Id
order by Surname,personName,FatherName
";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@product_Id", productId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OfferPerson()
                        {

                            productionID = reader.GetInt64OrDefaultValue(0),
                            productID = reader.GetInt64OrDefaultValue(1),
                            personName = reader.GetStringOrEmpty(2),
                            surname = reader.GetStringOrEmpty(3),
                            organizationName = reader.GetStringOrEmpty(4),
                            fatherName = reader.GetStringOrEmpty(5),
                            addresPerson = reader.GetStringOrEmpty(6),
                            productAddress = reader.GetStringOrEmpty(7),
                            productOrigin = reader.GetInt64OrDefaultValue(8),
                            personID = reader.GetInt64OrDefaultValue(9),
                            email = reader.GetStringOrEmpty(10)

                        });
                    }


                    connection.Close();
                }

                return result;
            }
        }
//        public List<ProductionDetail> GetTotalOffer1(OfferProductionDetailSearch opds)
//        {

//            var result = new List<ProductionDetail>();
//            StringBuilder squery = new StringBuilder();
//            StringBuilder squeryunion = new StringBuilder();
//            var query1 = @" with cte(Id) AS 
// (
//  SELECT au.Id
//  FROM dbo.tblPRM_AdminUnit au
// ";


//            squeryunion.Append(query1);

//            var queryadminID = @" WHERE  Id=@addressID ";
//            if (opds.adminID != 0)
//            {
//                squeryunion.Append(queryadminID);
//            }
//            var query2 = @"  
//
//   UNION ALL 
//   SELECT au.Id
//
//  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
//  
//  )
//  --select ID from cte;
//  select tb.* from(select FirstTable.Id, 
//                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, 
//                      ev.name as KategoryName,FirstTable.ProductCatalogParentID,FirstTable.Email
//					  ,FirstTable.Name,FirstTable.Surname,FirstTable.FatherName
//					,FirstTable.address_Id--,
//					 -- FirstTable.PinNumber--,FirstTable.adrID
//					,FirstTable.personID,FirstTable.productAddress_Id,pc.ProductName as  ProductParentName,FirstTable.personAddress,FirstTable.organizationName,FirstTable.total_price,
//FirstTable.quantity,an.unit_price, FirstTable.productOrigin, FirstTable.organizationAddress,au.Name as productOriginName,FirstTable.price/FirstTable.count as unitPrice
//					   from (   
//                     select distinct op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
//                    pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,us.Email,person.Name,person.Surname,
//					person.FatherName,person.birtday,person.gender,person.profilePicture,person.address_Id--,person.PinNumber
//			--,adr.Id as adrID
//			  ,person.Id as personID,op.productAddress_Id,adr.fullAddress as personAddress,adr.addressDesc as personAddressDesc
// ,fo.name as organizationName ,op.total_price,op.productOrigin, padr.fullAddress as organizationAddress,SUM(pcal.price) as price,COUNT(pcal.price) as count
//                      from [dbo].[tblOffer_Production] op 
//     
//                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
//                     left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
//                      join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
//                     left join [dbo].[tblPerson] person on us.Id=person.UserId and person.Status=1 
//                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
//                     left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id and ev.Status=1
//                     left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1  
//					 left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
//				 left join  [dbo].[tblForeign_Organization] fo on op.user_Id=fo.userId and fo.Status=1
//                left join tblProductionCalendar pcal on pcal.offer_Id=op.Id and pcal.Status=1
//			 left  join tblProductAddress padr on op.productAddress_Id=padr.Id and padr.Status=1
//			   join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id 
//	   
//           
//		   	   and  au.Id 
//			   in (
//							select Id from cte
//							 		)
//     
//      
//					 
//                      where   
//             
//                op.state_eV_Id=2 and 
//                op.Status=1 
//group by op.Id,op.unit_price,op.quantity,op.description,op.product_Id,pc.ProductName,ev.name,
//				prc.EnumCategoryId,prc.EnumValueId,pa.fullAddress,pa.addressDesc,op.user_Id,pc.ProductCatalogParentID,
//				us.Email,person.Name,person.Surname,person.FatherName,person.profilePicture,person.birtday,
//				person.gender,person.address_Id,person.Id,op.productAddress_Id,adr.fullAddress,adr.addressDesc,fo.name,
//				op.total_price,op.productOrigin,padr.fullAddress
//                    ) as FirstTable   
//                     left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1  
//					 left join [dbo].[tblProductCatalog] pc on pc.Id=FirstTable.ProductCatalogParentID and pc.Status=1
//				left join tblAnnouncement an on an.product_id=FirstTable.product_Id and an.Status=1
//	    left join tblPRM_AdminUnit au on FirstTable.productOrigin=au.Id and au.Status=1
//           
//  
//            ) as tb where tb.EnumCategoryId=5 --and tb.RoleId in (11,15)
//
//";
//            var query3 = @"
//order by Surname,Name,FatherName
//OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
//FETCH NEXT @RecordsPerPage ROWS ONLY
//    ";
//            squery.Append(squeryunion.ToString());



//            squery.Append(query2);

//            var queryProductId = @" and product_Id=@product_Id";
//            var queryCountryID = @" and tb.productOrigin=@countryId ";

//            if (opds.productID != 0)
//            {
//                squery.Append(queryProductId);
//            }
//            if (opds.countryId != 0)
//            {
//                squery.Append(queryCountryID);
//            }



//            squery.Append(query3);
//            using (var connection = new SqlConnection(DBUtil.ConnectionString))
//            {
//                connection.Open();

//                using (var command = new SqlCommand(squery.ToString(), connection))
//                {




//                    command.Parameters.AddWithValue("@PageNo", opds.page);

//                    command.Parameters.AddWithValue("@RecordsPerPage", opds.pageSize);




//                    command.Parameters.AddWithValue("@product_Id", opds.productID);

//                    command.Parameters.AddWithValue("@addressID", opds.adminID);
//                    command.Parameters.AddWithValue("@countryId", opds.countryId);

//                    var reader = command.ExecuteReader();

//                    while (reader.Read())
//                    {


//                        /*score.Score = score == DBNull.Value ? 0 : Convert.ToInt32(score);*/

//                        result.Add(new ProductionDetail()
//                        {
//                            productionID = reader.GetInt64OrDefaultValue(0),

//                            productId = reader.GetInt64OrDefaultValue(1),
//                            productName = reader.GetStringOrEmpty(2),

//                            fullAddress = reader.GetStringOrEmpty(3),
//                            addressDesc = reader.GetStringOrEmpty(4),
//                            enumCategoryId = reader.GetInt64OrDefaultValue(5),
//                            enumValueName = reader.GetStringOrEmpty(6),

//                            email = reader.GetStringOrEmpty(8),
//                            name = reader.GetStringOrEmpty(9),
//                            surname = reader.GetStringOrEmpty(10),
//                            fatherName = reader.GetStringOrEmpty(11),
//                            adress_Id = reader.GetInt64OrDefaultValue(12),


//                            personID = reader.GetInt64OrDefaultValue(13),

//                            productParentName = reader.GetStringOrEmpty(15),
//                            personAdress = reader.GetStringOrEmpty(16),

//                            organizationName = reader.GetStringOrEmpty(17),
//                            totalPrice = reader.GetDecimalOrDefaultValue(18),
//                            quantity = reader.GetDecimalOrDefaultValue(19),
//                            unitPriceAnnouncement = reader.GetDecimalOrDefaultValue(20),
//                            productOrigin = reader.GetInt64OrDefaultValue(21),

//                            organizationAddress = reader.GetStringOrEmpty(22),
//                            productOriginName = reader.GetStringOrEmpty(23),
//                            unitPrice = reader.GetDecimalOrDefaultValue(24),
//                        });
//                    }
//                }



//                connection.Close();
//            }

//            return result;
//        } 
        public List<ProductionDetail> GetTotalOffer1(OfferProductionDetailSearch opds)
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
  select tb.*,pc.ProductName as parentName,an.unit_price as annUNitprice from(
select op.Id,op.quantity,op.description,op.product_Id, pc.ProductName,op.productOrigin,pc.ProductCatalogParentID,ev.name as status,
prc.EnumCategoryId,prc.EnumValueId,ev1.name as enumValueName,fo.name as orgName,person.Name as personName,
person.Surname,person.FatherName,adr.fullAddress as prsAddress,padr.fullAddress as productAddress,au1.Name as productOriginName,u.Email,
padr.fullAddress,padr.addressDesc from tblOffer_Production op
 join tblProductCatalog pc on pc.Id=op.product_Id and pc.Status=1
 left join tblEnumValue ev on ev.Id=op.state_eV_Id and ev.Status=1
 left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 left join tblEnumValue ev1 on ev1.Id=prc.EnumValueId and  ev1.Status=1
 left join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
 left join tblPerson person on person.UserId=op.user_Id and person.Status=1
 left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
  left  join tblProductAddress padr on op.productAddress_Id=padr.Id and padr.Status=1
   join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id and au.Status=1

		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
     
   left join tblPRM_AdminUnit au1 on au1.Id=op.productOrigin and au1.Status=1
   
   left join tblUser u on u.Id=op.user_Id and u.Status=1
where op.Status=1 and op.state_eV_Id=2
) as tb
 join tblProductCatalog pc on pc.Id=tb.ProductCatalogParentID and pc.Status=1
left join tblAnnouncement an on an.product_id=tb.product_Id and an.Status=1
 where tb.EnumCategoryId=5

";
            var query3 = @"
order by Surname,personName,FatherName
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY
    ";
            squery.Append(squeryunion.ToString());



            squery.Append(query2);

            var queryProductId = @" and tb.product_Id=@product_Id";
            var queryCountryID = @" and tb.productOrigin=@countryId ";

            if (opds.productID != 0)
            {
                squery.Append(queryProductId);
            }
            if (opds.countryId != 0)
            {
                squery.Append(queryCountryID);
            }



            squery.Append(query3);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {




                    command.Parameters.AddWithValue("@PageNo", opds.page);

                    command.Parameters.AddWithValue("@RecordsPerPage", opds.pageSize);
                    command.Parameters.AddWithValue("@product_Id", opds.productID);

                    command.Parameters.AddWithValue("@addressID", opds.adminID);
                    command.Parameters.AddWithValue("@countryId", opds.countryId);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {


                     

                        result.Add(new ProductionDetail()
                        {
                            productionID = reader.GetInt64OrDefaultValue(0),
                           
                            quantity = reader.GetDecimalOrDefaultValue(1),
                          
                            description=reader.GetStringOrEmpty(2),
                            productId = reader.GetInt64OrDefaultValue(3),
                            productName = reader.GetStringOrEmpty(4),
                            productOrigin = reader.GetInt64OrDefaultValue(5),
                            enumCategoryId = reader.GetInt64OrDefaultValue(8),
                            enumValueName = reader.GetStringOrEmpty(10),
                            organizationName = reader.GetStringOrEmpty(11),
                            name = reader.GetStringOrEmpty(12),
                            surname = reader.GetStringOrEmpty(13),
                            fatherName = reader.GetStringOrEmpty(14),
                            personAdress = reader.GetStringOrEmpty(15),
                            organizationAddress = reader.GetStringOrEmpty(16),
                            productOriginName = reader.GetStringOrEmpty(17),
                            email = reader.GetStringOrEmpty(18),
                            fullAddress = reader.GetStringOrEmpty(19),
                            addressDesc = reader.GetStringOrEmpty(20),
                            productParentName = reader.GetStringOrEmpty(21),
                            unitPriceAnnouncement=reader.GetDecimalOrDefaultValue(22),

                           });
                    }
                }



                connection.Close();
            }

            return result;
        }
        public decimal GetProductPriceByOfferID(Int64 offerID,Int64 productID)
        {
            decimal count = 0;

            var query1 = @" select top 1 ISNULL(price.unit_price,0) as unitPrice,op.Id,dbo.f_getRub_by_BigIntDate(ContractDate)as date1,dbo.f_getYear_by_BigIntDate(ContractDate)as rub1,
op.product_Id from tblProductPrice price
left join tblOffer_Production op on op.product_Id=price.productId AND op.Status=1
 join tblContractDetailTemp con on op.Id=con.offerID and con.Status=1
where price.Status=1 and dbo.f_getRub_by_BigIntDate(ContractDate)<=partOfYear and dbo.f_getYear_by_BigIntDate(ContractDate)<=year
 and op.product_Id=@productID
 order by year desc,partOfYear desc";
            var query = @" select top 1 ISNULL(price.unit_price,0) as unitPrice,op.Id,dbo.f_getRub_by_BigIntDate(ContractDate)as date1,dbo.f_getYear_by_BigIntDate(ContractDate)as rub1,
op.product_Id from tblProductPrice price
left join tblOffer_Production op on op.product_Id=price.productId AND op.Status=1
 join tblContractDetailTemp con on op.Id=con.offerID and con.Status=1
where price.Status=1 and 
(dbo.f_getYear_by_BigIntDate(ContractDate)>year and price.productId=@productID and price.Status=1  and con.offerID=@offerID) or
 (dbo.f_getYear_by_BigIntDate(ContractDate)=year and price.partOfYear<=dbo.f_getRub_by_BigIntDate(ContractDate ) and   con.offerID=@offerID and price.productId=@productID and price.Status=1)
 and op.product_Id=@productID
 order by year desc,partOfYear desc
";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@offerID", offerID);
                    command.Parameters.AddWithValue("@productID", productID);
                   
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToDecimal(reader["unitPrice"]);
                    }

                }
                connection.Close();
            }

            return count;
        }
        public decimal GetProductPriceByOfferID1( Int64 productID)
        {
            decimal count = 0;

            var query1 = @"select dbo.returnUnitPrice(con.ContractDate,op.product_Id) as unitPrice,op.Id,dbo.f_getRub_by_BigIntDate(ContractDate)as date1,dbo.f_getYear_by_BigIntDate(ContractDate)as rub1,
op.product_Id,pc.ProductName,con.ContractDate from tblProductPrice price
left join tblOffer_Production op on op.product_Id=price.productId AND op.Status=1
 join tblContractDetailTemp con on op.Id=con.offerID and con.Status=1
 left join tblProductCatalog pc on pc.Id=op.product_Id and pc.Status=1
where price.Status=1 
and op.product_Id=@productID 
 --order by year desc,partOfYear desc";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {

                   
                    command.Parameters.AddWithValue("@productID", productID);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToDecimal(reader["unitPrice"]);
                    }

                }
                connection.Close();
            }

            return count;
        }
        public Price GetUnitPriceByProdcutID(Int64 productID)
        {

            Price result = new Price();
            var query1 = @"select top 1 ISNULL(price.unit_price,0),SUM(dp.quantity) as toplam,SUM(dp.quantity)*ISNULL(price.unit_price,0)as totalPrice,dp.product_Id from tblProductPrice price
 join tblDemand_Production dp on dp.product_Id=price.productId and dp.Status=1
 join tblProductCatalogControl prc on prc.ProductId=dp.product_Id and prc.Status=1
 join tblProductCatalog pc on dp.product_Id=pc.Id and pc.Status=1
 join tblEnumValue ev on prc.EnumValueId=ev.Id  and ev.Status=1 
where price.Status=1  and price.productId=@productID  and (dbo.returnYear()>year and price.productId=@productID and price.Status=1) or
 (dbo.returnYear()=year and price.partOfYear<=dbo.returnPartOfYear() and price.productId=@productID and price.Status=1)
 and ev.enumCategory_enumCategoryId=5
and dp.state_eV_Id=2
 group by price.unit_price,price.year,price.partOfYear,dp.product_Id,
 pc.ProductCatalogParentID,pc.ProductName,price.unit_price,
ev.name,pc.productCode
,prc.EnumValueId  
order by year desc,partOfYear desc";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {


                    command.Parameters.AddWithValue("@productID", productID);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        /* productCatalog = new tblProductCatalog()*/

                       result = new Price()
                        {
                           unit_price=reader.GetDecimalOrDefaultValue(0),
                         
                           totalPrice=reader.GetDecimalOrDefaultValue(2),
                           productId=reader.GetInt64OrDefaultValue(3),
                        };
                    }
                }
                connection.Close();
            }

            return result;
        }
        public Price GetOfferUnitPriceByProdcutID(Int64 productID)
        {

            Price result = new Price();
            var query1 = @"select top 1 ISNULL(price.unit_price,0),ISNULL(SUM(dp.quantity),0) as toplam,SUM(dp.quantity)*ISNULL(price.unit_price,0)as totalPrice,dp.product_Id from tblProductPrice price
 join tblOffer_Production dp on dp.product_Id=price.productId and dp.Status=1
 join tblProductCatalogControl prc on prc.ProductId=dp.product_Id and prc.Status=1
 join tblProductCatalog pc on dp.product_Id=pc.Id and pc.Status=1
 join tblEnumValue ev on prc.EnumValueId=ev.Id  and ev.Status=1 
where price.Status=1  and price.productId=@productID  and (dbo.returnYear()>year and price.productId=@productID and price.Status=1) or
 (dbo.returnYear()=year and price.partOfYear<=dbo.returnPartOfYear() and price.productId=@productID and price.Status=1)
 and ev.enumCategory_enumCategoryId=5
and dp.state_eV_Id=2
 group by price.unit_price,price.year,price.partOfYear,dp.product_Id,
 pc.ProductCatalogParentID,pc.ProductName,price.unit_price,
ev.name,pc.productCode
,prc.EnumValueId  
order by year desc,partOfYear desc";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {


                    command.Parameters.AddWithValue("@productID", productID);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        /* productCatalog = new tblProductCatalog()*/

                        result = new Price()
                        {
                            unit_price = reader.GetDecimalOrDefaultValue(0),

                            totalPrice = reader.GetDecimalOrDefaultValue(2),
                            productId = reader.GetInt64OrDefaultValue(3),
                        };
                    }
                }
                connection.Close();
            }

            return result;
        }
    }
}

