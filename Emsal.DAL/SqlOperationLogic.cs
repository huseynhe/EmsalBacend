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
            var query = @"select table2.*,pc.ProductName as productParentName from(
                     select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                       FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id,FirstTable.ProductCatalogParentID from (   
                      select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                      pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID
                      from [dbo].[tblPotential_Production] op  
                      left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id    and pc.Status=1
                     left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id   and pa.Status=1
                     left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                    left join [dbo].[tblProductionControl] prc on op.Id=prc.Potential_Production_Id and prc.Status=1
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                    left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id    and ec.Status=1
                     where    
					 op.createdUser=@createdUser and 
					 op.isSelected=1  and op.Status=1
                    ) as FirstTable   
                  left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1
				  )as table2 
				  left join [dbo].[tblProductCatalog] pc on pc.Id=table2.ProductCatalogParentID ;";

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
                            productParentName = reader.GetStringOrEmpty(14),


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
            var query = @" select table2.*,pc.ProductName as parentName from(
select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                   FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                    ev.name as KategoryName,FirstTable.user_Id,FirstTable.ProductCatalogParentID from (   
                   select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                   pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID
                    from [dbo].[tblPotential_Production] op  
                   left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id   and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                    left join [dbo].[tblProductionControl] prc on op.Id=prc.Potential_Production_Id    and prc.Status=1 
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                   left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id    and ec.Status=1
                   where  op.Status=1  
				   and op.state_eV_Id=@state_eV_Id
                   ) as FirstTable   
                    left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id  and ev.Status=1
					)as table2
					 left join [dbo].[tblProductCatalog] pc  on pc.Id=table2.ProductCatalogParentID ;";

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
                            productParentName = reader.GetStringOrEmpty(14),


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
                   where  op.Status=1    and op.monitoring_eV_Id=@monintoring_eV_Id and  op.updatedUser='KTN'
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
				 and branchResp.adminUnitId=80400001
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
select table3.*  from( select table2.*,person.Id as personID, adr.Id as adressId, adr.adminUnit_Id from (

select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                   FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                    ev.name as KategoryName,FirstTable.user_Id,FirstTable.ProductCatalogParentID,pc.ProductName as productParentName  from (   
                   select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                   pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID
                    from [dbo].[tblPotential_Production] op  
                   left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id   and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                    left join [dbo].[tblProductionControl] prc on op.Id=prc.Potential_Production_Id    and prc.Status=1 
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                   left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id    and ec.Status=1

                   where  op.Status=1 and op.state_eV_Id=@state_eV_Id
                   ) as FirstTable   
				      left join [dbo].[tblProductCatalog] pc  on pc.Id=FirstTable.ProductCatalogParentID   and pc.Status=1 
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
                            productParentName = reader.GetStringOrEmpty(14),


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
                      ev.name as KategoryName,FirstTable.user_Id,FirstTable.potentialProductsQuantity from (   
                      select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                      pa.fullAddress,pa.addressDesc ,op.user_Id ,pp.quantity as potentialProductsQuantity
                     from [dbo].[tblOffer_Production] op   
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1
                     left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1 
                     left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                     left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1  
					 left join  [dbo].[tblPotential_Production] pp on us.Id=pp.user_Id and pp.Status=1
                     where  op.user_Id=@userID and 
					 op.isSelected=1 and op.Status=1
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
                            potentialProductQuantity = reader.GetDecimalOrDefaultValue(13),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<OfferProducts> GetOrderProducts()
        {
            var result = new List<OfferProducts>();
            var query = @"  select distinct FirstTable.*,pc.ProductName as parentName from (   
                      select  op.product_Id,pc.ProductName,pc.ProductCatalogParentID
                     from [dbo].[tblOffer_Production] op  , [tblProductCatalog] pc

                    
                     where  op.product_Id=pc.Id and 
					 --op.isSelected=1 and
					  op.Status=1
					  and op.state_eV_Id=2 and pc.canBeOrder=1
                    ) as FirstTable   
                    left join [dbo].[tblProductCatalog] pc on FirstTable.ProductCatalogParentID=pc.Id and pc.Status=1 ;";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OfferProducts()
                        {
                            productID = reader.GetInt64OrDefaultValue(0),
                            productName = reader.GetStringOrEmpty(1),
                            parentName = reader.GetStringOrEmpty(3),


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
            var query = @"   select  table2.*,pc.ProductName as  ProductParentName,adr.fullAddress as personAddress,adr.addressDesc as personAddressDesc,ev.name as userType
 ,fo.name as organizationName,fo.voen  from(  select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id ,FirstTable.ProductCatalogParentID,FirstTable.Email
					  ,FirstTable.Name,FirstTable.Surname,FirstTable.FatherName,FirstTable.birtday,
					  FirstTable.gender,FirstTable.profilePicture,FirstTable.address_Id,FirstTable.PinNumber,FirstTable.adrID,FirstTable.potentialProductsQuantity,FirstTable.userType_eV_ID
					   from (   
                     select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,us.Email,person.Name,person.Surname,
					person.FatherName,person.birtday,person.gender,person.profilePicture,person.address_Id,person.PinNumber
			,adr.Id as adrID,pp.quantity as potentialProductsQuantity,us.userType_eV_ID
                      from [dbo].[tblOffer_Production] op 
     
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                     left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
                     left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                     left  join [dbo].[tblPerson] person on us.Id=person.UserId and person.Status=1 
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                     left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id and ev.Status=1
                    left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1  
					left join  [dbo].[tblAddress] adr on adr.Id=person.address_Id and adr.Status=1
					 left join  [dbo].[tblPotential_Production] pp on us.Id=pp.user_Id and pp.Status=1
                      where   
      op.state_eV_Id= @state_eV_Id and
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
	                        where table2.EnumCategoryId=5
						order by table2.ProductName
						";

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
                            productParentName = reader.GetStringOrEmpty(26),
                            personAdress = reader.GetStringOrEmpty(27),
                            personAdressDesc = reader.GetStringOrEmpty(28),
                            userType = reader.GetStringOrEmpty(29),
                            organizationName = reader.GetStringOrEmpty(30),
                            voen = reader.GetStringOrEmpty(31),










                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<ProductionDetail> GetOfferProductionDetailistForMonitoringEVId(Int64 monintoring_eV_Id, Int64 userID)
        {
            var result = new List<ProductionDetail>();
            var query = @"    
		 select table3.* ,ev.description as userType,fo.name as organizationName,fo.voen--,person.Name as organizationManagerName
,ad.Name as productOriginName,r.Name as roleName,r.Description as roleDescription from( select table2.*,pc.ProductName as ProductParentName, person.Id as personID, adr.Id as adressId, adr.adminUnit_Id 
,person.Name as personName,person.Surname,person.FatherName,person.PinNumber
from (select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id ,FirstTable.ProductCatalogParentID,FirstTable.potentialProductsQuantity,FirstTable.productOrigin,FirstTable.userType_eV_ID
					  ,FirstTable.contractId,FirstTable.RoleId,FirstTable.Email from (   
                     select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,pp.quantity as potentialProductsQuantity,op.productOrigin,us.userType_eV_ID ,
					op.contractId,ur.RoleId,us.Email
					
                      from [dbo].[tblOffer_Production]  op  
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
				    left join  [dbo].[tblUserRole] ur on us.Id=ur.UserId and ur.Status=1
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                     left join [dbo].[tblEnumValue] ev on op.monitoring_eV_Id=ev.Id and ev.Status=1
                    left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1 
						 left join  [dbo].[tblPotential_Production] pp on us.Id=pp.user_Id and pp.Status=1 
						
						
                      where    op.Status=1  
				  and op.monitoring_eV_Id=@monintoring_eV_Id  
					  and op.state_eV_Id=2 --and op.updatedUser='KTN'
                    ) as FirstTable   
                    left  join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1) as table2
					  left join [dbo].[tblProductCatalog] pc  on pc.Id= table2.ProductCatalogParentID  and pc.Status=1 
					  join [dbo].[tblPerson] person on table2.user_Id=person.UserId
					left  join [dbo].[tblAddress] adr on person.address_Id=adr.Id) as table3
					 --left join [dbo].[tblPerson] person on table3.manager_Id=person.Id and person.Status=1
					  left join   [dbo].[tblEnumValue] ev on table3.userType_eV_ID=ev.Id and ev.Status=1
					  left join [dbo].[tblPRM_AdminUnit] ad on table3.productOrigin =ad.Id and ad.Status=1 
				       left join  [dbo].[tblForeign_Organization] fo on table3.user_Id= fo.userId and fo.Status=1
                           left join tblRole r on table3.RoleId=r.Id and r.Status=1
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
				-- and branchResp.adminUnitId=80400001
        --    
	and user_Id=@userID
				) as table4
				 )
	
             union 
			 

		 select table3.* ,ev.description as userType,fo.name as organizationName,fo.voen--,person.Name as organizationManagerName
,ad.Name as productOriginName,r.Name as roleName,r.Description as roleDescription from( select table2.*,pc.ProductName as ProductParentName,fo.Id as personID,  adr.Id as adressId, adr.adminUnit_Id 
,fo.name as personName,fo.description as  Surname, '' as FatherName,'' as PinNumber
from (select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id ,FirstTable.ProductCatalogParentID,FirstTable.potentialProductsQuantity,FirstTable.productOrigin,FirstTable.userType_eV_ID
					  ,FirstTable.contractId,FirstTable.RoleId,FirstTable.Email from (   
                     select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,pp.quantity as potentialProductsQuantity,op.productOrigin,us.userType_eV_ID ,
					op.contractId,ur.RoleId,us.Email
					
                      from [dbo].[tblOffer_Production]  op  
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
				    left join  [dbo].[tblUserRole] ur on us.Id=ur.UserId and ur.Status=1
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                     left join [dbo].[tblEnumValue] ev on op.monitoring_eV_Id=ev.Id and ev.Status=1
                    left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1 
						 left join  [dbo].[tblPotential_Production] pp on us.Id=pp.user_Id and pp.Status=1 
						
						
                      where    op.Status=1  
				  and op.monitoring_eV_Id=@monintoring_eV_Id  
					  and op.state_eV_Id=2 --and op.updatedUser='KTN'
                    ) as FirstTable   
                    left  join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1) as table2
					  left join [dbo].[tblProductCatalog] pc  on pc.Id= table2.ProductCatalogParentID  and pc.Status=1 
					  join [dbo].[tblForeign_Organization] fo on table2.user_Id=fo.UserId
					left  join [dbo].[tblAddress] adr on fo.address_Id=adr.Id) as table3
					 --left join [dbo].[tblPerson] person on table3.manager_Id=person.Id and person.Status=1
					  left join   [dbo].[tblEnumValue] ev on table3.userType_eV_ID=ev.Id and ev.Status=1
					  left join [dbo].[tblPRM_AdminUnit] ad on table3.productOrigin =ad.Id and ad.Status=1 
				       left join  [dbo].[tblForeign_Organization] fo on table3.user_Id= fo.userId and fo.Status=1
                           left join tblRole r on table3.RoleId=r.Id and r.Status=1
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
				-- and branchResp.adminUnitId=80400001
        --    
		and user_Id=@userID
				) as table4 
				
	)
           
			
							
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
                            potentialProductQuantity = reader.GetDecimalOrDefaultValue(14),
                            productOrigin = reader.GetInt64OrDefaultValue(15),
                            userType_eV_ID = reader.GetInt64OrDefaultValue(16),
                            contractID = reader.GetInt64OrDefaultValue(17),

                            roleID = reader.GetInt64OrDefaultValue(18),
                            email = reader.GetStringOrEmpty(19),
                            productParentName = reader.GetStringOrEmpty(20),
                            personID = reader.GetInt64OrDefaultValue(21),








                            userType = reader.GetStringOrEmpty(28),
                            organizationName = reader.GetStringOrEmpty(29),


                            productOriginName = reader.GetStringOrEmpty(31),


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
select table3.* ,pc.ProductName as potentialProduct from( select table2.*, person.Id as personID, adr.Id as adressId, adr.adminUnit_Id,pp.quantity as potentialProductQunatity from (select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
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
				 where table3.EnumCategoryId=5
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
                            productParentName = reader.GetStringOrEmpty(13),
                            roleName = reader.GetStringOrEmpty(16),
                            potentialProductQuantity = reader.GetDecimalOrDefaultValue(20),
                            potentialProduct = reader.GetStringOrEmpty(21),


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
            var query = @"  select  secondTable.*,pc.ProductName as ProductParentName,fo.name as organizationName from (select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description, 
                       FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                       ev.name as KategoryName,FirstTable.user_Id,FirstTable.ProductCatalogParentID,FirstTable.fullForeignOrganization,FirstTable.forgId from (   
                      select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                       adr.fullAddress,adr.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,adr.fullForeignOrganization,adr.forgId
                      from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id    and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id   and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id  and prc.Status=1   
                        left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id  and ev.Status=1
                      left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1   
                         where 
						op.user_Id=@userID and 
						  ev.Id!=13 and op.isSelected=1 
and op.Status=1
                      ) as FirstTable   
                       left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1 
					  
					   ) secondTable

					      left join [dbo].[tblProductCatalog] pc on pc.Id=secondTable.ProductCatalogParentID
                          left join tblForeign_Organization fo on secondTable.user_Id=fo.userId and fo.Status=1  
                         ";

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
                            fullForeignOrganization = reader.GetStringOrEmpty(14),
                            forgId = reader.GetInt64OrDefaultValue(15),

                            productParentName = reader.GetStringOrEmpty(16),
                            organizationName = reader.GetStringOrEmpty(17),

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
            var query = @"  select distinct secondTable.*,pc.ProductName as parentName from 
     ( select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description, 
                        FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id,FirstTable.name,FirstTable.grup_Id,FirstTable.ProductCatalogParentID ,FirstTable.priceProductId from (   
                      select op.Id,price.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId , 
                       adr.fullAddress,adr.addressDesc ,op.user_Id ,fo.name,op.grup_Id ,
        pc.ProductCatalogParentID,price.productId as priceProductId
                       from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
            left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
          left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1

                       where      op.Status=1 
                     and op.state_eV_Id=@state_eV_Id
                      ) as FirstTable   
                        left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1
      ) secondTable
     
                  left join [dbo].[tblProductCatalog] pc on pc.Id=secondTable.ProductCatalogParentID
";

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
                            organizationName = reader.GetStringOrEmpty(13),
                            groupId = reader.GetStringOrEmpty(14),
                            productParentId = reader.GetInt64OrDefaultValue(15),
                            priceProductId = reader.GetInt64OrDefaultValue(16),
                            productParentName = reader.GetStringOrEmpty(17),


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
            var query = @"
 select distinct table2.*,pc.ProductName as productParentName from(select FirstTabe.Id,FirstTabe.ProductName, FirstTabe.ToplamTutar,FirstTabe.EnumValueId,FirstTabe.description,pprice.unit_price,FirstTabe.ProductCatalogParentID
                            from( select pc.Id,pc.ProductName, SUM(dp.quantity) as ToplamTutar,pcontrol.EnumValueId,ev.description,pc.ProductCatalogParentID from [EmsalDB].[dbo].[tblDemand_Production] dp,
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
                              and dp.Status=1
                            group by pc.Id,pc.ProductName, pcontrol.EnumValueId ,ev.description,pc.ProductCatalogParentID) as FirstTabe
                            left join [dbo].[tblProductPrice] pprice on FirstTabe.Id=pprice.productId
                               and pprice.year=@year and pprice.partOfYear=@partOfYear
							   ) as table2 
							   left join tblProductCatalog pc on pc.Id=table2.ProductCatalogParentID";
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
                            productParentName = reader.GetStringOrEmpty(7),

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
            var query = @" select distinct tb.*,op.state_eV_Id from( select p.Name,p.Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name as RoleName,p.PinNumber ,'' as voen
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id in (11,15)
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
                               and r.Id in (11,15)
							 and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1  
							 ) as tb
							  join tblOffer_Production op on tb.userId=op.user_Id and op.Status=1
							  and ((tb.RoleId  = 11 AND op.state_eV_Id=2) OR (tb.RoleId=15))
					

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
            var query = @"

 ;WITH cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au
WHERE Id = @adminUnit_Id
  UNION ALL
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.id
  )
  --SELECT  Id
 -- FROM cte
select FirstTable.* ,aunit.Name as ParantName,op.state_eV_Id from (
 select p.Name as PersonName,p.Surname,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.RoleId,r.Name as RoleName,adr.adminUnit_Id,au.Name as AdminUnitName,au.ParentID,p.PinNumber ,'' as voen--,pc.ProductName
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 dbo.tblPRM_AdminUnit au
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id in (11,15)
							 and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 
							 and adr.adminUnit_Id=au.Id
						and adr.adminUnit_Id in (
						select Id from cte
						)
							 and au.Status=1
                           
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 
							  left join tblOffer_Production op on op.user_Id=FirstTable.userId and op.Status=1
                           and ((FirstTable.RoleId  = 11 AND op.state_eV_Id=2) OR (FirstTable.RoleId=15))
							 union 

							 select FirstTable.* ,aunit.Name as ParantName,op.state_eV_Id from (
 select fo.Name as PersonName,fo.description as Surname ,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.RoleId,r.Name as RoleName,adr.adminUnit_Id,au.Name as AdminUnitName,au.ParentID,'' as PinNumber , fo.voen --,pc.ProductName
                             from dbo.tblUser u ,[dbo].[tblForeign_Organization] fo,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 dbo.tblPRM_AdminUnit au
                             where u.Id=fo.userId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                              and r.Id in (11,15)
							 and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 
							 and adr.adminUnit_Id=au.Id
						    and adr.adminUnit_Id in (
							select Id from cte
							 		)
							 and au.Status=1
                          
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 
							 left join tblOffer_Production op on op.user_Id=FirstTable.userId and op.Status=1
                           and ((FirstTable.RoleId  = 11 AND op.state_eV_Id=2) OR (FirstTable.RoleId=15))
 ";

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
            var query = @"  select table2.*,pc.ProductName as parentName from(
					  select pc.Id as ProductID, pc.ProductName,
                        pprice.Id as priceID,pprice.unit_price,pprice.year,pprice.partOfYear,pc.ProductCatalogParentID
                         from [dbo].[tblProductCatalog] pc , [dbo].[tblProductPrice] pprice 
                         where pc.Id=pprice.productId 
                         and pprice.Status=1 and pc.canBeOrder=1 and pc.Status=1
                          and pprice.year=@year and  pprice.partOfYear=@partOfYear
						  )as table2
					 left join [dbo].[tblProductCatalog] pc  on pc.Id=table2.ProductCatalogParentID; ";
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
                            productParentName = reader.GetStringOrEmpty(7),



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
            var query = @"     select table1.Id, table1.ProductName,table1.canBeOrder,pc.ProductName as ParentName,pc.ProductDescription from ( select pc.Id,pc.ProductCatalogParentID, pc.ProductName,pc.canBeOrder,pc.ProductDescription from tblProductCatalog pc where  pc.canBeOrder=1 and pc.Status=1and pc.Id not in(
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
                            productParentName = reader.GetStringOrEmpty(3),
                            ProductDescription = reader.GetStringOrEmpty(4),



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
        public List<tblForeign_Organization> GetForeign_OrganizationsListForID(Int64 ID)
        {
            var result = new List<tblForeign_Organization>();
            var query = @"  WITH cte AS( 
                      SELECT  Id, parent_Id,  name,  CAST(Id AS varbinary(max)) AS Level 
                     FROM    tblForeign_Organization WHERE   Id = @ID
                      UNION ALL 
                      SELECT  t.Id, t.parent_Id,t.name, Level + CAST(t.Id AS varbinary(max)) AS Level 
                      FROM    tblForeign_Organization t INNER JOIN cte r ON r.parent_Id = t.Id) 
                      SELECT  cte.Id,cte.parent_Id,cte.name,cte.Level 
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
                        result.Add(new tblForeign_Organization()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            parent_Id = reader.GetInt64OrDefaultValue(1),
                            name = reader.GetStringOrEmpty(2),


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
            // Int64 count1=R
            var query = @" select table1.* ,pc.ProductName as productParentName from(  select  pc.ProductName,COUNT(*) as Count,'Offer' as type,pc.ProductCatalogParentID from [dbo].[tblOffer_Production] op,
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
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID
					)))
                    )
                    or
                    ( 
                    au.Id in
                    (
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID
					))
                    ))
					  group by pc.ProductName ,pc.ProductCatalogParentID
					)as table1    left join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1


                  
                    union
             select table1.* ,pc.ProductName as productParentName from(select  pc.ProductName,COUNT(*) as Count ,'Demand' as type
			 ,pc.ProductCatalogParentID from  [dbo].[tblDemand_Production] dp ,
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
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID
					)))
                    )
                    or
                    ( 
                    au.Id in
                    (
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID=@parentID
					))
                    ))
					 group by pc.ProductName,pc.ProductCatalogParentID)as table1 
					left join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1
                    order by  table1.ProductName asc ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@parentID", adminID);
                    // Int64 s=
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemandOfferDetail()
                        {
                            productName = reader.GetStringOrEmpty(0),
                            count = Convert.ToInt32(reader["Count"]),
                            // count = reader.GetInt64OrDefaultValue(1),
                            productType = reader.GetStringOrEmpty(2),
                            productParentName = reader.GetStringOrEmpty(4),

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
                            productID = reader.GetInt64OrDefaultValue(2),
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
        {   /* join pc in context.tblProductCatalogs on p.product_id equals pc.ProductCatalogParentID*/
            var result = new List<DemandOfferDetail>();
            var query = @"  select secondTable.*,pc.ProductName as  ProductParentName ,pp.quantity  from(
 select  pc.ProductName,COUNT(*) as Count ,'Offer' as type,pc.ProductCatalogParentID from  [dbo].[tblOffer_Production] dp,
                    [dbo].[tblProductCatalog] pc,

                    [dbo].[tblProductAddress] pa,

                    [dbo].[tblPRM_AdminUnit] au
                   
                    where dp.product_Id=pc.Id
                    and pc.Status=1 and dp.Status=1
                    and  pa.Id=dp.productAddress_Id
                    and au.Id=pa.adminUnit_Id
					

                    and (
                    (au.Id in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in(
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au  --where au.ParentID=@parentID
					))))
                    or
                    ( 
                    au.Id in
                    (
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au --where au.ParentID=@parentID
					))))

                   group by pc.ProductName,pc.ProductCatalogParentID 

					) as secondTable
					
                  left join [dbo].[tblProductCatalog] pc on pc.Id=secondTable.ProductCatalogParentID
				  left join  [dbo].[tblPotential_Production] pp on pc.Id=pp.product_Id and pp.Status=1

		      order by secondTable.ProductName asc  ";
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
                            productParentName = reader.GetStringOrEmpty(4),
                            quantity = reader.GetDecimalOrDefaultValue(5),
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
            var query = @"   select secondTable.*,pc.ProductName as  ProductParentName  from(
 select  pc.ProductName,COUNT(*) as Count ,'Demand' as type,pc.ProductCatalogParentID from  [dbo].[tblDemand_Production] dp ,
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
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au  where au.ParentID=@parentID
					))))
                    or
                    ( 
                    au.Id in
                    (
                    select au.Id from  [dbo].[tblPRM_AdminUnit] au where au.ParentID in
                    (select au.Id from  [dbo].[tblPRM_AdminUnit] au  where au.ParentID=@parentID
					))))

                   group by pc.ProductName,pc.ProductCatalogParentID 

					) as secondTable
					
                  left join [dbo].[tblProductCatalog] pc on pc.Id=secondTable.ProductCatalogParentID

		      order by secondTable.ProductName asc   ";
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
                            productParentName = reader.GetStringOrEmpty(4)


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



        public List<ProductionCalendarDetail> GetProductionCalendarDemand()
        {
            var result = new List<ProductionCalendarDetail>();
            var query = @" select table1.type_eV_Id,table1.Production_Id,table1.year,table1.day,table1.oclock,
			 table1.partOfyear,table1.months_eV_Id,table1.offer_Id,table1.demand_Id,
			 table1.Production_type_eV_Id,table1.price,table1.transportation_eV_Id,
			 table1.quantity,table1.MonthName,table1.MonthDescription, ev.name as TypeName, ev.description as TypeDescription from (select pc.type_eV_Id,pc.Production_Id,pc.year,pc.day,pc.oclock,pc.partOfyear,
             pc.months_eV_Id,pc.offer_Id,
              pc.demand_Id,pc.Production_type_eV_Id,pc.price,pc.transportation_eV_Id,pc.quantity, ev.name as MonthName,ev.description as MonthDescription from [dbo].[tblProductionCalendar]pc ,[dbo].[tblEnumValue] ev 
             where pc.months_eV_Id=ev.Id and pc.Status=1    ) table1,
             [dbo].[tblEnumValue] ev 
			 where table1.type_eV_Id=ev.Id
							 ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionCalendarDetail()
                        {
                            type_eV_Id = reader.GetInt64OrDefaultValue(0),
                            production_Id = reader.GetInt64OrDefaultValue(1),
                            year = reader.GetInt64OrDefaultValue(2),
                            day = reader.GetInt64OrDefaultValue(3),
                            oclock = reader.GetInt64OrDefaultValue(4),
                            partOfyear = reader.GetInt64OrDefaultValue(5),
                            months_eV_Id = reader.GetInt64OrDefaultValue(6),
                            offer_Id = reader.GetInt64OrDefaultValue(7),
                            demand_Id = reader.GetInt64OrDefaultValue(8),
                            Production_type_eV_Id = reader.GetInt64OrDefaultValue(9),
                            price = reader.GetDecimalOrDefaultValue(10),
                            transportation_eV_Id = reader.GetInt64OrDefaultValue(11),
                            quantity = reader.GetDecimalOrDefaultValue(12),
                            MonthName = reader.GetStringOrEmpty(13),
                            MonthDescription = reader.GetStringOrEmpty(14),
                            TypeName = reader.GetStringOrEmpty(15),
                            TypeDescription = reader.GetStringOrEmpty(16),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        ///
        public List<ProductionCalendarDetail> GetProductionCalendarOfferId(Int64 offer)
        {
            var result = new List<ProductionCalendarDetail>();
            var query = @"select table1.type_eV_Id,table1.Production_Id,table1.year,table1.day,table1.oclock,
			 table1.partOfyear,table1.months_eV_Id,table1.offer_Id,table1.demand_Id,
			 table1.Production_type_eV_Id,table1.price,table1.transportation_eV_Id,
			 table1.quantity,table1.MonthName,table1.MonthDescription, ev.name as TypeName, ev.description as TypeDescription,table1.Id from (select pc.Id, pc.type_eV_Id,pc.Production_Id,pc.year,pc.day,pc.oclock,pc.partOfyear,
             pc.months_eV_Id,pc.offer_Id,
              pc.demand_Id,pc.Production_type_eV_Id,pc.price,pc.transportation_eV_Id,pc.quantity, ev.name as MonthName,ev.description as MonthDescription from [dbo].[tblProductionCalendar]pc ,[dbo].[tblEnumValue] ev 
           where pc.months_eV_Id=ev.Id and pc.Status=1 
			 and pc.offer_Id=@offer_Id 
			  ) 
			 table1,
             [dbo].[tblEnumValue] ev 
			 where table1.type_eV_Id=ev.Id


							 ";


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@offer_Id", offer);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionCalendarDetail()
                        {

                            type_eV_Id = reader.GetInt64OrDefaultValue(0),
                            production_Id = reader.GetInt64OrDefaultValue(1),
                            year = reader.GetInt64OrDefaultValue(2),
                            day = reader.GetInt64OrDefaultValue(3),
                            oclock = reader.GetInt64OrDefaultValue(4),
                            partOfyear = reader.GetInt64OrDefaultValue(5),
                            months_eV_Id = reader.GetInt64OrDefaultValue(6),
                            offer_Id = reader.GetInt64OrDefaultValue(7),
                            demand_Id = reader.GetInt64OrDefaultValue(8),
                            Production_type_eV_Id = reader.GetInt64OrDefaultValue(9),
                            price = reader.GetDecimalOrDefaultValue(10),
                            transportation_eV_Id = reader.GetInt64OrDefaultValue(11),
                            quantity = reader.GetDecimalOrDefaultValue(12),
                            MonthName = reader.GetStringOrEmpty(13),
                            MonthDescription = reader.GetStringOrEmpty(14),
                            TypeName = reader.GetStringOrEmpty(15),
                            TypeDescription = reader.GetStringOrEmpty(16),
                            ID = reader.GetInt64OrDefaultValue(17),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<ProductionCalendarDetail> GetProductionCalendarOfferId1(Int64 offer, Int64 startDate, Int64 endDate)
        {
            var result = new List<ProductionCalendarDetail>();
            StringBuilder squery = new StringBuilder();
            var query = @"select table1.type_eV_Id,table1.Production_Id,table1.year,table1.day,table1.oclock,
			 table1.partOfyear,table1.months_eV_Id,table1.offer_Id,table1.demand_Id,
			 table1.Production_type_eV_Id,table1.price,table1.transportation_eV_Id,
			 table1.quantity,table1.MonthName,table1.MonthDescription, ev.name as TypeName, ev.description as TypeDescription,table1.Id,
 table1.date1 from (select pc.Id, pc.type_eV_Id,pc.Production_Id,pc.year,pc.day,pc.oclock,pc.partOfyear,
             pc.months_eV_Id,pc.offer_Id,
              pc.demand_Id,pc.Production_type_eV_Id,pc.price,pc.transportation_eV_Id,pc.quantity, ev.name as MonthName,ev.description as MonthDescription
,dbo.dateReturn(pc.offer_Id,pc.year,pc.months_eV_Id,pc.day) as date1 from [dbo].[tblProductionCalendar]pc ,[dbo].[tblEnumValue] ev 
             where pc.months_eV_Id=ev.Id and pc.Status=1 
			 and pc.offer_Id=@offer_Id 
			  ) 
			 table1,
             [dbo].[tblEnumValue] ev 
			 where table1.type_eV_Id=ev.Id


							 ";
            var query1 = @"and table1.date1 between @startDate and @endDate
			  ";
            squery.Append(query);
            if (startDate != 0 || endDate != 0)
            {
                squery.Append(query1);
            }

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@offer_Id", offer);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionCalendarDetail()
                        {

                            type_eV_Id = reader.GetInt64OrDefaultValue(0),
                            production_Id = reader.GetInt64OrDefaultValue(1),
                            year = reader.GetInt64OrDefaultValue(2),
                            day = reader.GetInt64OrDefaultValue(3),
                            oclock = reader.GetInt64OrDefaultValue(4),
                            partOfyear = reader.GetInt64OrDefaultValue(5),
                            months_eV_Id = reader.GetInt64OrDefaultValue(6),
                            offer_Id = reader.GetInt64OrDefaultValue(7),
                            demand_Id = reader.GetInt64OrDefaultValue(8),
                            Production_type_eV_Id = reader.GetInt64OrDefaultValue(9),
                            price = reader.GetDecimalOrDefaultValue(10),
                            transportation_eV_Id = reader.GetInt64OrDefaultValue(11),
                            quantity = reader.GetDecimalOrDefaultValue(12),
                            MonthName = reader.GetStringOrEmpty(13),
                            MonthDescription = reader.GetStringOrEmpty(14),
                            TypeName = reader.GetStringOrEmpty(15),
                            TypeDescription = reader.GetStringOrEmpty(16),
                            ID = reader.GetInt64OrDefaultValue(17),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<ProductionCalendarDetail> GetProductionCalendarDemandId(Int64 demand)
        {
            var result = new List<ProductionCalendarDetail>();

            var query = @"  select  table1.type_eV_Id,table1.Production_Id,table1.year,table1.day,table1.oclock,
			 table1.partOfyear,table1.months_eV_Id,table1.offer_Id,table1.demand_Id,
			 table1.Production_type_eV_Id,table1.price,table1.transportation_eV_Id,
			 table1.quantity,table1.MonthName,table1.MonthDescription, ev.name as TypeName, ev.description as TypeDescription,
			 table1.Id,table1.date_
			  from (
			 select pc.Id, pc.type_eV_Id,pc.Production_Id,pc.year,pc.day,pc.oclock,pc.partOfyear,
             pc.months_eV_Id,pc.offer_Id,
              pc.demand_Id,pc.Production_type_eV_Id,pc.price,pc.transportation_eV_Id,pc.quantity, ev.name as MonthName,
ev.description as MonthDescription,pc.date_ 
from [dbo].[tblProductionCalendar]pc ,[dbo].[tblEnumValue] ev 
             where pc.months_eV_Id=ev.Id and pc.Status=1 and pc.demand_Id=@demand_Id 
) table1,
             [dbo].[tblEnumValue] ev 
			 where table1.type_eV_Id=ev.Id 
   ";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@demand_Id", demand);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionCalendarDetail()
                        {

                            type_eV_Id = reader.GetInt64OrDefaultValue(0),
                            production_Id = reader.GetInt64OrDefaultValue(1),
                            year = reader.GetInt64OrDefaultValue(2),
                            day = reader.GetInt64OrDefaultValue(3),
                            oclock = reader.GetInt64OrDefaultValue(4),
                            partOfyear = reader.GetInt64OrDefaultValue(5),
                            months_eV_Id = reader.GetInt64OrDefaultValue(6),
                            offer_Id = reader.GetInt64OrDefaultValue(7),
                            demand_Id = reader.GetInt64OrDefaultValue(8),
                            Production_type_eV_Id = reader.GetInt64OrDefaultValue(9),
                            price = reader.GetDecimalOrDefaultValue(10),
                            transportation_eV_Id = reader.GetInt64OrDefaultValue(11),
                            quantity = reader.GetDecimalOrDefaultValue(12),

                            MonthName = reader.GetStringOrEmpty(13),
                            MonthDescription = reader.GetStringOrEmpty(14),
                            TypeName = reader.GetStringOrEmpty(15),
                            TypeDescription = reader.GetStringOrEmpty(16),
                            ID = reader.GetInt64OrDefaultValue(17),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        //dila
        public List<ProductionCalendarDetail> GetProductionCalendarDemandId1(Int64 demand, Int64 startDate, Int64 endDate)
        {
            var result = new List<ProductionCalendarDetail>();
            StringBuilder squery = new StringBuilder();
            var query = @"  select  table1.type_eV_Id,table1.Production_Id,table1.year,table1.day,table1.oclock,
			 table1.partOfyear,table1.months_eV_Id,table1.offer_Id,table1.demand_Id,
			 table1.Production_type_eV_Id,table1.price,table1.transportation_eV_Id,
			 table1.quantity,table1.MonthName,table1.MonthDescription, ev.name as TypeName, ev.description as TypeDescription,table1.date1,
			 table1.Id
			  from (
			 select pc.Id, pc.type_eV_Id,pc.Production_Id,pc.year,pc.day,pc.oclock,pc.partOfyear,
             pc.months_eV_Id,pc.offer_Id,
              pc.demand_Id,pc.Production_type_eV_Id,pc.price,pc.transportation_eV_Id,pc.quantity, ev.name as MonthName,ev.description as MonthDescription,
  dbo.dateReturn(pc.offer_Id,pc.year,pc.months_eV_Id,pc.day) as date1   from [dbo].[tblProductionCalendar]pc ,[dbo].[tblEnumValue] ev 
             where pc.months_eV_Id=ev.Id and pc.Status=1 and pc.demand_Id=@demand_Id 
) table1,
             [dbo].[tblEnumValue] ev 
			 where table1.type_eV_Id=ev.Id
   ";
            var query1 = @"and table1.date1 between @startDate and @endDate
			  ";

            squery.Append(query.ToString());
            ///!string.IsNullOrEmpty(Convert.ToString(this.SecRelationship))

            if (startDate != 0 && endDate != 0)
            {
                squery.Append(query1);
            }

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@demand_Id", demand);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionCalendarDetail()
                        {

                            type_eV_Id = reader.GetInt64OrDefaultValue(0),
                            production_Id = reader.GetInt64OrDefaultValue(1),
                            year = reader.GetInt64OrDefaultValue(2),
                            day = reader.GetInt64OrDefaultValue(3),
                            oclock = reader.GetInt64OrDefaultValue(4),
                            partOfyear = reader.GetInt64OrDefaultValue(5),
                            months_eV_Id = reader.GetInt64OrDefaultValue(6),
                            offer_Id = reader.GetInt64OrDefaultValue(7),
                            demand_Id = reader.GetInt64OrDefaultValue(8),
                            Production_type_eV_Id = reader.GetInt64OrDefaultValue(9),
                            price = reader.GetDecimalOrDefaultValue(10),
                            transportation_eV_Id = reader.GetInt64OrDefaultValue(11),
                            quantity = reader.GetDecimalOrDefaultValue(12),

                            MonthName = reader.GetStringOrEmpty(13),
                            MonthDescription = reader.GetStringOrEmpty(14),
                            TypeName = reader.GetStringOrEmpty(15),
                            TypeDescription = reader.GetStringOrEmpty(16),
                            ID = reader.GetInt64OrDefaultValue(18),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public ProductionDetail GetOfferProductionDetailById(Int64 offer_id)
        {
            var result = new ProductionDetail();
            var query = @"  select table2.*,pc.ProductName as  ProductParentName  from(	 select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id ,FirstTable.ProductCatalogParentID,
					  FirstTable.Email
					  ,FirstTable.Name,FirstTable.Surname,FirstTable.FatherName,FirstTable.birtday,
					  FirstTable.gender,FirstTable.profilePicture,FirstTable.address_Id,FirstTable.PinNumber
					  ,FirstTable.productPotentialQuantity from (   
                     select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,us.Email,person.Name,person.Surname,
					person.FatherName,person.birtday,person.gender,person.profilePicture,person.address_Id,person.PinNumber
					,pp.quantity as productPotentialQuantity
                      from [dbo].[tblOffer_Production] op  
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id  and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
					left join [dbo].[tblPerson] person on op.user_Id=person.UserId and person.Status=1
                     left join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id  and prc.Status=1   
                     left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id and ev.Status=1
                    left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1   
					left join [dbo].[tblPotential_Production] pp on us.Id=pp.user_Id and pp.Status=1
                      where   
					  op.Id= @offer_id and
					   op.Status=1
                    ) as FirstTable   
                     left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1		
					 ) as table2	
					  left join [dbo].[tblProductCatalog] pc on pc.Id=table2.ProductCatalogParentID
 where table2.EnumCategoryId=5";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@offer_id", offer_id);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        result.productionID = reader.GetInt64OrDefaultValue(0);
                        result.unitPrice = reader.GetDecimalOrDefaultValue(1);
                        result.quantity = reader.GetDecimalOrDefaultValue(2);
                        result.description = reader.GetStringOrEmpty(3);
                        result.productId = reader.GetInt64OrDefaultValue(4);
                        result.productName = reader.GetStringOrEmpty(5);
                        result.Status = reader.GetStringOrEmpty(6);
                        result.fullAddress = reader.GetStringOrEmpty(7);
                        result.addressDesc = reader.GetStringOrEmpty(8);
                        result.enumCategoryId = reader.GetInt64OrDefaultValue(9);
                        result.enumValueId = reader.GetInt64OrDefaultValue(10);
                        result.enumValueName = reader.GetStringOrEmpty(11);
                        result.userId = reader.GetInt64OrDefaultValue(12);
                        result.email = reader.GetStringOrEmpty(14);
                        result.name = reader.GetStringOrEmpty(15);
                        result.surname = reader.GetStringOrEmpty(16);
                        result.fatherName = reader.GetStringOrEmpty(17);

                        result.birtday = reader.GetInt64OrDefaultValue(18);
                        result.gender = reader.GetStringOrEmpty(19);
                        result.profilPicture = reader.GetStringOrEmpty(20);
                        result.adress_Id = reader.GetInt64OrDefaultValue(21);
                        result.pinNumber = reader.GetStringOrEmpty(22);
                        result.potentialProductQuantity = reader.GetDecimalOrDefaultValue(23);
                        result.productParentName = reader.GetStringOrEmpty(24);


                    };
                }
                connection.Close();

            }
            return result;
        }


        public tblProductCatalog GetProducParentProductByProductID(Int64 productID)
        {
            tblProductCatalog productCatalog = new tblProductCatalog();
            var query = @"  select * from [dbo].[tblProductCatalog] pc where pc.Status=1 and  pc.Id=
(select pcUnit.ProductCatalogParentID from   [dbo].[tblProductCatalog] pcUnit  where pcUnit.Id=@ProductID
and pcUnit.Status=1) order by pc.ProductName ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        productCatalog = new tblProductCatalog()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            ProductCatalogParentID = reader.GetInt64OrDefaultValue(1),
                            ProductName = reader.GetStringOrEmpty(2),
                            ProductDescription = reader.GetStringOrEmpty(3),
                            canBeOrder = reader.GetInt64OrDefaultValue(4),
                            CatalogLevel = reader.GetInt64OrDefaultValue(5),
                            measurementUnit_enumValueId = reader.GetInt64OrDefaultValue(6),



                        };
                    }
                }
                connection.Close();
            }

            return productCatalog;
        }
        public tblProductCatalog GetProducParentProductByEnumV(Int64 productID)
        {
            tblProductCatalog productCatalog = new tblProductCatalog();
            var query = @"  select * from [dbo].[tblProductCatalog] pc where pc.Status=1 and  pc.Id=
(select pcUnit.ProductCatalogParentID from   [dbo].[tblProductCatalog] pcUnit  where pcUnit.Id=@ProductID
and pcUnit.Status=1) ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        productCatalog = new tblProductCatalog()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            ProductCatalogParentID = reader.GetInt64OrDefaultValue(1),
                            ProductName = reader.GetStringOrEmpty(2),
                            ProductDescription = reader.GetStringOrEmpty(3),
                            canBeOrder = reader.GetInt64OrDefaultValue(4),
                            CatalogLevel = reader.GetInt64OrDefaultValue(5),
                            measurementUnit_enumValueId = reader.GetInt64OrDefaultValue(6),



                        };
                    }
                }
                connection.Close();
            }

            return productCatalog;
        }

        public List<tblProductCatalog> GetProducListByUserID(Int64 userID, Int64 productId)
        {
            var result = new List<tblProductCatalog>();
            StringBuilder squery = new StringBuilder();
            var query = @" 
 select * from tblProductCatalog pc where 
  pc.Id in(select pp.product_Id 
  from tblPotential_Production pp where pp.user_Id=@userID and pp.Status=1)  
  union 
    select * from tblProductCatalog pc where 
  pc.Id in(select op.product_Id 
  from tblOffer_Production op where op.user_Id=@userID and op.Status=1)   
 
";
            var queryProduct = @"  and pc.Id=@ProductId";
            var queryOrder = @" order by pc.ProductName";
            squery.Append(query);
            if (productId != 0)
            {
                squery.Append(queryProduct);
            }
            squery.Append(queryOrder);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new tblProductCatalog()
                        {

                            Id = reader.GetInt64OrDefaultValue(0),
                            ProductCatalogParentID = reader.GetInt64OrDefaultValue(1),
                            ProductName = reader.GetStringOrEmpty(2),
                            ProductDescription = reader.GetStringOrEmpty(3),
                            canBeOrder = reader.GetInt64OrDefaultValue(4),
                            CatalogLevel = reader.GetInt64OrDefaultValue(5),
                            measurementUnit_enumValueId = reader.GetInt64OrDefaultValue(6),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<ProductionDetail> GetDemandProductDetailInfoForAccounting(Int64 stateId, Int64 year, Int64 partOfYear)
        {
            var result = new List<ProductionDetail>();
            var query = @"   select table1.* ,pc.ProductName as productParentName,ev.name as kategoryName ,quantity*unit_price as totalPrice from(    select fo.name, op.Id as ProductionId, pc.ProductName, pc.Id,pc.ProductCatalogParentID,
	price.unit_price,
	
	padr.fullAddress ,
	op.quantity, 
	ev.createdDate,ev.updatedDate,ev.LastUpdatedStatus,prc.EnumValueId,padr.addressDesc
	from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
					   left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
					   left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
							
					  left join [dbo].[tblProductAddress] padr on padr.Id=op.address_Id and padr.Status=1
                       where      op.Status=1 --and op.isSelected=1
                   and op.state_eV_Id=2  
                  and price.year= @year and price.partOfYear=@partOfYear
					  )as table1
					   left join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1
					     left join [dbo].[tblEnumValue] ev on table1.EnumValueId=ev.Id and ev.Status=1
	where 	 enumCategory_enumCategoryId=5 ";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", stateId);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@partOfYear", partOfYear);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionDetail()
                        {
                            name = reader.GetStringOrEmpty(0),
                            productionID = reader.GetInt64OrDefaultValue(1),
                            productName = reader.GetStringOrEmpty(2),
                            productId = reader.GetInt64OrDefaultValue(3),
                            productParentId = reader.GetInt64OrDefaultValue(4),
                            unitPrice = reader.GetDecimalOrDefaultValue(5),
                            fullAddress = reader.GetStringOrEmpty(6),
                            quantity = reader.GetDecimalOrDefaultValue(7),

                            createdDate = reader.GetInt64OrDefaultValue(8),
                            updatedDate = reader.GetInt64OrDefaultValue(9),
                            lastUpdateStatus = reader.GetInt64OrDefaultValue(10),
                            addressDesc = reader.GetStringOrEmpty(12),
                            productParentName = reader.GetStringOrEmpty(13),
                            kategoryName = reader.GetStringOrEmpty(14),
                            productUnitPrice = reader.GetDecimalOrDefaultValue(15),




                        });
                    }
                }
                connection.Close();
            }

            return result;
        }




        public List<ProductionDetail> GetDemandProductsForAccounting(Int64 stateId)
        {
            var result = new List<ProductionDetail>();
            var query = @"
select 
table1.* ,ev.name as kategoryName,  totatq * unit_price  as totalPrice,pc.ProductName as productParentName 
from( 
select fo.name, pc.ProductName,Sum (cal.quantity) as totatq,price.unit_price ,prc.EnumValueId
,padr.fullAddress,op.state_eV_Id,pc.ProductCatalogParentID,padr.addressDesc
 from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                     left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
					   	   left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
						    left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
							left join [dbo].[tblProductionCalendar] cal on cal.Production_Id=op.Id and cal.Status=1
							left join [dbo].[tblProductAddress] padr on op.address_Id=padr.Id and padr.Status=1
							 where      op.Status=1 and  op.state_eV_Id=@state_eV_Id 
							 	group by 
				fo.name, pc.ProductName,price.unit_price ,prc.EnumValueId
,padr.fullAddress,op.state_eV_Id,pc.ProductCatalogParentID,padr.addressDesc	
                
							)as table1
							  left join [dbo].[tblEnumValue] ev on table1.EnumValueId=ev.Id and ev.Status=1
							  left join [dbo].[tblProductCatalog] pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
							   where enumCategory_enumCategoryId=5
";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", stateId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionDetail()
                        {
                            name = reader.GetStringOrEmpty(0),
                            productName = reader.GetStringOrEmpty(1),
                            quantity = reader.GetDecimalOrDefaultValue(2),
                            unitPrice = reader.GetDecimalOrDefaultValue(3),
                            fullAddress = reader.GetStringOrEmpty(5),
                            addressDesc = reader.GetStringOrEmpty(8),
                            kategoryName = reader.GetStringOrEmpty(9),
                            totalPrice = reader.GetDecimalOrDefaultValue(10),
                            productParentName = reader.GetStringOrEmpty(11)






                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<DemandOfferDetail> GetDemandOfferProductionTotal(Int64 adressID)
        {
            var result = new List<DemandOfferDetail>();
            var query = @"
    


select distinct table1.Id,table1.ProductCatalogParentID, pc.ProductName as ParantName ,
 table1.ProductName,
        table1.description, 
			'Offer' as type ,Sum(quantity) as OfferDemand,table1.unit_price
			 from (
			select pc.Id,pc.ProductCatalogParentID, pc.ProductName,op.Id as ProducitonID,op.quantity
			,ev.description
		,price.unit_price,op.productAddress_Id
		from dbo.tblOffer_Production op
         join dbo.tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1 
         join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id 
		 and prc.Production_type_eV_Id=3 and prc.Status=1 and prc.EnumCategoryId  =5
         left  join  dbo.tblEnumValue ev on ev.Id=prc.EnumValueId and ev.Status=1
        left join dbo.tblEnumCategory ec on ec.Id=ev.enumCategory_enumCategoryId and ec.Status=1 and ec.Id=5
		  join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
		 
		   
           
         where  op.Status=1 --and	op.productAddress_Id=		
		  
		 ) table1,
         dbo.tblProductCatalog pc where table1.ProductCatalogParentID=pc.ID
		-- and table1.adressID=@adressID
         group by table1.Id,table1.ProductCatalogParentID, table1.ProductName ,pc.ProductName,
	    table1.description,table1.unit_price
union  all
        select distinct table1.Id,table1.ProductCatalogParentID, pc.ProductName as ParantName , table1.ProductName,
         table1.description,'Demand' as type, Sum(quantity) as OfferDemand,table1.unit_price
         from (select pc.Id,pc.ProductCatalogParentID, pc.ProductName,dp.quantity,
		 ev.description 
		 ,price.unit_price,dp.address_Id
		  from [dbo].[tblDemand_Production] dp
		  join dbo.tblProductCatalog pc on dp.product_Id=pc.Id and pc.Status=1 
		  join [dbo].[tblProductionControl] prc on dp.Id=prc.Demand_Production_Id 
		  and prc.Production_type_eV_Id=28 and prc.Status=1 and prc.EnumCategoryId=5
		  left join  dbo.tblEnumValue ev on ev.Id=prc.EnumValueId and ev.Status=1
          left join dbo.tblEnumCategory ec on ec.Id=ev.enumCategory_enumCategoryId and ec.Status=1 and ec.Id=5
          join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
		   
           
		  
          where  dp.Status=1) table1,
          dbo.tblProductCatalog pc where table1.ProductCatalogParentID=pc.ID
		  
		-- and table1.adressID=@adressID
          group by table1.Id,table1.ProductCatalogParentID, table1.ProductName ,pc.ProductName,
          table1.description ,table1.unit_price
         order by ParantName asc


		  ---843498,05 kq



		
";
            var queryDate = @" and table1.Id in (select  distinct
			 table1.Production_Id
			  from (
			 select 
           
              pc.Production_Id,
			  dbo.dateReturn(pc.demand_Id,pc.year,pc.months_eV_Id,pc.day) as date1
     from [dbo].[tblProductionCalendar]pc
             where  pc.Status=1 
 
			
         ) table1
           
			 where   table1.date1  between @startDate and @endDate
			 )";
            var queryEnd = @"";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@addressID", adressID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemandOfferDetail()
                        {
                            productID = reader.GetInt64OrDefaultValue(0),
                            productParentName = reader.GetStringOrEmpty(2),
                            productName = reader.GetStringOrEmpty(3),

                            kategoryName = reader.GetStringOrEmpty(4),
                            type = reader.GetStringOrEmpty(5),
                            offerDemand = reader.GetDecimalOrDefaultValue(6),
                            unitPrice = reader.GetDecimalOrDefaultValue(7),





                        });
                    }
                }
                connection.Close();
            }

            return result;

        }
//        public List<DemandOfferDetail> GetDemandOfferProductionTotal(Int64 adressID)
//        {
//            var result = new List<DemandOfferDetail>();
//            var query = @"
//    
//
//
//select distinct table1.Id,table1.ProductCatalogParentID, pc.ProductName as ParantName ,
// table1.ProductName,
//        table1.description, 
//			'Offer' as type ,Sum(quantity) as OfferDemand,table1.unit_price
//			 from (
//			select pc.Id,pc.ProductCatalogParentID, pc.ProductName,op.Id as ProducitonID,op.quantity
//			,ev.description
//		,price.unit_price,op.productAddress_Id
//		from dbo.tblOffer_Production op
//         join dbo.tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1 
//         join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id 
//		 and prc.Production_type_eV_Id=3 and prc.Status=1 and prc.EnumCategoryId  =5
//         left  join  dbo.tblEnumValue ev on ev.Id=prc.EnumValueId and ev.Status=1
//        left join dbo.tblEnumCategory ec on ec.Id=ev.enumCategory_enumCategoryId and ec.Status=1 and ec.Id=5
//		  join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
//		 
//		   
//           
//         where  op.Status=1 --and	op.productAddress_Id=		
//		  
//		 ) table1,
//         dbo.tblProductCatalog pc where table1.ProductCatalogParentID=pc.ID
//		-- and table1.adressID=@adressID
//         group by table1.Id,table1.ProductCatalogParentID, table1.ProductName ,pc.ProductName,
//	    table1.description,table1.unit_price
//union  all
//        select distinct table1.Id,table1.ProductCatalogParentID, pc.ProductName as ParantName , table1.ProductName,
//         table1.description,'Demand' as type, Sum(quantity) as OfferDemand,table1.unit_price
//         from (select pc.Id,pc.ProductCatalogParentID, pc.ProductName,dp.quantity,
//		 ev.description 
//		 ,price.unit_price,dp.address_Id
//		  from [dbo].[tblDemand_Production] dp
//		  join dbo.tblProductCatalog pc on dp.product_Id=pc.Id and pc.Status=1 
//		  join [dbo].[tblProductionControl] prc on dp.Id=prc.Demand_Production_Id 
//		  and prc.Production_type_eV_Id=28 and prc.Status=1 and prc.EnumCategoryId=5
//		  left join  dbo.tblEnumValue ev on ev.Id=prc.EnumValueId and ev.Status=1
//          left join dbo.tblEnumCategory ec on ec.Id=ev.enumCategory_enumCategoryId and ec.Status=1 and ec.Id=5
//          join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
//		   
//           
//		  
//          where  dp.Status=1) table1,
//          dbo.tblProductCatalog pc where table1.ProductCatalogParentID=pc.ID
//		  
//	--and table1.adressID=@adressID
//          group by table1.Id,table1.ProductCatalogParentID, table1.ProductName ,pc.ProductName,
//          table1.description ,table1.unit_price
//         order by ParantName asc
//
//
//		  ---843498,05 kq
//
//
//
//		
//";
//            var queryDate = @" and table1.Id in (select  distinct
//			 table1.Production_Id
//			  from (
//			 select 
//           
//              pc.Production_Id,
//			  dbo.dateReturn(pc.demand_Id,pc.year,pc.months_eV_Id,pc.day) as date1
//     from [dbo].[tblProductionCalendar]pc
//             where  pc.Status=1 
// 
//			
//         ) table1
//           
//			 where   table1.date1  between @startDate and @endDate
//			 )";
//            var queryEnd = @"";

//            using (var connection = new SqlConnection(DBUtil.ConnectionString))
//            {
//                connection.Open();

//                using (var command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@addressID", adressID);
//                    var reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        result.Add(new DemandOfferDetail()
//                        {
//                            productID = reader.GetInt64OrDefaultValue(0),
//                            productParentName = reader.GetStringOrEmpty(2),
//                            productName = reader.GetStringOrEmpty(3),

//                            kategoryName = reader.GetStringOrEmpty(4),
//                            type = reader.GetStringOrEmpty(5),
//                            offerDemand = reader.GetDecimalOrDefaultValue(6),
//                            unitPrice = reader.GetDecimalOrDefaultValue(7),





//                        });
//                    }
//                }
//                connection.Close();
//            }

//            return result;

//        }
        public List<UserInfo> GetPersonalinformationByRoleId(PotensialUserForAdminUnitIdList ops)
        {
            var result = new List<UserInfo>();
            StringBuilder squery = new StringBuilder();
            //var query1 = @"";
            var query = @" ;WITH RESULTS AS
    (
        SELECT *
            , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn
            , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed
        FROM ( select tb.*  from(
select table1.* ,au.Name as parentName  from( select person.Id, person.Name as personName,person.Surname,person.FatherName,person.gender
,person.profilePicture,person.LastUpdatedStatus,person.createdDate,person.createdUser,u.Email,u.Id as userID,
 u.userType_eV_ID,ev.name as usertype,
ur.RoleId,r.Name as roleName,r.Description as roleDescription,adr.fullAddress,adr.adminUnit_Id,
au.Name as adminUnitName,au.ParentID,'' as huquqiSexsinAdi,person.PinNumber,'' as voen,person.birtday,person.Status,adr.addressDesc  from tblPerson person
 join tblUser u on person.UserId=u.Id and u.Status=1
 left join tblUserRole ur on ur.UserId=person.UserId and ur.Status=1
 left join tblRole r on ur.RoleId=r.Id and r.Status=1
 left join tblAddress adr on adr.user_Id=person.UserId and adr.Status=1
 left join tblPRM_AdminUnit au on adr.adminUnit_Id=au.Id and au.Status=1
 left join tblEnumValue ev on ev.Id=u.userType_eV_ID and ev.Status=1
 join tblOffer_Production op on op.user_Id=u.Id
where person.Status=1 and u.userType_eV_ID=26 and ur.RoleId=24
) as table1
left join tblPRM_AdminUnit au on table1.ParentID=au.Id and au.Status=1
union 
select table1.* ,au.Name as parentName  from( select person.Id, person.Name as personName,person.Surname,person.FatherName,person.gender
,person.profilePicture,person.LastUpdatedStatus,person.createdDate,person.createdUser,u.Email,u.Id as userID,u.userType_eV_ID,
ev.name as usertype, ur.RoleId,r.Name as roleName,r.Description as roleDescription,adr.fullAddress,adr.adminUnit_Id,
au.Name as adminUnitName,au.ParentID,fo.name as huquqiSexsinAdi,'' as pinNumber,fo.voen,person.birtday ,person.Status,adr.addressDesc from tblPerson person
 join tblUser u on person.UserId=u.Id and u.Status=1
 left join tblUserRole ur on ur.UserId=person.UserId and ur.Status=1
 left join tblRole r on ur.RoleId=r.Id and r.Status=1
 left join tblAddress adr on adr.user_Id=person.UserId and adr.Status=1
 left join tblPRM_AdminUnit au on adr.adminUnit_Id=au.Id and au.Status=1
 left join tblForeign_Organization fo on fo.userId=person.UserId and fo.Status=1
 left join tblEnumValue ev on ev.Id=u.userType_eV_ID and ev.Status=1
 join tblOffer_Production op on op.user_Id=u.Id
where person.Status=1 and u.userType_eV_ID=50 and ur.RoleId=24
) as table1
left join tblPRM_AdminUnit au on table1.ParentID=au.Id and au.Status=1
) as tb where  Status=1
";
            var query2 = @" ) as tb 
           
           )SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";


            squery.Append(query);
            var queryRole = @" and  RoleId=@RoleId";
            var queryUser = @"   and adminUnit_Id 
		   in (
				
				select table4.Id from 
				(select aunit.Id as userID, aunit.Id ,aunit.Name 
				from [dbo].[tblUser] us,
				[dbo].tblPRM_KTNBranch  ktnbranch,

				[dbo].[tblBranchResponsibility] branchResp,

				[dbo].[tblPRM_AdminUnit] aunit

				where  us.KTN_ID=ktnbranch.Id
				and ktnbranch.Id= branchResp.branchId
	            and branchResp.branchType_eVId=52
                  and us.Id=@userID
			) as table4

			)
			";

            var queryPinNumber = @" and PinNumber like '%'+@PinNumber+'%'";
            var queryVoen = @"  and voen like '%'+@voen+'%'";
            var queryName = @" and ((personName like '%'+@name+'%') 
			 or (Surname like '%'+@name+'%') or( FatherName like '%'+@name+'%') or( huquqiSexsinAdi like '%'+@name+'%'))";
            var queryAddress = @" and (fullAddress like '%'+@address+'%' or addressDesc like '%'+@address+'%')";
            if (ops.roleID != 0)
            {
                squery.Append(queryRole);
            }
            if (ops.userID != 0)
            {
                squery.Append(queryUser);
            }
            if (!string.IsNullOrEmpty(ops.pinNumber))
            {
                squery.Append(queryPinNumber);
            }
            if (!string.IsNullOrEmpty(ops.voen))
            {
                squery.Append(queryVoen);
            }
            if (!string.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);
            }
            if (!string.IsNullOrEmpty(ops.address))
            {
                squery.Append(queryAddress);
            }
            squery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
                    command.Parameters.AddWithValue("@userID", ops.userID);
                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@PinNumber", ops.pinNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@voen", ops.voen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@name", ops.name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@address", ops.address ?? (object)DBNull.Value);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new UserInfo()
                        {
                            personId = reader.GetInt64OrDefaultValue(0),
                            name = reader.GetStringOrEmpty(1),
                            surname = reader.GetStringOrEmpty(2),
                            fatherName = reader.GetStringOrEmpty(3),
                            gender = reader.GetStringOrEmpty(4),
                            profilPicture = reader.GetStringOrEmpty(5),
                            lastUpdateStatus = reader.GetInt64OrDefaultValue(6),
                            createdDate = reader.GetInt64OrDefaultValue(7),
                            email = reader.GetStringOrEmpty(9),
                            userID = reader.GetInt64OrDefaultValue(10),
                            userTypeID = reader.GetInt64OrDefaultValue(11),
                            userType = reader.GetStringOrEmpty(12),
                            roleID = reader.GetInt64OrDefaultValue(13),
                            roleName = reader.GetStringOrEmpty(14),
                            roleDescription = reader.GetStringOrEmpty(15),
                            fullAddress = reader.GetStringOrEmpty(16),



                            adminUnitID = reader.GetInt64OrDefaultValue(17),
                            adminUnitName = reader.GetStringOrEmpty(18),
                            OrganisationName = reader.GetStringOrEmpty(20),
                            pinNumber = reader.GetStringOrEmpty(21),
                            voen = reader.GetStringOrEmpty(22),
                            birtday = reader.GetInt64OrDefaultValue(23),
                            adressDesc = reader.GetStringOrEmpty(25),
                            parantName = reader.GetStringOrEmpty(26),














                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public Int64 GetPersonalinformationByRoleId_OPC(PotensialUserForAdminUnitIdList ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            //var query1 = @"";
            var query = @" select COUNT(*) as Count from(
select table1.* ,au.Name as parentName  from( select person.Id, person.Name as personName,person.Surname,person.FatherName,person.gender
,person.profilePicture,person.LastUpdatedStatus,person.createdDate,person.createdUser,u.Email,u.Id as userID,
 u.userType_eV_ID,ev.name as usertype,
ur.RoleId,r.Name as roleName,r.Description as roleDescription,adr.fullAddress,adr.adminUnit_Id,
au.Name as adminUnitName,au.ParentID,'' as huquqiSexsinAdi,person.PinNumber,'' as voen,person.birtday,person.Status  from tblPerson person
 join tblUser u on person.UserId=u.Id and u.Status=1
 left join tblUserRole ur on ur.UserId=person.UserId and ur.Status=1
 left join tblRole r on ur.RoleId=r.Id and r.Status=1
 left join tblAddress adr on adr.user_Id=person.UserId and adr.Status=1
 left join tblPRM_AdminUnit au on adr.adminUnit_Id=au.Id and au.Status=1
 left join tblEnumValue ev on ev.Id=u.userType_eV_ID and ev.Status=1
 join tblOffer_Production op on op.user_Id=u.Id
where person.Status=1 and u.userType_eV_ID=26 and ur.RoleId=24
) as table1
left join tblPRM_AdminUnit au on table1.ParentID=au.Id and au.Status=1
union 
select table1.* ,au.Name as parentName  from( select person.Id, person.Name as personName,person.Surname,person.FatherName,person.gender
,person.profilePicture,person.LastUpdatedStatus,person.createdDate,person.createdUser,u.Email,u.Id as userID,u.userType_eV_ID,
ev.name as usertype, ur.RoleId,r.Name as roleName,r.Description as roleDescription,adr.fullAddress,adr.adminUnit_Id,
au.Name as adminUnitName,au.ParentID,fo.name as huquqiSexsinAdi,'' as pinNumber,fo.voen,person.birtday ,person.Status from tblPerson person
 join tblUser u on person.UserId=u.Id and u.Status=1
 left join tblUserRole ur on ur.UserId=person.UserId and ur.Status=1
 left join tblRole r on ur.RoleId=r.Id and r.Status=1
 left join tblAddress adr on adr.user_Id=person.UserId and adr.Status=1
 left join tblPRM_AdminUnit au on adr.adminUnit_Id=au.Id and au.Status=1
 left join tblForeign_Organization fo on fo.userId=person.UserId and fo.Status=1
 left join tblEnumValue ev on ev.Id=u.userType_eV_ID and ev.Status=1
 join tblOffer_Production op on op.user_Id=u.Id
where person.Status=1 and u.userType_eV_ID=50 and ur.RoleId=24
) as table1
left join tblPRM_AdminUnit au on table1.ParentID=au.Id and au.Status=1
) as tb where  Status=1
";
            squery.Append(query);
            var queryRole = @" and  RoleId=@RoleId";
            var queryPinNumber = @" and PinNumber like '%'+@PinNumber+'%'";
            var queryVoen = @"  and voen like '%'+@voen+'%'";
            var queryUser = @"   and adminUnit_Id 
		   in (
				
				select table4.Id from 
				(select aunit.Id as userID, aunit.Id ,aunit.Name 
				from [dbo].[tblUser] us,
				[dbo].tblPRM_KTNBranch  ktnbranch,

				[dbo].[tblBranchResponsibility] branchResp,

				[dbo].[tblPRM_AdminUnit] aunit

				where  us.KTN_ID=ktnbranch.Id
				and ktnbranch.Id= branchResp.branchId
	            and branchResp.branchType_eVId=52
                  and us.Id=@userID
			) as table4

			)
			";

            var queryName = @" and ((personName like '%'+@name+'%') 
			 or (Surname like '%'+@name+'%') or( FatherName like '%'+@name+'%') or( huquqiSexsinAdi like '%'+@name+'%'))";
            var queryAddress = @" and (fullAddress like '%'+@address+'%' or addressDesc like '%'+@address+'%')";
            if (ops.roleID != 0)
            {
                squery.Append(queryRole);
            }
            if (ops.userID != 0)
            {
                squery.Append(queryUser);
            }
            if (!string.IsNullOrEmpty(ops.pinNumber))
            {
                squery.Append(queryPinNumber);
            }
            if (!string.IsNullOrEmpty(ops.voen))
            {
                squery.Append(queryVoen);
            }
            if (!string.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);
            }
            if (!string.IsNullOrEmpty(ops.address))
            {
                squery.Append(queryAddress);
            }
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
                    command.Parameters.AddWithValue("@userID", ops.userID);
                    command.Parameters.AddWithValue("@PinNumber", ops.pinNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@voen", ops.voen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@name", ops.name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@address", ops.address ?? (object)DBNull.Value);
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
        public PersonInformation GetPersonInformationUserID(Int64 userId)
        {
            PersonInformation productCatalog = new PersonInformation();
            var query = @"  
                select p.Id,p.Name,p.Surname,p.FatherName,p.PinNumber,p.UserId,p.gender,p.birtday,p.educationLevel_eV_Id
				,p.job_eV_Id,p.address_Id,p.Status,p.LastUpdatedStatus,p.createdUser,p.createdDate,p.updatedUser,p.updatedDate,
				p.profilePicture,r.Id as roleID, r.Name as roleName,r.Description,r.Id from	tblPerson p
				left join dbo.tblUser u on u.Id=p.UserId 
				left join tblUserRole ur on u.Id=ur.UserId 
				 join dbo.tblRole r on ur.RoleId=r.Id 
				and r.Id in (11,15)
               where u.Id=@UserId 
							 ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        productCatalog = new PersonInformation()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            Surname = reader.GetStringOrEmpty(2),
                            FatherName = reader.GetStringOrEmpty(3),
                            PinNumber = reader.GetStringOrEmpty(4),
                            UserId = reader.GetInt64OrDefaultValue(5),
                            gender = reader.GetStringOrEmpty(6),
                            birtday = reader.GetInt64OrDefaultValue(7),
                            educationLevel_eV_Id = reader.GetInt64OrDefaultValue(8),
                            job_eV_Id = reader.GetInt64OrDefaultValue(9),
                            address_Id = reader.GetInt64OrDefaultValue(10),
                            Status = reader.GetInt64OrDefaultValue(11),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(12),
                            createdUser = reader.GetStringOrEmpty(13),
                            createdDate = reader.GetInt64OrDefaultValue(14),
                            updatedUser = reader.GetStringOrEmpty(15),

                            updatedDate = reader.GetInt64OrDefaultValue(16),
                            profilePicture = reader.GetStringOrEmpty(17),
                            roleId = reader.GetInt64OrDefaultValue(18),
                            roleName = reader.GetStringOrEmpty(19),
                            roleDescription = reader.GetStringOrEmpty(20),

















                        };
                    }
                }
                connection.Close();
            }

            return productCatalog;
        }

        public PersonInformation GetPersonInformationuserIDNew(Int64 userID)
        {
            PersonInformation productCatalog = new PersonInformation();
            var query = @"  
                select p.Id,p.Name,p.Surname,p.FatherName,p.PinNumber,p.UserId,p.gender,p.birtday,p.educationLevel_eV_Id
				,p.job_eV_Id,p.address_Id,p.Status,p.LastUpdatedStatus,p.createdUser,p.createdDate,p.updatedUser,p.updatedDate,
				p.profilePicture,r.Id as roleID, r.Name as roleName,r.Description,r.Id 
				from	tblUser u
				 join dbo.tblPerson p on u.Id=p.UserId 
				 join tblUserRole ur on u.Id=ur.UserId 
				 join dbo.tblRole r on ur.RoleId=r.Id 
				and r.Id in (11,15)
               where u.Id=@UserId 
							 ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        productCatalog = new PersonInformation()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            Surname = reader.GetStringOrEmpty(2),
                            FatherName = reader.GetStringOrEmpty(3),
                            PinNumber = reader.GetStringOrEmpty(4),
                            UserId = reader.GetInt64OrDefaultValue(5),
                            gender = reader.GetStringOrEmpty(6),
                            birtday = reader.GetInt64OrDefaultValue(7),
                            educationLevel_eV_Id = reader.GetInt64OrDefaultValue(8),
                            job_eV_Id = reader.GetInt64OrDefaultValue(9),
                            address_Id = reader.GetInt64OrDefaultValue(10),
                            Status = reader.GetInt64OrDefaultValue(11),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(12),
                            createdUser = reader.GetStringOrEmpty(13),
                            createdDate = reader.GetInt64OrDefaultValue(14),
                            updatedUser = reader.GetStringOrEmpty(15),

                            updatedDate = reader.GetInt64OrDefaultValue(16),
                            profilePicture = reader.GetStringOrEmpty(17),
                            roleId = reader.GetInt64OrDefaultValue(18),
                            roleName = reader.GetStringOrEmpty(19),
                            roleDescription = reader.GetStringOrEmpty(20),

















                        };
                    }
                }
                connection.Close();
            }

            return productCatalog;
        }
        public List<PersonInformation> GetPersonInformationByPinNumber(string PinNumber)
        {
            List<PersonInformation> productCatalog = new List<PersonInformation>();
            var query = @"  
                select p.Id,p.Name,p.Surname,p.FatherName,p.PinNumber,p.UserId,p.gender,p.birtday,p.educationLevel_eV_Id
				,p.job_eV_Id,p.address_Id,p.Status,p.LastUpdatedStatus,p.createdUser,p.createdDate,p.updatedUser,p.updatedDate,
				p.profilePicture,r.Id as roleID, r.Name as roleName,r.Description,r.Id from	tblPerson p
				left join dbo.tblUser u on u.Id=p.UserId 
				left join tblUserRole ur on u.Id=ur.UserId 
				 join dbo.tblRole r on ur.RoleId=r.Id 
				--and r.Id in (11,15)
               where p.Status=1 and p.PinNumber=@PinNumber
							 ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PinNumber", PinNumber);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        productCatalog.Add(new PersonInformation()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            Surname = reader.GetStringOrEmpty(2),
                            FatherName = reader.GetStringOrEmpty(3),
                            PinNumber = reader.GetStringOrEmpty(4),
                            UserId = reader.GetInt64OrDefaultValue(5),
                            gender = reader.GetStringOrEmpty(6),
                            birtday = reader.GetInt64OrDefaultValue(7),
                            educationLevel_eV_Id = reader.GetInt64OrDefaultValue(8),
                            job_eV_Id = reader.GetInt64OrDefaultValue(9),
                            address_Id = reader.GetInt64OrDefaultValue(10),
                            Status = reader.GetInt64OrDefaultValue(11),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(12),
                            createdUser = reader.GetStringOrEmpty(13),
                            createdDate = reader.GetInt64OrDefaultValue(14),
                            updatedUser = reader.GetStringOrEmpty(15),

                            updatedDate = reader.GetInt64OrDefaultValue(16),
                            profilePicture = reader.GetStringOrEmpty(17),
                            roleId = reader.GetInt64OrDefaultValue(18),
                            roleName = reader.GetStringOrEmpty(19),
                            roleDescription = reader.GetStringOrEmpty(20),

                        });
                    }
                }
                connection.Close();
            }

            return productCatalog;
        }
        public List<DemandOfferDetail> GetDemandProductionAmountOfEachProduct(Int64 startDate, Int64 endDate)
        {
            var result = new List<DemandOfferDetail>();
            StringBuilder squery = new StringBuilder();
            var query = @" select tb.* from( select table1.Id, pc.ProductName as ParantName , Sum(quantity) as totalQuantity,table1.ProductCatalogParentID,table1.ProductName,table1.unit_price,table1.name as kategoryName ,table1.enumKategorID
from (select pc.Id,pc.ProductCatalogParentID, pc.ProductName,dp.quantity,ev.name ,price.unit_price ,ev.enumCategory_enumCategoryId as enumKategorID
from [dbo].[tblDemand_Production] dp
left  join dbo.tblProductCatalog pc on dp.product_Id=pc.Id and pc.Status=1 
left join [dbo].[tblProductionControl] prc on dp.Id=prc.Demand_Production_Id and prc.Production_type_eV_Id=28 and prc.Status=1
 left join  dbo.tblEnumValue ev on ev.Id=prc.EnumValueId and ev.Status=1
left  join dbo.tblEnumCategory ec on ec.Id=ev.enumCategory_enumCategoryId and ec.Status=1 --and ec.Id=5

left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
where  dp.Status=1 and dp.state_eV_Id=2
                         ";
            var queryDate = @" and dp.Id in (
select  distinct
			 table1.demand_Id
			  from (
			 select 
           
              pc.demand_Id,
			  dbo.dateReturn(pc.demand_Id,pc.year,pc.months_eV_Id,pc.day) as date1
     from [dbo].[tblProductionCalendar]pc
             where  pc.Status=1 
 
			
         ) table1
           
			 where   table1.date1  between @startDate and @endDate)";
            var queryEnd = @") as table1
left join   dbo.tblProductCatalog pc on table1.ProductCatalogParentID=pc.ID and pc.Status=1
  group by table1.Id,table1.ProductCatalogParentID, table1.ProductName , pc.ProductName,
 
 table1.name,table1.ProductName ,table1.unit_price,table1.enumKategorID
 )as tb
 order by tb.ParantName";
            squery.Append(query);
            if (startDate != 0 || endDate != 0)
            {
                squery.Append(queryDate);
            }
            squery.Append(queryEnd);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemandOfferDetail()
                        {
                            productID = reader.GetInt64OrDefaultValue(0),
                            productParentName = reader.GetStringOrEmpty(1),
                            quantity = reader.GetDecimalOrDefaultValue(2),
                            productName = reader.GetStringOrEmpty(4),
                            unitPrice = reader.GetDecimalOrDefaultValue(5),
                            kategoryName = reader.GetStringOrEmpty(6),
                            enumKategoryID = reader.GetInt64OrDefaultValue(7)


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<ProductPriceDetail> GetDemandProductionListNotPrice(Int64 year, Int64 partOfYear)
        {
            var result = new List<ProductPriceDetail>();
            var query = @"select distinct table1.Id, table1.ProductName,table1.canBeOrder,pc.ProductName as ParentName,pc.ProductDescription from ( select pc.Id,pc.ProductCatalogParentID, pc.ProductName,pc.canBeOrder,pc.ProductDescription from tblProductCatalog pc
	 , [dbo].[tblDemand_Production] op
	   where  pc.canBeOrder=1 and pc.Status=1 and pc.Id=op.product_Id and op.Status=1 and op.state_eV_Id=2 and  pc.Id  not in(
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
                            productParentName = reader.GetStringOrEmpty(3),
                            ProductDescription = reader.GetStringOrEmpty(4),





                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        #region Optimastion

        public List<ProductionDetail> GetDemandProductionDetailistForEValueId_OP(GetDemandProductionDetailistForEValueIdSearch ops)
        {

            var result = new List<ProductionDetail>();
            StringBuilder squery = new StringBuilder();
            var query1 = @" WITH RESULTS AS
    (
        SELECT *
            , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn
            , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed
        FROM (select distinct secondTable.*,pc.ProductName as parentName  from 
     ( select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description, 
                        FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id,FirstTable.name,FirstTable.grup_Id,FirstTable.ProductCatalogParentID ,FirstTable.priceProductId
					   from (   
                      select op.Id,price.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId,
                       adr.fullAddress,adr.addressDesc ,op.user_Id ,fo.name,op.grup_Id ,
        pc.ProductCatalogParentID,price.productId as priceProductId
                       from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
            left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
          left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
		
                       where      op.Status=1 
                     and op.state_eV_Id=@state_eV_Id 

                     ";
            var query2 = @")SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";
            squery.Append(query1);
            var queryProduct = @" and product_Id=@product_Id";
            var queryOrgName = @" and name like '%'+@name+'%'";
            var queryDate = @" and op.Id  in  (select  distinct
			 table1.demand_Id
			  from (
			 select 
           
              pc.demand_Id,
			  dbo.dateReturn(pc.demand_Id,pc.year,pc.months_eV_Id,pc.day) as date1
     from [dbo].[tblProductionCalendar]pc
             where  pc.Status=1 
 
			and pc.demand_Id=op.Id 
         ) table1
           
			 where   table1.date1  between @startDate and @endDate)";
            var queryEnd = @"  ) as FirstTable   
                        left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1
      ) secondTable
     
                  left join [dbo].[tblProductCatalog] pc on pc.Id=secondTable.ProductCatalogParentID) as tb
              where tb.EnumCategoryId=5";
            if (ops.endate != 0 || ops.startDate != 0)
            {
                squery.Append(queryDate);
            }
            squery.Append(queryEnd);
            if (ops.prodcutID != 0)
            {
                squery.Append(queryProduct);
            }
            if (!string.IsNullOrEmpty(ops.organizationName))
            {
                squery.Append(queryOrgName);
            }


            squery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@product_Id", ops.prodcutID);
                    command.Parameters.AddWithValue("@name", ops.organizationName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@startDate", ops.startDate);
                    command.Parameters.AddWithValue("@endDate", ops.endate);
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
                            organizationName = reader.GetStringOrEmpty(13),
                            groupId = reader.GetStringOrEmpty(14),
                            productParentId = reader.GetInt64OrDefaultValue(15),
                            priceProductId = reader.GetInt64OrDefaultValue(16),
                            productParentName = reader.GetStringOrEmpty(17),



                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public long GetDemandProductionDetailistForEValueId_OPC(GetDemandProductionDetailistForEValueIdSearch ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query = @"    select COUNT(*) as Count from( select distinct secondTable.*,pc.ProductName as parentName  from 
     ( select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description, 
                        FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id,FirstTable.name,FirstTable.grup_Id,FirstTable.ProductCatalogParentID ,FirstTable.priceProductId from (   
                      select op.Id,price.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId , 
                       adr.fullAddress,adr.addressDesc ,op.user_Id ,fo.name,op.grup_Id ,
        pc.ProductCatalogParentID,price.productId as priceProductId
                       from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
            left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
          left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1

                       where      op.Status=1 
                     and op.state_eV_Id=@state_eV_Id  
                     
";
            squery.Append(query);
            var queryProduct = @" and product_Id=@product_Id";
            var queryOrgName = @" and name like '%'+@name+'%'";
            var queryDate = @" and op.Id  in  (select  distinct
			 table1.demand_Id
			  from (
			 select 
           
              pc.demand_Id,
			  dbo.dateReturn(pc.demand_Id,pc.year,pc.months_eV_Id,pc.day) as date1
     from [dbo].[tblProductionCalendar]pc
             where  pc.Status=1 
 
			and pc.demand_Id=op.Id 
         ) table1
           
			 where   table1.date1  between @startDate and @endDate)";
            var queryEnd = @"  ) as FirstTable   
                        left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1
      ) secondTable
     
                  left join [dbo].[tblProductCatalog] pc on pc.Id=secondTable.ProductCatalogParentID) as tb
where tb.EnumCategoryId=5";
            if (ops.startDate != 0 && ops.endate != 0)
            {
                squery.Append(queryDate);
            }
            squery.Append(queryEnd);
            if (ops.prodcutID != 0)
            {
                squery.Append(queryProduct);
            }
            if (!string.IsNullOrEmpty(ops.organizationName))
            {
                squery.Append(queryOrgName);
            }
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    //  command.Parameters.AddWithValue("@Name", ops.name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@product_Id", ops.prodcutID);
                    command.Parameters.AddWithValue("@name", ops.organizationName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@startDate", ops.startDate);
                    command.Parameters.AddWithValue("@endDate", ops.endate);
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


        public List<ProductionDetail> GetOfferProductionDetailistForEValueId_OP(OfferProductionDetailSearch1 ops)
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

            var queryadminID = @" where Id=@addressID ";
            if (ops.adminID != 0)
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
					  FirstTable.userType_eV_ID,FirstTable.personID,FirstTable.productAddress_Id,FirstTable.state_eV_Id
					   from (   
                     select distinct op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,us.Email,person.Name,person.Surname,
					person.FatherName,person.birtday,person.gender,person.profilePicture,person.address_Id,person.PinNumber
			,adr.Id as adrID,pp.quantity as potentialProductsQuantity,us.userType_eV_ID
			  ,person.Id as personID,op.productAddress_Id,op.state_eV_Id
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
               -- op.state_eV_Id= @state_eV_Id and
        op.Status=1 
                  

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
            var queryState = @" and tb.state_eV_Id=@state_eV_Id";
            var queryProductId = @" and product_Id=@productID  ";
            var queryRoleId = @" and RoleId=@roleID ";
            var queryName = @" and  (Name like '%'+@name+'%' or Surname like '%'+@name+'%'  or FatherName like '%'+@name+'%')  ";
            //var queryVoen = @" and voen like '%'+@voen+'%' ";//voen=@voen
         //   var queryPinNumber = @" and  (PinNumber like '%'+@pinNumber+'%' or voen like '%'+@voen+'%' ) ";
            var queryPinNumber = @" and  (PinNumber like '%'+@pinNumber+'%')";
          var queryVoen = @" and (voen like '%'+@voen+'%')";
            var queryUserType = @" and  userType_eV_ID=@usertypeEvId      ";
            var queryEnd = @"   ) as FirstTable   
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
           

            ) as tb where tb.EnumCategoryId=5";
            var queryDate = @" and op.Id in (select distinct table1.offer_Id
  from (select distinct pc.offer_Id,
           
			   dbo.dateReturn(pc.offer_Id,pc.year,pc.months_eV_Id,pc.day) as date1  from [dbo].[tblProductionCalendar]pc ,[dbo].[tblEnumValue] ev 
           where pc.months_eV_Id=ev.Id and pc.Status=1 
			-- and pc.offer_Id=@offer_Id 
			  ) 
			 table1
           
 where 

  table1.date1 between @startDate and @endDate)
                  ";
            if (ops.startDate != 0 && ops.endDate != 0)
            {
                squery.Append(queryDate);
            }
            squery.Append(queryEnd);
            if (!String.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);
            }
            if (ops.productID != 0)
            {
                squery.Append(queryProductId);
            }
            if (ops.state_eV_Id != 0)
            {
                squery.Append(queryState);
            }
            if (ops.roleID != 0)
            {
                squery.Append(queryRoleId);
            }
            if (ops.usertypeEvId != 0)
            {
                squery.Append(queryUserType);
            }
            if (!String.IsNullOrEmpty(ops.pinNumber) )
            {
                squery.Append(queryPinNumber);
            }
            if ( !String.IsNullOrEmpty(ops.voen))
            {
                 squery.Append(queryVoen);
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
                    command.Parameters.AddWithValue("@pinNumber", ops.pinNumber.GetStringOrEmptyData() );
                    command.Parameters.AddWithValue("@usertypeEvId", ops.usertypeEvId);
                    command.Parameters.AddWithValue("@addressID", ops.adminID);
                    command.Parameters.AddWithValue("@startDate", ops.startDate);
                    command.Parameters.AddWithValue("@endDate", ops.endDate);

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
                            productParentName = reader.GetStringOrEmpty(29),
                            personAdress = reader.GetStringOrEmpty(30),
                            personAdressDesc = reader.GetStringOrEmpty(31),
                            userType = reader.GetStringOrEmpty(32),
                            organizationName = reader.GetStringOrEmpty(33),
                            voen = reader.GetStringOrEmpty(34),










                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public long GetOfferProductionDetailistForEValueId_OPC(OfferProductionDetailSearch1 ops)
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
                op.state_eV_Id= @state_eV_Id and
        op.Status=1 
                     ";

            var queryadminID = @"   where Id= @addressID ";
            squeryID.Append(query1);
            if (ops.adminID != 0)
            {
                squeryID.Append(queryadminID);
            }
            squery.Append(squeryID.ToString());
            squery.Append(query2);
            var queryProductId = @" and product_Id=@productID  ";
            var queryRoleId = @" and RoleId=@roleID ";
            var queryName = @" and  (Name like '%'+@name+'%' or Surname like '%'+@name+'%'  or FatherName like '%'+@name+'%')  ";
            //var queryVoen = @" and voen like '%'+@voen+'%' ";//voen=@voen
            var queryPinNumber = @" and  (PinNumber like '%'+@pinNumber+'%' or voen like '%'+@voen+'%' ) ";
            var queryUserType = @" and  userType_eV_ID=@usertypeEvId      ";
            var queryDate = @" and op.Id in (select distinct table1.offer_Id
  from (select distinct pc.offer_Id,
           
			   dbo.dateReturn(pc.offer_Id,pc.year,pc.months_eV_Id,pc.day) as date1  from [dbo].[tblProductionCalendar]pc ,[dbo].[tblEnumValue] ev 
           where pc.months_eV_Id=ev.Id and pc.Status=1 
			-- and pc.offer_Id=@offer_Id 
			  ) 
			 table1
           
 where 

  table1.date1 between @startDate and @endDate)
                  ";
            var queryEnd = @" ) as FirstTable   
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
           

            ) as tb
  where tb.EnumCategoryId=5";
            if (ops.startDate != 0 || ops.endDate != 0)
            {
                squery.Append(queryDate);
            }
            squery.Append(queryEnd);
            if (!String.IsNullOrEmpty(ops.name))
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


            if (!String.IsNullOrEmpty(ops.pinNumber)||!String.IsNullOrEmpty(ops.voen))
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
                    command.Parameters.AddWithValue("@startDate", ops.startDate);
                    command.Parameters.AddWithValue("@endDate", ops.endDate);
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


        public List<ProductionDetail> GetPotensialProductionDetailistForEValueId_OP(GetDemandProductionDetailistForEValueIdSearch1 ops)
        {
            var result = new List<ProductionDetail>();
            StringBuilder squery = new StringBuilder();
            var query1 = @" WITH RESULTS AS (
  SELECT * , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed FROM 
(
select table2.*,pc.ProductName as parentName,person.Name as personName,person.Surname,person.FatherName from(
select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                   FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                    ev.name as KategoryName,FirstTable.user_Id,FirstTable.ProductCatalogParentID from (   
                   select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                   pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID
                    from [dbo].[tblPotential_Production] op  
                   left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id   and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                    left join [dbo].[tblProductionControl] prc on op.Id=prc.Potential_Production_Id    and prc.Status=1 
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                   left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id    and ec.Status=1
                   where  op.Status=1  
				   and op.state_eV_Id=@state_eV_Id
                   ) as FirstTable   
                    left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id  and ev.Status=1
					)as table2
					 left join [dbo].[tblProductCatalog] pc  on pc.Id=table2.ProductCatalogParentID
				    left join tblPerson person on table2.user_Id=person.UserId and person.Status=1
					 				) as tb  where tb.EnumCategoryId=5 ";
            var query2 = @"
									 )  SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";
            squery.Append(query1);
            if (ops.prodcutID != 0 && String.IsNullOrEmpty(ops.Name))
            {
                squery.Append("  and product_Id=@product_Id");
            }
            if (ops.prodcutID == 0 && !String.IsNullOrEmpty(ops.Name))
            {
                squery.Append("  and (personName like '%'+@person+'%' or Surname like '%'+@person+'%' or FatherName like '%'+@person+'%')");
            }
            if (ops.prodcutID != 0 && !String.IsNullOrEmpty( ops.Name))
            {
                squery.Append(" and product_Id=@product_Id  and (personName like '%'+@person+'%' or Surname like '%'+@person+'%'or FatherName like '%'+@person+'%')");
            }
            squery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@person", ops.Name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@product_Id", ops.prodcutID);

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
                            productParentName = reader.GetStringOrEmpty(14),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public long GetPotensialProductionDetailistForEValueId_OPC(GetDemandProductionDetailistForEValueIdSearch1 ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query = @"  select COUNT(*) as Count from(select table2.*,pc.ProductName as parentName,person.Name as personName,person.Surname,person.FatherName  from(
select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                   FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                    ev.name as KategoryName,FirstTable.user_Id,FirstTable.ProductCatalogParentID from (   
                   select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                   pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID
                    from [dbo].[tblPotential_Production] op  
                   left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1 
                    left join [dbo].[tblProductAddress] pa on op.productAddress_Id=pa.Id   and pa.Status=1
                    left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                    left join [dbo].[tblProductionControl] prc on op.Id=prc.Potential_Production_Id    and prc.Status=1 
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                   left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id    and ec.Status=1
                   where  op.Status=1  
				   and op.state_eV_Id=@state_eV_Id
                   ) as FirstTable   
                    left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id  and ev.Status=1
					)as table2
					 left join [dbo].[tblProductCatalog] pc  on pc.Id=table2.ProductCatalogParentID
                     left join tblPerson person on table2.user_Id=person.UserId and person.Status=1
					 ) as tb
  where tb.EnumCategoryId=5
";
            squery.Append(query);
            if (ops.prodcutID != 0 && String.IsNullOrEmpty( ops.Name))
            {
                squery.Append("  and product_Id=@product_Id");
            }
            if (ops.prodcutID == 0 &&  !String.IsNullOrEmpty(ops.Name))
            {
                squery.Append("  and (personName like '%'+@person+'%' or Surname like '%'+@person+'%' or FatherName like '%'+@person+'%')");
            }
            if (ops.prodcutID != 0 &&  !String.IsNullOrEmpty(ops.Name))
            {
                squery.Append(" and product_Id=@product_Id  and (personName like '%'+@person+'%' or Surname like '%'+@person+'%'or FatherName like '%'+@person+'%')");
            }

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);

                    command.Parameters.AddWithValue("@person", ops.Name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@product_Id", ops.prodcutID);
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


        public List<ProductionDetail> GetDemandProductionDetailistForUser_OP(DemandProductionDetailistForUser ops)
        {
            var result = new List<ProductionDetail>();
            StringBuilder squery = new StringBuilder();
            var query = @"   WITH RESULTS AS ( SELECT * , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed FROM ( select  secondTable.*,pc.ProductName as ProductParentName from (select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description, 
                       FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                       ev.name as KategoryName,FirstTable.user_Id,FirstTable.ProductCatalogParentID,FirstTable.fullForeignOrganization,FirstTable.forgId,FirstTable.RoleId,FirstTable.userType_eV_ID from (   
                      select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                       adr.fullAddress,adr.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,adr.fullForeignOrganization,adr.forgId,ur.RoleId,us.userType_eV_ID
                      from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id    and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id   and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id  and prc.Status=1   
                        left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id  and ev.Status=1
                      left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1   
                      left join tblUserRole ur on us.Id=ur.UserId and ur.Status=1
                         where 
						op.user_Id=@userID and 
						  ev.Id!=13 and op.isSelected=1
and op.Status=1
 
                      ) as FirstTable   
                       left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1 
					  
					   ) secondTable

					      left join [dbo].[tblProductCatalog] pc on pc.Id=secondTable.ProductCatalogParentID
                     )  as tb  where tb.EnumCategoryId=5
 
                         ";
            string query1 = @")  SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";
            squery.Append(query);
            //if (ops.productName != null && ops.roleID==0 && ops.userType_eV_ID==0)
            //{
            //    squery.Append(" and (ProductName Like '%' + @productName + '%' or ProductParentName Like '%' + @productName + '%') ");
            //}
            //else  if (ops.roleID != 0 && ops.productName == null  && ops.userType_eV_ID == 0)
            //{
            //    squery.Append(" and RoleId=@RoleId");
            //}
            //else  if (ops.userType_eV_ID != 0 && ops.roleID == 0 && ops.productName == null)
            //{
            //    squery.Append(" and userType_eV_ID=@userType_eV_ID");
            //}
            //else   if (ops.roleID != 0 && ops.productName == null && ops.userType_eV_ID != 0)
            //{
            //    squery.Append(" and RoleId=@RoleId and userType_eV_ID=@userType_eV_ID");
            //}
            //else  if (ops.roleID != 0 && ops.productName != null && ops.userType_eV_ID == 0)
            //{
            //    squery.Append(" and RoleId=@RoleId  and (ProductName Like '%' + @productName + '%' or ProductParentName Like '%' + @productName + '%') ");
            //}
            //else  if (ops.roleID != 0 && ops.productName != null && ops.userType_eV_ID != 0)
            //{
            //    squery.Append(" and RoleId=@RoleId and userType_eV_ID=@userType_eV_ID  and (ProductName Like '%' + @productName + '%' or ProductParentName Like '%' + @productName + '%') ");
            //}
            //else  if (ops.roleID == 0 && ops.productName != null && ops.userType_eV_ID != 0)
            //{
            //    squery.Append(" and userType_eV_ID=@userType_eV_ID  and (ProductName Like '%' + @productName + '%' or ProductParentName Like '%' + @productName + '%') ");
            //}
            if (ops.productID != 0)
            {
                squery.Append(" and product_Id=@product_Id");
            }
            squery.Append(query1);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@userID", ops.userID);
                    command.Parameters.AddWithValue("@product_Id", ops.productID);
                    command.Parameters.AddWithValue("@page_num", ops.page_num);
                    command.Parameters.AddWithValue("@page_size", ops.page_size);

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
                            fullForeignOrganization = reader.GetStringOrEmpty(14),
                            forgId = reader.GetInt64OrDefaultValue(15),

                            productParentName = reader.GetStringOrEmpty(18),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public long GetDemandProductionDetailistForUser_OPC(DemandProductionDetailistForUser ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query = @"  select COUNT(*) as Count from( select  secondTable.*,pc.ProductName as ProductParentName from (select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description, 
                       FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                       ev.name as KategoryName,FirstTable.user_Id,FirstTable.ProductCatalogParentID,FirstTable.fullForeignOrganization,FirstTable.forgId,FirstTable.RoleId,FirstTable.userType_eV_ID  from (   
                      select op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                       adr.fullAddress,adr.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,adr.fullForeignOrganization,adr.forgId,ur.RoleId,us.userType_eV_ID
                      from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id    and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id   and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id  and prc.Status=1   
                        left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id  and ev.Status=1
                      left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1  
  left join tblUserRole ur on us.Id=ur.UserId and ur.Status=1 
                         where 
						op.user_Id=@userID and 
						  ev.Id!=13 and op.isSelected=1 
and op.Status=1
                      ) as FirstTable   
                       left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1 
					  
					   ) secondTable

					      left join [dbo].[tblProductCatalog] pc on pc.Id=secondTable.ProductCatalogParentID
 ) as tb
  where tb.EnumCategoryId=5
                         ";
            squery.Append(query);

            if (ops.productID != 0)
            {
                squery.Append(" and product_Id=@product_Id");
            }
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@userID", ops.userID);
                    command.Parameters.AddWithValue("@product_Id", ops.productID);



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

        public List<UserInfo> GetPotensialUserList_OP(PotensialUserForAdminUnitIdList ops)
        {
            var result = new List<UserInfo>();
            StringBuilder squery = new StringBuilder();
            StringBuilder squeryunion = new StringBuilder();
            var queryAdminID = @"  with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au";
            squeryunion.Append(queryAdminID);

            var queryadminID = @" WHERE  Id=@adminUnitID ";
            if (ops.adminUnitID != 0)
            {
                squeryunion.Append(queryadminID);
            }
            var query = @" 
   UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
  --select ID from cte;
  , RESULTS AS (
 SELECT * , ROW_NUMBER() OVER (ORDER BY tb.Name,tb.Surname,tb.adminUnitName DESC) AS rn
, ROW_NUMBER() OVER (ORDER BY tb.Name,tb.Surname,tb.adminUnitName ASC) AS rn_reversed FROM 
(select distinct tb.* --,op.state_eV_Id
from ( select p.Name,p.Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name as roleName,p.PinNumber ,'' as voen,au.Name as adminUnitName
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 tblPRM_AdminUnit au
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id in (11,15) and u.userType_eV_ID=26
							 and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 and au.Id=adr.adminUnit_Id and au.Status=1  and  au.Id 
			                  in (
							select Id from cte
							 		)
							 union 
							 select fo.Name,fo.description as Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name,'' as PinNumber , fo.voen,au.Name as adminUnitName
                             from dbo.tblUser u ,[dbo].[tblForeign_Organization] fo,
							 tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,tblPRM_AdminUnit au
                             where u.Id=fo.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                               and r.Id in (11,15) and u.userType_eV_ID=50
							 and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 and au.Id=adr.adminUnit_Id and au.Status=1  and  au.Id 
			   in (
							select Id from cte
							 		)
							 )as tb
							left join tblOffer_Production op on op.user_Id=tb.userId and op.Status=1
                             and ((tb.RoleId  = 11 AND op.state_eV_Id=2) OR (tb.RoleId=15 ))
							 
							 ) as tb where (tb.PinNumber!='' or tb.voen!='') 
 
                         ";
            var query1 = @")  SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";
            var queryRole = @" and RoleId=@RoleId";
            var queryName = @"  and Name like '%'+@name+'%' or Surname like '%'+@name+'%' or userType like '%'+@name+'%' ";
            var queryadminName = @" and adminUnitName=@adminUnitName";

            squery.Append(squeryunion.ToString());
            squery.Append(query);


            if (ops.roleID != 0)
            {
                squery.Append(queryRole);
            }

            if (!String.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);
            }
            if (!String.IsNullOrEmpty(ops.adminUnitName))
            {
                squery.Append(queryadminName);
            }

            squery.Append(query1);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@name", ops.name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@adminUnitName", ops.adminUnitName.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@adminUnitID", ops.adminUnitID);



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
                            adminUnitName = reader.GetStringOrEmpty(13),
                            //state_Ev_ID = reader.GetInt64OrDefaultValue(14),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public Int64 GetPotensialUserList_OPC1(PotensialUserForAdminUnitIdList ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query = @" select COUNT(*) as Count from(select distinct tb.* ,op.state_eV_Id from ( select p.Name,p.Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name as roleName,p.PinNumber ,'' as voen,u.Status
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id in (11,15) and u.userType_eV_ID=26
							 and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1
							 union 
							 select fo.Name,fo.description as Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name,'' as PinNumber , fo.voen,u.Status
                             from dbo.tblUser u ,[dbo].[tblForeign_Organization] fo,
							 tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r
                             where u.Id=fo.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                               and r.Id in (11,15)  and u.userType_eV_ID=50
							 and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1  
							 )as tb
							left join tblOffer_Production op on op.user_Id=tb.userId and op.Status=1
                           and ((tb.RoleId  = 11 AND op.state_eV_Id=2) OR (tb.RoleId=15))
							 ) as tb  where Status=1
 
 
                         ";
            squery.Append(query);
            var queryRoleId = @" and RoleId=@RoleId";
            var queryName = @"  and Name like '%'+@name+'%' or Surname like '%'+@name+'%' or userType like '%'+@name+'%' ";
            // var queryState = @" and RoleId=11 and state_eV_Id=2";

            if (ops.roleID != 0)
            {
                squery.Append(queryRoleId);
            }



            if (!String.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);
            }

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@name", ops.name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
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
        public Int64 GetPotensialUserList_OPC(PotensialUserForAdminUnitIdList ops)
        {

            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            StringBuilder squeryunion = new StringBuilder();
            var queryAdminID = @"  with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblPRM_AdminUnit au";
            squeryunion.Append(queryAdminID);

            var queryadminID = @" WHERE  Id=@adminUnitID ";
            if (ops.adminUnitID != 0)
            {
                squeryunion.Append(queryadminID);
            }
            var query = @" 
   UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
  SELECT count(*) as count from 
(select distinct tb.* --,op.state_eV_Id 
from ( select p.Name,p.Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name as roleName,p.PinNumber ,'' as voen,au.Name as adminUnitName
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 tblPRM_AdminUnit au
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id in (11,15) and u.userType_eV_ID=26
							 and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 and au.Id=adr.adminUnit_Id and au.Status=1 
							 and  au.Id 
			                  in (
							select Id from cte
							 		)
							 union 
							 select fo.Name,fo.description as Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name,'' as PinNumber , fo.voen,au.Name as adminUnitName
                             from dbo.tblUser u ,[dbo].[tblForeign_Organization] fo,
							 tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,tblPRM_AdminUnit au
                             where u.Id=fo.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                               and r.Id in (11,15) and u.userType_eV_ID=50
							 and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 and au.Id=adr.adminUnit_Id and au.Status=1  
							 and  au.Id 
			                  in (
							select Id from cte
							 		)
							 )as tb
							left join tblOffer_Production op on op.user_Id=tb.userId and op.Status=1
                             and ((tb.RoleId  = 11 AND op.state_eV_Id=2) OR (tb.RoleId=15)) 
							 
							 ) as tb where (tb.PinNumber!='' or tb.voen!='') 
                         ";

            var queryRole = @" and RoleId=@RoleId";
            var queryName = @"  and Name like '%'+@name+'%' or Surname like '%'+@name+'%' or userType like '%'+@name+'%' ";
            var queryadminName = @" and adminUnitName=@adminUnitName";

            squery.Append(squeryunion.ToString());
            squery.Append(query);


            if (ops.roleID != 0)
            {
                squery.Append(queryRole);
            }

            if (!String.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryName);
            }
            if (!String.IsNullOrEmpty(ops.adminUnitName))
            {
                squery.Append(queryadminName);
            }


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);

                    command.Parameters.AddWithValue("@name", ops.name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@adminUnitName", ops.adminUnitName.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@adminUnitID", ops.adminUnitID);



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
        public List<UserInfo> GetPotensialUserForAdminUnitIdList_OP(PotensialUserForAdminUnitIdList ops)
        {
            var result = new List<UserInfo>();
            StringBuilder squery = new StringBuilder();
            var query1 = @"WITH RESULTS AS (
 SELECT * , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed FROM 
(
 select distinct FirstTable.* ,aunit.Name as ParantName,op.state_eV_Id from (
 select au.Id, p.Name as PersonName,p.Surname,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.RoleId,r.Name as RoleName,adr.adminUnit_Id,au.Name as AdminUnitName,au.ParentID,p.PinNumber ,'' as voen--,pc.ProductName
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 dbo.tblPRM_AdminUnit au
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id in (11,15)
							 and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 
							 and adr.adminUnit_Id=au.Id --and u.userType_eV_ID=26
						and adr.adminUnit_Id=@adminUnit_Id
							 and au.Status=1
                           
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 
							left join tblOffer_Production op on op.user_Id=FirstTable.userId and op.Status=1
                            and ((FirstTable.RoleId  = 11 AND op.state_eV_Id=2) OR (FirstTable.RoleId=15))
							 union 

							 select distinct FirstTable.* ,aunit.Name as ParantName,op.state_eV_Id from (
 select au.Id, fo.Name as PersonName,fo.description as Surname ,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.RoleId,r.Name as RoleName,adr.adminUnit_Id,au.Name as AdminUnitName,au.ParentID,'' as PinNumber , fo.voen --,pc.ProductName
                             from dbo.tblUser u ,[dbo].[tblForeign_Organization] fo,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 dbo.tblPRM_AdminUnit au
                             where u.Id=fo.userId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                              and r.Id in (11,15)
							 and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 
							 --and u.userType_eV_ID=50
							 and adr.adminUnit_Id=au.Id
						   and adr.adminUnit_Id=@adminUnit_Id
							 and au.Status=1
                          
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 
							left join tblOffer_Production op on op.user_Id=FirstTable.userId and op.Status=1
                           and ((FirstTable.RoleId  = 11 AND op.state_eV_Id=2) OR (FirstTable.RoleId=15))
							
						
 ) as tb 	where (PinNumber!=' ' or voen!=' ')  ";

            var query2 = @"  )  SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";
            squery.Append(query1);
            var queryname = @" and (PersonName Like  '%' + @name + '%' or Surname Like  '%' + @name + '%') ";
            var queryAdminName = @" and AdminUnitName Like '%' + @AdminUnitName + '%'";
            //  var queryState = @" and RoleId=11 and state_eV_Id=2";
            var queryRoleId = @" and RoleId=@RoleId";

            if (ops.roleID != 0)
            {
                squery.Append(queryRoleId);
            }


            if (!String.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryname);
            }
            if (!String.IsNullOrEmpty(ops.adminUnitName))
            {
                squery.Append(queryAdminName);
            }
            //Like '%' + @product + '%'
            squery.Append(query2);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@adminUnit_Id", ops.adminUnitID);
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@name", ops.name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AdminUnitName", ops.adminUnitName ?? (object)DBNull.Value);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new UserInfo()
                        {
                            ID = reader.GetInt64OrDefaultValue(0),
                            name = reader.GetStringOrEmpty(1),
                            surname = reader.GetStringOrEmpty(2),
                            fullAddress = reader.GetStringOrEmpty(3),
                            email = reader.GetStringOrEmpty(4),
                            userID = reader.GetInt64OrDefaultValue(5),
                            userTypeID = reader.GetInt64OrDefaultValue(6),
                            userType = reader.GetStringOrEmpty(7),
                            userRoleID = reader.GetInt64OrDefaultValue(8),
                            roleName = reader.GetStringOrEmpty(9),
                            adminUnitID = reader.GetInt64OrDefaultValue(10),
                            adminUnitName = reader.GetStringOrEmpty(11),
                            parentAdminUnitID = reader.GetInt64OrDefaultValue(12),
                            pinNumber = reader.GetStringOrEmpty(13),
                            voen = reader.GetStringOrEmpty(14),
                            parentAdminUnitName = reader.GetStringOrEmpty(15),
                            state_Ev_ID = reader.GetInt64OrDefaultValue(16),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public Int64 GetPotensialUserForAdminUnitIdList_OPC(PotensialUserForAdminUnitIdList ops)
        {
            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query = @" select COUNT(*) as Count from(
 select distinct FirstTable.* ,aunit.Name as ParantName,op.state_eV_Id from (
 select au.Id, p.Name as PersonName,p.Surname,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.RoleId,r.Name as RoleName,adr.adminUnit_Id,au.Name as AdminUnitName,au.ParentID,p.PinNumber ,'' as voen--,pc.ProductName
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 dbo.tblPRM_AdminUnit au
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                             and r.Id in (11,15)
							 and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 
							 and adr.adminUnit_Id=au.Id --and u.userType_eV_ID=26
						and adr.adminUnit_Id=@adminUnit_Id
							 and au.Status=1
                           
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 
							  left join dbo.tblOffer_Production op on op.user_Id=FirstTable.userId and op.Status=1
 and ((FirstTable.RoleId  = 11 AND op.state_eV_Id=2) OR (FirstTable.RoleId=15))
							 union 

							 select distinct FirstTable.* ,aunit.Name as ParantName,op.state_eV_Id from (
 select au.Id, fo.Name as PersonName,fo.description as Surname ,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.RoleId,r.Name as RoleName,adr.adminUnit_Id,au.Name as AdminUnitName,au.ParentID,'' as PinNumber , fo.voen --,pc.ProductName
                             from dbo.tblUser u ,[dbo].[tblForeign_Organization] fo,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 dbo.tblPRM_AdminUnit au
                             where u.Id=fo.userId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
                              and r.Id in (11,15)
							 and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 
							 --and u.userType_eV_ID=50
							 and adr.adminUnit_Id=au.Id
						  and adr.adminUnit_Id=@adminUnit_Id
							 and au.Status=1
                          
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 
							  left join dbo.tblOffer_Production op on op.user_Id=FirstTable.userId and op.Status=1
 and ((FirstTable.RoleId  = 11 AND op.state_eV_Id=2) OR (FirstTable.RoleId=15))
							
						
 ) as tb 	where (PinNumber!=' ' or voen!=' ') ";

            var queryname = @" and PersonName Like  '%' + @name + '%' or Surname Like  '%' + @name + '%' ";
            var queryAdminName = @" and AdminUnitName Like '%' + @AdminUnitName + '%'";

            var queryRoleId = @" and RoleId=@RoleId";
            squery.Append(query);
            if (ops.roleID != 0)
            {
                squery.Append(queryRoleId);
            }



            if (!String.IsNullOrEmpty(ops.name))
            {
                squery.Append(queryname);
            }
            if (!String.IsNullOrEmpty(ops.adminUnitName))
            {
                squery.Append(queryAdminName);
            }
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@adminUnit_Id", ops.adminUnitID);
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);

                    command.Parameters.AddWithValue("@name", ops.name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AdminUnitName", ops.adminUnitName ?? (object)DBNull.Value);
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

        public List<ProductionDetail> GetDemandProductDetailInfoForAccounting_OP(GetDemandProductionDetailistForEValueIdSearch ops, Int64 year, Int64 partOfYear)
        {
            var result = new List<ProductionDetail>();
            StringBuilder squery = new StringBuilder();
            var query = @" WITH RESULTS AS ( 
SELECT * , ROW_NUMBER() OVER (ORDER BY tb.name DESC) AS rn , ROW_NUMBER() OVER (ORDER BY tb.name ASC) AS rn_reversed FROM 
( select table1.* ,pc.ProductName as productParentName,ev.name as kategoryName ,quantity*unit_price as totalPrice  from(    select fo.name, op.Id ,op.product_Id, pc.ProductName,pc.ProductCatalogParentID,
	price.unit_price,
	
	padr.fullAddress ,
	op.quantity, 
	ev.createdDate,ev.updatedDate,ev.LastUpdatedStatus,prc.EnumValueId,padr.addressDesc
	from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
					   left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
					   left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
							
					  left join [dbo].[tblProductAddress] padr on padr.Id=op.address_Id and padr.Status=1
                       where      op.Status=1 --and op.isSelected=1
                   and op.state_eV_Id=@state_eV_Id  
                  and price.year= @year and price.partOfYear=@partOfYear
					  
  ";
            var query1 = @" )  SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC";
            var queryDate = @"  and op.Id in (
select  distinct
			 table1.demand_Id
			  from (
			 select 
           
              pc.demand_Id,
			  dbo.dateReturn(pc.demand_Id,pc.year,pc.months_eV_Id,pc.day) as date1
     from [dbo].[tblProductionCalendar]pc
             where  pc.Status=1 
 
			
         ) table1
           
			 where   table1.date1  between @startDate and @endDate
			 )";
            var queryEnd = @" )as table1
					   left join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1
					     left join [dbo].[tblEnumValue] ev on table1.EnumValueId=ev.Id and ev.Status=1
	where 	 enumCategory_enumCategoryId=5 
	) as tb ";
            squery.Append(query);
            if (ops.startDate != 0 && ops.endate != 0)
            {
                squery.Append(queryDate);
            }
            squery.Append(queryEnd);
            if (ops.prodcutID != 0)
            {
                squery.Append(" where product_Id=@productID");
            }
            squery.Append(query1);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@productID", ops.prodcutID);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@partOfYear", partOfYear);
                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@page_size", ops.pageSize);
                    command.Parameters.AddWithValue("@startDate", ops.startDate);//endDate
                    command.Parameters.AddWithValue("@endDate", ops.endate);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionDetail()
                        {
                            name = reader.GetStringOrEmpty(0),
                            productionID = reader.GetInt64OrDefaultValue(1),
                            productId = reader.GetInt64OrDefaultValue(2),
                            productName = reader.GetStringOrEmpty(3),

                            productParentId = reader.GetInt64OrDefaultValue(4),
                            unitPrice = reader.GetDecimalOrDefaultValue(5),
                            fullAddress = reader.GetStringOrEmpty(6),
                            quantity = reader.GetDecimalOrDefaultValue(7),

                            createdDate = reader.GetInt64OrDefaultValue(8),
                            updatedDate = reader.GetInt64OrDefaultValue(9),
                            lastUpdateStatus = reader.GetInt64OrDefaultValue(10),
                            addressDesc = reader.GetStringOrEmpty(12),
                            productParentName = reader.GetStringOrEmpty(13),
                            kategoryName = reader.GetStringOrEmpty(14),
                            totalPrice = reader.GetDecimalOrDefaultValue(15),




                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public Int64 GetDemandProductsForAccounting_OPC(DemandProductsForAccountingSearch ops)
        {
            Int64 count = 0;
            var result = new List<ProductionDetail>();
            var query = @"
select COUNT(*) as Count from(select table1.* ,ev.name as kategoryName,  totatq * unit_price  as totalPrice,pc.ProductName as productParentName 
from( 
select fo.name, pc.ProductName,Sum (cal.quantity) as totatq,price.unit_price ,prc.EnumValueId
,padr.fullAddress,op.state_eV_Id,pc.ProductCatalogParentID,padr.addressDesc
 from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                     left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
					   	   left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
						    left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
							left join [dbo].[tblProductionCalendar] cal on cal.Production_Id=op.Id and cal.Status=1
							left join [dbo].[tblProductAddress] padr on op.address_Id=padr.Id and padr.Status=1
							 where      op.Status=1 and  op.state_eV_Id=2 
							 	group by 
				fo.name, pc.ProductName,price.unit_price ,prc.EnumValueId
,padr.fullAddress,op.state_eV_Id,pc.ProductCatalogParentID,padr.addressDesc	
                
							)as table1
							  left join [dbo].[tblEnumValue] ev on table1.EnumValueId=ev.Id and ev.Status=1
							  left join [dbo].[tblProductCatalog] pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
							   where enumCategory_enumCategoryId=5
							   )as tb
";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);

                    command.Parameters.AddWithValue("@address", ops.adressName);
                    command.Parameters.AddWithValue("@product", ops.productName);
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
        public List<ProductionDetail> GetDemandProductsForAccounting_OP(DemandProductsForAccountingSearch ops)
        {
            StringBuilder sQuery = new StringBuilder();
            var result = new List<ProductionDetail>();
            var query1 = @" WITH RESULTS AS ( SELECT * , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed FROM 
(select table1.* ,ev.name as kategoryName,  totatq * unit_price  as totalPrice,pc.ProductName as productParentName ,ev.enumCategory_enumCategoryId
from( 
select op.Id, fo.name, pc.ProductName,Sum (cal.quantity) as totatq,price.unit_price ,prc.EnumValueId
,padr.fullAddress,op.state_eV_Id,pc.ProductCatalogParentID,padr.addressDesc,ur.RoleId,us.userType_eV_ID
 from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                     left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
					   	   left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
						    left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
							left join [dbo].[tblProductionCalendar] cal on cal.Production_Id=op.Id and cal.Status=1
							left join [dbo].[tblProductAddress] padr on op.address_Id=padr.Id and padr.Status=1
	                          left join tblUserRole ur on us.Id=ur.UserId and ur.Status=1
							 where      op.Status=1 and  op.state_eV_Id=@state_eV_Id 
							 	group by 
				fo.name, pc.ProductName,price.unit_price ,prc.EnumValueId
,padr.fullAddress,op.state_eV_Id,pc.ProductCatalogParentID,padr.addressDesc	,op.Id,ur.RoleId,us.userType_eV_ID
                
							)as table1
							  left join [dbo].[tblEnumValue] ev on table1.EnumValueId=ev.Id and ev.Status=1
							  left join [dbo].[tblProductCatalog] pc on table1.ProductCatalogParentID=pc.Id and pc.Status=1
							  
							   ) as tb
							    where enumCategory_enumCategoryId=5
";
            string query2 = @" )  SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC";
            sQuery.Append(query1);
            if (ops.productName != null && ops.adressName == null)
            {
                sQuery.Append(" and ProductName Like '%' + @product + '%' or productParentName Like '%' + @product + '%'");
            }
            if (ops.adressName != null && ops.productName == null)
            {
                sQuery.Append(" and fullAddress Like '%' + @address + '%' or addressDesc Like '%' + @address + '%'");
            }
            if (ops.adressName != null && ops.productName != null)
            {
                sQuery.Append(" and (fullAddress Like '%' + @address + '%' or addressDesc Like '%' + @address + '%') and (  ProductName Like '%' + @product + '%' or productParentName Like '%' + @product + '%')");
            }

            sQuery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(sQuery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@page_num", ops.page_num);
                    command.Parameters.AddWithValue("@page_size", ops.page_size);
                    command.Parameters.AddWithValue("@address", ops.adressName);
                    command.Parameters.AddWithValue("@product", ops.productName);


                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionDetail()
                        {

                            productionID = reader.GetInt64OrDefaultValue(0),
                            name = reader.GetStringOrEmpty(1),
                            productName = reader.GetStringOrEmpty(2),
                            quantity = reader.GetDecimalOrDefaultValue(3),
                            unitPrice = reader.GetDecimalOrDefaultValue(4),
                            fullAddress = reader.GetStringOrEmpty(6),
                            addressDesc = reader.GetStringOrEmpty(9),
                            kategoryName = reader.GetStringOrEmpty(12),
                            totalPrice = reader.GetDecimalOrDefaultValue(13),
                            productParentName = reader.GetStringOrEmpty(14)






                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public float GetDemandProductDetailInfoForAccounting_OPP(GetDemandProductionDetailistForEValueIdSearch ops, Int64 year, Int64 partOfYear)
        {
            float totalPrice = 0;
            var result = new List<ProductionDetail>();
            StringBuilder squery = new StringBuilder();
            var query = @"   select SUM( tb.unit_price) as totalPrice ,productId
 from(select table1.* ,pc.ProductName as productParentName,ev.name as kategoryName ,quantity*unit_price as totalPrice,pc.id as productId from(    select fo.name, op.Id as ProductionId, pc.ProductName, pc.Id,pc.ProductCatalogParentID,
	price.unit_price,
	
	padr.fullAddress ,
	op.quantity, 
	ev.createdDate,ev.updatedDate,ev.LastUpdatedStatus,prc.EnumValueId,padr.addressDesc
	from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
					   left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
					   left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
							
					  left join [dbo].[tblProductAddress] padr on padr.Id=op.address_Id and padr.Status=1
                       where      op.Status=1 --and op.isSelected=1
                 and op.state_eV_Id=@state_eV_Id
                 and price.year= @year and price.partOfYear=@partOfYear
					 
";
            var query1 = @" group by tb.productId  ";
            var queryDate = @"  and op.Id in (
select  distinct
			 table1.demand_Id
			  from (
			 select 
           
              pc.demand_Id,
			  dbo.dateReturn(pc.demand_Id,pc.year,pc.months_eV_Id,pc.day) as date1
     from [dbo].[tblProductionCalendar]pc
             where  pc.Status=1 
 
			
         ) table1
           
			 where   table1.date1  between @startDate and @endDate
			 )";
            var queryEnd = @"  )as table1
					   left join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1
					     left join [dbo].[tblEnumValue] ev on table1.EnumValueId=ev.Id and ev.Status=1
	where 	 enumCategory_enumCategoryId=5 
	)as tb ";
            squery.Append(query);
            if (ops.startDate != 0 || ops.endate != 0)
            {
                squery.Append(queryDate);
            }
            squery.Append(queryEnd);
            if (ops.prodcutID != 0)
            {
                squery.Append(" where  productId=@productId ");
            }
            squery.Append(query1);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@productId", ops.prodcutID);
                    command.Parameters.AddWithValue("@partOfYear", partOfYear);
                    command.Parameters.AddWithValue("@startDate ", ops.startDate);
                    command.Parameters.AddWithValue("@endDate", ops.endate);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        totalPrice = Convert.ToInt32(reader["totalPrice"]);

                    }
                }
                connection.Close();
            }

            return totalPrice;
        }
        public Int64 GetDemandProductDetailInfoForAccounting_OPC(GetDemandProductionDetailistForEValueIdSearch ops, Int64 year, Int64 partOfYear)
        {
            Int64 count = 0;
            var result = new List<ProductionDetail>();
            StringBuilder squery = new StringBuilder();
            var query = @"  select COUNT(*) as Count from(  select table1.* ,pc.ProductName as productParentName,ev.name as kategoryName ,quantity*unit_price as totalPrice  from(    select fo.name, op.Id ,op.product_Id, pc.ProductName,pc.ProductCatalogParentID,
	price.unit_price,
	
	padr.fullAddress ,
	op.quantity, 
	ev.createdDate,ev.updatedDate,ev.LastUpdatedStatus,prc.EnumValueId,padr.addressDesc
	from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
					   left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
					   left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
							
					  left join [dbo].[tblProductAddress] padr on padr.Id=op.address_Id and padr.Status=1
                       where      op.Status=1 --and op.isSelected=1
                   and op.state_eV_Id=@state_eV_Id
                  and price.year= @year and price.partOfYear=@partOfYear
					   ";
            var queryDate = @"  and op.Id in (
select  distinct
			 table1.demand_Id
			  from (
			 select 
           
              pc.demand_Id,
			  dbo.dateReturn(pc.demand_Id,pc.year,pc.months_eV_Id,pc.day) as date1
     from [dbo].[tblProductionCalendar]pc
             where  pc.Status=1 
 
			
         ) table1
           
			 where   table1.date1  between @startDate and @endDate
			 )";
            var queryEnd = @" )as table1
					   left join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1
					     left join [dbo].[tblEnumValue] ev on table1.EnumValueId=ev.Id and ev.Status=1
	where 	 enumCategory_enumCategoryId=5 
	) as tb";
            squery.Append(query);
            if (ops.startDate != 0 && ops.endate != 0)
            {
                squery.Append(queryDate);
            }
            squery.Append(queryEnd);
            if (ops.prodcutID != 0)
            {
                squery.Append(" where product_Id=@productID");
            }

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {

                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@productID", ops.prodcutID);

                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@partOfYear", partOfYear);
                    command.Parameters.AddWithValue("@startDate", ops.startDate);
                    command.Parameters.AddWithValue("@endDate", ops.endate);
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
        public List<DemandDetialOPA> GetDemandProductionDetailistForEValueId_OPA(Int64 state_eV_Id, int page, int pageSize)
        {
            var result = new List<DemandDetialOPA>();
            var query = @" 
 ; WITH RESULTS AS
    (
        SELECT *
            , ROW_NUMBER() OVER (ORDER BY tb.name DESC) AS rn
            , ROW_NUMBER() OVER (ORDER BY tb.name ASC) AS rn_reversed
        FROM 
     ( 
	 select FirstTable.Id,pc.ProductName as parentName, FirstTable.unit_price,FirstTable.quantity, 
                        FirstTable.ProductName,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.name,FirstTable.ProductCatalogParentID  from (   
                      select op.Id,price.unit_price,op.quantity,pc.ProductName,prc.EnumCategoryId, prc.EnumValueId , 
                       adr.fullAddress,adr.addressDesc  ,fo.name,
        pc.ProductCatalogParentID
                       from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                     
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
            left join [dbo].[tblForeign_Organization] fo on op.user_Id=fo.userId and fo.Status=1
          left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1

                       where      op.Status=1 
                     and op.state_eV_Id=@state_eV_Id
                      ) as FirstTable   
                        left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1
						 left join [dbo].[tblProductCatalog] pc on pc.Id=FirstTable.ProductCatalogParentID and pc.Status=1
     
               ) as tb
              where tb.EnumCategoryId=5
    )
    SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC 

";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", state_eV_Id);
                    command.Parameters.AddWithValue("@page_num", page);
                    command.Parameters.AddWithValue("@page_size", pageSize);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemandDetialOPA()
                        {
                            productionID = reader.GetInt64OrDefaultValue(0),
                            productParentName = reader.GetStringOrEmpty(1),
                            unitPrice = reader.GetDecimalOrDefaultValue(2),
                            quantity = reader.GetDecimalOrDefaultValue(3),
                            productName = reader.GetStringOrEmpty(4),


                            fullAddress = reader.GetStringOrEmpty(5),
                            addressDesc = reader.GetStringOrEmpty(6),
                            enumValueName = reader.GetStringOrEmpty(9),
                            organizationName = reader.GetStringOrEmpty(10),
                            enumValueDescription = reader.GetStringOrEmpty(9),


                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<ProductionDetail> GetDemandProductDetailInfoForAccounting_Search(Int64 stateId, Int64 year, Int64 partOfYear, string product_Id)
        {
            var result = new List<ProductionDetail>();
            var query = @" select table1.* ,pc.ProductName as productParentName,ev.name as kategoryName ,quantity*unit_price as totalPrice from(    select fo.name, op.Id as ProductionId, pc.ProductName, pc.Id,pc.ProductCatalogParentID,
	price.unit_price,
	
	padr.fullAddress ,
	op.quantity, 
	ev.createdDate,ev.updatedDate,ev.LastUpdatedStatus,prc.EnumValueId,padr.addressDesc
	from [dbo].[tblDemand_Production] op  
                       left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id and pc.Status=1
                       left join [dbo].[tblProductAddress] adr on op.address_Id=adr.Id  and adr.Status=1
                       left join [dbo].[tblUser] us on op.[user_Id]=us.Id  and us.Status=1
                       left join [dbo].[tblProductionControl] prc on op.Id=prc.Demand_Production_Id and prc.Status=1   
                       left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id   and ev.Status=1
                       left join [dbo].[tblEnumCategory] ec on ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1
					   left join [dbo].[tblForeign_Organization] fo on us.Id=fo.userId and fo.Status=1
					   left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
							
					  left join [dbo].[tblProductAddress] padr on padr.Id=op.address_Id and padr.Status=1
                       where      op.Status=1 --and op.isSelected=1
                   and op.state_eV_Id=@state_eV_Id  
                  and price.year= @year and price.partOfYear=@partOfYear
and  product_Id Like '%' + @product_Id + '%'
					  )as table1
					   left join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1
					     left join [dbo].[tblEnumValue] ev on table1.EnumValueId=ev.Id and ev.Status=1
	where 	 enumCategory_enumCategoryId=5
";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", stateId);
                    command.Parameters.AddWithValue("@product_Id", product_Id);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@partOfYear", partOfYear);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductionDetail()
                        {
                            name = reader.GetStringOrEmpty(0),
                            productionID = reader.GetInt64OrDefaultValue(1),
                            productName = reader.GetStringOrEmpty(2),
                            productId = reader.GetInt64OrDefaultValue(3),
                            productParentId = reader.GetInt64OrDefaultValue(4),
                            unitPrice = reader.GetDecimalOrDefaultValue(5),
                            fullAddress = reader.GetStringOrEmpty(6),
                            quantity = reader.GetDecimalOrDefaultValue(7),

                            createdDate = reader.GetInt64OrDefaultValue(8),
                            updatedDate = reader.GetInt64OrDefaultValue(9),
                            lastUpdateStatus = reader.GetInt64OrDefaultValue(10),
                            addressDesc = reader.GetStringOrEmpty(12),
                            productParentName = reader.GetStringOrEmpty(13),
                            kategoryName = reader.GetStringOrEmpty(14),
                            totalPrice = reader.GetDecimalOrDefaultValue(15),







                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        #endregion

        #region Yeni Hesabatlar

        public List<OfferProductionDetail> GetOfferGroupedProductionDetailistForAccounting()
        {

            var result = new List<OfferProductionDetail>();
            var query = @"  
 select table2.AdminName,table2.ADR, table2.ProductName, table2.productParentName,table2.product_Id,
  sum (table2.quantity) as totalQuantity, table2.olcuVahidi
  from (
  
   select table1.*,pc.ProductName as productParentName,au.Name as AdminName ,ev.name as olcuVahidi from ( 
  select op.Id,op.unit_price,op.quantity, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                 pa.adminUnit_Id,  pa.fullAddressId, dbo.splitstring(pa.fullAddressId)  as ADR, pa.fullAddress ,pa.addressDesc ,
          			
				 op.user_Id ,pc.ProductCatalogParentID
				  
                      from [dbo].[tblOffer_Production] op 
                    , [dbo].[tblProductCatalog] pc 
                     ,[dbo].[tblProductAddress] pa 
					 
                    , [dbo].[tblProductionControl] prc 
                     , [dbo].[tblEnumValue] ev
                    , [dbo].[tblEnumCategory] ec  
                      where   
      op.state_eV_Id= 2  and op.Status=1  and  op.product_Id=pc.Id   and pc.Status=1 
	  and op.productAddress_Id=pa.Id  and pa.Status=1
	
	  and  op.Id=prc.Offer_Production_Id  and prc.Status=1    and   op.state_eV_Id=ev.Id and ev.Status=1
	  and  ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1 ) as table1
	  left join [dbo].[tblProductCatalog] pc on table1.ProductCatalogParentID=pc.ID
	  left join  [dbo].[tblPRM_AdminUnit] au on table1.ADR=au.Id
	   join dbo.tblEnumValue ev on table1.EnumValueId=ev.Id and ev.enumCategory_enumCategoryId=5
	  
	  ) as table2, dbo.tblPRM_AdminUnit au where table2.ADR=au.Id
	   
	  group by  table2.AdminName,table2.ADR,table2.ProductName,table2.productParentName ,table2.product_Id, table2.olcuVahidi

	  order by AdminName



";


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OfferProductionDetail()
                        {
                            adminName = reader.GetStringOrEmpty(0),
                            adminID = Int64.Parse(reader.GetStringOrEmpty(1)),
                            productName = reader.GetStringOrEmpty(2),
                            productParentName = reader.GetStringOrEmpty(3),
                            productID = reader.GetInt64OrDefaultValue(4),
                            totalQuantity = reader.GetDecimalOrDefaultValue(5),
                            quantityType = reader.GetStringOrEmpty(6),




                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<OfferProductionDetail> GetOfferGroupedProductionDetailistForAccountingByAdmin_UnitID(Int64 addressID)
        {

            var result = new List<OfferProductionDetail>();
            var query = @"  
 select table2.AdminName,table2.ADR, table2.ProductName, table2.productParentName,table2.product_Id,
  sum (table2.quantity) as totalQuantity, table2.olcuVahidi
  from (
  
   select table1.*,pc.ProductName as productParentName,au.Name as AdminName ,ev.name as olcuVahidi from ( 
  select op.Id,op.unit_price,op.quantity, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                 pa.adminUnit_Id,  pa.fullAddressId, dbo.splitstring(pa.fullAddressId)  as ADR, pa.fullAddress ,pa.addressDesc ,
          			
				 op.user_Id ,pc.ProductCatalogParentID
				  
                      from [dbo].[tblOffer_Production] op 
                    , [dbo].[tblProductCatalog] pc 
                     ,[dbo].[tblProductAddress] pa 
					 
                    , [dbo].[tblProductionControl] prc 
                     , [dbo].[tblEnumValue] ev
                    , [dbo].[tblEnumCategory] ec  
                      where   
      op.state_eV_Id= 2  and op.Status=1  and  op.product_Id=pc.Id   and pc.Status=1 
	  and op.productAddress_Id=pa.Id  and pa.Status=1
	
	  and  op.Id=prc.Offer_Production_Id  and prc.Status=1    and   op.state_eV_Id=ev.Id and ev.Status=1
	  and  ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1 ) as table1
	  left join [dbo].[tblProductCatalog] pc on table1.ProductCatalogParentID=pc.ID
	  left join  [dbo].[tblPRM_AdminUnit] au on table1.ADR=au.Id
	   join dbo.tblEnumValue ev on table1.EnumValueId=ev.Id and ev.enumCategory_enumCategoryId=5
	  where ADR=@addressID
	  ) as table2, dbo.tblPRM_AdminUnit au where table2.ADR=au.Id
	   
	  group by  table2.AdminName,table2.ADR,table2.ProductName,table2.productParentName ,table2.product_Id, table2.olcuVahidi

	  order by AdminName




";


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@addressID", addressID);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OfferProductionDetail()
                        {
                            adminName = reader.GetStringOrEmpty(0),
                            adminID = Int64.Parse(reader.GetStringOrEmpty(1)),
                            productName = reader.GetStringOrEmpty(2),
                            productParentName = reader.GetStringOrEmpty(3),
                            productID = reader.GetInt64OrDefaultValue(4),
                            totalQuantity = reader.GetDecimalOrDefaultValue(5),
                            quantityType = reader.GetStringOrEmpty(6),




                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<OfferProductionDetail> GetOfferGroupedProductionDetailistForAccountingByProductId(Int64 product_Id)
        {

            var result = new List<OfferProductionDetail>();
            var query = @"  
 select table2.AdminName,table2.ADR, table2.ProductName, table2.productParentName,table2.product_Id,
  sum (table2.quantity) as totalQuantity, table2.olcuVahidi
  from (
  
   select table1.*,pc.ProductName as productParentName,au.Name as AdminName ,ev.name as olcuVahidi from ( 
  select op.Id,op.unit_price,op.quantity, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                 pa.adminUnit_Id,  pa.fullAddressId, dbo.splitstring(pa.fullAddressId)  as ADR, pa.fullAddress ,pa.addressDesc ,
          			
				 op.user_Id ,pc.ProductCatalogParentID
				  
                      from [dbo].[tblOffer_Production] op 
                    , [dbo].[tblProductCatalog] pc 
                     ,[dbo].[tblProductAddress] pa 
					 
                    , [dbo].[tblProductionControl] prc 
                     , [dbo].[tblEnumValue] ev
                    , [dbo].[tblEnumCategory] ec  
                      where   
      op.state_eV_Id= 2  and op.Status=1  and  op.product_Id=pc.Id   and pc.Status=1 
	  and op.productAddress_Id=pa.Id  and pa.Status=1
	
	  and  op.Id=prc.Offer_Production_Id  and prc.Status=1    and   op.state_eV_Id=ev.Id and ev.Status=1
	  and  ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1 ) as table1
	  left join [dbo].[tblProductCatalog] pc on table1.ProductCatalogParentID=pc.ID
	  left join  [dbo].[tblPRM_AdminUnit] au on table1.ADR=au.Id
	   join dbo.tblEnumValue ev on table1.EnumValueId=ev.Id and ev.enumCategory_enumCategoryId=5
	  where product_Id=@product_Id
	  ) as table2, dbo.tblPRM_AdminUnit au where table2.ADR=au.Id
	   
	  group by  table2.AdminName,table2.ADR,table2.ProductName,table2.productParentName ,table2.product_Id, table2.olcuVahidi

	  order by AdminName




";


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@product_Id", product_Id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OfferProductionDetail()
                        {
                            adminName = reader.GetStringOrEmpty(0),
                            adminID = Int64.Parse(reader.GetStringOrEmpty(1)),
                            productName = reader.GetStringOrEmpty(2),
                            productParentName = reader.GetStringOrEmpty(3),
                            productID = reader.GetInt64OrDefaultValue(4),
                            totalQuantity = reader.GetDecimalOrDefaultValue(5),
                            quantityType = reader.GetStringOrEmpty(6),




                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<OfferProductionDetail> GetOfferGroupedProductionDetailistForAccountingByRoleId(Int64 RoleId)
        {

            var result = new List<OfferProductionDetail>();
            StringBuilder squery = new StringBuilder();
            var query = @"  
 select table2.AdminName,table2.ADR, table2.ProductName, table2.productParentName,table2.product_Id,
  sum (table2.quantity) as totalQuantity, table2.olcuVahidi,table2.RoleId
  from (
  
   select table1.*,pc.ProductName as productParentName,au.Name as AdminName ,ev.name as olcuVahidi from ( 
  select op.Id,op.unit_price,op.quantity, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                 pa.adminUnit_Id,  pa.fullAddressId, dbo.splitstring(pa.fullAddressId)  as ADR, pa.fullAddress ,pa.addressDesc ,
          			
				 op.user_Id ,pc.ProductCatalogParentID,ur.RoleId
				  
                      from [dbo].[tblOffer_Production] op 
                    , [dbo].[tblProductCatalog] pc 
                     ,[dbo].[tblProductAddress] pa 
					 
                    , [dbo].[tblProductionControl] prc 
                     , [dbo].[tblEnumValue] ev
                    , [dbo].[tblEnumCategory] ec  
					  ,tblUser u
			 ,tblUserRole ur
                      where   
      op.state_eV_Id= 2  and op.Status=1  and  op.product_Id=pc.Id   and pc.Status=1 
	  and op.productAddress_Id=pa.Id  and pa.Status=1
 and op.user_Id=u.Id and u.Status=1 
       and u.Id=ur.UserId and ur.Status=1
	
	 --  and ur.RoleId in (11,15)

	 
";
            var query2 = @"  and  op.Id=prc.Offer_Production_Id  and prc.Status=1    and   op.state_eV_Id=ev.Id and ev.Status=1
	  and  ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1 ) as table1
	  left join [dbo].[tblProductCatalog] pc on table1.ProductCatalogParentID=pc.ID
	  left join  [dbo].[tblPRM_AdminUnit] au on table1.ADR=au.Id
	   join dbo.tblEnumValue ev on table1.EnumValueId=ev.Id and ev.enumCategory_enumCategoryId=5
	  
	  ) as table2, dbo.tblPRM_AdminUnit au where table2.ADR=au.Id
	   
	  group by  table2.AdminName,table2.ADR,table2.ProductName,table2.productParentName ,table2.product_Id, table2.olcuVahidi,table2.RoleId

	  order by AdminName";
            var queryroleID = @"    and ur.RoleId= @RoleId";
            squery.Append(query);
            if (RoleId != 0)
            {
                squery.Append(queryroleID);
            }
            squery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@RoleId", RoleId);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OfferProductionDetail()
                        {
                            adminName = reader.GetStringOrEmpty(0),
                            adminID = Int64.Parse(reader.GetStringOrEmpty(1)),
                            productName = reader.GetStringOrEmpty(2),
                            productParentName = reader.GetStringOrEmpty(3),
                            productID = reader.GetInt64OrDefaultValue(4),
                            totalQuantity = reader.GetDecimalOrDefaultValue(5),
                            quantityType = reader.GetStringOrEmpty(6),
                            // userType=reader.GetStringOrEmpty(7),
                            roleID = reader.GetInt64OrDefaultValue(7),



                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        //getDemandByForganistion

        public List<OfferProductionDetail> GetOfferGroupedProductionDetailistForAccountingBySearch(OfferProductionDetailSearch ops)
        {

            var result = new List<OfferProductionDetail>();
            StringBuilder sQuery = new StringBuilder();
            string query1 = @"
 select table2.AdminName,table2.ADR, table2.ProductName, table2.productParentName,table2.product_Id,
  sum (table2.quantity) as totalQuantity, table2.olcuVahidi,table2.olcuVahidiDesc
  from (
  
   select table1.*,pc.ProductName as productParentName,au.Name as AdminName ,ev.name as olcuVahidi,ev.description as olcuVahidiDesc from ( 
  select op.Id,op.unit_price,op.quantity, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                 pa.adminUnit_Id,  pa.fullAddressId, dbo.splitstring(pa.fullAddressId)  as ADR, pa.fullAddress ,pa.addressDesc ,
          			
				 op.user_Id ,pc.ProductCatalogParentID,ev.description
				  
                      from [dbo].[tblOffer_Production] op 
                    , [dbo].[tblProductCatalog] pc 
                     ,[dbo].[tblProductAddress] pa 
					 
                    , [dbo].[tblProductionControl] prc 
                     , [dbo].[tblEnumValue] ev
                    , [dbo].[tblEnumCategory] ec  
					  ,tblUser u
			 ,tblUserRole ur
                      where   
    op.Status=1  and  op.product_Id=pc.Id   and pc.Status=1 
	  and op.productAddress_Id=pa.Id  and pa.Status=1
 and op.user_Id=u.Id and u.Status=1 
       and u.Id=ur.UserId and ur.Status=1";

            string query2 = @"
 and  op.Id=prc.Offer_Production_Id  and prc.Status=1    and   op.state_eV_Id=ev.Id and ev.Status=1
	  and  ev.enumCategory_enumCategoryId=ec.Id and ec.Status=1 ) as table1
	  left join [dbo].[tblProductCatalog] pc on table1.ProductCatalogParentID=pc.ID
	  left join  [dbo].[tblPRM_AdminUnit] au on table1.ADR=au.Id
	   join dbo.tblEnumValue ev on table1.EnumValueId=ev.Id and ev.enumCategory_enumCategoryId=5
	  
	  ) as table2, dbo.tblPRM_AdminUnit au where table2.ADR=au.Id
	   
	  group by  table2.AdminName,table2.ADR,table2.ProductName,table2.productParentName ,table2.product_Id, table2.olcuVahidi,table2.olcuVahidiDesc

	  order by AdminName";


            sQuery.Append(query1);
            var queryAdminID = @" and  dbo.splitstring(pa.fullAddressId)=@adminID ";
            var queryProductID = @" and op.product_Id =@productID ";
            var queryRoleId = @" and ur.RoleId=  @RoleID";
            var queryStateId = @" and  op.state_eV_Id= @state_eV_Id ";
            if (ops.adminID != 0)
            {
                sQuery.Append(queryAdminID);

            }
            if (ops.productID != 0)
            {
                sQuery.Append(queryProductID);
            }
            if (ops.roleID != 0)
            {
                sQuery.Append(queryRoleId);
            }


            if (ops.state_eV_Id != 0)
            {
                sQuery.Append(queryStateId);
            }
            sQuery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(sQuery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@RoleId", ops.roleID);
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@productID", ops.productID);
                    command.Parameters.AddWithValue("@adminID", ops.adminID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OfferProductionDetail()
                        {
                            adminName = reader.GetStringOrEmpty(0),
                            adminID = Int64.Parse(reader.GetStringOrEmpty(1)),
                            productName = reader.GetStringOrEmpty(2),
                            productParentName = reader.GetStringOrEmpty(3),
                            productID = reader.GetInt64OrDefaultValue(4),
                            totalQuantity = reader.GetDecimalOrDefaultValue(5),
                            quantityType = reader.GetStringOrEmpty(6),
                            quantityTypeDescription = reader.GetStringOrEmpty(7),
                            // userType=reader.GetStringOrEmpty(7),
                            // roleID=reader.GetInt64OrDefaultValue(8),



                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public Int64 GetDemandByForganistion_OPC(DemandForegnOrganization1 ops)
        {

            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            StringBuilder squeryunion = new StringBuilder();
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
  FROM dbo.tblPRM_AdminUnit au
 ";
            var query2 = @" 
   UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )


select COUNT(*) as Count from(
 select table1.*,pc.ProductName As ParentProductName,au.Name as regionName,pc.Status from ( 
 
  select fo.Id, adr.fullAddress as FullAddress,
  dbo.splitstring(adr.fullAddress)  as AdminName,
  aunit.Name as AdminName1,fo.name  As OrganisationName,fo.voen,pc.ProductCatalogParentID,
  pc.ProductName,prc.EnumValueId,ev.name as unitOfMeasurement,pprice.unit_price,
  person.Id as personID,
  person.Name as ManagerName,person.Surname as ManagerSurname,dp.quantity,person.FatherName,person.PinNumber,
  aunit.Id as adminUNItID,aunit.ParentRegionID,pc.Id as productID,fo.Id as organizationID
 
  
  from tblForeign_Organization fo 
  join tblDemand_Production dp on fo.userId=dp.user_Id
  left join [dbo].[tblProductionControl] prc on dp.Id=prc.Demand_Production_Id  and prc.Status=1  
   and prc.EnumCategoryId=5
  left join tblEnumValue ev on prc.EnumValueId=ev.Id
    
   join tblProductCatalog pc on dp.product_Id=pc.Id
    join tblAddress  adr on fo.address_Id=adr.Id
   join tblPRM_AdminUnit aunit on adr.adminUnit_Id=aunit.Id
 and  aunit.Id
			   in (
							select Id from cte
							
							 		)
   join tblProductPrice pprice on dp.product_Id=pprice.productId
  
    and pprice.Status=1
   left join tblPerson person on fo.manager_Id=person.Id
  where  fo.Status=1 and 
  dp.Status=1 and  pprice.year=@year and dp.state_eV_Id=2

  

           ";

            squeryunion.Append(query1);

            var queryadminID = @" WHERE  Id=@addressID ";
            var queryRegion = @" and ParentRegionID=@ParentRegionID";
            var queryProductId = @" and productID=@productID";
            var queryOrganization = @"  and organizationID=@organizationID";
            var queryAdminUNItList = @" and  tb.adminUNItID in (SELECT number FROM @tbl) ";
            var queryDate = @" and dp.Id  in  (select  distinct
			 table1.demand_Id
			  from (
			 select 
           
              pc.demand_Id,
			  dbo.dateReturn(pc.demand_Id,pc.year,pc.months_eV_Id,pc.day) as date1
     from [dbo].[tblProductionCalendar]pc
             where  pc.Status=1 
 
			and pc.demand_Id=dp.Id 
         ) table1
           
			 where   table1.date1  between @startDate and @endDate)";
            var queryEnd = @"  )as table1
	 join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1
	left join tblPRM_AdminUnit au on table1.ParentRegionID=au.ID and au.Status=1
	   

) as tb where Status=1 ";
            if (ops.addressID != 0)
            {
                squeryunion.Append(queryadminID);
            }
            squery.Append(squeryunion.ToString());
            squery.Append(query2);
            if (ops.startDate != 0 && ops.endDate != 0)
            {
                squery.Append(queryDate);
            }
            squery.Append(queryEnd);
            if (ops.productID != 0)
            {
                squery.Append(queryProductId);
            }
            if (ops.organizationID != 0)
            {
                squery.Append(queryOrganization);
            }
            if (ops.regionId != 0)
            {
                squery.Append(queryRegion);
            }
            if (!string.IsNullOrEmpty(ops.listAdminID))
            {
                squery.Append(queryAdminUNItList);
            }
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {

                    command.Parameters.AddWithValue("@year", ops.year);
                    command.Parameters.AddWithValue("@ParentRegionID", ops.regionId);
                    command.Parameters.AddWithValue("@productID", ops.productID);
                    command.Parameters.AddWithValue("@organizationID", ops.organizationID);
                    command.Parameters.AddWithValue("@addressID", ops.addressID);
                    command.Parameters.AddWithValue("@adminUNItID", ops.listAdminID.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@startDate", ops.startDate);
                    command.Parameters.AddWithValue("@endDate", ops.endDate);

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
        public List<OrganizationDetail> GetDemandByForganistion_OP(DemandForegnOrganization1 ops)
        {

            var result = new List<OrganizationDetail>();
            StringBuilder squery = new StringBuilder();
            StringBuilder squeryunion = new StringBuilder();
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
  FROM dbo.tblPRM_AdminUnit au
 ";
            var query2 = @"   UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )


, RESULTS AS(
        SELECT *
            , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn
            , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed
        FROM (select table1.*,pc.ProductName As ParentProductName,au.Name as regionName,pc.Status from ( 
 
  select fo.Id, adr.fullAddress as FullAddress,
 LTrim(RTrim(dbo.splitstring(adr.fullAddress)))   as AdminName,
  aunit.Name as AdminName1,fo.name  As OrganisationName,fo.voen,pc.ProductCatalogParentID,
  pc.ProductName,prc.EnumValueId,ev.name as unitOfMeasurement,pprice.unit_price,
  person.Id as personID,
  person.Name as ManagerName,person.Surname as ManagerSurname,dp.quantity,person.FatherName,person.PinNumber,
  aunit.Id as adminUNItID,aunit.ParentRegionID,pc.Id as productID,fo.Id as organizationID
 
  
  from tblForeign_Organization fo 
  join tblDemand_Production dp on fo.userId=dp.user_Id
  left join [dbo].[tblProductionControl] prc on dp.Id=prc.Demand_Production_Id  and prc.Status=1  
   and prc.EnumCategoryId=5
  left join tblEnumValue ev on prc.EnumValueId=ev.Id
    
   join tblProductCatalog pc on dp.product_Id=pc.Id
    join tblAddress  adr on fo.address_Id=adr.Id
   join tblPRM_AdminUnit aunit on adr.adminUnit_Id=aunit.Id
 and  aunit.Id
			   in (
							select Id from cte
							
							 		)
   join tblProductPrice pprice on dp.product_Id=pprice.productId
   --and pprice.partOfYear=4 and pprice.year=2016
    and pprice.Status=1
   left join tblPerson person on fo.manager_Id=person.Id
  where  fo.Status=1 and 
  dp.Status=1  and  pprice.year=@year and dp.state_eV_Id=2

 ";
            var query3 = @"  )SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";


            squeryunion.Append(query1);
            squery.Append(squeryunion.ToString());
            var queryadminID = @" WHERE  Id=@addressID ";
            var queryRegion = @" and ParentRegionID=@ParentRegionID";
            var queryProductId = @" and productID=@productID";
            var queryOrganization = @"  and organizationID=@organizationID";
            var queryAdminUNItList = @" and  tb.adminUNItID in (SELECT number FROM @tbl) ";
            var queryDate = @" and dp.Id  in  (select  distinct
			 table1.demand_Id
			  from (
			 select 
           
              pc.demand_Id,
			  dbo.dateReturn(pc.demand_Id,pc.year,pc.months_eV_Id,pc.day) as date1
     from [dbo].[tblProductionCalendar]pc
             where  pc.Status=1 
 
			and pc.demand_Id=dp.Id 
         ) table1
           
			 where   table1.date1  between @startDate and @endDate)";
            var queryEnd = @"   )as table1
	 join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1
	left join tblPRM_AdminUnit au on table1.ParentRegionID=au.ID and au.Status=1
	   

) as tb where Status=1";

            if (ops.addressID != 0)
            {
                squeryunion.Append(queryadminID);
            }

            squery.Append(query2);
            if (ops.startDate != 0 || ops.endDate != 0)
            {
                squery.Append(queryDate);
            }
            squery.Append(queryEnd);
            if (ops.productID != 0)
            {
                squery.Append(queryProductId);
            }
            if (ops.organizationID != 0)
            {
                squery.Append(queryOrganization);
            }
            if (ops.regionId != 0)
            {
                squery.Append(queryRegion);
            }
            if (!string.IsNullOrEmpty(ops.listAdminID))
            {
                squery.Append(queryAdminUNItList);
            }
            squery.Append(query3);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {

                    command.Parameters.AddWithValue("@year", ops.year);
                    command.Parameters.AddWithValue("@page_size", ops.page_size);
                    command.Parameters.AddWithValue("@page_num", ops.page);
                    command.Parameters.AddWithValue("@ParentRegionID", ops.regionId);
                    command.Parameters.AddWithValue("@productID", ops.productID);
                    command.Parameters.AddWithValue("@organizationID", ops.organizationID);
                    command.Parameters.AddWithValue("@addressID", ops.addressID);
                    command.Parameters.AddWithValue("@adminUNItID", ops.listAdminID.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@startDate", ops.startDate);
                    command.Parameters.AddWithValue("@endDate", ops.endDate);



                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OrganizationDetail()
                        {

                            fullAddress = reader.GetStringOrEmpty(1),
                            adminName = reader.GetStringOrEmpty(2),
                            adminName1 = reader.GetStringOrEmpty(3),
                            organizationName = reader.GetStringOrEmpty(4),
                            voen = reader.GetStringOrEmpty(5),
                            prodcutName = reader.GetStringOrEmpty(7),
                            unitOfMeasurement = reader.GetStringOrEmpty(9),
                            unit_price = reader.GetDecimalOrDefaultValue(10),
                            personID = reader.GetInt64OrDefaultValue(11),
                            managerName = reader.GetStringOrEmpty(12),
                            managerSurname = reader.GetStringOrEmpty(13),
                            quantity = reader.GetDecimalOrDefaultValue(14),
                            fatherName = reader.GetStringOrEmpty(15),
                            pinNumber = reader.GetStringOrEmpty(16),
                            adminUNit_ID = reader.GetInt64OrDefaultValue(17),
                            parentRegionID = reader.GetInt64OrDefaultValue(18),
                            prodcutID = reader.GetInt64OrDefaultValue(19),
                            organizationID = reader.GetInt64OrDefaultValue(20),
                            parentProductName = reader.GetStringOrEmpty(21),
                            regionName = reader.GetStringOrEmpty(22),





                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        #endregion

        public List<DemandDetail> GetDemand_ProductionsByStateAndUserID1(Int64 userID, Int64 stateEvID)
        {
            var result = new List<DemandDetail>();
            var query = @" select distinct tb.* ,pc.ProductName as parentName from( select distinct  op.Id as productionID,pc.ProductCatalogParentID,pc.ProductName
,ev.description as kategoryName,pa.fullAddress,pa.addressDesc,op.state_eV_Id ,op.user_Id from tblDemand_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and op.Status=1
 join tblProductCatalogControl prc on op.product_Id=prc.ProductId and prc.Status=1
 join tblEnumValue ev on prc.EnumValueId=ev.Id and ev.Status=1
 join tblProductAddress pa on pa.Id=op.address_Id and pa.Status=1
where op.Status=1 and op.user_Id=@user_Id and op.state_eV_Id=@state_eV_Id
) as tb
left join tblProductCatalog pc on tb.ProductCatalogParentID=pc.Id and pc.Status=1


							 ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_Id", userID);
                    command.Parameters.AddWithValue("@state_eV_Id", stateEvID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemandDetail()
                        {

                            productionID = reader.GetInt64OrDefaultValue(0),
                            productName = reader.GetStringOrEmpty(2),
                            kategoryName = reader.GetStringOrEmpty(3),
                            fullAddress = reader.GetStringOrEmpty(4),
                            addressDesc = reader.GetStringOrEmpty(5),
                            state_Ev_Id = reader.GetInt64OrDefaultValue(6),
                            userId = reader.GetInt64OrDefaultValue(7),
                            parentName = reader.GetStringOrEmpty(8),







                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<AdminUnitRegion> GetPRM_AdminUnitRegionList()
        {
            var result = new List<AdminUnitRegion>();
            StringBuilder squery = new StringBuilder();
            var query = @" select distinct tb.*,au.Name as regionName from(select au.ParentRegionID from tblPRM_AdminUnit au
where au.Status=1
)as tb
 join tblPRM_AdminUnit au on tb.ParentRegionID=au.Id and au.Status=1
where au.Id not in (20000002,51000002,61100001)
							 ";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new AdminUnitRegion()
                        {






                            ID = reader.GetInt64OrDefaultValue(0),
                            regionName = reader.GetStringOrEmpty(1)



                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


        public List<AnnouncementDetail> GetAnnouncementDetails_OP(int page, int pageSize)
        {
            Int64 curentDate = DateTime.Now.getInt64ShortDate();
            var result = new List<AnnouncementDetail>();
            StringBuilder squery = new StringBuilder();
            var query = @"select tb.*,pc.ProductName as parentName from(select an.Id,an.product_id ,pc.ProductName,pc.ProductCatalogParentID from tblAnnouncement an
 left join tblProductCatalog pc on an.product_id=pc.Id and pc.Status=1
where an.Status=1
)as tb
left join tblProductCatalog pc on tb.ProductCatalogParentID=pc.Id and pc.Status=1
--where tb.product_id=@product_id
 order by parentName, ProductName
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY
							 ";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PageNo", page);
                    command.Parameters.AddWithValue("@RecordsPerPage", pageSize);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new AnnouncementDetail()
                        {

                            announcementID = reader.GetInt64OrDefaultValue(0),
                            productId = reader.GetInt64OrDefaultValue(1),
                            productName = reader.GetStringOrEmpty(2),
                            parentName = reader.GetStringOrEmpty(4),








                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<AnnouncementDetail> GetAnnouncementDetailsByProductId_OP(int productID, int page, int pageSize)
        {
            Int64 curentDate = DateTime.Now.getInt64ShortDate();
            var result = new List<AnnouncementDetail>();
            StringBuilder squery = new StringBuilder();
            var query = @"with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblProductCatalog au
  where Id=@product_id

   UNION ALL 
   SELECT au.Id

  FROM dbo.tblProductCatalog au  JOIN cte c ON au.ProductCatalogParentID = c.Id
  
  )
select tb.*,pc.ProductName as parentName from(select an.Id,an.product_id ,pc.ProductName,pc.ProductCatalogParentID from tblAnnouncement an
left join tblProductCatalog pc on an.product_id=pc.Id and pc.Status=1
where an.Status=1
)as tb
left join tblProductCatalog pc on tb.ProductCatalogParentID=pc.Id and pc.Status=1
where tb.product_id in (select Id from cte)
 order by parentName, ProductName
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY
							 ";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PageNo", page);
                    command.Parameters.AddWithValue("@RecordsPerPage", pageSize);
                    command.Parameters.AddWithValue("@product_id", productID);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new AnnouncementDetail()
                        {

                            announcementID = reader.GetInt64OrDefaultValue(0),
                            productId = reader.GetInt64OrDefaultValue(1),
                            productName = reader.GetStringOrEmpty(2),
                            parentName = reader.GetStringOrEmpty(4),








                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<tblPRM_AdminUnit> GetPRM_AdminUnitRegionByAddressId(Int64 ID)
        {
            var result = new List<tblPRM_AdminUnit>();
            StringBuilder squery = new StringBuilder();
            StringBuilder squery1 = new StringBuilder();
            if (ID != 0)
            {
                var query = @" select au.* from tblPRM_AdminUnit au
where au.Status=1  and au.ParentRegionID =@ID and au.ParentID=1
           



							 ";
                var query1 = @"with cte(Id) AS 
 (
  SELECT au.Id
 
  FROM dbo.tblPRM_AdminUnit au
   where au.ParentRegionID=@ID
 

   UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
   select au.* from  tblPRM_AdminUnit au where au.Status=1 
	   
           
		   	   and  au.Id 
			   in (
							select Id from cte
							 		)";
                squery.Append(query);
            }
            else if (ID == 0)
            {
                var query = @"  with cte(Id) AS 
 (
  SELECT au.Id
 
  FROM dbo.tblPRM_AdminUnit au
   where au.Id=1
 

   UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
   select au.* from  tblPRM_AdminUnit au where au.Status=1 
	   
           
		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
           ";
                squery.Append(query);
            }
            squery1.Append(squery.ToString());
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery1.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new tblPRM_AdminUnit()
                        {
                            Id = reader.GetInt64OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            Description = reader.GetStringOrEmpty(2),
                            ParentID = reader.GetInt64OrDefaultValue(3),
                            EnumValueID = reader.GetInt64OrDefaultValue(4),
                            Status = reader.GetInt64OrDefaultValue(5),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(6),
                            createdUser = reader.GetStringOrEmpty(7),
                            createdDate = reader.GetInt64OrDefaultValue(8),
                            updatedUser = reader.GetStringOrEmpty(9),
                            updatedDate = reader.GetInt64OrDefaultValue(10),
                            iamasId = reader.GetInt64OrDefaultValue(11),
                            isCity = reader.GetBoolean(12),
                            ParentRegionID = reader.GetInt64OrDefaultValue(13),







                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public Int64 GetAnnouncementDetails_OPC()
        {

            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query = @" select Count(*) as Count from (select tb.*,pc.ProductName as parentName from(select an.Id,an.product_id ,pc.ProductName,pc.ProductCatalogParentID from tblAnnouncement an
 left join tblProductCatalog pc on an.product_id=pc.Id and pc.Status=1
where an.Status=1
)as tb
left join tblProductCatalog pc on tb.ProductCatalogParentID=pc.Id and pc.Status=1
--where tb.product_id=@product_id
) as tb
 
";


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {


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
        public Int64 GetAnnouncementDetailsByProductId_OPC(Int64 productID)
        {

            Int64 count = 0;
            StringBuilder squery = new StringBuilder();
            var query = @" 
with cte(Id) AS 
 (
  SELECT au.Id
  FROM dbo.tblProductCatalog au
  where Id=@product_id

   UNION ALL 
   SELECT au.Id

  FROM dbo.tblProductCatalog au  JOIN cte c ON au.ProductCatalogParentID = c.Id
  
  )
select Count(*) as Count from (select tb.*,pc.ProductName as parentName from(select an.Id,an.product_id ,pc.ProductName,pc.ProductCatalogParentID from tblAnnouncement an
 left join tblProductCatalog pc on an.product_id=pc.Id and pc.Status=1
where an.Status=1
)as tb
left join tblProductCatalog pc on tb.ProductCatalogParentID=pc.Id and pc.Status=1
where tb.product_id in (select Id from cte)
) as tb
 
";


            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@product_id", productID);
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
        public List<DemandDetails> GetDemand_ProductionsByStateAndUserID_OP(tblDemand_Production item, int page, int pageSize)
        {
            var result = new List<DemandDetails>();
            //  StringBuilder squery = new StringBuilder();
            var query = @" 
 
select tb.*,pc.ProductName as parentName from(select dp.*,pc.ProductName,pc.ProductCatalogParentID
from tblDemand_Production dp
join tblProductCatalog pc on dp.product_Id=pc.Id and pc.Status=1
where dp.Status=1
) as tb
join tblProductCatalog pc on pc.Id=tb.ProductCatalogParentID and pc.Status=1
where tb.user_Id=@user_Id and tb.state_eV_Id=@state_eV_Id
order by ProductName,parentName asc
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY
 
";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PageNo", page);
                    command.Parameters.AddWithValue("@RecordsPerPage", pageSize);
                    command.Parameters.AddWithValue("@user_Id", item.user_Id);
                    command.Parameters.AddWithValue("@state_eV_Id", item.state_eV_Id);


                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new DemandDetails()
                        {

                            Id = reader.GetInt64OrDefaultValue(0),
                            grup_Id = reader.GetStringOrEmpty(1),
                            title = reader.GetStringOrEmpty(2),
                            description = reader.GetStringOrEmpty(3),
                            unit_price = reader.GetDecimalOrDefaultValue(4),
                            total_price = reader.GetDecimalOrDefaultValue(5),
                            quantity = reader.GetDecimalOrDefaultValue(6),
                            quantity_type_eV_Id=reader.GetInt64OrDefaultValue(7),
                            startDate=reader.GetInt64OrDefaultValue(8),
                            endDate=reader.GetInt64OrDefaultValue(9),
                            isSelected=reader.GetBoolean(10),
                            isAnnouncement=reader.GetBoolean(11),
                            Status=reader.GetInt64OrDefaultValue(12),
                            LastUpdatedStatus=reader.GetInt64OrDefaultValue(13),
                            createdUser=reader.GetStringOrEmpty(14),
                            createdDate=reader.GetInt64OrDefaultValue(15),
                            updatedUser=reader.GetStringOrEmpty(16),
                            updatedDate=reader.GetInt64OrDefaultValue(17),
                            address_Id=reader.GetInt64OrDefaultValue(18),
                            product_Id=reader.GetInt64OrDefaultValue(19),
                            user_Id=reader.GetInt64OrDefaultValue(20),
                            state_eV_Id=reader.GetInt64OrDefaultValue(21),
                            fullProductId=reader.GetStringOrEmpty(22),
                            monitoring_eV_Id=reader.GetInt64OrDefaultValue(23),
                            isNew=reader.GetInt64OrDefaultValue(24),
                            productName=reader.GetStringOrEmpty(25),
                            parentName=reader.GetStringOrEmpty(27)



                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<OfferDetails> GetOffer_ProductionsByUserId_OP(Int64 userId, int page, int pageSize)
        {
            var result = new List<OfferDetails>();
            //  StringBuilder squery = new StringBuilder();
            var query = @" 
 
select tb.*,pc.ProductName as parentName from(select op.*,pc.ProductName,pc.ProductCatalogParentID from tblOffer_Production op
 join tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1
where op.Status=1
) as tb
join tblProductCatalog pc on pc.Id=tb.ProductCatalogParentID and pc.Status=1
where tb.user_Id=@user_Id
order by ProductName,parentName asc
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY

 
";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PageNo", page);
                    command.Parameters.AddWithValue("@RecordsPerPage", pageSize);
                    command.Parameters.AddWithValue("@user_Id", userId);
                 

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new OfferDetails()
                        {

                            Id = reader.GetInt64OrDefaultValue(0),
                            grup_Id = reader.GetStringOrEmpty(1),
                            title = reader.GetStringOrEmpty(2),
                            description = reader.GetStringOrEmpty(3),
                            unit_price = reader.GetDecimalOrDefaultValue(4),
                            total_price = reader.GetDecimalOrDefaultValue(5),
                            quantity = reader.GetDecimalOrDefaultValue(6),
                            quantity_type_eV_Id = reader.GetInt64OrDefaultValue(7),
                            startDate = reader.GetInt64OrDefaultValue(8),
                            endDate = reader.GetInt64OrDefaultValue(9),
                            isSelected = reader.GetBoolean(10),
                         
                            Status = reader.GetInt64OrDefaultValue(11),
                            LastUpdatedStatus = reader.GetInt64OrDefaultValue(12),
                            createdUser = reader.GetStringOrEmpty(13),
                            createdDate = reader.GetInt64OrDefaultValue(14),
                            updatedUser = reader.GetStringOrEmpty(15),
                            updatedDate = reader.GetInt64OrDefaultValue(16),
                            potentialProduct_Id = reader.GetInt64OrDefaultValue(17),
                            product_Id = reader.GetInt64OrDefaultValue(18),
                            productAddress_Id=reader.GetInt64OrDefaultValue(19),
                            user_Id = reader.GetInt64OrDefaultValue(20),
                            state_eV_Id = reader.GetInt64OrDefaultValue(21),
                          
                            monitoring_eV_Id = reader.GetInt64OrDefaultValue(22),
                            productOrigin=reader.GetInt64OrDefaultValue(23),
                            contractId=reader.GetInt64OrDefaultValue(24),
                            isNew = reader.GetInt64OrDefaultValue(25),
                            productName = reader.GetStringOrEmpty(26),
                            parentName = reader.GetStringOrEmpty(28)



                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<UserDetails> GetUsersByUserType_OP(Int64 usertype_eV_iD, int page, int pageSize)
        {
            var result = new List<UserDetails>();
            //  StringBuilder squery = new StringBuilder();
            var query = @" 
select tb.*,au.Name as regionName from(select u.*,au.ParentRegionID,fo.name as organizationName,
person.Name ,person.Surname,person.FatherName from tblUser u
left join tblAddress adr on adr.user_Id=u.Id and adr.Status=1
left join tblPRM_AdminUnit au on adr.adminUnit_Id=au.Id and au.Status=1
left join tblForeign_Organization fo on fo.userId=u.Id and fo.Status=1
left join tblPerson person on person.Id=fo.manager_Id and person.Status=1
where u.Status=1
) as tb

left join tblPRM_AdminUnit au on tb.ParentRegionID=au.Id and au.Status=1
where tb.userType_eV_ID=@userType_eV_ID
order by Username,organizationName
OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY
 
";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PageNo", page);
                    command.Parameters.AddWithValue("@RecordsPerPage", pageSize);
                    command.Parameters.AddWithValue("@userType_eV_ID", usertype_eV_iD);


                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new UserDetails()
                        {

                            Id=reader.GetInt64OrDefaultValue(0),
                            Username=reader.GetStringOrEmpty(1),
                            Email=reader.GetStringOrEmpty(2),
                            Password=reader.GetStringOrEmpty(3),
                            LastLoginIP=reader.GetStringOrEmpty(4),
                            LastLoginDate=reader.GetInt64OrDefaultValue(5),
                            ProfileImageUrl=reader.GetStringOrEmpty(6),
                            Status=reader.GetInt64OrDefaultValue(7),
                            LastUpdatedStatus=reader.GetInt64OrDefaultValue(8),
                            createdUser=reader.GetStringOrEmpty(9),
                            createdDate=reader.GetInt64OrDefaultValue(10),
                            updatedUser=reader.GetStringOrEmpty(11),
                            updatedDate=reader.GetInt64OrDefaultValue(12),
                            userType_eV_ID=reader.GetInt64OrDefaultValue(13),
                            ASC_ID=reader.GetInt64OrDefaultValue(14),
                            KTN_ID=reader.GetInt64OrDefaultValue(15),
                            TaxexType=reader.GetInt16(16),
                            organizationName=reader.GetStringOrEmpty(18),
                            name=reader.GetStringOrEmpty(19),
                            surName=reader.GetStringOrEmpty(20),
                            fatherName=reader.GetStringOrEmpty(21),
                            regionName=reader.GetStringOrEmpty(22),






                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<ProductionDetail> GetOfferProductionDetailistForEValueId_OPEX(OfferProductionDetailSearch ops, int startDate, int endDate)
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

            var queryadminID = @" where Id=@addressID ";
            if (ops.adminID != 0)
            {
                squeryunion.Append(queryadminID);
            }
            var query2 = @"  

   UNION ALL 
   SELECT au.Id

  FROM dbo.tblPRM_AdminUnit au  JOIN cte c ON au.parentId = c.Id
  
  )
  --select ID from cte;
  

  
	select tb.* from(	select  table2.*,pc.ProductName as  ProductParentName,adr.fullAddress as personAddress,adr.addressDesc as personAddressDesc,ev.name as userType
 ,fo.name as organizationName,fo.voen,ur.RoleId ,padr.Id as prodcutAdressId
  from(  
 select FirstTable.Id,FirstTable.unit_price,FirstTable.quantity,FirstTable.description,  
                  FirstTable.product_Id,FirstTable.ProductName,FirstTable.Status,FirstTable.fullAddress,FirstTable.addressDesc,FirstTable.EnumCategoryId, FirstTable.EnumValueId, 
                      ev.name as KategoryName,FirstTable.user_Id ,FirstTable.ProductCatalogParentID,FirstTable.Email
					  ,FirstTable.Name,FirstTable.Surname,FirstTable.FatherName,FirstTable.birtday,
					  FirstTable.gender,FirstTable.profilePicture,FirstTable.address_Id,
					  FirstTable.PinNumber,FirstTable.adrID,FirstTable.potentialProductsQuantity,
					  FirstTable.userType_eV_ID,FirstTable.personID,FirstTable.productAddress_Id,FirstTable.state_eV_Id
					   from (   
                     select distinct op.Id,op.unit_price,op.quantity,op.description, op.product_Id,pc.ProductName,ev.name as Status,prc.EnumCategoryId, prc.EnumValueId ,  
                    pa.fullAddress,pa.addressDesc ,op.user_Id ,pc.ProductCatalogParentID,us.Email,person.Name,person.Surname,
					person.FatherName,person.birtday,person.gender,person.profilePicture,person.address_Id,person.PinNumber
			,adr.Id as adrID,pp.quantity as potentialProductsQuantity,us.userType_eV_ID
			  ,person.Id as personID,op.productAddress_Id,op.state_eV_Id
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
                --op.state_eV_Id= @state_eV_Id and
        op.Status=1 
                  

";
            var query3 = @"

order by tb.Name
			OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
FETCH NEXT @RecordsPerPage ROWS ONLY
    ";
            squery.Append(squeryunion.ToString());


            squery.Append(query2);
            var queryState = @"  and tb.state_eV_Id=@state_eV_Id";
            var queryProductId = @" and product_Id=@productID  ";
            var queryRoleId = @" and RoleId=@roleID ";
            var queryName = @" and  (Name like '%'+@name+'%' or Surname like '%'+@name+'%'  or FatherName like '%'+@name+'%')  ";
            //var queryVoen = @" and voen like '%'+@voen+'%' ";//voen=@voen
            var queryPinNumber = @" and  (PinNumber like '%'+@pinNumber+'%' or voen like '%'+@voen+'%' ) ";
            var queryUserType = @" and  userType_eV_ID=@usertypeEvId      ";
            var queryEnd = @"   ) as FirstTable   
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
           

            ) as tb where tb.EnumCategoryId=5";
            var queryDate = @" and op.Id in (select distinct table1.offer_Id
  from (select distinct pc.offer_Id,
           
			   dbo.dateReturn(pc.offer_Id,pc.year,pc.months_eV_Id,pc.day) as date1  from [dbo].[tblProductionCalendar]pc ,[dbo].[tblEnumValue] ev 
           where pc.months_eV_Id=ev.Id and pc.Status=1 
			-- and pc.offer_Id=@offer_Id 
			  ) 
			 table1
           
 where 

  table1.date1 between @startDate and @endDate)
                  ";
            if (startDate != 0 || endDate != 0)
            {
                squery.Append(queryDate);
            }
            squery.Append(queryEnd);
            if (ops.state_eV_Id != 0)
            {
                squery.Append(queryState);
            }
            if (!string.IsNullOrEmpty(ops.name))
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
            if (!String.IsNullOrEmpty(ops.pinNumber))
            {
                squery.Append(queryPinNumber);
            }

            //if (ops.voen != null || ops.pinNumber != null)
            //{
            //    squery.Append(queryVoen);
            //}
            squery.Append(query3);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@PageNo", ops.page);
                    command.Parameters.AddWithValue("@RecordsPerPage", ops.pageSize);
                    command.Parameters.AddWithValue("@roleID", ops.roleID);
                    command.Parameters.AddWithValue("@name", ops.name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@productID", ops.productID);
                    command.Parameters.AddWithValue("@voen", ops.voen.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@pinNumber", ops.pinNumber.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@usertypeEvId", ops.usertypeEvId);
                    command.Parameters.AddWithValue("@addressID", ops.adminID);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);

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
                            productParentName = reader.GetStringOrEmpty(29),
                            personAdress = reader.GetStringOrEmpty(30),
                            personAdressDesc = reader.GetStringOrEmpty(31),
                            userType = reader.GetStringOrEmpty(32),
                            organizationName = reader.GetStringOrEmpty(33),
                            voen = reader.GetStringOrEmpty(34),










                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
        
    } 

}


