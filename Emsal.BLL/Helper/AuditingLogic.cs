using Emsal.DAL;
using Emsal.DAL.CodeObjects;
using Emsal.Utility.CustomObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Emsal.BLL.Helper
{
    public class AuditingLogic
    {

        public static T SetAuditingInfo<T>(BaseInput baseinput, int crudOperation, T t) //where T : Audit
        {
            Object genericObj = null;

            if (typeof(T) == typeof(tblProductCatalog))
            {
                tblProductCatalog item = (tblProductCatalog)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblProductCatalogControl))
            {

                tblProductCatalogControl item = (tblProductCatalogControl)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createDate = DateTime.Now.getInt64Date();
                    item.createUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updateDate = DateTime.Now.getInt64Date();
                    item.updateUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updateDate = DateTime.Now.getInt64Date();
                    item.updateUser = baseinput.userName;
                }
                genericObj = item;
            }

            else if (typeof(T) == typeof(tblEnumValue))
            {

                tblEnumValue item = (tblEnumValue)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }

            else if (typeof(T) == typeof(tblEnumCategory))
            {

                tblEnumCategory item = (tblEnumCategory)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }

                genericObj = item;
            }
            else if (typeof(T) == typeof(tblPerson))
            {

                tblPerson item = (tblPerson)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }

            else if (typeof(T) == typeof(tblUser))
            {

                tblUser item = (tblUser)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblProductionControl))
            {

                tblProductionControl item = (tblProductionControl)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createDate = DateTime.Now.getInt64Date();
                    item.createUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updateDate = DateTime.Now.getInt64Date();
                    item.updateUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updateDate = DateTime.Now.getInt64Date();
                    item.updateUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblProduction_Calendar))
            {

                tblProduction_Calendar item = (tblProduction_Calendar)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }

            else if (typeof(T) == typeof(tblProductAddress))
            {

                tblProductAddress item = (tblProductAddress)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }

            else if (typeof(T) == typeof(tblProduct_Document))
            {

                tblProduct_Document item = (tblProduct_Document)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }

            else if (typeof(T) == typeof(tblDemand_Production))
            {

                tblDemand_Production item = (tblDemand_Production)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
         
            else if (typeof(T) == typeof(tblProduction_Document))
            {

                tblProduction_Document item = (tblProduction_Document)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }

            else if (typeof(T) == typeof(tblComMessage))
            {

                tblComMessage item = (tblComMessage)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }


            else if (typeof(T) == typeof(tblPRM_Thoroughfare))
            {

                tblPRM_Thoroughfare item = (tblPRM_Thoroughfare)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblAddress))
            {

                tblAddress item = (tblAddress)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblCommunication))
            {

                tblCommunication item = (tblCommunication)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblOffer_Production))
            {

                tblOffer_Production item = (tblOffer_Production)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblPotential_Production))
            {

                tblPotential_Production item = (tblPotential_Production)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblAnnouncement))
            {

                tblAnnouncement item = (tblAnnouncement)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblEmployee))
            {

                tblEmployee item = (tblEmployee)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblRole))
            {

                tblRole item = (tblRole)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblExpertise))
            {

                tblExpertise item = (tblExpertise)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblTitle))
            {

                tblTitle item = (tblTitle)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblParty))
            {

                tblParty item = (tblParty)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblOrganization))
            {

                tblOrganization item = (tblOrganization)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblForeign_Organization))
            {

                tblForeign_Organization item = (tblForeign_Organization)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date(); ;
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblUserRole))
            {

                tblUserRole item = (tblUserRole)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date();
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date();
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblProductPrice))
            {

                tblProductPrice item = (tblProductPrice)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = 1;
                    item.createdDate = DateTime.Now.getInt64Date();
                    item.createdUser = baseinput.userName;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date();
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                    item.LastUpdatedStatus = DateTime.Now.getInt64Date();
                    item.updatedDate = DateTime.Now.getInt64Date();
                    item.updatedUser = baseinput.userName;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblAuthenticatedPart))
            {

                tblAuthenticatedPart item = (tblAuthenticatedPart)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblPrivilegedRole))
            {

                tblPrivilegedRole item = (tblPrivilegedRole)(object)t;


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblProductProfileImage))
            {

                tblProductProfileImage item = (tblProductProfileImage)(object)t;


            


                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblPRM_KTNBranch))
            {

                tblPRM_KTNBranch item = (tblPRM_KTNBranch)(object)t;





                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblPRM_ASCBranch))
            {

                tblPRM_ASCBranch item = (tblPRM_ASCBranch)(object)t;





                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                }
                genericObj = item;

            }
            else if (typeof(T) == typeof(tblBranchResponsibility))
            {

                tblBranchResponsibility item = (tblBranchResponsibility)(object)t;





                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                }
                genericObj = item;
            }
           
            else if (typeof(T) == typeof(tblProductionCalendar))
            {

                tblProductionCalendar item = (tblProductionCalendar)(object)t;





                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblComMessageAttachment))
            {

                tblComMessageAttachment item = (tblComMessageAttachment)(object)t;





                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblContract))
            {

                tblContract item = (tblContract)(object)t;





                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblPropertyDetail))
            {

                tblPropertyDetail item = (tblPropertyDetail)(object)t;





                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                }
                genericObj = item;
            }
            else if (typeof(T) == typeof(tblPropertyType))
            {

                tblPropertyType item = (tblPropertyType)(object)t;





                //insert Operation
                if (crudOperation == 1)
                {
                    item.Status = 1;
                }
                //update Operation
                else if (crudOperation == 2)
                {
                    item.Status = 1;
                }
                //delete Operation
                else if (crudOperation == 3)
                {
                    item.Status = 0;
                }
                genericObj = item;
            }
            return (T)Convert.ChangeType(genericObj, typeof(T));

             
        }


    }
}
