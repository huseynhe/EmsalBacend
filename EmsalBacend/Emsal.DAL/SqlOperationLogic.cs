using Emsal.DAL.CustomObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Emsal.Utility.CustomObjects;

namespace Emsal.DAL
{
    public class SqlOperationLogic
    {


        public List<ProductionDetail> GetProductionDetailist()
        {
            var result = new List<ProductionDetail>();
            var query = "select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description, "
                         + " FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc, "
                         + " ev.name as OlcuVahidi from (  "
                         + " select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumValueId , "
                         + " pa.fullAddress,pa.addressDesc "
                         + " from [dbo].[tblOffer_Production] op  "
                         + " join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id  "
                         + " join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id "
                         + " join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id   "
                         + " join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id  "
                         + " join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id  "
                         + " where  ec.name='State' and ev.name='Tesdiqlenen'  "
                         + " ) as FirstTable  "
                         + " join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id ";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionDetail()
                        {
                            productionID = reader.GetInt64OrDefaultValue(0),
                            unitPrice = reader.GetDecimalOrDefaultValue(1),
                            quantity = reader.GetDecimalOrDefaultValue(2),
                            description = reader.GetStringOrEmpty(3),
                            productId = reader.GetInt32(4),
                            productName = reader.GetStringOrEmpty(5),
                            Status = reader.GetStringOrEmpty(6),
                            fullAddress = reader.GetStringOrEmpty(7),
                            addressDesc = reader.GetStringOrEmpty(8),
                            enumCategoryId = reader.GetInt64OrDefaultValue(9),
                            enumValueId = reader.GetInt64OrDefaultValue(10),
                            enumValueName = reader.GetStringOrEmpty(11),
                            userId = reader.GetInt64OrDefaultValue(12),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<tblPRM_AdminUnit> GetAdminUnitListForID(Int64 ID)
        {
            var result = new List<tblPRM_AdminUnit>();
            var query = @"  WITH cte AS( 
                      SELECT  Id, ParentID,  name,  CAST(Id AS varbinary(max)) AS Level 
                     FROM    tblPRM_AdminUnit WHERE   Id = @ID
                      UNION ALL 
                      SELECT  t.Id, t.ParentID,t.name, Level + CAST(t.Id AS varbinary(max)) AS Level 
                      FROM    tblPRM_AdminUnit t INNER JOIN cte r ON r.ParentID = t.Id) 
                      SELECT  cte.Id,cte.ParentID,cte.name,cte.Level 
                     FROM    cte order by cte.Level desc";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@ID", ID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new tblPRM_AdminUnit()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            ParentID = reader.GetInt64OrDefaultValue(1),
                            Name = reader.GetStringOrEmpty(2),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<tblProductCatalog> GetProductCatalogListForID(Int64 ID)
        {
            var result = new List<tblProductCatalog>();
            var query = "WITH cte AS( "
                     + " SELECT  Id, ProductCatalogParentID,  ProductName,  CAST(Id AS varbinary(max)) AS Level "
                     + " FROM    dbo.tblProductCatalog WHERE   Id = @ID  "
                     + " UNION ALL "
                     + " SELECT  t.Id, t.ProductCatalogParentID,t.ProductName, Level + CAST(t.Id AS varbinary(max)) AS Level "
                     + " FROM      dbo.tblProductCatalog t INNER JOIN cte r ON r.ProductCatalogParentID = t.Id) "
                     + " SELECT  cte.Id,cte.ProductCatalogParentID,cte.ProductName,cte.Level "
                     + " FROM    cte order by cte.Level desc ";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@ID", ID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new tblProductCatalog()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            ProductCatalogParentID = reader.GetInt64OrDefaultValue(1),
                            ProductName = reader.GetStringOrEmpty(2),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<ProductionDetail> GetPotensialProductionDetailistForUser(Int64 userID)
        {
            var result = new List<ProductionDetail>();
            var query = @" select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                       FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id from (   
                      select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                      pa.fullAddress,pa.addressDesc ,op.user_Id 
                      from [dbo].[tblPotential_Production] op  
                      left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id    and pc.Status=1
                     left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id   and pa.Status=1
                     left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                    left join [dbo].[tblProductionControl] prc on op.Id=prc.Potential_Production_Id and prc.Status=1
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                    left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id    and ec.Status=1
                     where  op.user_Id=@userID and op.isSelected=1  and op.Status=1
                    ) as FirstTable   
                  left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1 ;";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
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


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<ProductionDetail> GetPotensialProductionDetailistForCreateUser(string createdUser)
        {
            var result = new List<ProductionDetail>();
            var query = @" select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                       FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id from (   
                      select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                      pa.fullAddress,pa.addressDesc ,op.user_Id 
                      from [dbo].[tblPotential_Production] op  
                      left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id    and pc.Status=1
                     left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id   and pa.Status=1
                     left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                    left join [dbo].[tblProductionControl] prc on op.Id=prc.Potential_Production_Id and prc.Status=1
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                    left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id    and ec.Status=1
                     where    op.createdUser=@createdUser and op.isSelected=1  and op.Status=1
                    ) as FirstTable   
                  left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1 ;";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@createdUser", createdUser);
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


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<ProductionDetail> GetPotensialProductionDetailistForEValueId(Int64 state_eV_Id)
        {
            var result = new List<ProductionDetail>();
            var query = @" select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                   FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                    ev.name as KategoryName,FirstTable.user_Id from (   
                   select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                   pa.fullAddress,pa.addressDesc ,op.user_Id 
                    from [dbo].[tblPotential_Production] op  
                   left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id   and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                    left join [dbo].[tblProductionControl] prc on op.Id=prc.Potential_Production_Id    and prc.Status=1 
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                   left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id    and ec.Status=1
                   where  op.Status=1  and op.state_eV_Id=@state_eV_Id
                   ) as FirstTable   
                    left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id  and ev.Status=1 ;";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", state_eV_Id);
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


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<ProductionDetail> GetPotensialProductionDetailistForMonitoringEVId(Int64 userID, Int64 monintoring_eV_Id)
        {
            var result = new List<ProductionDetail>();
            var query = @" 
select table3.* from( select table2.*,person.Id as personID, adr.Id as adressId, adr.adminUnit_Id from (

select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                   FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                    ev.name as KategoryName,FirstTable.user_Id from (   
                   select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                   pa.fullAddress,pa.addressDesc ,op.user_Id 
                    from [dbo].[tblPotential_Production] op  
                   left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id   and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                    left join [dbo].[tblProductionControl] prc on op.Id=prc.Potential_Production_Id    and prc.Status=1 
                    left join [dbo].[tblEnumValue] ev on op.monitoring_eV_Id=ev.Id    and ev.Status=1
                   left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id    and ec.Status=1
                   where  op.Status=1    and op.monitoring_eV_Id=@monintoring_eV_Id
                   ) as FirstTable   
                      join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id  and ev.Status=1) as table2
					  join [dbo].[tblPerson] person on table2.user_Id=person.UserId
					  join [dbo].[tblAddress] adr on person.address_Id=adr.Id) as table3
					 where table3.adminUnit_Id in 			
					 
				(select table4.Id from 
				(select us.Id as userID, aunit.Id ,aunit.Name from [dbo].[tblUser] us,
				[dbo].[tblPRM_ASCBranch] ascbranch,

				[dbo].[tblBranchResponsibility] branchResp,

				[dbo].[tblPRM_AdminUnit] aunit

				 where  us.ASC_ID=ascbranch.Id
				 and ascbranch.Id= branchResp.branchId
	             and branchResp.branchType_eVId=51
				 and (aunit.Id=branchResp.adminUnitId 
				 or aunit.ParentID= branchResp.adminUnitId)
				 --and branchResp.adminUnitId=80400001
                  and us.Id=@userID
				) as table4 )
               ;";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@monintoring_eV_Id", monintoring_eV_Id);
                    command.Parameters.AddWithValue("@userID", userID);
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


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<ProductionDetail> GetPotensialProductionDetailistForStateEVId(Int64 userID, Int64 state_eV_Id)
        {
            var result = new List<ProductionDetail>();
            var query = @" 
select table3.* from( select table2.*,person.Id as personID, adr.Id as adressId, adr.adminUnit_Id from (

select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                   FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                    ev.name as KategoryName,FirstTable.user_Id from (   
                   select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                   pa.fullAddress,pa.addressDesc ,op.user_Id 
                    from [dbo].[tblPotential_Production] op  
                   left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id   and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                    left join [dbo].[tblProductionControl] prc on op.Id=prc.Potential_Production_Id    and prc.Status=1 
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                   left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id    and ec.Status=1
                   where  op.Status=1    and op.state_eV_Id=@state_eV_Id
                   ) as FirstTable   
                     join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id  and ev.Status=1) as table2
					  join [dbo].[tblPerson] person on table2.user_Id=person.UserId
					  join [dbo].[tblAddress] adr on person.address_Id=adr.Id) as table3
					 where table3.adminUnit_Id in 			
					 
				(select table4.Id from 
				(select us.Id as userID, aunit.Id ,aunit.Name from [dbo].[tblUser] us,
                 [dbo].[tblPRM_KTNBranch]  ktnbranch,
				[dbo].[tblBranchResponsibility] branchResp,

				[dbo].[tblPRM_AdminUnit] aunit

				 where  us.KTN_ID=ktnbranch.Id
				 and ktnbranch.Id= branchResp.branchId
	              and branchResp.branchType_eVId=52
				 and (aunit.Id=branchResp.adminUnitId 
				 or aunit.ParentID= branchResp.adminUnitId)
				 --and branchResp.adminUnitId=80400001
                 and us.Id=@userID
				) as table4 )
               ;";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", state_eV_Id);
                    command.Parameters.AddWithValue("@userID", userID);
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


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }



        public List<ProductionDetail> GetOfferProductionDetailistForUser(Int64 userID)
        {
            var result = new List<ProductionDetail>();
            var query = @" select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                     FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id from (   
                      select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                      pa.fullAddress,pa.addressDesc ,op.user_Id 
                     from [dbo].[tblOffer_Production] op   
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1
                     left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1 
                     left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                     left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1   
                     where  op.user_Id=@userID and op.isSelected=1 and op.Status=1
                    ) as FirstTable   
                    left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1 ;";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
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


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<ProductionDetail> GetOfferProductionDetailistForEValueId(Int64 state_eV_Id)
        {
            var result = new List<ProductionDetail>();
            var query = @" select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id from (   
                     select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id 
                      from [dbo].[tblOffer_Production] op  
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                     left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id and ev.Status=1
                    left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1   
                      where   op.state_eV_Id= @state_eV_Id and op.Status=1
                    ) as FirstTable   
                     left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1;";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", state_eV_Id);
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


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<ProductionDetail> GetOfferProductionDetailistForMonitoringEVId(Int64 userID, Int64 monintoring_eV_Id)
        {
            var result = new List<ProductionDetail>();
            var query = @" 
select table3.* from( select table2.*,person.Id as personID, adr.Id as adressId, adr.adminUnit_Id from (select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id from (   
                     select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id 
                      from [dbo].[tblOffer_Production] op  
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                     left join [dbo].[tblEnumValue] ev on op.monitoring_eV_Id=ev.Id and ev.Status=1
                    left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1   
                      where    op.Status=1  and op.monitoring_eV_Id=@monintoring_eV_Id
                    ) as FirstTable   
                    left  join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1) as table2
					  join [dbo].[tblPerson] person on table2.user_Id=person.UserId
					  join [dbo].[tblAddress] adr on person.address_Id=adr.Id) as table3
					 where table3.adminUnit_Id in 			
					 
				(select table4.Id from 
				(select us.Id as userID, aunit.Id ,aunit.Name from [dbo].[tblUser] us,
				[dbo].[tblPRM_ASCBranch] ascbranch,

				[dbo].[tblBranchResponsibility] branchResp,

				[dbo].[tblPRM_AdminUnit] aunit

				 where  us.ASC_ID=ascbranch.Id
				 and ascbranch.Id= branchResp.branchId
	              and branchResp.branchType_eVId=51
				 and (aunit.Id=branchResp.adminUnitId 
				 or aunit.ParentID= branchResp.adminUnitId)
				 --and branchResp.adminUnitId=80400001
                 and us.Id=@userID
				) as table4 )

					";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@monintoring_eV_Id", monintoring_eV_Id);
                    command.Parameters.AddWithValue("@userID", userID);
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


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<ProductionDetail> GetOfferProductionDetailistForStateEVId(Int64 userID, Int64 state_eV_Id)
        {
            var result = new List<ProductionDetail>();
            var query = @" 
select table3.* from( select table2.*,person.Id as personID, adr.Id as adressId, adr.adminUnit_Id from (select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id from (   
                     select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id 
                      from [dbo].[tblOffer_Production] op  
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                     left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id and ev.Status=1
                    left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1   
                      where    op.Status=1  and op.state_eV_Id=@state_eV_Id
                    ) as FirstTable   
                   left   join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1) as table2
					  join [dbo].[tblPerson] person on table2.user_Id=person.UserId
					  join [dbo].[tblAddress] adr on person.address_Id=adr.Id) as table3
					 where table3.adminUnit_Id in 			
					 
					(select table4.Id from 
				(select us.Id as userID, aunit.Id ,aunit.Name from [dbo].[tblUser] us,
                 [dbo].[tblPRM_KTNBranch]  ktnbranch,
				[dbo].[tblBranchResponsibility] branchResp,

				[dbo].[tblPRM_AdminUnit] aunit

				 where  us.KTN_ID=ktnbranch.Id
				 and ktnbranch.Id= branchResp.branchId
	             and branchResp.branchType_eVId=52
				 and (aunit.Id=branchResp.adminUnitId 
				 or aunit.ParentID= branchResp.adminUnitId)
				 --and branchResp.adminUnitId=80400001
                 and us.Id=@userID
				) as table4 )

					";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", state_eV_Id);
                    command.Parameters.AddWithValue("@userID", userID);
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


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<ProductionDetail> GetDemandProductionDetailistForUser(Int64 userID)
        {
            var result = new List<ProductionDetail>();
            var query = @"   select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description, 
                       FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                       ev.name as KategoryName,FirstTable.user_Id from (   
                      select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                       adr.fullAddress,adr.addressDesc ,op.user_Id 
                      from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id    and pc.Status=1
                       left join [dbo].[tblAddress] adr on op.address_Id=adr.Id   and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id  and prc.Status=1   
                        left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id  and ev.Status=1
                      left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1   
                         where  op.user_Id=@userID and  ev.Id!=13 and op.isSelected=1  and op.Status=1
                      ) as FirstTable   
                       left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1 ; ";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
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


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<ProductionDetail> GetDemandProductionDetailistForEValueId(Int64 state_eV_Id)
        {
            var result = new List<ProductionDetail>();
            var query = @"    select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description, 
                        FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id,FirstTable.name,FirstTable.grup_Id  from (   
                      select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                       adr.fullAddress,adr.addressDesc ,op.user_Id ,fo.name,op.grup_Id 
                       from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                     left join [dbo].[tblAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
					   	   left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
                       where      op.Status=1 and op.state_eV_Id=@state_eV_Id 
                      ) as FirstTable   
                        left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", state_eV_Id);
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
                            organizationName = reader.GetStringOrEmpty(13)


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<DemanProductionGroup> GetDemanProductionGroupList(Int64 startDate, Int64 endDate, Int64 year, Int64 partOfYear)
        {
            var result = new List<DemanProductionGroup>();
            var query = @" select FirstTabe.Id,FirstTabe.ProductName, FirstTabe.ToplamTutar,FirstTabe.EnumValueId,FirstTabe.name,pprice.unit_price
                            from( select pc.Id,pc.ProductName, SUM(dp.quantity) as ToplamTutar,pcontrol.EnumValueId,ev.name from [EmsalDB].[dbo].[tblDemand_Production] dp,
                            [dbo].[tblProductCatalog] pc ,
                            [dbo].[tblProductionControl] pcontrol,
                            [dbo].[tblEnumCategory] ec,
                            [dbo].[tblEnumValue] ev
                            where dp.product_Id=pc.Id and pcontrol.Demand_Production_Id=dp.Id
                            and ec.name='olcuVahidi' and ec.Id=ev.enumCategory_enumCategoryId
                            and ev.Id=pcontrol.EnumValueId
                            and pc.Status=1 and pcontrol.Status=1 and ev.Status=1 and ev.Status=1
                            and dp.createdDate BETWEEN @value1 AND  @value2
                            and dp.isSelected=0
                            and dp.isAnnouncement is Null
                            group by pc.Id,pc.ProductName, pcontrol.EnumValueId ,ev.name) as FirstTabe
                            left join [dbo].[tblProductPrice] pprice on FirstTabe.Id=pprice.productId
                                and pprice.year=@year and pprice.partOfYear=@partOfYear";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@value1", startDate);
                    command.Parameters.AddWithValue("@value2", endDate);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@partOfYear", partOfYear);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemanProductionGroup()
                        {
                            productId = reader.GetInt64OrDefaultValue(0),
                            productName = reader.GetStringOrEmpty(1),
                            totalQuantity = reader.GetDecimalOrDefaultValue(2),
                            enumValuId = reader.GetInt64OrDefaultValue(3),
                            enumValueName = reader.GetStringOrEmpty(4),
                            unitPrice = reader.GetDecimalOrDefaultValue(5),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<UserInfo> GetPotensialUserList()
        {
            var result = new List<UserInfo>();
            var query = @"  select p.Name,p.Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name,p.PinNumber ,'' as voen
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id=15
							 and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1
							 union 
							 select fo.Name,fo.description as Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name,'' as PinNumber , fo.voen
                             from dbo.tblUser u ,[dbo].[tblForeign_Organization] fo,
							 tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r
                             where u.Id=fo.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id=15
							 and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1  

                         ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new UserInfo()
                        {
                            name = reader.GetStringOrEmpty(0),
                            surname = reader.GetStringOrEmpty(1),
                            fullAddress = reader.GetStringOrEmpty(2),
                            email = reader.GetStringOrEmpty(3),
                            userID = reader.GetInt64OrDefaultValue(4),
                            userTypeID = reader.GetInt64OrDefaultValue(5),
                            userType = reader.GetStringOrEmpty(6),
                            userRoleID = reader.GetInt64OrDefaultValue(7),
                            roleID = reader.GetInt64OrDefaultValue(8),
                           
                            pinNumber = reader.GetStringOrEmpty(11),
                            voen = reader.GetStringOrEmpty(12),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<UserInfo> GetPotensialUserForAdminUnitIdList(Int64 adminUnitId)
        {
            var result = new List<UserInfo>();
            var query = @" select FirstTable.* ,aunit.Name as ParantName from (
 select p.Name as PersonName,p.Surname,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.RoleId,r.Name as RoleName,adr.adminUnit_Id,au.Name as AdminUnitName,au.ParentID,p.PinNumber ,'' as voen
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 dbo.tblPRM_AdminUnit au
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                              and r.Id=15
							 and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 
							 and adr.adminUnit_Id=au.Id
						  and adr.adminUnit_Id=@adminUnit_Id
							 and au.Status=1)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 

							 union 

							 select FirstTable.* ,aunit.Name as ParantName from (
 select fo.Name as PersonName,fo.description as Surname ,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.RoleId,r.Name as RoleName,adr.adminUnit_Id,au.Name as AdminUnitName,au.ParentID,'' as PinNumber , fo.voen 
                             from dbo.tblUser u ,[dbo].[tblForeign_Organization] fo,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 dbo.tblPRM_AdminUnit au
                             where u.Id=fo.userId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                              and r.Id=15
							 and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 
							 and adr.adminUnit_Id=au.Id
						    and adr.adminUnit_Id=@adminUnit_Id
							 and au.Status=1)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id  ";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@adminUnit_Id", adminUnitId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new UserInfo()
                        {
                            name = reader.GetStringOrEmpty(0),
                            surname = reader.GetStringOrEmpty(1),
                            fullAddress = reader.GetStringOrEmpty(2),
                            email = reader.GetStringOrEmpty(3),
                            userID = reader.GetInt64OrDefaultValue(4),
                            userTypeID = reader.GetInt64OrDefaultValue(5),
                            userType = reader.GetStringOrEmpty(6),
                            userRoleID = reader.GetInt64OrDefaultValue(7),
                            roleName = reader.GetStringOrEmpty(8),
                            adminUnitID = reader.GetInt64OrDefaultValue(9),
                            adminUnitName = reader.GetStringOrEmpty(10),
                            parentAdminUnitID = reader.GetInt64OrDefaultValue(11),
                            pinNumber = reader.GetStringOrEmpty(12),
                            voen = reader.GetStringOrEmpty(13),
                            parentAdminUnitName = reader.GetStringOrEmpty(14),

                           
                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<UserInfo> GetPotensialUserListByName(string name)
        {
            var result = new List<UserInfo>();
            var query = @"   select FirstTable.* ,aunit.Name as ParantName from 
 (select p.Name as personName,p.Surname,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                            ur.RoleId,r.Name as roleName,adr.adminUnit_Id,au.Name as AdminName,au.ParentID,p.PinNumber ,'' as voen
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
        dbo.tblPRM_AdminUnit au
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id=15
        and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
        and ev.Status=1 and r.Status=1 
        and adr.adminUnit_Id=au.Id
      and p.Name=@name
        and au.Status=1) as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id

		union 

		select FirstTable.* ,aunit.Name as ParantName from 
 (select fo.Name as personName,fo.description as Surname,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                            ur.RoleId,r.Name as roleName,adr.adminUnit_Id,au.Name as AdminName,au.ParentID,'' as PinNumber , fo.voen 
                             from dbo.tblUser u ,[dbo].[tblForeign_Organization] fo ,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
        dbo.tblPRM_AdminUnit au
                             where u.Id=fo.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id=15
        and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
        and ev.Status=1 and r.Status=1 
        and adr.adminUnit_Id=au.Id
        and fo.Name=@name
        and au.Status=1) as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new UserInfo()
                        {
                            name = reader.GetStringOrEmpty(0),
                            surname = reader.GetStringOrEmpty(1),
                            fullAddress = reader.GetStringOrEmpty(2),
                            email = reader.GetStringOrEmpty(3),
                            userID = reader.GetInt64OrDefaultValue(4),
                            userTypeID = reader.GetInt64OrDefaultValue(5),
                            userType = reader.GetStringOrEmpty(6),
                            userRoleID = reader.GetInt64OrDefaultValue(7),
                            roleName = reader.GetStringOrEmpty(8),
                            adminUnitID = reader.GetInt64OrDefaultValue(9),
                            adminUnitName = reader.GetStringOrEmpty(10),
                            parentAdminUnitID = reader.GetInt64OrDefaultValue(11),

                            pinNumber = reader.GetStringOrEmpty(12),
                            voen = reader.GetStringOrEmpty(13),
                            parentAdminUnitName = reader.GetStringOrEmpty(14),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<UserInfo> GetPotensialUserListBySurname(string surname)
        {
            var result = new List<UserInfo>();
            var query = @"    select FirstTable.* ,aunit.Name as ParantName from 
 (select p.Name as personName,p.Surname,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                            ur.RoleId ,r.Name as roleName,adr.adminUnit_Id,au.Name as AdminName,au.ParentID,p.PinNumber
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
        dbo.tblPRM_AdminUnit au
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id=15
        and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
        and ev.Status=1 and r.Status=1 
        and adr.adminUnit_Id=au.Id
       and p.Surname=@surname
        and au.Status=1) as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@surname", surname);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new UserInfo()
                        {
                            name = reader.GetStringOrEmpty(0),
                            surname = reader.GetStringOrEmpty(1),
                            fullAddress = reader.GetStringOrEmpty(2),
                            email = reader.GetStringOrEmpty(3),
                            userID = reader.GetInt64OrDefaultValue(4),
                            userTypeID = reader.GetInt64OrDefaultValue(5),
                            userType = reader.GetStringOrEmpty(6),
                            userRoleID = reader.GetInt64OrDefaultValue(7),
                            roleName = reader.GetStringOrEmpty(8),
                            adminUnitID = reader.GetInt64OrDefaultValue(9),
                            adminUnitName = reader.GetStringOrEmpty(10),
                            parentAdminUnitID = reader.GetInt64OrDefaultValue(11),
                            pinNumber = reader.GetStringOrEmpty(12),
                            parentAdminUnitName = reader.GetStringOrEmpty(13),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<UserInfo> GetPotensialUserListByAdminUnitName(string adminUnitName)
        {
            var result = new List<UserInfo>();
            var query = @"  select FirstTable.* ,aunit.Name as ParantName from 
 (select p.Name as personName,p.Surname,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.RoleId,r.Name as roleName,adr.adminUnit_Id,au.Name as AdminName,au.ParentID
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
        dbo.tblPRM_AdminUnit au
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id=15
        and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
        and ev.Status=1 and r.Status=1 
        and adr.adminUnit_Id=au.Id
       and au.Name=@adminUnitName
        and au.Status=1) as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@adminUnitName", adminUnitName);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new UserInfo()
                        {
                            name = reader.GetStringOrEmpty(0),
                            surname = reader.GetStringOrEmpty(1),
                            fullAddress = reader.GetStringOrEmpty(2),
                            email = reader.GetStringOrEmpty(3),
                            userID = reader.GetInt64OrDefaultValue(4),
                            userTypeID = reader.GetInt64OrDefaultValue(5),
                            userType = reader.GetStringOrEmpty(6),
                            userRoleID = reader.GetInt64OrDefaultValue(7),
                            roleName = reader.GetStringOrEmpty(8),
                            adminUnitID = reader.GetInt64OrDefaultValue(9),
                            adminUnitName = reader.GetStringOrEmpty(10),
                            parentAdminUnitID = reader.GetInt64OrDefaultValue(11),
                            parentAdminUnitName = reader.GetStringOrEmpty(12),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<ProductPriceDetail> GetProductPriceList(Int64 year, Int64 partOfYear)
        {
            var result = new List<ProductPriceDetail>();
            var query = @"  select pc.Id as ProductID, pc.ProductName,
                        pprice.Id as priceID,pprice.unit_price,pprice.year,pprice.partOfYear
                         from [dbo].[tblProductCatalog] pc , [dbo].[tblProductPrice] pprice 
                         where pc.Id=pprice.productId 
                         and pprice.Status=1 and pc.canBeOrder=1 and pc.Status=1
                          and pprice.year=@year and  pprice.partOfYear=@partOfYear ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@partOfYear", partOfYear);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductPriceDetail()
                        {
                            productID = reader.GetInt64OrDefaultValue(0),
                            productName = reader.GetStringOrEmpty(1),
                            priceID = reader.GetInt64OrDefaultValue(2),
                            unit_price = reader.GetDecimalOrDefaultValue(3),
                            year = reader.GetInt64OrDefaultValue(4),
                            partOfYear = reader.GetInt64OrDefaultValue(5),



                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<ProductPriceDetail> GetProductPriceListNotPrice(Int64 year, Int64 partOfYear)
        {
            var result = new List<ProductPriceDetail>();
            var query = @"    select table1.Id, table1.ProductName,table1.canBeOrder,pc.ProductName from ( select pc.Id,pc.ProductCatalogParentID, pc.ProductName,pc.canBeOrder from tblProductCatalog pc where  pc.canBeOrder=1 and pc.Status=1and pc.Id not in(
                        select pprice.productId from tblProductPrice pprice
                        where    pprice.Status=1 
                               and pprice.year=@year and  pprice.partOfYear=@partOfYear
								)) as table1 , tblProductCatalog pc
								where table1.ProductCatalogParentID=pc.Id
							 ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@partOfYear", partOfYear);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductPriceDetail()
                        {
                            productID = reader.GetInt64OrDefaultValue(0),
                            productName = reader.GetStringOrEmpty(1),
                            canBeOrder = reader.GetInt64OrDefaultValue(2),
                            productParentName = reader.GetStringOrEmpty(3)



                        });
                    }
                }
                connection.Close();
            }

            return result;
        }




        //ferid
        public List<tblUser> GetGovernmentOrganisations(Int64 govOrgRoleEnum)
        {
            var result = new List<tblUser>();
            var query = @" SELECT * FROM dbo.tblUser
                            Where tblUser.Id in 
                            (Select tblUserRole.UserId from tblUserRole 
                            Where tblUserRole.RoleId = @govOrgRoleEnum ) 
                            and tblUser.Status = 1;  ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@govOrgRoleEnum", govOrgRoleEnum);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new tblUser()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            Username = reader.GetStringOrEmpty(1),
                            Email = reader.GetStringOrEmpty(2),
                            Password = reader.GetStringOrEmpty(3),
                            LastLoginIP = reader.GetStringOrEmpty(4),
                            LastLoginDate = reader.GetInt64OrDefaultValue(5),
                            ProfileImageUrl = reader.GetStringOrEmpty(6),
                            Status = reader.GetInt64OrDefaultValue(7),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(8),
                            createdUser = reader.GetStringOrEmpty(9),
                            createdDate = reader.GetInt64OrDefaultValue(10),
                            updatedUser = reader.GetStringOrEmpty(11),
                            updatedDate = reader.GetInt64OrDefaultValue(12),
                            userType_eV_ID = reader.GetInt64OrDefaultValue(13),
                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<tblUser> GetOrganisationTypeUsers(Int64 govRoleEnum, long userType)
        {
            var result = new List<tblUser>();
            var query = @" SELECT * FROM dbo.tblUser
                           where Id not in 
                            (Select dbo.tblUserRole.UserId from dbo.tblUserRole 
                             Where RoleId = @govRoleEnum  and Status = 1) and (userType_eV_ID = @userType and Status = 1)  ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@govRoleEnum", govRoleEnum);
                    command.Parameters.AddWithValue("@userType", userType);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new tblUser()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            Username = reader.GetStringOrEmpty(1),
                            Email = reader.GetStringOrEmpty(2),
                            Password = reader.GetStringOrEmpty(3),
                            LastLoginIP = reader.GetStringOrEmpty(4),
                            LastLoginDate = reader.GetInt64OrDefaultValue(5),
                            ProfileImageUrl = reader.GetStringOrEmpty(6),
                            Status = reader.GetInt64OrDefaultValue(7),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(8),
                            createdUser = reader.GetStringOrEmpty(9),
                            createdDate = reader.GetInt64OrDefaultValue(10),
                            updatedUser = reader.GetStringOrEmpty(11),
                            updatedDate = reader.GetInt64OrDefaultValue(12),
                            userType_eV_ID = reader.GetInt64OrDefaultValue(13),
                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<tblRole> GetRolesNotOwnedByUser(long userId)
        {
            var result = new List<tblRole>();
            var query = @"Select * from dbo.tblRole 
                          Where Id not in (Select RoleId from dbo.tblUserRole where UserId = @userId and Status = 1) and Status = 1";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new tblRole()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            Status = reader.GetInt64OrDefaultValue(2),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(3),
                            createdUser = reader.GetStringOrEmpty(4),
                            createdDate = reader.GetInt64OrDefaultValue(5),
                            updatedUser = reader.GetStringOrEmpty(6),
                            updatedDate = reader.GetInt64OrDefaultValue(7),
                            party_Id = reader.GetInt64OrDefaultValue(8),
                            roleType_enumCategoryId = reader.GetInt32(9),
                            Description = reader.GetStringOrEmpty(10)
                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<tblRole> GetRolesNotAllowedInPage(long pageId)
        {
            var result = new List<tblRole>();
            var query = @"Select * from dbo.tblRole where Id not in 
                           (SELECT RoleID from dbo.tblPrivilegedRole 
                            where  AuthenticatedPartID = @pageId and Status = 1)";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@pageId", pageId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new tblRole()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            Status = reader.GetInt64OrDefaultValue(2),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(3),
                            createdUser = reader.GetStringOrEmpty(4),
                            createdDate = reader.GetInt64OrDefaultValue(5),
                            updatedUser = reader.GetStringOrEmpty(6),
                            updatedDate = reader.GetInt64OrDefaultValue(7),
                            party_Id = reader.GetInt64OrDefaultValue(8),
                            roleType_enumCategoryId = reader.GetInt32(9),
                            Description = reader.GetStringOrEmpty(10)
                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        ///



        #region Report 


        public List<DemandOfferDetail> GetDemandOfferDetailID(long adminID)
        {
            var result = new List<DemandOfferDetail>();
            var query = @" select  pc.ProductName,COUNT(*) as Count,'Offer' as type from [dbo].[tblOffer_Production] op,
                    [dbo].[tblProductCatalog] pc,

                    [dbo].[tblProductAddress] pa,

                    [dbo].[tblPRM_AdminUnit] au
                    where op.product_Id=pc.Id
                    and pc.Status=1 and op.Status=1
                    and  pa.Id=op.productAddress_Id
                    and au.Id=pa.adminUnit_Id

                    and (
                    (au.Id in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in(
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID)))
                    )
                    or
                    ( 
                    au.Id in
                    (
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID))
                    ))

                    group by pc.ProductName 
                    union
                    select  pc.ProductName,COUNT(*) as Count ,'Demand' as type from  [dbo].[tblDemand_Production] dp ,
                    [dbo].[tblProductCatalog] pc,

                    [dbo].[tblProductAddress] pa,

                    [dbo].[tblPRM_AdminUnit] au
                    where dp.product_Id=pc.Id
                    and pc.Status=1 and dp.Status=1
                    and  pa.Id=dp.address_Id
                    and au.Id=pa.adminUnit_Id

                    and (
                    (au.Id in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in(
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID)))
                    )
                    or
                    ( 
                    au.Id in
                    (
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID))
                    ))

                    group by pc.ProductName  order by pc.ProductName asc ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@parentID", adminID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemandOfferDetail()
                        {
                            productName = reader.GetStringOrEmpty(0),
                            count = Convert.ToInt32(reader["Count"]),
                            productType = reader.GetStringOrEmpty(2),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<DemandOfferDetail> GetDemandOfferDetailByProductID()
        {
            var result = new List<DemandOfferDetail>();
            var query = @" 		select au.Name,table1.ProductName, table1.ProductID, table1.Count,table1.type from 
		( select  au.ParentID , pc.ProductName,pc.Id as ProductID, COUNT(*) as Count,'Offer' as type 
           from  [dbo].[tblOffer_Production] op,
                    [dbo].[tblProductCatalog] pc,
                    [dbo].[tblProductAddress] pa,

                    [dbo].[tblPRM_AdminUnit] au
                    where op.product_Id=pc.Id
                    and pc.Status=1 and op.Status=1
                    and  pa.Id=op.productAddress_Id
                    and au.Id=pa.adminUnit_Id
					group by pc.ProductName ,pc.Id,au.ParentID)  as table1,

					[dbo].[tblPRM_AdminUnit] au 
                     where table1.ParentID=au.Id

				union 

			select au.Name,table1.ProductName, table1.ProductID, table1.Count,table1.type from (
			 select  au.ParentID , pc.ProductName, pc.Id as ProductID , COUNT(*) as Count,'Demand' as type
			     from  [dbo].[tblDemand_Production] dp,
                    [dbo].[tblProductCatalog] pc,
                    [dbo].[tblProductAddress] pa,

                    [dbo].[tblPRM_AdminUnit] au
                    where dp.product_Id=pc.Id
                    and pc.Status=1 and dp.Status=1
                    and  pa.Id=dp.address_Id
                    and au.Id=pa.adminUnit_Id
                    group by pc.ProductName ,pc.Id, au.ParentID)  as table1,

					[dbo].[tblPRM_AdminUnit] au 
                     where table1.ParentID=au.Id ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                   // command.Parameters.AddWithValue("@parentID", productID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemandOfferDetail()
                        {
                            adminUnittName = reader.GetStringOrEmpty(0),
                            productName = reader.GetStringOrEmpty(1),
                            productID=reader.GetInt64OrDefaultValue(2),
                            count = Convert.ToInt32(reader["Count"]),
                            productType = reader.GetStringOrEmpty(4),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<DemandOfferDetail> GetOfferDetailByAmdminID(long adminID)
        {
            var result = new List<DemandOfferDetail>();
            var query = @" select  pc.ProductName,COUNT(*) as Count,'Offer' as type from [dbo].[tblOffer_Production] op,
                    [dbo].[tblProductCatalog] pc,

                    [dbo].[tblProductAddress] pa,

                    [dbo].[tblPRM_AdminUnit] au
                    where op.product_Id=pc.Id
                    and pc.Status=1 and op.Status=1
                    and  pa.Id=op.productAddress_Id
                    and au.Id=pa.adminUnit_Id

                    and (
                    (au.Id in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in(
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID)))
                    )
                    or
                    ( 
                    au.Id in
                    (
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID))
                    ))

                    group by pc.ProductName  order by pc.ProductName asc ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@parentID", adminID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemandOfferDetail()
                        {
                            productName = reader.GetStringOrEmpty(0),
                            count = Convert.ToInt32(reader["Count"]),
                            productType = reader.GetStringOrEmpty(2),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<DemandOfferDetail> GetDemanDetailByAdminID(long adminID)
        {
            var result = new List<DemandOfferDetail>();
            var query = @"  select  pc.ProductName,COUNT(*) as Count ,'Demand' as type from  [dbo].[tblDemand_Production] dp ,
                    [dbo].[tblProductCatalog] pc,

                    [dbo].[tblProductAddress] pa,

                    [dbo].[tblPRM_AdminUnit] au
                    where dp.product_Id=pc.Id
                    and pc.Status=1 and dp.Status=1
                    and  pa.Id=dp.address_Id
                    and au.Id=pa.adminUnit_Id

                    and (
                    (au.Id in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in(
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID)))
                    )
                    or
                    ( 
                    au.Id in
                    (
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID))
                    ))

                    group by pc.ProductName  order by pc.ProductName asc ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@parentID", adminID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemandOfferDetail()
                        {
                            productName = reader.GetStringOrEmpty(0),
                            count = Convert.ToInt32(reader["Count"]),
                            productType = reader.GetStringOrEmpty(2),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<PotentialClientDetail> GetPotentialClientCount()
        {
            var result = new List<PotentialClientDetail>();
            var query = @"  select au.Name,table1.fromAdminUnit,table1.count,table1.createdDate from (	select   au.ParentID ,  fromAdminUnit=
								CASE  u.createdUser
									WHEN 'asan' THEN 'Asan'
									ELSE 'KTN'
								END,COUNT(*) as count,u.createdDate
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r, [dbo].[tblPRM_AdminUnit] au
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id=15
							 and au.Id=adr.adminUnit_Id and au.Status=1
							 and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1
							 group by  au.ParentID ,u.createdUser,u.createdDate
							 ) as table1 , [dbo].[tblPRM_AdminUnit] au
							 where table1.ParentID=au.Id
							
							 union 
						   select au.Name,table1.fromAdminUnit,table1.count,table1.createdDate  from (	select   au.ParentID ,  fromAdminUnit=
								CASE  u.createdUser
									WHEN 'asan' THEN 'Asan'
									ELSE 'KTN'
								END,COUNT(*) as count,u.createdDate
                             from dbo.tblUser u ,[dbo].[tblForeign_Organization] fo,
							 tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,[dbo].[tblPRM_AdminUnit] au
                             where u.Id=fo.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id=15
							 and au.Id=adr.adminUnit_Id and au.Status=1
							 and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1
								 group by  au.ParentID ,u.createdUser,u.createdDate
							 ) as table1 , [dbo].[tblPRM_AdminUnit] au
							 where table1.ParentID=au.Id
							 ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@parentID", adminID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new PotentialClientDetail()
                        {
                            adminUnitName = reader.GetStringOrEmpty(0),
                            fromOrganisation = reader.GetStringOrEmpty(1),
                            count = Convert.ToInt32(reader["Count"]),
                            createdDate = reader.GetInt64OrDefaultValue(3),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

      

        #endregion
    }

}
