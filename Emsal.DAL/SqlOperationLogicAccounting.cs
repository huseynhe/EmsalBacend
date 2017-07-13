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

  
		 
 select table1.Id,table1.unit_price,table1.quantity,table1.description
,table1.product_Id,table1.EnumCategoryId,table1.ProductName,table1.personName,table1.Surname,
table1.FatherName,table1.gender,table1.birtday,table1.profilePicture
,table1.PinNumber,table1.organizationName,table1.voen,table1.fullAddress,table1.addressDesc,
table1.userType_eV_ID,table1.Email,table1.potentialQuantity,table1.personId,table1.yearEvId, table1.enumvaule_year ,pc.ProductName as productParentName,ev.name as kategoryName,adr.fullAddress as personAddress,adr.addressDesc as personAdrDesc
,ev1.name as userType,con.[ContractNumber ],con.Id as contTempID,table1.RoleId from (select  op.Id ,op.unit_price,op.quantity,
 op.description,op.product_Id, pc.ProductName,pc.ProductCatalogParentID
,
person.Name as personName,person.Surname,person.FatherName,person.gender,person.birtday,person.profilePicture, person.PinNumber,fo.name as organizationName,fo.voen
,pa.fullAddress,pa.addressDesc, prc.EnumValueId,prc.EnumCategoryId,ur.RoleId,u.userType_eV_ID,op.productAddress_Id,u.Email,person.Id as personId,
pp.quantity as potentialQuantity,person.address_Id,person.UserId,op.yearEvId,ev.name as enumvaule_year
 from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1  --and pc.Id in (select Id from cte)

left join tblPerson person on person.UserId=op.user_Id and op.Status=1
left join tblForeign_Organization fo on op.user_Id=fo.userId and fo.Status=1
left join tblProductAddress pa on pa.Id=op.productAddress_Id and pa.Status=1
left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
left join tblUserRole ur on op.user_Id=ur.UserId  and ur.Status=1
left join tblUser u on op.user_Id=u.Id and u.Status=1
 left join tblPotential_Production pp on pp.Id=op.potentialProduct_Id and pp.Status=1
 left join tblEnumValue ev on ev.Id=op.yearEvId and ev.Status=1 and op.Status=1
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
            var queryYearID = @"  and yearEvId=@yearEvId";
            if (opds.roleID != 0)
            {
                squery.Append(queryRoleID);
            }
            if (opds.yearEvId != 0)
            {
                squery.Append(queryYearID);
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

                    command.Parameters.AddWithValue("@Name", opds.name.GetStringOrEmptyData());


                    command.Parameters.AddWithValue("@product_Id", opds.productID);
                    command.Parameters.AddWithValue("@yearEvId", opds.yearEvId);

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
                            enumCategoryId = reader.GetInt64OrDefaultValue(5),
                            productName = reader.GetStringOrEmpty(6),

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
                            userType_eV_ID = reader.GetInt64OrDefaultValue(18),
                            email = reader.GetStringOrEmpty(19),
                            potentialProductQuantity = reader.GetDecimalOrDefaultValue(20),
                            personID = reader.GetInt64OrDefaultValue(21),
                            yearEvId = reader.GetInt64OrDefaultValue(22),
                            enumvaule_year = reader.GetStringOrEmpty(23),
                            productParentName = reader.GetStringOrEmpty(24),
                            enumValueName = reader.GetStringOrEmpty(25),
                            personAdress = reader.GetStringOrEmpty(26),
                            personAdressDesc = reader.GetStringOrEmpty(27),
                            userType = reader.GetStringOrEmpty(28),
                            ContractNumber = reader.GetStringOrEmpty(29),
                            contTempID = reader.GetInt64OrDefaultValue(30),

                            roleID = reader.GetInt64OrDefaultValue(31),


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
 select table1.Id,table1.unit_price,table1.quantity,table1.description
,table1.product_Id,table1.EnumCategoryId,table1.ProductName,table1.personName,table1.Surname,
table1.FatherName,table1.gender,table1.birtday,table1.profilePicture
,table1.PinNumber,table1.organizationName,table1.voen,table1.fullAddress,table1.addressDesc,
table1.userType_eV_ID,table1.Email,table1.potentialQuantity,table1.personId,table1.yearEvId, pc.ProductName as productParentName,ev.name as kategoryName,adr.fullAddress as personAddress,adr.addressDesc as personAdrDesc
,ev1.name as userType,con.[ContractNumber ],con.Id as contTempID,table1.RoleId from (select  op.Id ,op.unit_price,op.quantity,
 op.description,op.product_Id, pc.ProductName,pc.ProductCatalogParentID
,
person.Name as personName,person.Surname,person.FatherName,person.gender,person.birtday,person.profilePicture, person.PinNumber,fo.name as organizationName,fo.voen
,pa.fullAddress,pa.addressDesc, prc.EnumValueId,prc.EnumCategoryId,ur.RoleId,u.userType_eV_ID,op.productAddress_Id,u.Email,person.Id as personId,
pp.quantity as potentialQuantity,person.address_Id,person.UserId,op.yearEvId
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
            var queryYearID = @" and tb.yearEvId=@yearEvId";
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
            if (opds.yearEvId != 0)
            {
                squery.Append(queryYearID);
            }





            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@monitoring_eV_Id", opds.monintoring_eV_Id);



                    command.Parameters.AddWithValue("@Name", (opds.name.GetStringOrEmptyData()));


                    command.Parameters.AddWithValue("@product_Id", opds.productID);

                    command.Parameters.AddWithValue("@yearEvId", opds.yearEvId);

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
,r.Name as roleName,op.potentialProduct_Id,op.user_Id,pa.adminUnit_Id,op.yearEvId,ev1.name as enumValueYear
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
    left join [dbo].[tblEnumValue] ev1 on op.yearEvId=ev1.Id and ev.Status=1
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
            var queryYear = @" and yearEvId=@yearEvId";
            var queryUserID = @" and tb.user_Id=@user_Id";
            if (ops.roleID >0)
            {
                squery.Append(queryroleID);
            }
            if (!string.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);

            }
            if (ops.productID >0)
            {
                squery.Append(queryProduct);

            }
            if (ops.yearEvId > 0)
            {
                squery.Append(queryYear);

            }
            if (ops.userID > 0)
            {
                squery.Append(queryUserID);

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
                    command.Parameters.AddWithValue("@yearEvId", ops.yearEvId);
                    command.Parameters.AddWithValue("@product_Id", ops.productID);

                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@user_Id", ops.userID);
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
                            voen = reader.GetStringOrEmpty(14),

                            fullAddress = reader.GetStringOrEmpty(15),
                            addressDesc = reader.GetStringOrEmpty(16),
                            enumValueId = reader.GetInt64OrDefaultValue(17),
                            enumCategoryId = reader.GetInt64OrDefaultValue(18),
                            roleID = reader.GetInt64OrDefaultValue(19),
                            roleName = reader.GetStringOrEmpty(24),
                            userId = reader.GetInt64OrDefaultValue(26),
                            yearEvId = reader.GetInt64OrDefaultValue(28),
                            enumvaule_year = reader.GetStringOrEmpty(29),
                            productParentName = reader.GetStringOrEmpty(30),
                            enumValueName = reader.GetStringOrEmpty(31),
                            potentialProduct = reader.GetStringOrEmpty(32),



                            potentialProductQuantity = reader.GetDecimalOrDefaultValue(33),




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
,r.Name as roleName,op.potentialProduct_Id,op.user_Id,pa.adminUnit_Id,op.yearEvId,ev1.name as enumValueYear
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
  left join [dbo].[tblEnumValue] ev1 on op.yearEvId=ev1.Id and ev.Status=1
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
            var queryYear = @" and yearEvId=@yearEvId";
            var queryUserID = @" and tb.user_Id=@user_Id";
            if (ops.roleID > 0)
            {
                squery.Append(queryroleID);
            }
            if (ops.yearEvId > 0)
            {
                squery.Append(queryYear);
            }
            if (!string.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);

            }
            if (ops.productID > 0)
            {
                squery.Append(queryProduct);

            }
            if (ops.userID > 0)
            {
                squery.Append(queryUserID);

            }

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@yearEvId", ops.yearEvId);
                    command.Parameters.AddWithValue("@Name", ops.name.GetStringOrEmptyData());

                    command.Parameters.AddWithValue("@product_Id", ops.productID);
                    command.Parameters.AddWithValue("@roleId", ops.roleID);
                    command.Parameters.AddWithValue("@user_Id", ops.userID);
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
,pa.fullAddress,prc.EnumValueId,prc.EnumCategoryId,ur.RoleId,u.userType_eV_ID,op.productAddress_Id,u.Email,person.Id as personId,
op.yearEvId,ev.name as enumVName
 from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1  and pc.Id in (select Id from cte)
left join tblProduct_Document pdocument on pc.Id=pdocument.Product_catalog_Id and pdocument.Status=1
left join tblPerson person on person.UserId=op.user_Id and op.Status=1
left join tblForeign_Organization fo on op.user_Id=fo.userId and fo.Status=1
left join tblProductAddress pa on op.Id=pa.offer_production_id and pa.Status=1
left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
left join tblUserRole ur on op.user_Id=ur.UserId  and ur.Status=1
left join tblUser u on op.user_Id=u.Id and u.Status=1
left join tblEnumValue ev on op.yearEvId=ev.Id and op.Status=1
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



            var queryYearID = @" and yearEvId=@yearEvId  ";
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
            if (ops.yearEvId != 0)
            {
                squery.Append(queryYearID);
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
                    command.Parameters.AddWithValue("@yearEvId", ops.yearEvId);

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
                            yearEvId = reader.GetInt64OrDefaultValue(25),
                            enumvaule_year = reader.GetStringOrEmpty(26),
                            productParentName = reader.GetStringOrEmpty(27),
                            kategoryName = reader.GetStringOrEmpty(28),

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
,pa.fullAddress,prc.EnumValueId,prc.EnumCategoryId,ur.RoleId,u.userType_eV_ID,op.productAddress_Id,u.Email,person.Id as personId,
op.yearEvId,ev.name as enumVName
 from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1 and pc.Id in (select Id from cte)
left join tblProduct_Document pdocument on pc.Id=pdocument.Product_catalog_Id and pdocument.Status=1
left join tblPerson person on person.UserId=op.user_Id and op.Status=1
left join tblForeign_Organization fo on op.user_Id=fo.userId and fo.Status=1
left join tblProductAddress pa on op.Id=pa.offer_production_id and pa.Status=1
left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
left join tblUserRole ur on op.user_Id=ur.UserId and ur.Status=1
left join tblUser u on op.user_Id=u.Id and u.Status=1
left join tblEnumValue ev on op.yearEvId=ev.Id and op.Status=1
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
            var queryUserType = @" and  userType_eV_ID=@usertypeEvId";
            var queryYearID = @" and yearEvId=@yearEvId";

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
            if (ops.yearEvId != 0)
            {
                squery.Append(queryYearID);
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
                    command.Parameters.AddWithValue("@yearEvId", ops.yearEvId);

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
            else if (ops.contractStatus == true)
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
            else if (ops.contractStatus == false)
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
        public List<ForeignOrganization> GetDemandGovermentOrganisatinByAdminID(string adminIDList)
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
  
  )select distinct fo.Id, fo.name as organizationName,pa.adminUnit_Id from tblForeign_Organization  fo
 join tblAddress pa on fo.address_Id=pa.Id and pa.Status=1
join tblDemand_Production op on op.user_Id=fo.userId and fo.Status=1
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
,prc.EnumValueId ,pc.productCode,ev1.name as enumYear from tblDemand_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 left join tblProductPrice price on op.product_Id=price.productId and price.Status=1 and price.year=2016 and price.partOfYear=4
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 join tblEnumValue ev on prc.EnumValueId=ev.Id  and ev.Status=1 
left join tblEnumValue ev1 on op.yearEvId=ev1.Id  and ev1.Status=1 
where op.Status=1 --and op.product_Id=250 
and ev.enumCategory_enumCategoryId=5
and op.state_eV_Id=2


";
            var query11 = @" group by op.product_Id,pc.ProductCatalogParentID,pc.ProductName,price.unit_price,
ev.name,pc.productCode
,prc.EnumValueId   ,ev1.name
) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 left join tblProduct_Document pdocument on table1.product_Id=pdocument.Product_catalog_Id and pdocument.Status=1";
            var queryproduct = @" where product_Id=@product_Id";
            var queryYear = @"  and op.yearEvId=@yearEvId";

            var query2 = @" order by parentName, ProductName
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY";

            squery.Append(query);
            if (ops.yearEvId != 0)
            {
                squery.Append(queryYear);
            }
            squery.Append(query11);
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
                    command.Parameters.AddWithValue("@yearEvId", ops.yearEvId);
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
                            enumValueYear = reader.GetStringOrEmpty(7),
                            productParentName = reader.GetStringOrEmpty(8),

                            documentUrl = reader.GetStringOrEmpty(9),
                            documentName = reader.GetStringOrEmpty(10),





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


";
            squery.Append(query);
            var queryproduct = @" where tb.product_Id=@product_Id";
            var queryYear = @" and op.yearEvId=@yearEvId";
            var query2 = @" group by op.product_Id,pc.ProductCatalogParentID,pc.ProductName,price.unit_price,
ev.name
,prc.EnumValueId  

) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
 left join tblProduct_Document pdocument on pc.Id=pdocument.Product_catalog_Id and pdocument.Status=1
--join tblOffer_Production op on op.product_Id=table1.product_Id and op.Status=1
) as tb";
            if (ops.yearEvId != 0)
            {
                squery.Append(queryYear);
            }
            squery.Append(query2);
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
                    command.Parameters.AddWithValue("@yearEvId", ops.yearEvId);
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
                            edvStatus = reader.GetInt64OrDefaultValue(19),


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


            if (ops.monthID != 0)
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
,prc.EnumValueId,au.ParentRegionID,op.Status,ev1.name as enumValueYear from tblDemand_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
 join tblProductPrice price on op.product_Id=price.productId and price.Status=1 --and price.year=2016 and price.partOfYear=4
 join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 join tblEnumValue ev on prc.EnumValueId=ev.Id  and ev.Status=1 
 join tblProductAddress pa on pa.Id=op.address_Id and pa.Status=1
 left join tblEnumValue ev1 on ev1.Id=op.yearEvId and ev1.Status=1
 join tblPRM_AdminUnit au on au.Id=pa.adminUnit_Id and au.Status=1
  and  au.Id 
			   in (
							select Id from cte
							 		)
where op.Status=1
and ev.enumCategory_enumCategoryId=5


";
            var query11 = @" group by op.product_Id,pc.ProductCatalogParentID,pc.ProductName,price.unit_price,
ev.name
,prc.EnumValueId  ,au.ParentRegionID,op.Status,ev1.name

) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
  join tblPRM_AdminUnit pa on table1.ParentRegionID=pa.Id and pa.Status=1
 where table1.Status=1
";
            var queryYear = @" and op.yearEvId=@yearEvId";
            var query3 = @"

order by regionName,ProductName
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY
    ";
            squery.Append(squeryunion.ToString());
            squery.Append(query2);
            if (ops.yearEvId != 0)
            {
                squery.Append(queryYear);
            }
            squery.Append(query11);
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
                    command.Parameters.AddWithValue("@yearEvId", ops.yearEvId);

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
                            enumValueYear = reader.GetStringOrEmpty(9),
                            parentName = reader.GetStringOrEmpty(10),
                            totalPrice = reader.GetDecimalOrDefaultValue(11),
                            regionName = reader.GetStringOrEmpty(12)







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
left join tblEnumValue ev on prc.EnumValueId=ev.Id  and ev.Status=1 
 join tblProductAddress pa on pa.Id=op.address_Id and pa.Status=1
 join tblPRM_AdminUnit au on au.Id=pa.adminUnit_Id and au.Status=1
  and  au.Id 
			   in (
							select Id from cte
							 		)
where op.Status=1
and ev.enumCategory_enumCategoryId=5


  ";

            var queryadminID = @"  WHERE Id=@adminID ";
            squeryID.Append(query1);
            if (ops.adminID != 0)
            {
                squeryID.Append(queryadminID);
            }
            squery.Append(squeryID.ToString());
            squery.Append(query2);
            var query = @" group by op.product_Id,pc.ProductCatalogParentID,pc.ProductName,price.unit_price,
ev.name
,prc.EnumValueId  ,au.ParentRegionID,op.Status

) as table1
 join tblProductCatalog pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
  join tblPRM_AdminUnit pa on table1.ParentRegionID=pa.Id and pa.Status=1
  ) as tb
  where tb.Status=1  --tb.product_Id=@product_Id";
            var queryProduct = @" and product_Id=@product_Id";
            var queryYear = @" and op.yearEvId=@yearEvId";
            if (ops.yearEvId != 0)
            {
                squery.Append(queryYear);
            }
            squery.Append(query);
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
                    command.Parameters.AddWithValue("@yearEvId", ops.yearEvId);
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
padr.fullAddress,padr.addressDesc,op.yearEvId from tblOffer_Production op
 join tblProductCatalog pc on pc.Id=op.product_Id and pc.Status=1
 left join tblEnumValue ev on ev.Id=op.state_eV_Id and ev.Status=1
 left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 left join tblEnumValue ev1 on ev1.Id=prc.EnumValueId and  ev1.Status=1
  join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
 left join tblPerson person on person.UserId=op.user_Id and person.Status=1
 left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
  left  join tblProductAddress padr on op.productAddress_Id=padr.Id and padr.Status=1
   join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id and au.Status=1

		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
     
   left join tblPRM_AdminUnit au1 on au1.Id=op.productOrigin and au1.Status=1
   
		   left join tblEnumValue ev2 on ev2.Id=op.yearEvId and ev2.Status=1 	 
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
            var queryYEar = @" and yearEvId=@yearEvId";

            if (opds.countryId != 0)
            {
                squery.Append(queryCountryID);
            }


            if (opds.productID != 0)
            {
                squery.Append(queryProductId);
            }
            if (opds.yearEvId != 0)
            {
                squery.Append(queryYEar);
            }

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {


                    command.Parameters.AddWithValue("@product_Id", opds.productID);
                    command.Parameters.AddWithValue("@addressID", opds.adminID);
                    command.Parameters.AddWithValue("@countryId", opds.countryId);
                    command.Parameters.AddWithValue("@yearEvId", opds.yearEvId);

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
        //     
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
padr.fullAddress,padr.addressDesc ,op.yearEvId,ev2.name as enumYear,person.Id as pID from tblOffer_Production op
 join tblProductCatalog pc on pc.Id=op.product_Id and pc.Status=1
 left join tblEnumValue ev on ev.Id=op.state_eV_Id and ev.Status=1
 left join tblProductCatalogControl prc on prc.ProductId=op.product_Id and prc.Status=1
 left join tblEnumValue ev1 on ev1.Id=prc.EnumValueId and  ev1.Status=1
  join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
 left join tblPerson person on person.UserId=op.user_Id and person.Status=1
 left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
  left  join tblProductAddress padr on op.productAddress_Id=padr.Id and padr.Status=1
   join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id and au.Status=1

		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
     
   left join tblPRM_AdminUnit au1 on au1.Id=op.productOrigin and au1.Status=1
     left join tblEnumValue ev2 on ev2.Id=op.yearEvId and ev2.Status=1 
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
            var queryEnum = @" and yearEvId=@yearEvId";
            if (opds.productID != 0)
            {
                squery.Append(queryProductId);
            }
            if (opds.countryId != 0)
            {
                squery.Append(queryCountryID);
            }
            if (opds.yearEvId != 0)
            {
                squery.Append(queryEnum);
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
                    command.Parameters.AddWithValue("@yearEvId", opds.yearEvId);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {




                        result.Add(new ProductionDetail()
                        {
                            productionID = reader.GetInt64OrDefaultValue(0),

                            quantity = reader.GetDecimalOrDefaultValue(1),

                            description = reader.GetStringOrEmpty(2),
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
                            yearEvId = reader.GetInt64OrDefaultValue(21),
                            enumvaule_year = reader.GetStringOrEmpty(22),
                            personID = reader.GetInt64OrDefaultValue(23),
                            productParentName = reader.GetStringOrEmpty(24),
                            unitPriceAnnouncement = reader.GetDecimalOrDefaultValue(25),


                        });
                    }
                }



                connection.Close();
            }

            return result;
        }
        public decimal GetProductPriceByOfferID(Int64 offerID, Int64 productID)
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
        public decimal GetProductPriceByOfferID1(Int64 productID)
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
        public List<Price> GetDemandTotalPriceByYearEvId1(Int64 orgId, Int64 yearEvID)
        {

            List<Price> result = new List<Price>();
            var query = @" select op.unit_price*dp.quantity as totalUnitPrice,op.productId,op.partOfYear from tblProductPrice op
left join tblDemand_Production dp on dp.product_Id=op.productId and dp.Status=1
left join tblForeign_Organization fo on fo.Status=1 and fo.userId=dp.user_Id and fo.Status=1
where op.Status=1 --and op.productId=@productId and partOfYear=@partOfYear
   and fo.Id=@orgId and dp.yearEvId=@yearEvID";
            var query1 = @" select op.product_Id,cal.partOfyear,cal.transportation_eV_Id*cal.quantity as Price from tblDemand_Production op
join tblProductionCalendar cal on cal.demand_Id=op.Id and cal.Status=1
join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
where op.Status=1  and fo.Id=@orgId and op.yearEvId=@yearEvID ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {


                    command.Parameters.AddWithValue("@yearEvID", yearEvID);
                    command.Parameters.AddWithValue("@orgId", orgId);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        /* productCatalog = new tblProductCatalog()*/

                        result.Add(new Price()
                        {


                            productId = reader.GetInt64OrDefaultValue(0),
                            partOfYear = reader.GetInt64OrDefaultValue(1),
                            price = reader.GetDecimalOrDefaultValue(2),
                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<DemandPrice> GetDemandCalPriceByYearEvId(Int64 orgId, Int64 yearEvID)
        {

            List<DemandPrice> result = new List<DemandPrice>();

            var query1 = @" select op.product_Id,cal.partOfyear,cal.transportation_eV_Id,cal.quantity  from tblDemand_Production op
join tblProductionCalendar cal on cal.demand_Id=op.Id and cal.Status=1
join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
where op.Status=1  and fo.Id=@orgId and op.yearEvId=@yearEvID ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {


                    command.Parameters.AddWithValue("@yearEvID", yearEvID);
                    command.Parameters.AddWithValue("@orgId", orgId);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        /* productCatalog = new tblProductCatalog()*/

                        result.Add(new DemandPrice()
                        {


                            product_id = reader.GetInt64OrDefaultValue(0),
                            partOfYear = reader.GetInt64OrDefaultValue(1),
                            transportation_eV_Id = reader.GetInt64OrDefaultValue(2),
                            quantity = reader.GetDecimalOrDefaultValue(3),
                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public decimal GetDemandTotalPriceByYearEvId(Int64 orgId, Int64 yearEvID)
        {
            decimal count = 0;

            var query1 = @"select SUM(tb.totalPrice) as totalUnitPrice from(select op.*,dbo.StripWWWandCom(op.product_Id)* op.quantity as totalPrice from tblDemand_Production op

left join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
where op.Status=1  and op.yearEvId=@yearEvID and  fo.Id=@orgId
) as tb";
            var query = @" select op.unit_price*dp.quantity as totalUnitPrice from tblProductPrice op
left join tblDemand_Production dp on dp.product_Id=op.productId and dp.Status=1
left join tblForeign_Organization fo on fo.Status=1 and fo.userId=dp.user_Id and fo.Status=1
where op.Status=1 --and op.productId=@productId and partOfYear=@partOfYear
   and fo.Id=@orgId and dp.yearEvId=@yearEvID";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@yearEvID", yearEvID);
                    command.Parameters.AddWithValue("@orgId", orgId);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToDecimal(reader["totalUnitPrice"]);
                    }

                }
                connection.Close();
            }

            return count;
        }
        public decimal GetDemandTotalPriceByProductID(Int64 productId, Int64 partOfYear)
        {
            decimal count = 0;

            //            var query1 = @"select SUM(tb.totalPrice) as totalUnitPrice from(select op.*,dbo.StripWWWandCom(op.product_Id)* op.quantity as totalPrice from tblDemand_Production op
            //
            //left join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
            //where op.Status=1 and op.product_Id in (select product_Id from tblProductPrice pc where pc.Status=1 and pc.partOfYear=@partOfYear)
            //and op.product_Id=@product_Id
            //) as tb";
            var query1 = @" select unit_price from 
tblProductPrice op
where op.Status=1 and op.productId=@productId and partOfYear=@partOfYear and year=dbo.returnYear()
";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {


                    command.Parameters.AddWithValue("@productId", productId);
                    command.Parameters.AddWithValue("@partOfYear", partOfYear);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToDecimal(reader["unit_price"]);
                    }

                }
                connection.Close();
            }

            return count;
        }
        public decimal GetTotalPriceMarka(Int64 productId)
        {
            decimal count = 0;


            var query1 = @" select top 1 ISNULL(price.unit_price,0) as unitPrice,dbo.returnPartOfYear()as date1,dbo.returnYear()as rub1,
price.productId from tblProductPrice price
join tblProductCatalog pr on price.productId=pr.Id and   pr.Status=1
where price.Status=1 and 
(dbo.returnYear()>year and price.productId=@productId and price.Status=1  ) or
 (dbo.returnYear()=year and price.partOfYear<=dbo.returnPartOfYear() and price.productId=@productId and price.Status=1)
 and price.productId=@productId
 order by year desc,partOfYear desc
";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {


                    command.Parameters.AddWithValue("@productId", productId);

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
        public decimal GetTotalPriceMarkaNot(Int64 productId, Int64 partOfYear)
        {
            decimal count = 0;


            var query1 = @" select distinct dbo.StripWWWandCom(productId,@partOfYear) as unitPirce from tblProductPrice
where Status=1 and year=dbo.returnYear()  and productId=@productId 
";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {


                    command.Parameters.AddWithValue("@productId", productId);
                    command.Parameters.AddWithValue("@partOfYear", partOfYear);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToDecimal(reader["unitPirce"]);
                    }

                }
                connection.Close();
            }

            return count;
        }
        public Int64 GetProductControl33_OPC(Int64 productId)
        {
            Int64 count = 0;

            var query1 = @"select COUNT(*) as Count from 
tblProductCatalogControl op
where op.Status=1 and op.productId=@productId and EnumCategoryId=33

";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {



                    command.Parameters.AddWithValue("@productId", productId);

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
        public decimal GetTotalPriceMarkaNot_TotalDemand(Int64 productId, Int64 yearEvId, Int64 orgId)
        {
            decimal count = 0;


            var query1 = @" select distinct SUM(tb.totalPrice) as totalUnitPrice from(select op.*,dbo.StripWWWandCom1(@productId)* op.quantity as totalPrice 

from tblDemand_Production op

left join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
where op.Status=1 --and op.product_Id --in (select product_Id from tblProductPrice pc where pc.Status=1 and pc.partOfYear=1)
and op.product_Id=@productId and yearEvId=@yearEvId and fo.Id=@orgId
) as tb

";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {


                    command.Parameters.AddWithValue("@productId", productId);
                    command.Parameters.AddWithValue("@yearEvId", yearEvId);
                    command.Parameters.AddWithValue("@orgId", orgId);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToDecimal(reader["totalUnitPrice"]);
                    }

                }
                connection.Close();
            }

            return count;
        }
        public decimal GetTotalPriceMarka_totalDemand(Int64 productId, Int64 orgID)
        {
            decimal count = 0;


            var query1 = @" select top 1 ISNULL(price.unit_price,0),
dbo.returnPartOfYear()as date1,dbo.returnYear()as rub1,
price.productId from tblProductPrice price
join tblProductCatalog pr on price.productId=pr.Id and   pr.Status=1
join tblDemand_Production op on op.Status=1 and op.product_Id=price.productId
join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
where price.Status=1 and 
(dbo.returnYear()>year and price.productId=@productId and price.Status=1  ) or
 (dbo.returnYear()=year and price.partOfYear<=dbo.returnPartOfYear() and price.productId=@productId and price.Status=1)
 and price.productId=@productId and fo.Id=@Id
 order by year desc,partOfYear desc
";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {


                    command.Parameters.AddWithValue("@productId", productId);
                    command.Parameters.AddWithValue("@Id", orgID);

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
        public List<OrganizationList> GetOrganizationDetailist(DemandOfferProductsSearch ops)
        {

            var result = new List<OrganizationList>();
            StringBuilder squery = new StringBuilder();

            var query = @" 
   with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au
   where Id=1
   UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
  select tb.* from(select distinct fo.name as orgName,adr.fullAddress,adr.addressDesc,fo.Id as orgID,a.Name as regionName ,
u.Username ,ev.name as enumVYear
from tblForeign_Organization fo
join tblAddress adr on adr.user_Id=fo.userId and adr.Status=1
join tblDemand_Production op on op.user_Id=fo.userId and fo.Status=1
 join tblPRM_AdminUnit au on au.Id=adr.adminUnit_Id and au.Status=1 
join tblPRM_AdminUnit a on a.Id=au.ParentID and a.Status=1
join tblUser u on u.Id=op.user_Id and u.Status=1
join tblEnumValue ev on ev.Id=op.yearEvId and ev.Status=1
where fo.Status=1  




 

";
            var queryEnd = @") as tb
order by tb.regionName,tb.orgName
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY";
            var queryOrgName = @" and fo.name like '%'+@name+'%'";
            var queryOrgID = @" and  fo.Id=@orgID";
            var queryOParentRegionID = @" and  au.Id=@ParentRegionID";
            var queryState = @" and op.state_eV_Id=@state_eV_Id ";
            var queryYear = @" and op.yearEvId=@yearEvId";
            squery.Append(query);
            if (!string.IsNullOrEmpty(ops.organizationName))
            {
                squery.Append(queryOrgName);
            }
            if (ops.orgID != 0)
            {
                squery.Append(queryOrgID);
            }
            if (ops.regionID != 0)
            {
                squery.Append(queryOParentRegionID);
            }
            if (ops.stateEvId != 0)
            {
                squery.Append(queryState);
            }
            if (ops.yearEvId != 0)
            {
                squery.Append(queryYear);
            }

            squery.Append(queryEnd);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {



                    command.Parameters.AddWithValue("@orgID", ops.orgID);
                    command.Parameters.AddWithValue("@ParentRegionID", ops.regionID);
                    command.Parameters.AddWithValue("PageNo", ops.page);
                    command.Parameters.AddWithValue("RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@name", ops.organizationName.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@state_eV_Id", ops.stateEvId);
                    command.Parameters.AddWithValue("@yearEvId", ops.yearEvId);
                    //   command.Parameters.AddWithValue("RecordsPerPage",ops.);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OrganizationList()
                        {
                            orgName = reader.GetStringOrEmpty(0),
                            fullAddress = reader.GetStringOrEmpty(1),
                            addressDesc = reader.GetStringOrEmpty(2),

                            orgID = reader.GetInt64OrDefaultValue(3),
                            reionName = reader.GetStringOrEmpty(4),
                            username = reader.GetStringOrEmpty(5),
                            enumValueYear = reader.GetStringOrEmpty(6),

                        });
                    }

                }

                connection.Close();
            }

            return result;
        }
        public Int64 GetOrganizationCount(Int64 orgID)
        {
            Int64 count = 0;

            var query1 = @"select distinct COUNT(op.product_Id) as productSayi,fo.name from tblDemand_Production op
join tblForeign_Organization fo on fo.userId=op.user_Id and fo.Status=1
where op.Status=1 and fo.Id=@orgID
group by fo.name

";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {



                    command.Parameters.AddWithValue("@orgID", orgID);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader["productSayi"]);
                    }

                }
                connection.Close();
            }

            return count;
        }
        public List<Organizations> GetOrganizations(Int64 orgID)
        {

            var result = new List<Organizations>();
            StringBuilder squery = new StringBuilder();
            var query = @" 
;with name_tree as 
(
   select Id, parent_Id,name as s
   from tblForeign_Organization
  where Id = @orgID -- this is the starting point you want in your recursion
   union all
   select C.Id, C.parent_Id, name as r
   from tblForeign_Organization c
   join name_tree p on C.Id = P.parent_Id  -- this is the recursion
   -- Since your parent id is not NULL the recursion will happen continously.
   -- For that we apply the condition C.id<>C.parentid 
    AND C.Id<>C.parent_Id 
) 
-- Here you can insert directly to a temp table without CREATE TABLE synthax
select *
--INTO #TEMP
from name_tree where Id!=@orgID
OPTION (MAXRECURSION 0)

--SELECT * FROM #TEMP";
            //            var query1 = @" select fo.name,f.name as parentName,fo.parent_Id from tblForeign_Organization fo
            // join tblForeign_Organization f on f.Id=fo.parent_Id and f.Status=1
            //where fo.Status=1 and fo.parent_Id=@orgID";
            var QUERY12 = @"select tb.name,fo.name as parentName,tb.Id as d,fo.Id,fo.parent_Id,fo1.name as fo1Name,fo1.parent_Id aspID from(select fo.* from tblForeign_Organization
fo where fo.Status=1
) as tb
join tblForeign_Organization  fo on fo.Id=tb.parent_Id and fo.Status=1
left join tblForeign_Organization fo1 on fo.parent_Id=fo1.Id and fo1.Status=1
where tb.Id=@orgID
";
            var query1 = @" select tb.parentName+','+tb.fo1Name as orgName from(select tb.name,fo.name as parentName,tb.Id as d,fo.Id,fo.parent_Id,fo1.name as fo1Name,fo1.parent_Id aspID from(select fo.* from tblForeign_Organization
fo where fo.Status=1
) as tb
join tblForeign_Organization  fo on fo.Id=tb.parent_Id and fo.Status=1
left join tblForeign_Organization fo1 on fo.parent_Id=fo1.Id and fo1.Status=1
where  tb.Id=@orgID
) as tb";
            squery.Append(query);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {





                    command.Parameters.AddWithValue("@orgID", orgID);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Organizations()
                        {

                            orgName = reader.GetStringOrEmpty(2),

                        });
                    }

                }

                connection.Close();
            }

            return result;
        }

        public Int64 GetOrganizationDetailist_OPC(DemandOfferProductsSearch ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();

            var query = @" 
    with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au
   where Id=1
   UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
 select COUNT(*) as Count from(select distinct fo.name as orgName,adr.fullAddress,adr.addressDesc,fo.Id as orgID,
a.Name as regionName ,ev.name as enumVYear
from tblForeign_Organization fo
join tblAddress adr on adr.user_Id=fo.userId and adr.Status=1
join tblDemand_Production op on op.user_Id=fo.userId and fo.Status=1
 join tblPRM_AdminUnit au on au.Id=adr.adminUnit_Id and au.Status=1 
join tblPRM_AdminUnit a on a.Id=au.ParentID and a.Status=1
join tblEnumValue ev on ev.Id=op.yearEvId and ev.Status=1
where fo.Status=1 


 
 ";

            var queryOrgName = @" and fo.name like '%'+@name+'%'";
            var queryOrgID = @" and  fo.Id=@orgID";
            var queryOParentRegionID = @" and  au.Id=@ParentRegionID";
            var queryState = @" and op.state_eV_Id=@state_eV_Id ";
            var qeryEnd = @" ) as tb";
            var queryYear = @" and op.yearEvId=@yearEvId";
            squery.Append(query);
            if (!string.IsNullOrEmpty(ops.organizationName))
            {
                squery.Append(queryOrgName);
            }
            if (ops.orgID != 0)
            {
                squery.Append(queryOrgID);
            }
            if (ops.regionID != 0)
            {
                squery.Append(queryOParentRegionID);
            }
            if (ops.stateEvId != 0)
            {
                squery.Append(queryState);
            }
            if (ops.yearEvId != 0)
            {
                squery.Append(queryYear);
            }

            squery.Append(qeryEnd);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {



                    command.Parameters.AddWithValue("@orgID", ops.orgID);
                    command.Parameters.AddWithValue("@ParentRegionID", ops.regionID);
                    command.Parameters.AddWithValue("PageNo", ops.page);
                    command.Parameters.AddWithValue("RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@name", ops.organizationName.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@state_eV_Id", ops.stateEvId);
                    command.Parameters.AddWithValue("@yearEvId", ops.yearEvId);
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
        public List<EvaluationDetails> GetEvaluation_OP(EvaluationObjects ops)
        {

            var result = new List<EvaluationDetails>();
            StringBuilder squery = new StringBuilder();
            var query1 = @" DECLARE @list NVARCHAR(MAX)
SET @list =@EvumValId;

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
END;
with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblEvaluation au
 
";
            var query2 = @"

   UNION ALL 
   SELECT au.Id

  FROM dbo.tblEvaluation au  JOIN cte c ON au.parentId = c.Id
  
  )
select tb.*,ev.Name as parentName,enum.Name as enumV,enum.description,enum1.description as evaluationQuestionLocationDesc,
enum2.description as offerUserTypeDesc from(select * from tblEvaluation
where Status=1
) as tb
left join tblEvaluation ev on ev.Id=tb.ParentId and ev.Status=1
left join tblEnumValue enum on enum.Id=tb.EvumValId and enum.Status=1
left join tblEnumValue enum1 on enum1.Id=tb.evaluationQuestionLocationEValId and enum.Status=1
left join tblEnumValue enum2 on enum2.Id=tb.offerUserTypeEValId and enum.Status=1
where tb.Status=1  and  tb.Id
			   in (
							select Id from cte
							 		)   ";
            var queryEnd = @" order by tb.EvumValId,tb.Number,tb.Sort
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY";
            var queryParentID = @"  and tb.ParentId=@ParentId";
            var queryEnum = @" and tb.EvumValId in (SELECT number FROM @tbl)";
            var queryName = @" and tb.Name  like '%'+@Name+'%'";
            var queryParent = @" and ev.Name like '%'+@ParentName+'%'";
            var queryIsActive = @" and tb.isActive=@isActive";

            var queryOfferUser = @" WHERE offerUserTypeEValId=@offerUserTypeEValId";
            var queryevaluationQuestionLocationEValId = @" WHERE evaluationQuestionLocationEValId=@evaluationQuestionLocationEValId";
            var queryOff = @" WHERE offerUserTypeEValId=@offerUserTypeEValId and  evaluationQuestionLocationEValId=@evaluationQuestionLocationEValId";
            squery.Append(query1);
            if (ops.offerUserTypeEValId > 0)
            {
                squery.Append(queryOfferUser);
            }
            if (ops.evaluationQuestionLocationEValId > 0)
            {
                squery.Append(queryevaluationQuestionLocationEValId);
            }
            if (ops.evaluationQuestionLocationEValId>0 && ops.offerUserTypeEValId>0)
            {
                squery.Append(queryOff);
            }
            squery.Append(query2);
            if (ops.parentId > 0)
            {
                squery.Append(queryParentID);
            }
            if (!String.IsNullOrEmpty(ops.enumValueIDList))
            {
                squery.Append(queryEnum);
            }
            if (!String.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);
            }
            if (!String.IsNullOrEmpty(ops.parentName))
            {
                squery.Append(queryParent);
            }
            if (ops.isActive != null)
            {
                squery.Append(queryIsActive);
            }
           
           
            squery.Append(queryEnd);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {





                    command.Parameters.AddWithValue("@RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@ParentId", ops.parentId);
                    command.Parameters.AddWithValue("@EvumValId", ops.enumValueIDList.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@Name", ops.name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@ParentName", ops.parentName.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@offerUserTypeEValId", ops.offerUserTypeEValId);
                    command.Parameters.AddWithValue("@evaluationQuestionLocationEValId", ops.evaluationQuestionLocationEValId);
                    if (ops.isActive != null)
                    {
                        command.Parameters.AddWithValue("@isActive", ops.isActive);
                    }

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new EvaluationDetails()
                        {

                            Id = reader.GetInt64OrDefaultValue(0),
                            ParentId = reader.GetInt64OrDefaultValue(1),
                            Name = reader.GetStringOrEmpty(2),
                            Description = reader.GetStringOrEmpty(3),
                            EvumValId = reader.GetInt64OrDefaultValue(4),
                            Point = reader.GetInt64OrDefaultValue(5),
                            IsMultiselect = reader.GetBoolean(6),
                            IsAttachment = reader.GetBoolean(7),
                            IsNote = reader.GetBoolean(8),
                            IsQuestion = reader.GetBoolean(9),
                            IsAnswer = reader.GetBoolean(10),
                            Sort = reader.GetInt64OrDefaultValue(11),
                            IsDeleted = reader.GetBoolean(12),
                            isActive = reader.GetBoolean(13),
                            Status = reader.GetInt64OrDefaultValue(14),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(15),
                            createdUser = reader.GetStringOrEmpty(16),
                            createdDate = reader.GetInt64OrDefaultValue(17),
                            updatedUser = reader.GetStringOrEmpty(18),
                            updatedDate = reader.GetInt64OrDefaultValue(19),
                            number = reader.GetStringOrEmpty(20),
                            offerUserTypeEValId=reader.GetInt64OrDefaultValue(21),
                            isRequired=reader.GetBoolean(22),
                            evaluationQuestionLocationEValId=reader.GetInt64OrDefaultValue(23),
                            parentName = reader.GetStringOrEmpty(24),
                            enumValueName = reader.GetStringOrEmpty(25),
                            evaluationQuestionLocationDesc=reader.GetStringOrEmpty(27),
                            offerUserTypeDesc = reader.GetStringOrEmpty(28),




                        });
                    }

                }

                connection.Close();
            }

            return result;
        }
        public Int64 GetEvaluation_OPC(EvaluationObjects ops)
        {

            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query1 = @" DECLARE @list NVARCHAR(MAX)
SET @list =49;

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
END;
with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblEvaluation au

 ";
            var query = @"

UNION ALL 
   SELECT au.Id

  FROM dbo.tblEvaluation au  JOIN cte c ON au.parentId = c.Id
  
  )
select COUNT(*) as Count from( select tb.*,ev.Name as parentName from(select * from tblEvaluation
where Status=1
) as tb
left join tblEvaluation ev on ev.Id=tb.ParentId and ev.Status=1 
left join tblEnumValue enum on enum.Id=tb.EvumValId and enum.Status=1
where tb.Status=1 and  tb.Id
			   in (
							select Id from cte
							 		) ";
            var queryEnd = @" ) as tb";
            var queryParentID = @"  and tb.ParentId=@ParentId";
            var queryEnum = @" and tb.EvumValId in (SELECT number FROM @tbl)";
            var queryName = @" and tb.Name  like '%'+@Name+'%'";
            var queryParent = @" and ev.Name like '%'+@ParentName+'%'";
            var queryIsActive = @" and tb.isActive=@isActive";
           
            var queryOfferUser = @" WHERE offerUserTypeEValId=@offerUserTypeEValId";
            var queryevaluationQuestionLocationEValId = @" WHERE evaluationQuestionLocationEValId=@evaluationQuestionLocationEValId";
            var queryOff = @" WHERE offerUserTypeEValId=@offerUserTypeEValId and  evaluationQuestionLocationEValId=@evaluationQuestionLocationEValId";
            squery.Append(query1);
            if (ops.offerUserTypeEValId > 0)
            {
                squery.Append(queryOfferUser);
            }
            if (ops.evaluationQuestionLocationEValId > 0)
            {
                squery.Append(queryevaluationQuestionLocationEValId);
            }
            if (ops.evaluationQuestionLocationEValId > 0 && ops.offerUserTypeEValId > 0)
            {
                squery.Append(queryOff);
            }
            squery.Append(query);
            if (ops.parentId > 0)
            {
                squery.Append(queryParentID);
            }
            if (!String.IsNullOrEmpty(ops.enumValueIDList))
            {
                squery.Append(queryEnum);
            }
            if (!String.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);
            }
            if (!String.IsNullOrEmpty(ops.parentName))
            {
                squery.Append(queryParent);
            }
            if (ops.isActive  !=null)
            {
                squery.Append(queryIsActive);
            }
          
            squery.Append(queryEnd);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {





                    command.Parameters.AddWithValue("@RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@ParentId", ops.parentId);
                    command.Parameters.AddWithValue("@EvumValId", ops.enumValueIDList.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@Name", ops.name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@ParentName", ops.parentName.GetStringOrEmptyData());
                    if (ops.isActive != null)
                    {
                        command.Parameters.AddWithValue("@isActive", ops.isActive);
                    }

                    command.Parameters.AddWithValue("@offerUserTypeEValId", ops.offerUserTypeEValId);
                    command.Parameters.AddWithValue("@evaluationQuestionLocationEValId", ops.evaluationQuestionLocationEValId);
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
        public Int64 GetEvaluationAttachment_OPC(EvaluationObjects ops)
        {

            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query = @"
select COUNT(*) as Count from( select * from tblEvaluationAttachment
where Status=1 

";
            var queryEnd = @" ) as tb";
            var queryUserId = @"  and  UserId=@UserId";
            var queryisApproved = @" and isApproved=@isApproved";
            var queryisApproved1 = @" and isApproved=1";
            var querygroupId = @" and groupId  like '%'+@groupId+'%'";
            var queryEvaluationId = @" and EvaluationId=@EvaluationId";
            squery.Append(query);
            if (ops.userId > 0)
            {
                squery.Append(queryUserId);
            }
            if (!String.IsNullOrEmpty(ops.groupId))
            {
                squery.Append(querygroupId);
            }
            if (ops.isApproved !=null)
            {
                squery.Append(queryisApproved);
            }
           
            if (ops.evaluationId > 0)
            {
                squery.Append(queryEvaluationId);
            }
            squery.Append(queryEnd);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {






                    command.Parameters.AddWithValue("@RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@UserId", ops.userId);
                    command.Parameters.AddWithValue("@EvaluationId", ops.evaluationId);
                    command.Parameters.AddWithValue("@groupId", ops.groupId.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@isApproved", ops.isApproved);

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
        public List<EvaluationAttachmentDetails> GetEvaluationAttachment_OP(EvaluationObjects ops)
        {

            var result = new List<EvaluationAttachmentDetails>();
            StringBuilder squery = new StringBuilder();
            var query = @" select * from tblEvaluationAttachment
where Status=1
  ";
            var queryEnd = @" order by EvaluationId
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY";
            var queryUserId = @"  and  UserId=@UserId";
            var queryisApproved = @" and isApproved=@isApproved";
            var queryisApproved1 = @" and isApproved=1";
            var querygroupId = @" and groupId  like '%'+@groupId+'%'";
            var queryEvaluationId = @" and EvaluationId=@EvaluationId";
            squery.Append(query);
            if (ops.userId > 0)
            {
                squery.Append(queryUserId);
            }
            if (!String.IsNullOrEmpty(ops.groupId))
            {
                squery.Append(querygroupId);
            }
            if (ops.isApproved != null)
            {
                squery.Append(queryisApproved);
            }
          
            if (ops.evaluationId > 0)
            {
                squery.Append(queryEvaluationId);
            }
            squery.Append(queryEnd);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {





                    command.Parameters.AddWithValue("@RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@UserId", ops.userId);
                    command.Parameters.AddWithValue("@EvaluationId", ops.evaluationId);
                    command.Parameters.AddWithValue("@groupId", ops.groupId.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@isApproved", ops.isApproved);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new EvaluationAttachmentDetails()
                        {

                            Id = reader.GetInt64OrDefaultValue(0),
                            UserId = reader.GetInt64OrDefaultValue(1),
                            EvaluationId = reader.GetInt64OrDefaultValue(2),
                            Status = reader.GetInt64OrDefaultValue(3),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(4),
                            createdUser = reader.GetStringOrEmpty(5),
                            createdDate = reader.GetInt64OrDefaultValue(6),
                            updatedUser = reader.GetStringOrEmpty(7),
                            updatedDate = reader.GetInt64OrDefaultValue(8),
                            documentUrl = reader.GetStringOrEmpty(9),
                            documentName = reader.GetStringOrEmpty(10),
                            documentRealName = reader.GetStringOrEmpty(11),
                            documentSize = reader.GetInt64OrDefaultValue(12),
                            document_type_ev_Id = reader.GetStringOrEmpty(13),
                            groupId = reader.GetStringOrEmpty(14),
                            isApproved = reader.GetBoolean(15),






                        });
                    }

                }

                connection.Close();
            }

            return result;
        }
        public Int64 GetEvaluationResult_OPC(EvaluationObjects ops)
        {

            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query = @"
select COUNT(*) as Count from( select * from tblEvaluationResult
where Status=1 

";
            var queryEnd = @" ) as tb";
            var queryUserId = @"  and  UserId=@UserId";
            var queryisApproved = @" and isApproved=@isApproved";
            var queryisApproved1 = @" and isApproved=1";
            var querygroupId = @" and groupId  like '%'+@groupId+'%'";
            var queryEvaluationId = @" and EvaluationId=@EvaluationId";
            squery.Append(query);
            if (ops.userId > 0)
            {
                squery.Append(queryUserId);
            }
            if (!String.IsNullOrEmpty(ops.groupId))
            {
                squery.Append(querygroupId);
            }
            if (ops.isApproved != null)
            {
                squery.Append(queryisApproved);
            }

            if (ops.evaluationId > 0)
            {
                squery.Append(queryEvaluationId);
            }
            squery.Append(queryEnd);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {






                    command.Parameters.AddWithValue("@RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@UserId", ops.userId);
                    command.Parameters.AddWithValue("@EvaluationId", ops.evaluationId);
                    command.Parameters.AddWithValue("@groupId", ops.groupId.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@isApproved", ops.isApproved);

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
        public List<tblEvaluationResult> GetEvaluationResult_OP(EvaluationObjects ops)
        {

            var result = new List<tblEvaluationResult>();
            StringBuilder squery = new StringBuilder();
            var query = @" select * from tblEvaluationResult
where Status=1
  ";
            var queryEnd = @" order by EvaluationId
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY";
            var queryUserId = @"  and  UserId=@UserId";
            var queryisApproved = @" and isApproved=@isApproved";
            var queryisApproved1 = @" and isApproved=1";
            var querygroupId = @" and groupId  like '%'+@groupId+'%'";
            var queryEvaluationId = @" and EvaluationId=@EvaluationId";
            squery.Append(query);
            if (ops.userId > 0)
            {
                squery.Append(queryUserId);
            }
            if (!String.IsNullOrEmpty(ops.groupId))
            {
                squery.Append(querygroupId);
            }
            if (ops.isApproved != null)
            {
                squery.Append(queryisApproved);
            }

            if (ops.evaluationId > 0)
            {
                squery.Append(queryEvaluationId);
            }
            squery.Append(queryEnd);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {





                    command.Parameters.AddWithValue("@RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@UserId", ops.userId);
                    command.Parameters.AddWithValue("@EvaluationId", ops.evaluationId);
                    command.Parameters.AddWithValue("@groupId", ops.groupId.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@isApproved", ops.isApproved);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new tblEvaluationResult()
                        {

                            Id = reader.GetInt64OrDefaultValue(0),
                            UserId = reader.GetInt64OrDefaultValue(1),
                            EvaluationId = reader.GetInt64OrDefaultValue(2),
                            Point=reader.GetInt64OrDefaultValue(3),
                            Note=reader.GetStringOrEmpty(4),
                            Status = reader.GetInt64OrDefaultValue(5),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(6),
                            createdUser = reader.GetStringOrEmpty(7),
                            createdDate = reader.GetInt64OrDefaultValue(8),
                            updatedUser = reader.GetStringOrEmpty(9),
                            updatedDate = reader.GetInt64OrDefaultValue(10),
                            
                            groupId = reader.GetStringOrEmpty(11),
                            isApproved = reader.GetBoolean(12),






                        });
                    }

                }

                connection.Close();
            }

            return result;
        }
        public Int64 GetEvaluationResultQuestion_OPC(EvaluationObjects ops)
        {

            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query = @"
select COUNT(*) as Count from( select * from tblEvaluationResultQuestion
where Status=1 

";
            var queryEnd = @" ) as tb";
            var queryUserId = @"  and  UserId=@UserId";
            var queryisApproved = @" and isApproved=@isApproved";
            var queryisApproved1 = @" and isApproved=1";
            var querygroupId = @" and groupId  like '%'+@groupId+'%'";
            var queryEvaluationId = @" and EvaluationId=@EvaluationId";
            squery.Append(query);
            if (ops.userId > 0)
            {
                squery.Append(queryUserId);
            }
            if (!String.IsNullOrEmpty(ops.groupId))
            {
                squery.Append(querygroupId);
            }
            if (ops.isApproved != null)
            {
                squery.Append(queryisApproved);
            }

            if (ops.evaluationId > 0)
            {
                squery.Append(queryEvaluationId);
            }
            squery.Append(queryEnd);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {






                    command.Parameters.AddWithValue("@RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@UserId", ops.userId);
                    command.Parameters.AddWithValue("@EvaluationId", ops.evaluationId);
                    command.Parameters.AddWithValue("@groupId", ops.groupId.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@isApproved", ops.isApproved);

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
        public List<tblEvaluationResultQuestion> GetEvaluationResultQuestion_OP(EvaluationObjects ops)
        {

            var result = new List<tblEvaluationResultQuestion>();
            StringBuilder squery = new StringBuilder();
            var query = @" select * from tblEvaluationResultQuestion
where Status=1
  ";
            var queryEnd = @" order by EvaluationId
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY";
            var queryUserId = @"  and  UserId=@UserId";
            var queryisApproved = @" and isApproved=@isApproved";
            var queryisApproved1 = @" and isApproved=1";
            var querygroupId = @" and groupId  like '%'+@groupId+'%'";
            var queryEvaluationId = @" and EvaluationId=@EvaluationId";
            squery.Append(query);
            if (ops.userId > 0)
            {
                squery.Append(queryUserId);
            }
            if (!String.IsNullOrEmpty(ops.groupId))
            {
                squery.Append(querygroupId);
            }
            if (ops.isApproved != null)
            {
                squery.Append(queryisApproved);
            }

            if (ops.evaluationId > 0)
            {
                squery.Append(queryEvaluationId);
            }
            squery.Append(queryEnd);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {





                    command.Parameters.AddWithValue("@RecordsPerPage", ops.page_size);
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@UserId", ops.userId);
                    command.Parameters.AddWithValue("@EvaluationId", ops.evaluationId);
                    command.Parameters.AddWithValue("@groupId", ops.groupId.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@isApproved", ops.isApproved);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new tblEvaluationResultQuestion()
                        {

                            Id = reader.GetInt64OrDefaultValue(0),
                            UserId = reader.GetInt64OrDefaultValue(1),
                            EvaluationId = reader.GetInt64OrDefaultValue(2),
                          
                            Note = reader.GetStringOrEmpty(3),
                            Status = reader.GetInt64OrDefaultValue(4),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(5),
                            createdUser = reader.GetStringOrEmpty(6),
                            createdDate = reader.GetInt64OrDefaultValue(7),
                            updatedUser = reader.GetStringOrEmpty(8),
                            updatedDate = reader.GetInt64OrDefaultValue(9),

                            groupId = reader.GetStringOrEmpty(10),
                            isApproved = reader.GetBoolean(11),






                        });
                    }

                }

                connection.Close();
            }

            return result;
        }
        public List<tblEnumValue> GetEnumValueListByProductID(string productIdList)
        {
            var result = new List<tblEnumValue>();

            var query = @" DECLARE @list NVARCHAR(MAX)
SET @list =@productIdList
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
select prc.EnumValueId,ec.name as  evList,pc.Id,pc.ProductName,pc1.ProductName as parentName from tblProductCatalog pc
join tblProductCatalogControl prc on prc.ProductId=pc.Id and prc.Status=1
join tblEnumValue ec on ec.Id=prc.EnumValueId and ec.Status=1
join tblProductCatalog pc1 on pc1.Id=pc.ProductCatalogParentID and pc1.Status=1
where pc.Status=1 and EnumCategoryId=34 and  pc.Id in (SELECT number FROM @tbl)





 ";

            var query1 = @"  DECLARE @list NVARCHAR(MAX)
SET @list =@productIdList
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
select ec.*from tblProductCatalog pc
join tblProductCatalogControl prc on prc.ProductId=pc.Id and prc.Status=1
join tblEnumValue ec on ec.Id=prc.EnumValueId and ec.Status=1
join tblProductCatalog pc1 on pc1.Id=pc.ProductCatalogParentID and pc1.Status=1

where pc.Status=1 and EnumCategoryId=34 and  pc.Id in (SELECT number FROM @tbl)
group by ec.Id,ec.enumCategory_enumCategoryId,ec.createdDate,ec.createdUser,ec.description,ec.name,
ec.LastUpdatedStatus,ec.updatedDate,ec.sort,ec.Status,ec.updatedDate,ec.updatedUser ";
         
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query1, connection))
                {
                    command.Parameters.AddWithValue("@productIdList", productIdList.GetStringOrEmptyData());

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new tblEnumValue()
                        {
                            Id=reader.GetInt64OrDefaultValue(0),
                            enumCategory_enumCategoryId=reader.GetInt64OrDefaultValue(1),
                            name=reader.GetStringOrEmpty(2),
                            description=reader.GetStringOrEmpty(3),
                            Status=reader.GetInt64OrDefaultValue(4),
                            LastUpdatedStatus=reader.GetInt64OrDefaultValue(5),
                            createdUser=reader.GetStringOrEmpty(6),
                            createdDate=reader.GetInt64OrDefaultValue(7),
                            updatedUser=reader.GetStringOrEmpty(8),
                            updatedDate=reader.GetInt64OrDefaultValue(9),
                            sort=reader.GetInt64OrDefaultValue(10),

                        });
                    }


                    connection.Close();
                }

                return result;
            }
        }
    }
}

