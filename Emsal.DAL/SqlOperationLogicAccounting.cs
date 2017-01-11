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
            StringBuilder squery1 = new StringBuilder();
            var query1 = @"WITH RESULTS AS ( SELECT * , ROW_NUMBER() OVER (ORDER BY tb.Id DESC) AS rn , ROW_NUMBER() OVER (ORDER BY tb.Id ASC) AS rn_reversed FROM 
(

	select table5.* from (	 select table3.* ,ev.description as userType,fo.name as organizationName,fo.voen--,person.Name as organizationManagerName
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
				 )) as table5 where table5.EnumCategoryId=5
	
            
			 

		
             
							
						";
            var query3 = @"  select table5.* from ( select table3.* ,ev.description as userType,fo.name as organizationName,fo.voen--,person.Name as organizationManagerName
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
				)) as table5 where table5.EnumCategoryId=5
	";
            var query2 = @"   ) as tb where tb.PinNumber!='' or tb.voen!=''
                    ) SELECT *
        , CAST(rn + rn_reversed - 1 AS INT) AS total_rows
        , CAST(CASE (rn + rn_reversed - 1) % @page_size
            WHEN 0 THEN (rn + rn_reversed - 1) / @page_size
            ELSE ((rn + rn_reversed - 1) / @page_size) + 1 
            END AS INT) AS total_pages
    FROM RESULTS a
    WHERE a.rn BETWEEN 1 + ((@page_num - 1) * @page_size) AND @page_num * @page_size
    ORDER BY rn ASC ";
            squery.Append(query1);
            squery1.Append(query3);

            if (opds.roleID != 0 && opds.productID == 0 && opds.name == null && opds.contractID == 0 && opds.personID == 0)
            {
                squery.Append(" and RoleId=@RoleId");
                squery1.Append(" and RoleId=@RoleId");

            }
            else if (opds.name != null && opds.roleID == 0 && opds.productID == 0 && opds.contractID == 0 && opds.personID == 0)
            {
                squery.Append(" and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name == null && opds.contractID == 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id");
                squery1.Append("  and product_Id=@product_Id");
            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name == null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("  and contractId=@contractId"); squery1.Append("  and contractId=@contractId");
            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name == null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append("  and personID=@personID"); squery1.Append("  and personID=@personID");
            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name == null && opds.contractID == 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId"); squery1.Append("  and product_Id=@product_Id and RoleId=@RoleId");
            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name != null && opds.contractID == 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name != null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId  and contractId=@contractId and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id and RoleId=@RoleId  and contractId=@contractId and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name != null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId  and personID=@personID and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id and RoleId=@RoleId  and personID=@personID and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name != null && opds.contractID == 0 && opds.personID == 0)
            {

                squery.Append("  and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID == 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append("  and product_Id=@product_Id and personID=@personID  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id and personID=@personID  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id  and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id  and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and personID=@personID    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name == null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("  and RoleId=@RoleId      and contractId=@contractId ");
                squery1.Append("  and RoleId=@RoleId      and contractId=@contractId ");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name == null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append("  and RoleId=@RoleId      and  personID=@personID  ");

                squery1.Append("  and RoleId=@RoleId      and  personID=@personID  ");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name == null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id    and  contractId=@contractId  ");
                squery1.Append("  and product_Id=@product_Id    and  contractId=@contractId  ");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name == null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append("  and product_Id=@product_Id    and  personID=@personID  ");

                squery1.Append("  and product_Id=@product_Id    and  personID=@personID  ");

            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name == null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append("  and  contractId=@contractId   and  personID=@personID  ");
                squery1.Append("  and  contractId=@contractId   and  personID=@personID  ");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name == null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append("  and product_Id=@product_Id  and contractId=@contractId and  personID=@personID and RoleId=@RoleId    ");
                squery1.Append("  and product_Id=@product_Id  and contractId=@contractId and  personID=@personID and RoleId=@RoleId    ");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

                squery1.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name != null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID and RoleId=@RoleId   and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and personID=@personID and RoleId=@RoleId   and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name != null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and RoleId=@RoleId and personID=@personID and product_Id=@product_Id   and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and RoleId=@RoleId and personID=@personID and product_Id=@product_Id   and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name == null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  ");
                squery1.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  ");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name == null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID  and RoleId=@RoleId  and contractId=@contractId  ");
                squery1.Append(" and personID=@personID  and RoleId=@RoleId  and contractId=@contractId  ");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name == null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  ");
                squery1.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  ");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name == null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append(" and RoleId=@RoleId  and product_Id=@product_Id  and contractId=@contractId  ");
                squery1.Append(" and RoleId=@RoleId  and product_Id=@product_Id  and contractId=@contractId  ");

            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and personID=@personID    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID and product_Id=@product_Id    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and personID=@personID and product_Id=@product_Id    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append(" and contractId=@contractId and product_Id=@product_Id    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and contractId=@contractId and product_Id=@product_Id    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name != null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID and RoleId=@RoleId    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and personID=@personID and RoleId=@RoleId    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name != null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append(" and contractId=@contractId and RoleId=@RoleId    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and contractId=@contractId and RoleId=@RoleId    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            squery.Append(" union ");
            squery.Append(squery1.ToString());

            squery.Append(query2);
            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {


                    command.Parameters.AddWithValue("@monintoring_eV_Id", opds.monintoring_eV_Id);

                    command.Parameters.AddWithValue("@page_num", opds.page);

                    command.Parameters.AddWithValue("@page_size", opds.pageSize);

                    command.Parameters.AddWithValue("@Name", (opds.name ?? (object)DBNull.Value));


                    command.Parameters.AddWithValue("@product_Id", opds.productID);

                    command.Parameters.AddWithValue("@userID", opds.userID);

                    command.Parameters.AddWithValue("@RoleId", opds.roleID);

                    command.Parameters.AddWithValue("@personID", opds.personID);

                    command.Parameters.AddWithValue("@contractId", opds.contractID);

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
                            roleName = reader.GetStringOrEmpty(32),
                            roleDescription = reader.GetStringOrEmpty(33),





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
            StringBuilder squery1 = new StringBuilder();
            var query1 = @" 
		 
	 
select COUNT(*) as count from (	select table5.* from (	 select table3.* ,ev.description as userType,fo.name as organizationName,fo.voen--,person.Name as organizationManagerName
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
				 )) as table5 where table5.EnumCategoryId=5
	
              
			 

		
	
          
			
							
            ";
            var query3 = @"
select table5.* from ( select table3.* ,ev.description as userType,fo.name as organizationName,fo.voen--,person.Name as organizationManagerName
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
				)) as table5 where table5.EnumCategoryId=5";
            var query2 = @"  ) as tb where tb.EnumCategoryId=5";
            squery.Append(query1);

            squery1.Append(query3);

            if (opds.roleID != 0 && opds.productID == 0 && opds.name == null && opds.contractID == 0 && opds.personID == 0)
            {
                squery.Append(" and RoleId=@RoleId");
                squery1.Append(" and RoleId=@RoleId");

            }
            else if (opds.name != null && opds.roleID == 0 && opds.productID == 0 && opds.contractID == 0 && opds.personID == 0)
            {
                squery.Append(" and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name == null && opds.contractID == 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id");
                squery1.Append("  and product_Id=@product_Id");
            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name == null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("  and contractId=@contractId"); squery1.Append("  and contractId=@contractId");
            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name == null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append("  and personID=@personID"); squery1.Append("  and personID=@personID");
            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name == null && opds.contractID == 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId"); squery1.Append("  and product_Id=@product_Id and RoleId=@RoleId");
            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name != null && opds.contractID == 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name != null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId  and contractId=@contractId and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id and RoleId=@RoleId  and contractId=@contractId and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name != null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId  and personID=@personID and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id and RoleId=@RoleId  and personID=@personID and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name != null && opds.contractID == 0 && opds.personID == 0)
            {

                squery.Append("  and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID == 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append("  and product_Id=@product_Id and personID=@personID  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id and personID=@personID  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id  and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("  and product_Id=@product_Id  and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append("    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and personID=@personID    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name == null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("  and RoleId=@RoleId      and contractId=@contractId ");
                squery1.Append("  and RoleId=@RoleId      and contractId=@contractId ");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name == null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append("  and RoleId=@RoleId      and  personID=@personID  ");

                squery1.Append("  and RoleId=@RoleId      and  personID=@personID  ");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name == null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append("  and product_Id=@product_Id    and  contractId=@contractId  ");
                squery1.Append("  and product_Id=@product_Id    and  contractId=@contractId  ");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name == null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append("  and product_Id=@product_Id    and  personID=@personID  ");

                squery1.Append("  and product_Id=@product_Id    and  personID=@personID  ");

            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name == null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append("  and  contractId=@contractId   and  personID=@personID  ");
                squery1.Append("  and  contractId=@contractId   and  personID=@personID  ");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name == null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append("  and product_Id=@product_Id  and contractId=@contractId and  personID=@personID and RoleId=@RoleId    ");
                squery1.Append("  and product_Id=@product_Id  and contractId=@contractId and  personID=@personID and RoleId=@RoleId    ");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

                squery1.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name != null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID and RoleId=@RoleId   and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and personID=@personID and RoleId=@RoleId   and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name != null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and RoleId=@RoleId and personID=@personID and product_Id=@product_Id   and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and RoleId=@RoleId and personID=@personID and product_Id=@product_Id   and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name == null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  ");
                squery1.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  ");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name == null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID  and RoleId=@RoleId  and contractId=@contractId  ");
                squery1.Append(" and personID=@personID  and RoleId=@RoleId  and contractId=@contractId  ");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name == null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  ");
                squery1.Append(" and personID=@personID  and product_Id=@product_Id  and contractId=@contractId  ");

            }
            else if (opds.productID != 0 && opds.roleID != 0 && opds.name == null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append(" and RoleId=@RoleId  and product_Id=@product_Id  and contractId=@contractId  ");
                squery1.Append(" and RoleId=@RoleId  and product_Id=@product_Id  and contractId=@contractId  ");

            }
            else if (opds.productID == 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and personID=@personID    and contractId=@contractId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID and product_Id=@product_Id    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and personID=@personID and product_Id=@product_Id    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID != 0 && opds.roleID == 0 && opds.name != null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append(" and contractId=@contractId and product_Id=@product_Id    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and contractId=@contractId and product_Id=@product_Id    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name != null && opds.contractID == 0 && opds.personID != 0)
            {

                squery.Append(" and personID=@personID and RoleId=@RoleId    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and personID=@personID and RoleId=@RoleId    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (opds.productID == 0 && opds.roleID != 0 && opds.name != null && opds.contractID != 0 && opds.personID == 0)
            {

                squery.Append(" and contractId=@contractId and RoleId=@RoleId    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
                squery1.Append(" and contractId=@contractId and RoleId=@RoleId    and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            squery.Append(" union ");
            squery.Append(squery1.ToString());
            squery.Append(query2);

            using (var connection = new SqlConnection(DBUtil.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(squery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@monintoring_eV_Id", opds.monintoring_eV_Id);



                    command.Parameters.AddWithValue("@Name", (opds.name ?? (object)DBNull.Value));


                    command.Parameters.AddWithValue("@product_Id", opds.productID);

                    command.Parameters.AddWithValue("@userID", opds.userID);

                    command.Parameters.AddWithValue("@RoleId", opds.roleID);

                    command.Parameters.AddWithValue("@personID", opds.personID);

                    command.Parameters.AddWithValue("@contractId", opds.contractID);
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
            //if (ops.roleID != 0)
            //{
            //    squery.Append(" and roleId=@roleId");
            //}
            //if (ops.name != null)
            //{
            //    squery.Append(" and personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%'");
            //}
            //if (ops.productID != 0)
            //{

            //    squery.Append("  and product_Id=@product_Id");
            //}
            if (ops.roleID != 0 && ops.productID == 0 && ops.name == null)
            {
                squery.Append(" and RoleId=@RoleId");
            }
            else if (ops.name != null && ops.roleID == 0 && ops.productID == 0)
            {
                squery.Append(" and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
            }
            else if (ops.productID != 0 && ops.roleID == 0 && ops.name == null)
            {

                squery.Append("  and product_Id=@product_Id");
            }
            else if (ops.productID != 0 && ops.roleID != 0 && ops.name == null)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId");
            }
            else if (ops.productID != 0 && ops.roleID != 0 && ops.name != null)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (ops.productID == 0 && ops.roleID != 0 && ops.name != null)
            {

                squery.Append("  and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (ops.productID != 0 && ops.roleID == 0 && ops.name != null)
            {

                squery.Append("  and product_Id=@product_Id and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

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
            if (ops.roleID != 0 && ops.productID == 0 && ops.name == null)
            {
                squery.Append(" and RoleId=@RoleId");
            }
            else if (ops.name != null && ops.roleID == 0 && ops.productID == 0)
            {
                squery.Append(" and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");
            }
            else if (ops.productID != 0 && ops.roleID == 0 && ops.name == null)
            {

                squery.Append("  and product_Id=@product_Id");
            }
            else if (ops.productID != 0 && ops.roleID != 0 && ops.name == null)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId");
            }
            else if (ops.productID != 0 && ops.roleID != 0 && ops.name != null)
            {

                squery.Append("  and product_Id=@product_Id and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (ops.productID == 0 && ops.roleID != 0 && ops.name != null)
            {

                squery.Append("  and RoleId=@RoleId  and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

            }
            else if (ops.productID != 0 && ops.roleID == 0 && ops.name != null)
            {

                squery.Append("  and product_Id=@product_Id and (personName like '%'+@Name+'%' or Surname like '%'+@Name+'%' or FatherName like '%'+@Name+'%' or organizationName like '%'+@Name+'%' or voen like '%'+@Name+'%' or PinNumber like '%'+@Name+'%')");

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
    }
}
