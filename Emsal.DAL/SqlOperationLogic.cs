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
        public List<OfferProducts> GetOfferProducts()
        {
            var result = new List<OfferProducts>();
            var query = @" select distinct FirstTable.*,pc.ProductName as parentName from (   
                      select  op.product_Id,pc.ProductName,pc.ProductCatalogParentID,ev.Id as enumValueID
                     from [dbo].[tblOffer_Production] op   
                     left join [dbo].[tblProductCatalog] pc  on op.product_Id=pc.Id   and pc.Status=1
                 
                    left join [dbo].[tblEnumValue] ev on op.state_eV_Id=ev.Id    and ev.Status=1
                    
                     where  
					 --op.isSelected=1 and
					  op.Status=1
					  and ev.Id=2 
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
                            parentName = reader.GetStringOrEmpty(4),


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
				  and op.monitoring_eV_Id=10118  
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
	and user_Id=20687
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
				  and op.monitoring_eV_Id=10118  
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
		and user_Id=20687
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
                            organizationName=reader.GetStringOrEmpty(29),


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
						  ev.Id!=13 and op.isSelected=1  and op.Status=1
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
            var query = @"  select p.Name,p.Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name,p.PinNumber ,'' as voen
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
select FirstTable.* ,aunit.Name as ParantName from (
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

							 union 

							 select FirstTable.* ,aunit.Name as ParantName from (
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
        public List<ProductionCalendarDetail> GetProductionCalendarDemandId(Int64 demand)
        {
            var result = new List<ProductionCalendarDetail>();
            var query = @"  select  table1.type_eV_Id,table1.Production_Id,table1.year,table1.day,table1.oclock,
			 table1.partOfyear,table1.months_eV_Id,table1.offer_Id,table1.demand_Id,
			 table1.Production_type_eV_Id,table1.price,table1.transportation_eV_Id,
			 table1.quantity,table1.MonthName,table1.MonthDescription, ev.name as TypeName, ev.description as TypeDescription,
			 table1.Id
			  from (
			 select pc.Id, pc.type_eV_Id,pc.Production_Id,pc.year,pc.day,pc.oclock,pc.partOfyear,
             pc.months_eV_Id,pc.offer_Id,
              pc.demand_Id,pc.Production_type_eV_Id,pc.price,pc.transportation_eV_Id,pc.quantity, ev.name as MonthName,ev.description as MonthDescription from [dbo].[tblProductionCalendar]pc ,[dbo].[tblEnumValue] ev 
             where pc.months_eV_Id=ev.Id and pc.Status=1 and pc.demand_Id=@demand_Id 
			   ) table1,
             [dbo].[tblEnumValue] ev 
			 where table1.type_eV_Id=ev.Id  ";
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
					  left join [dbo].[tblProductCatalog] pc on pc.Id=table2.ProductCatalogParentID	";

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

        public List<tblProductCatalog> GetProducListByUserID(Int64 userID)
        {
            var result = new List<tblProductCatalog>();
            var query = @" 
 select * from tblProductCatalog pc where 
  pc.Id in(select pp.product_Id 
  from tblPotential_Production pp where pp.user_Id=@userID and pp.Status=1)  
  union 
    select * from tblProductCatalog pc where 
  pc.Id in(select op.product_Id 
  from tblOffer_Production op where op.user_Id=@userID and op.Status=1)   
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
			'Offer' as type ,Sum(quantity) as OfferDemand,table1.unit_price from (
			select pc.Id,pc.ProductCatalogParentID, pc.ProductName,op.Id as ProducitonID,op.quantity
			,ev.description
		,price.unit_price from dbo.tblOffer_Production op
         join dbo.tblProductCatalog pc on op.product_Id=pc.Id and pc.Status=1 
         join [dbo].[tblProductionControl] prc on op.Id=prc.Offer_Production_Id 
		 and prc.Production_type_eV_Id=3 and prc.Status=1 and prc.EnumCategoryId  =5
         left  join  dbo.tblEnumValue ev on ev.Id=prc.EnumValueId and ev.Status=1
        left join dbo.tblEnumCategory ec on ec.Id=ev.enumCategory_enumCategoryId and ec.Status=1 and ec.Id=5
		  join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
         where  op.Status=1 		
		  
		 ) table1,
         dbo.tblProductCatalog pc where table1.ProductCatalogParentID=pc.ID
		-- and table1.adressID=@adressID
         group by table1.Id,table1.ProductCatalogParentID, table1.ProductName ,pc.ProductName,
	    table1.description,table1.unit_price

union all 
        select distinct table1.Id,table1.ProductCatalogParentID, pc.ProductName as ParantName , table1.ProductName,
         table1.description,'Demand' as type, Sum(quantity) as OfferDemand,table1.unit_price
         from (select pc.Id,pc.ProductCatalogParentID, pc.ProductName,dp.quantity,
		 ev.description 
		 ,price.unit_price
		  from [dbo].[tblDemand_Production] dp
		  join dbo.tblProductCatalog pc on dp.product_Id=pc.Id and pc.Status=1 
		  join [dbo].[tblProductionControl] prc on dp.Id=prc.Demand_Production_Id 
		  and prc.Production_type_eV_Id=28 and prc.Status=1 and prc.EnumCategoryId=5
		  left join  dbo.tblEnumValue ev on ev.Id=prc.EnumValueId and ev.Status=1
          left join dbo.tblEnumCategory ec on ec.Id=ev.enumCategory_enumCategoryId and ec.Status=1 and ec.Id=5
          join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
          where  dp.Status=1) table1,
          dbo.tblProductCatalog pc where table1.ProductCatalogParentID=pc.ID
		 --  and table1.adressID=@adressID
          group by table1.Id,table1.ProductCatalogParentID, table1.ProductName ,pc.ProductName,
          table1.description ,table1.unit_price
          order by ProductName asc


		  ---843498,05 kq


";

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@adressID", adressID);
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
        public List<UserInfo> GetPersonalinformationByRoleId(Int64 roleID, Int64 userId)
        {
            var result = new List<UserInfo>();
            var query = @"
select FirstTable.* ,aunit.Name as ParantName from (
 select  p.Id as personId,p.Name as PersonName,p.Surname,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.RoleId,r.Name as RoleName,adr.adminUnit_Id,au.Name as AdminUnitName,au.ParentID,p.PinNumber ,'' as voen--,pc.ProductName
							 ,p.FatherName,p.birtday,p.gender,p.profilePicture,p.LastUpdatedStatus,p.createdDate,p.createdUser,r.Description as roleDescription,
  '' as HuquqiSexsAdi
                             from dbo.tblUser u ,tblPerson p,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 dbo.tblPRM_AdminUnit au
                             where u.Id=p.UserId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
							 and u.userType_eV_ID=24
                              --and r.Id=15
							 and u.Status=1 and p.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 
							 and adr.adminUnit_Id=au.Id
						    and r.Id = @RoleId
							 and au.Status=1
                          
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 
							 left join   tblOffer_Production AS oproduct ON FirstTable.UserId = oproduct.user_Id
							 left join [dbo].[tblProductAddress] pa on oproduct.productAddress_Id=pa.Id
              where oproduct.state_eV_Id=13 and oproduct.monitoring_eV_Id=10118 and oproduct.Status=1
							 union 

							 select FirstTable.* ,aunit.Name as ParantName from (
 select p.Id as personId, p.Name as PersonName,p.Surname as Surname ,adr.fullAddress, u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.RoleId,r.Name as RoleName,adr.adminUnit_Id,au.Name as AdminUnitName,au.ParentID,'' as PinNumber , fo.voen --,pc.ProductName
							 	 ,p.FatherName,p.birtday,p.gender,p.profilePicture,p.LastUpdatedStatus,p.createdDate,p.createdUser,r.Description as roleDescription,
                      fo.name as HuquqiSexsAdi
                             from dbo.tblUser u ,[dbo].[tblForeign_Organization] fo,tblUserRole ur,dbo.tblAddress adr,dbo.tblEnumValue ev,dbo.tblRole r,
							 dbo.tblPRM_AdminUnit au,tblPerson p
							                              where u.Id=fo.userId and u.Id=ur.UserId and u.Id=adr.user_Id
                             and ur.RoleId=r.Id
                             and u.userType_eV_ID=ev.Id
							 and u.userType_eV_ID=50
                             -- and r.Id=15
							 and u.Status=1 and fo.Status=1 and ur.Status=1 and adr.Status=1
							 and ev.Status=1 and r.Status=1 
							 and adr.adminUnit_Id=au.Id
							 and u.Id=p.UserId and p.Status=1
						  and r.Id = @RoleId
							 and au.Status=1
                           
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 
							 left join tblOffer_Production AS oproduct ON FirstTable.UserId = oproduct.user_Id
	                        left join [dbo].[tblProductAddress] pa on oproduct.productAddress_Id=pa.Id
	                        where oproduct.state_eV_Id=13 and oproduct.monitoring_eV_Id=10118 and oproduct.Status=1;



						



							 
";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", roleID);
                    command.Parameters.AddWithValue("@UserId", userId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new UserInfo()
                        {
                            personId = reader.GetInt64OrDefaultValue(0),
                            name = reader.GetStringOrEmpty(1),
                            surname = reader.GetStringOrEmpty(2),

                            fullAddress = reader.GetStringOrEmpty(3),
                            email = reader.GetStringOrEmpty(4),
                            userID = reader.GetInt64OrDefaultValue(5),
                            userTypeID = reader.GetInt64OrDefaultValue(6),
                            userType = reader.GetStringOrEmpty(7),
                            roleID = reader.GetInt64OrDefaultValue(8),
                            roleName = reader.GetStringOrEmpty(9),
                            adminUnitID = reader.GetInt64OrDefaultValue(10),
                            adminUnitName = reader.GetStringOrEmpty(11),
                            pinNumber = reader.GetStringOrEmpty(13),
                            fatherName = reader.GetStringOrEmpty(15),
                            birtday = reader.GetInt64OrDefaultValue(16),
                            gender = reader.GetStringOrEmpty(17),
                            profilPicture = reader.GetStringOrEmpty(18),
                            lastUpdateStatus = reader.GetInt64OrDefaultValue(19),
                            createdDate = reader.GetInt64OrDefaultValue(20),
                            roleDescription = reader.GetStringOrEmpty(21),
                            OrganisationName = reader.GetStringOrEmpty(22),

                            parantName = reader.GetStringOrEmpty(23),







                        });
                    }
                }
                connection.Close();
            }

            return result;
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
        public List<DemandOfferDetail> GetDemandProductionAmountOfEachProduct()
        {
            var result = new List<DemandOfferDetail>();
            var query = @"  select table1.Id, pc.ProductName as ParantName , Sum(quantity) as totalQuantity,table1.ProductCatalogParentID,table1.ProductName,table1.unit_price,table1.name as kategoryName ,table1.enumKategorID
from (select pc.Id,pc.ProductCatalogParentID, pc.ProductName,dp.quantity,ev.name ,price.unit_price ,ev.enumCategory_enumCategoryId as enumKategorID
from [dbo].[tblDemand_Production] dp
left  join dbo.tblProductCatalog pc on dp.product_Id=pc.Id and pc.Status=1 
left join [dbo].[tblProductionControl] prc on dp.Id=prc.Demand_Production_Id and prc.Production_type_eV_Id=28 and prc.Status=1
 left join  dbo.tblEnumValue ev on ev.Id=prc.EnumValueId and ev.Status=1
left  join dbo.tblEnumCategory ec on ec.Id=ev.enumCategory_enumCategoryId and ec.Status=1 --and ec.Id=5

left join [dbo].[tblProductPrice] price on pc.Id=price.productId and price.Status=1
where  dp.Status=1 and dp.state_eV_Id=2) as table1
left join   dbo.tblProductCatalog pc on table1.ProductCatalogParentID=pc.ID and pc.Status=1
  group by table1.Id,table1.ProductCatalogParentID, table1.ProductName , pc.ProductName,
 
 table1.name,table1.ProductName ,table1.unit_price,table1.enumKategorID
 order by table1.Id asc
                         ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
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
                      ) as FirstTable   
                        left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1
      ) secondTable
     
                  left join [dbo].[tblProductCatalog] pc on pc.Id=secondTable.ProductCatalogParentID) as tb
              where tb.EnumCategoryId=5";
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
            if (ops.prodcutID != 0 && ops.organizationName == null)
            {
                squery.Append(" and product_Id=@product_Id");
            }
            else   if (ops.prodcutID == 0 && ops.organizationName != null)
            {
                squery.Append(" and name like '%'+@name+'%'");
            }
            else   if (ops.prodcutID != 0 && ops.organizationName != null)
            {
                squery.Append(" and product_Id=@product_Id and name like '%'+@name+'%'");
            }
            // where ProductName like '%'+@Name+'%' or productParentName like '%'+@Name+'%'
            //if (ops.person != null)
            //{
            //    squery.Append(" and personName like '%' + @person + '%' or Surname like '%' + @person + '%' or FatherName like '%' + @person + '%' ");
            //}
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
                      ) as FirstTable   
                        left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1
      ) secondTable
     
                  left join [dbo].[tblProductCatalog] pc on pc.Id=secondTable.ProductCatalogParentID) as tb
where tb.EnumCategoryId=5
";
            squery.Append(query);
            if (ops.prodcutID != 0 && ops.organizationName == null)
            {
                squery.Append(" and product_Id=@product_Id");
            }
            else if (ops.organizationName != null && ops.prodcutID == 0)
            {
                squery.Append(" and name like '%'+@name+'%'");
            }
            else if (ops.prodcutID != 0 && ops.organizationName != null)
            {
                squery.Append(" and product_Id=@product_Id and name like '%'+@name+'%'");
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


        public List<ProductionDetail> GetOfferProductionDetailistForEValueId_OP(OfferProductionDetailSearch ops)
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
                     join [dbo].[tblPerson] person on us.Id=person.UserId and person.Status=1 
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
               left join tblUserRole ur on table2.user_Id=ur.UserId and ur.Status=1
			 left  join tblProductAddress padr on table2.productAddress_Id=padr.Id and padr.Status=1
			  left join tblPRM_AdminUnit au on padr.adminUnit_Id =au.Id 
	   
           
		   	   and  au.Id 
			   in (
							select Id from cte
							 		)
           

            ) as tb where tb.EnumCategoryId=5

";
            var query3 = @")  SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";
            squery.Append(squeryunion.ToString());
            var queryadminID = @"  WHERE Id=@addressID ";
          
            squery.Append(query2);
            var queryProductId = @" and product_Id=@productID  ";
            var queryRoleId = @" and RoleId=@roleID ";
            var queryName = @" and  (Name like '%'+@name+'%' or Surname like '%'+@name+'%'  or FatherName like '%'+@name+'%')  ";
            //var queryVoen = @" and voen like '%'+@voen+'%' ";//voen=@voen
            var queryPinNumber = @" and  (PinNumber like '%'+@pinNumber+'%' or voen like '%'+@voen+'%' ) ";
            var queryUserType = @" and  userType_eV_ID=@usertypeEvId      ";
           
            if (ops.name!=null)
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
            if (ops.pinNumber != null || ops.voen!=null)
            {
                squery.Append(queryPinNumber);
            }
            if (ops.adminID!=0)
            {
                squeryunion.Append(queryadminID);
            }
            //if (ops.voen != null || ops.pinNumber!=null)
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
                          
                            personID=reader.GetInt64OrDefaultValue(26),
                            productAddressID=reader.GetInt64OrDefaultValue(27),
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

        public long GetOfferProductionDetailistForEValueId_OPC(OfferProductionDetailSearch ops)
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


        public List<ProductionDetail> GetPotensialProductionDetailistForEValueId_OP(GetDemandProductionDetailistForEValueIdSearch ops)
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
            if (ops.prodcutID!=0 && ops.person==null)
            {
                squery.Append("  and product_Id=@product_Id");
            }
            if (ops.prodcutID == 0 && ops.person != null)
            {
                squery.Append("  and (personName like '%'+@person+'%' or Surname like '%'+@person+'%' or FatherName like '%'+@person+'%')");
            }
            if (ops.prodcutID != 0 && ops.person != null)
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
                    command.Parameters.AddWithValue("@person", ops.person);
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
        public long GetPotensialProductionDetailistForEValueId_OPC(GetDemandProductionDetailistForEValueIdSearch ops)
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
            if (ops.prodcutID != 0 && ops.person == null)
            {
                squery.Append("  and product_Id=@product_Id");
            }
            if (ops.prodcutID == 0 && ops.person != null)
            {
                squery.Append("  and (personName like '%'+@person+'%' or Surname like '%'+@person+'%' or FatherName like '%'+@person+'%')");
            }
            if (ops.prodcutID != 0 && ops.person != null)
            {
                squery.Append(" and product_Id=@product_Id  and (personName like '%'+@person+'%' or Surname like '%'+@person+'%'or FatherName like '%'+@person+'%')");
            }
           
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                  
                    command.Parameters.AddWithValue("@person", ops.person);
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
						  ev.Id!=13 and op.isSelected=1  and op.Status=1
 
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
            if (ops.productID!=0)
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
						  ev.Id!=13 and op.isSelected=1  and op.Status=1
                      ) as FirstTable   
                       left join [dbo].[tblEnumValue] ev on FirstTable.EnumValueId=ev.Id and ev.Status=1 
					  
					   ) secondTable

					      left join [dbo].[tblProductCatalog] pc on pc.Id=secondTable.ProductCatalogParentID
 ) as tb
  where tb.EnumCategoryId=5
                         ";
            squery.Append(query);
            //if (ops.productName != null && ops.roleID == 0 && ops.userType_eV_ID == 0)
            //{
            //    squery.Append(" and (ProductName Like '%' + @productName + '%' or ProductParentName Like '%' + @productName + '%') ");
            //}
            //else if (ops.roleID != 0 && ops.productName == null && ops.userType_eV_ID == 0)
            //{
            //    squery.Append(" and RoleId=@RoleId");
            //}
            //else if (ops.userType_eV_ID != 0 && ops.roleID == 0 && ops.productName == null)
            //{
            //    squery.Append(" and userType_eV_ID=@userType_eV_ID");
            //}
            //else if (ops.roleID != 0 && ops.productName == null && ops.userType_eV_ID != 0)
            //{
            //    squery.Append(" and RoleId=@RoleId and userType_eV_ID=@userType_eV_ID");
            //}
            //else if (ops.roleID != 0 && ops.productName != null && ops.userType_eV_ID == 0)
            //{
            //    squery.Append(" and RoleId=@RoleId  and (ProductName Like '%' + @productName + '%' or ProductParentName Like '%' + @productName + '%') ");
            //}
            //else if (ops.roleID != 0 && ops.productName != null && ops.userType_eV_ID != 0)
            //{
            //    squery.Append(" and RoleId=@RoleId and userType_eV_ID=@userType_eV_ID  and (ProductName Like '%' + @productName + '%' or ProductParentName Like '%' + @productName + '%') ");
            //}
            //else if (ops.roleID == 0 && ops.productName != null && ops.userType_eV_ID != 0)
            //{
            //    squery.Append(" and userType_eV_ID=@userType_eV_ID  and (ProductName Like '%' + @productName + '%' or ProductParentName Like '%' + @productName + '%') ");
            //}
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

        public List<UserInfo> GetPotensialUserList_OP(int page, int pageSize)
        {
            var result = new List<UserInfo>();
            var query = @"  WITH RESULTS AS ( SELECT * , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed FROM 
(select p.Name,p.Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name as roleName,p.PinNumber ,'' as voen
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
							 ) as tb where tb.PinNumber!='' or tb.voen!=''
 )  SELECT *
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
                    command.Parameters.AddWithValue("@page_num", page);
                    command.Parameters.AddWithValue("@page_size", pageSize);
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
        public Int64 GetPotensialUserList_OPC()
        {
            Int64 count = 0;
            var query = @" select COUNT(*) as Count from(select p.Name,p.Surname,adr.fullAddress,u.Email,u.Id as userId,u.userType_eV_ID,ev.name as userType,
                             ur.Id as userRoleId ,ur.RoleId,r.Id ,r.Name as roleName,p.PinNumber ,'' as voen
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
 
                         ";
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        while (reader.Read())
                        {
                            count = Convert.ToInt32(reader["Count"]);
                        }
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
select FirstTable.* ,aunit.Name as ParantName from (
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
							 and adr.adminUnit_Id=au.Id
						and adr.adminUnit_Id=@adminUnit_Id
							 and au.Status=1
                           
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 

							 union 

							 select FirstTable.* ,aunit.Name as ParantName from (
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
							 and adr.adminUnit_Id=au.Id
						    and adr.adminUnit_Id=@adminUnit_Id
							 and au.Status=1
                          
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 
 ) as tb where tb.PinNumber!=' ' or tb.voen!=' '";

            var query2 = @")  SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";
            squery.Append(query1);
            if (ops.roleID != 0)
            {
                squery.Append(" and RoleId=@RoleId");
            }
            if (ops.name != null)
            {
                squery.Append(" and PersonName Like  '%' + @name + '%' or Surname Like  '%' + @name + '%'  ");
            }
            if (ops.adminUnitName != null)
            {
                squery.Append(" and AdminUnitName Like '%' + @AdminUnitName + '%' ");
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
                    command.Parameters.AddWithValue("@name", ops.name);
                    command.Parameters.AddWithValue("@AdminUnitName", ops.adminUnitName);

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
        public Int64 GetPotensialUserForAdminUnitIdList_OPC()
        {
            Int64 count = 0;
            var query = @" select COUNT(*) as Count from(
select FirstTable.* ,aunit.Name as ParantName from (
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
						and adr.adminUnit_Id=@adminUnit_Id
							 and au.Status=1
                           
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 

							 union 

							 select FirstTable.* ,aunit.Name as ParantName from (
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
						    and adr.adminUnit_Id=@adminUnit_Id
							 and au.Status=1
                          
							)
							 as FirstTable 
							 left join  dbo.tblPRM_AdminUnit aunit on FirstTable.ParentID=aunit.Id 
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

        public List<ProductionDetail> GetDemandProductDetailInfoForAccounting_OP(GetDemandProductionDetailistForEValueIdSearch ops,  Int64 year, Int64 partOfYear)
        {
            var result = new List<ProductionDetail>();
            StringBuilder squery = new StringBuilder();
            var query = @"  WITH RESULTS AS ( SELECT * , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed FROM 
( select table1.* ,pc.ProductName as productParentName,ev.name as kategoryName ,quantity*unit_price as totalPrice,pc.Id as productID from(    select fo.name, op.Id as ProductionId, pc.ProductName, pc.Id,pc.ProductCatalogParentID,
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
					  )as table1
					   left join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1
					     left join [dbo].[tblEnumValue] ev on table1.EnumValueId=ev.Id and ev.Status=1
	where 	 enumCategory_enumCategoryId=5 
	) as tb 
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
            squery.Append(query);
            if (ops.prodcutID!=0)
            {
                squery.Append(" where productID=@productID");
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
            if (ops.adressName != null && ops.productName==null)
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
					  )as table1
					   left join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1
					     left join [dbo].[tblEnumValue] ev on table1.EnumValueId=ev.Id and ev.Status=1
	where 	 enumCategory_enumCategoryId=5 
	)as tb 
";
            var query1 = @" group by tb.productId  ";
            squery.Append(query);
            if (ops.prodcutID!=0)
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
            var query = @"  select COUNT(*) as Count from(  select table1.* ,pc.ProductName as productParentName,ev.name as kategoryName ,quantity*unit_price as totalPrice,pc.Id as productID from(    select fo.name, op.Id as ProductionId, pc.ProductName, pc.Id,pc.ProductCatalogParentID,
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
					  )as table1
					   left join [dbo].[tblProductCatalog] pc on pc.Id=table1.ProductCatalogParentID and pc.Status=1
					     left join [dbo].[tblEnumValue] ev on table1.EnumValueId=ev.Id and ev.Status=1
	where 	 enumCategory_enumCategoryId=5 
	) as tb ";
            squery.Append(query);
            if (ops.prodcutID != 0)
            {
                squery.Append(" where productID=@productID");
            }

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@state_eV_Id", ops.state_eV_Id);
                    command.Parameters.AddWithValue("@productID", ops.prodcutID);
                    
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@partOfYear", partOfYear);
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
            , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn
            , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed
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
					  ,tblUser u
			 ,tblUserRole ur
                      where   
      op.state_eV_Id= 2  and op.Status=1  and  op.product_Id=pc.Id   and pc.Status=1 
	  and op.productAddress_Id=pa.Id  and pa.Status=1
 and op.user_Id=u.Id and u.Status=1 
       and u.Id=ur.UserId and ur.Status=1
	   and ur.RoleId= @RoleId
	 --  and ur.RoleId in (11,15)

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
                            // roleID=reader.GetInt64OrDefaultValue(8),



                        });
                    }
                }
                connection.Close();
            }

            return result;
        }


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

            if (ops.adminID != 0)
            {
                sQuery.Append("  and  dbo.splitstring(pa.fullAddressId)=@adminID ");

            }
            if (ops.productID != 0)
            {
                sQuery.Append(" and op.product_Id =@productID ");
            }
            if (ops.roleID != 0)
            {
                sQuery.Append("  and ur.RoleId=  @RoleID ");
            }


            if (ops.state_eV_Id != 0)
            {
                sQuery.Append("  and  op.state_eV_Id= @state_eV_Id ");
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

        #endregion



    }

}


