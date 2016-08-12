using Emsal.DAL;
using Emsal.DAL.CodeObjects;
using Emsal.DAL.CustomObjects;
using Emsal.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.BLL
{
    public partial class BusinessLogic
    {
        #region SqlOperation

        SqlOperationLogic sqloperationLogic = new SqlOperationLogic();
        public BaseOutput GetProductionDetailist(BaseInput baseinput, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                objlist = sqloperationLogic.GetProductionDetailist();
              
                foreach (var item in objlist)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    item.person = person;
                }
                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetAdminUnitListForID(BaseInput baseinput, Int64 ID, out List<tblPRM_AdminUnit> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetAdminUnitListForID(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetProductCatalogListForID(BaseInput baseinput, Int64 ID, out List<tblProductCatalog> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetProductCatalogListForID(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetPotensialProductionDetailistForUser(BaseInput baseinput, Int64 userID, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetPotensialProductionDetailistForUser(userID);
                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    tblForeign_Organization forganization = operationLogic.GetForeign_OrganizationByUserId(item.userId);
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByPotential_Production_Id(
                        new tblProduction_Document
                        {
                            Potential_Production_Id = item.productionID
                        });

                    item.productionDocumentList = productionDocumentList;
                    item.person = person;
                    item.foreignOrganization = forganization;
                    objlist.Add(item);
                }
                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetPotensialProductionDetailistForCreateUser(BaseInput baseinput, string createdUser, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetPotensialProductionDetailistForCreateUser(createdUser);
                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    tblForeign_Organization forganization = operationLogic.GetForeign_OrganizationByUserId(item.userId);
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByPotential_Production_Id(
                        new tblProduction_Document
                        {
                            Potential_Production_Id = item.productionID
                        });

                    item.productionDocumentList = productionDocumentList;
                    item.person = person;
                    item.foreignOrganization = forganization;
                    objlist.Add(item);
                }
                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetPotensialProductionDetailistForEValueId(BaseInput baseinput, Int64 state_eV_Id, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetPotensialProductionDetailistForEValueId(state_eV_Id);

                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    item.person = person;
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByPotential_Production_Id(
                          new tblProduction_Document
                          {
                              Potential_Production_Id = item.productionID
                          });

                    item.productionDocumentList = productionDocumentList;
                    objlist.Add(item);
                }

                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetPotensialProductionDetailistForMonitoringEVId(BaseInput baseinput, Int64 userID, Int64 monintoring_eV_Id, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetPotensialProductionDetailistForMonitoringEVId( userID,monintoring_eV_Id);

                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    item.person = person;
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByPotential_Production_Id(
                          new tblProduction_Document
                          {
                              Potential_Production_Id = item.productionID
                          });

                    item.productionDocumentList = productionDocumentList;
                    objlist.Add(item);
                }

                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetPotensialProductionDetailistForStateEVId(BaseInput baseinput, Int64 userID, Int64 state_eV_Id, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetPotensialProductionDetailistForStateEVId(userID, state_eV_Id);

                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    item.person = person;
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByPotential_Production_Id(
                          new tblProduction_Document
                          {
                              Potential_Production_Id = item.productionID
                          });

                    item.productionDocumentList = productionDocumentList;
                    objlist.Add(item);
                }

                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }


        public BaseOutput GetOfferProductionDetailistForUser(BaseInput baseinput, Int64 userID, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetOfferProductionDetailistForUser(userID);

                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    item.person = person;
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByOffer_Production_Id(
                        new tblProduction_Document
                        {
                            Offer_Production_Id = item.productionID
                        });

                    item.productionDocumentList = productionDocumentList;
                    objlist.Add(item);
                }
                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetOfferProductionDetailistForEValueId(BaseInput baseinput, Int64 state_eV_Id, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetOfferProductionDetailistForEValueId(state_eV_Id);
                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    item.person = person;
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByOffer_Production_Id(
                      new tblProduction_Document
                      {
                          Offer_Production_Id = item.productionID
                      });

                    item.productionDocumentList = productionDocumentList;
                    objlist.Add(item);
                }
                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }


        public BaseOutput GetOfferProductionDetailistForMonitoringEVId(BaseInput baseinput, Int64 userID, Int64 monintoring_eV_Id, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetOfferProductionDetailistForMonitoringEVId(userID,monintoring_eV_Id);
                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    item.person = person;
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByOffer_Production_Id(
                      new tblProduction_Document
                      {
                          Offer_Production_Id = item.productionID
                      });

                    item.productionDocumentList = productionDocumentList;
                    objlist.Add(item);
                }
                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetOfferProductionDetailistForStateEVId(BaseInput baseinput, Int64 userID, Int64 state_eV_Id, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetOfferProductionDetailistForStateEVId(userID, state_eV_Id);
                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    item.person = person;
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByOffer_Production_Id(
                      new tblProduction_Document
                      {
                          Offer_Production_Id = item.productionID
                      });

                    item.productionDocumentList = productionDocumentList;
                    objlist.Add(item);
                }
                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }



        public BaseOutput GetDemandProductionDetailistForUser(BaseInput baseinput, Int64 userID, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetDemandProductionDetailistForUser(userID);

                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    item.person = person;
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByDemand_Production_Id(
                new tblProduction_Document
                {
                    Demand_Production_Id = item.productionID
                });

                    item.productionDocumentList = productionDocumentList;
                    objlist.Add(item);
                }
                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetDemandProductionDetailistForEValueId(BaseInput baseinput, Int64 state_eV_Id, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetDemandProductionDetailistForEValueId(state_eV_Id);

                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    item.person = person;
                    item.foreignOrganization = operationLogic.GetForeign_OrganizationByUserId(item.userId);
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByDemand_Production_Id(
           new tblProduction_Document
           {
               Demand_Production_Id = item.productionID
           });

                    item.productionDocumentList = productionDocumentList;
                    objlist.Add(item);
                }
                itemList = objlist;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }


        public BaseOutput GetDemanProductionGroupList(BaseInput baseinput, Int64 startDate, Int64 endDate,Int64 year ,Int64 partOfYear, out List<DemanProductionGroup> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetDemanProductionGroupList(startDate, endDate, year, partOfYear);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }


        
        public BaseOutput GetPotensialUserList(BaseInput baseinput,  out  List<UserInfo>  itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPotensialUserList();

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }



        public BaseOutput GetPotensialUserForAdminUnitIdList(BaseInput baseinput, Int64 adminUnitId,  out  List<UserInfo> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPotensialUserForAdminUnitIdList(adminUnitId);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetPotensialUserListByName(BaseInput baseinput, string name, out List<UserInfo> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPotensialUserListByName(name);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetPotensialUserListBySurname(BaseInput baseinput, string surname, out List<UserInfo> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPotensialUserListBySurname(surname);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetPotensialUserListByAdminUnitName(BaseInput baseinput, string adminUnitName, out List<UserInfo> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPotensialUserListByAdminUnitName(adminUnitName);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }



        public BaseOutput GetProductPriceList(BaseInput baseinput, Int64 year, Int64 partOfYear, out  List<ProductPriceDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetProductPriceList(year,partOfYear);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }


        public BaseOutput GetProductPriceListNotPrice(BaseInput baseinput, Int64 year, Int64 partOfYear, out  List<ProductPriceDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetProductPriceListNotPrice(year, partOfYear);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }




        #endregion



        #region ferid
        public BaseOutput GetGovernmentOrganisations(BaseInput baseinput, Int64 governmentOrgEnum, out List<tblUser> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetGovernmentOrganisations(governmentOrgEnum);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }


        public BaseOutput GetOrganisationTypeUsers(BaseInput baseinput, long govRoleEnum, long userType, out List<tblUser> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetOrganisationTypeUsers(govRoleEnum, userType);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetRolesNotOwnedByUser(BaseInput baseinput, long userId, out List<tblRole> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetRolesNotOwnedByUser(userId);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetRolesNotAllowedInPage(BaseInput baseinput, long pageId, out List<tblRole> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetRolesNotAllowedInPage(pageId);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        #endregion


        #region Report

        public BaseOutput GetDemandOfferDetailID(BaseInput baseinput, long adminID, out List<DemandOfferDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetDemandOfferDetailID(adminID);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetDemandOfferDetailByProductID(BaseInput baseinput,  out List<DemandOfferDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetDemandOfferDetailByProductID();


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetOfferDetailByAmdminID(BaseInput baseinput, long adminID, out List<DemandOfferDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetOfferDetailByAmdminID(adminID);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetDemanDetailByAdminID(BaseInput baseinput, long adminID, out List<DemandOfferDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetDemanDetailByAdminID(adminID);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetPotentialClientCount(BaseInput baseinput,  out List<PotentialClientDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPotentialClientCount();


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }



        #endregion

    }
}
