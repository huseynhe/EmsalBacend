﻿using Emsal.DAL;
using Emsal.DAL.CodeObjects;
using Emsal.DAL.CustomObjects;
using Emsal.DAL.Enum;
using Emsal.DAL.SearchObject;
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

        public BaseOutput GetPersonalinformationByRoleId(BaseInput baseinput, Int64 roleId, Int64 userId, out List<UserInfo> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPersonalinformationByRoleId(roleId, userId);
                foreach (var item in itemList)
                {
                    List<tblCommunication> comlist = operationLogic.GetCommunicationByPersonId(item.personId);
                    item.personcomList = comlist;
                }
                
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetDemandOfferProductionTotal(BaseInput baseinput,Int64 adressID, out List<DemandOfferDetail> itemList)
        {
            BaseOutput baseOutput;
            List<DemandOfferDetail> objlist = new List<DemandOfferDetail>();
            try
            {
                itemList = sqloperationLogic.GetDemandOfferProductionTotal(adressID);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetDemandProductsForAccounting(BaseInput baseinput, Int64 state_eV_Id, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetDemandProductsForAccounting(state_eV_Id);

                foreach (var item in itemList)
                {

                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarDemandId(item.productionID);
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
        public BaseOutput GetDemandProductDetailInfoForAccounting(BaseInput baseinput, Int64 state_eV_Id, Int64 year, Int64 partOfYear, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetDemandProductDetailInfoForAccounting(state_eV_Id, year, partOfYear);

                foreach (var item in itemList)
                {
                    item.foreignOrganization = operationLogic.GetForeign_OrganizationByUserId(item.userId);

                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarDemandId(item.productionID);
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
        public BaseOutput GetForeign_OrganizationsListForID(BaseInput baseinput, Int64 ID, out List<tblForeign_Organization> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetForeign_OrganizationsListForID(ID);
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
                    if (person!=null)
                    {
                        
                   
                    item.person = person;
                    }
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
                itemList = sqloperationLogic.GetPotensialProductionDetailistForMonitoringEVId(userID, monintoring_eV_Id);

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
        public BaseOutput GetOfferProducts(BaseInput baseinput,  out List<OfferProducts> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList=sqloperationLogic.GetOfferProducts();
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

                    item.productDocumentList = operationLogic.GetProductDocumentsByProductCatalogId((long)item.productId);
                    item.productionDocumentList = productionDocumentList;
                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarOfferId(item.productionID);
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


        public BaseOutput GetOfferProductionDetailistForMonitoringEVId(BaseInput baseinput, Int64 monintoring_eV_Id, Int64 userID, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetOfferProductionDetailistForMonitoringEVId(monintoring_eV_Id, userID);
                foreach (var item in itemList)
                {
                    if (item.userType_eV_ID == 50)
                    {
                        tblPerson person = operationLogic.GetPersonByUserId(item.managerId);
                        item.person = person;
                    }
                    else
                    {
                        tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                        item.person = person;
                    }



                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByOffer_Production_Id(
                      new tblProduction_Document
                      {
                          Offer_Production_Id = item.productionID
                      });
                    List<tblCommunication> comlist = operationLogic.GetCommunicationByPersonId(item.personID);
                    item.personcomList = comlist;
                  
                    item.productionDocumentList = productionDocumentList;
                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarOfferId(item.productionID);
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
                    //tblPerson person = operationLogic.GetPersonByUserId(item.userId);

                    //item.person = person;
                    PersonInformation person = sqloperationLogic.GetPersonInformationUserID(item.userId);
                    item.personInformation = person;



                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByOffer_Production_Id(
                      new tblProduction_Document
                      {
                          Offer_Production_Id = item.productionID
                      });

                    item.productionDocumentList = productionDocumentList;
                    //List<tblProductionCalendar> productionCalendarList = sqloperationLogic.GetProductionCalendarListOfferId(
                    //    new tblProductionCalendar
                    //    {
                    //        offer_Id = item.productionID
                    //    }
                    //    );
                    ///burasi yeni elave olundu 

                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarOfferId(item.productionID);

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
                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarDemandId(item.productionID);
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
               grup_Id = item.groupId
           });

                    item.productionDocumentList = productionDocumentList;
                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarDemandId(item.productionID);

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
        public BaseOutput GetDemandProductionDetailistForEValueId_OPA(BaseInput baseinput, Int64 state_eV_Id,int page, int pageSize, out List<DemandDetialOPA> itemList)
        {
            BaseOutput baseOutput;
            List<DemandDetialOPA> objlist = new List<DemandDetialOPA>();
            try
            {
                itemList = sqloperationLogic.GetDemandProductionDetailistForEValueId_OPA(state_eV_Id,page,pageSize);

                foreach (var item in itemList)
                {


                    ProductionCalendarDetail pC = new ProductionCalendarDetail();
                    List<ProductionCalendarDetail> pList = new List<ProductionCalendarDetail>();
                    List<ProductionCalendarDetail> productiCtList = sqloperationLogic.GetProductionCalendarDemandId(item.productionID);
                    foreach (var item1 in productiCtList)
                    {
                        pC.TypeDescription = item1.TypeDescription;
                        pC.day = item1.day;
                        pC.MonthName = item1.MonthName;
                        pC.MonthDescription = item1.MonthDescription;
                        pC.transportation_eV_Id = item1.transportation_eV_Id;
                        pC.quantity = item1.quantity;
                        pC.price = item1.price;

                    }
                    pList.Add(pC);
                    item.productionCalendarList = pList;

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



        public BaseOutput GetDemanProductionGroupList(BaseInput baseinput, Int64 startDate, Int64 endDate, Int64 year, Int64 partOfYear, out List<DemanProductionGroup> itemList)
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



        public BaseOutput GetPotensialUserList(BaseInput baseinput, out List<UserInfo> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPotensialUserList();

                foreach (var item in itemList)
                {
                    List<ProductCatalogDetail> list = new List<ProductCatalogDetail>();
                    GetProducListByUserID(baseinput, item.userID, out list);
                    item.productCatalogDetailList = list;
                }


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }



        public BaseOutput GetPotensialUserForAdminUnitIdList(BaseInput baseinput, Int64 adminUnitId, out List<UserInfo> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPotensialUserForAdminUnitIdList(adminUnitId);
                foreach (var item in itemList)
                {
                    List<ProductCatalogDetail> list = new List<ProductCatalogDetail>();
                    GetProducListByUserID(baseinput, item.userID, out list);
                    item.productCatalogDetailList = list;

                }

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



        public BaseOutput GetProductPriceList(BaseInput baseinput, Int64 year, Int64 partOfYear, out List<ProductPriceDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetProductPriceList(year, partOfYear);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }


        public BaseOutput GetProductPriceListNotPrice(BaseInput baseinput, Int64 year, Int64 partOfYear, out List<ProductPriceDetail> itemList)
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

        public BaseOutput GetDemandProductionAmountOfEachProduct(BaseInput baseinput, out List<DemandOfferDetail> itemList)
        {
            BaseOutput baseOutput;
            List<DemandOfferDetail> objlist = new List<DemandOfferDetail>();
            try
            {
                itemList = sqloperationLogic.GetDemandProductionAmountOfEachProduct();


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetDemandProductionListNotPrice(BaseInput baseinput, Int64 year, Int64 partOfYear, out List<ProductPriceDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetDemandProductionListNotPrice(year, partOfYear);

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

        public BaseOutput GetDemandOfferDetailByProductID(BaseInput baseinput, out List<DemandOfferDetail> itemList)
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

        public BaseOutput GetPotentialClientCount(BaseInput baseinput, out List<PotentialClientDetail> itemList)
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
        public BaseOutput GetProductionCalendarDemand(BaseInput baseinput, out List<ProductionCalendarDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetProductionCalendarDemand();


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetProductionCalendarOfferId(BaseInput baseinput, Int64 offer_id, out List<ProductionCalendarDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetProductionCalendarOfferId(offer_id);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetProductionCalendarDemandId(BaseInput baseinput, Int64 demand, out List<ProductionCalendarDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetProductionCalendarDemandId(demand);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetOfferProductionDetailById(BaseInput baseinput, Int64 offer, out ProductionDetail item)
        {
            ProductionDetail obj;
            BaseOutput baseOutput;
            try
            {
                item = sqloperationLogic.GetOfferProductionDetailById(offer);

                tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                item.person = person;
                List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByOffer_Production_Id(
                  new tblProduction_Document
                  {
                      Offer_Production_Id = item.productionID
                  });

                item.productDocumentList = operationLogic.GetProductDocumentsByProductCatalogId((long)item.productId);
                item.productionDocumentList = productionDocumentList;
                item.productionCalendarList = sqloperationLogic.GetProductionCalendarOfferId(item.productionID);
                obj = item;

                item = obj;

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }

            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetProducParentProductByProductID(BaseInput baseinput, Int64 prodcutID, out tblProductCatalog pCatalog)
        {

            BaseOutput baseOutput;
            try
            {
                pCatalog = sqloperationLogic.GetProducParentProductByProductID(prodcutID);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }

            catch (Exception ex)
            {

                pCatalog = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetProducListByUserID(BaseInput baseinput, Int64 userID, out List<ProductCatalogDetail> pCatalogDetailList)
        {

            pCatalogDetailList = new List<ProductCatalogDetail>();

            BaseOutput baseOutput;
            try
            {

                List<tblProductCatalog> pCatalogList = sqloperationLogic.GetProducListByUserID(userID);


                foreach (var item in pCatalogList)
                {
                    ProductCatalogDetail pCatalogDetail = new ProductCatalogDetail();
                    pCatalogDetail.productCatalog = item;

                    try
                    {
                        pCatalogDetail.productName = sqloperationLogic.GetProducParentProductByProductID(item.Id).ProductName;
                    }
                    catch (Exception)
                    {


                    }
                    pCatalogDetailList.Add(pCatalogDetail);

                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }

            catch (Exception ex)
            {

                pCatalogDetailList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }


        }



        public BaseOutput GetPersonInformationUserID(BaseInput baseinput, Int64 userId, out PersonInformation pCatalog)
        {

            BaseOutput baseOutput;
            try
            {
                pCatalog = sqloperationLogic.GetPersonInformationUserID(userId);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }

            catch (Exception ex)
            {

                pCatalog = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetPersonInformationByPinNumber(BaseInput baseinput, string PinNumber, out List<PersonInformation> itemList)
        {

            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPersonInformationByPinNumber(PinNumber);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }

            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetProductCatalogsWithParent(BaseInput baseinput, out List<ProductCatalogDetail> pCatalogDetailList)
        {

            pCatalogDetailList = new List<ProductCatalogDetail>();

            BaseOutput baseOutput;
            try
            {
                OperationLogic operationLogic = new OperationLogic();
                List<tblProductCatalog> pCatalogList = operationLogic.GetProductCatalogs();


                foreach (var item in pCatalogList)
                {
                    if (item.Status == 1)
                    {
                        ProductCatalogDetail pCatalogDetail = new ProductCatalogDetail();
                        pCatalogDetail.productCatalog = item;

                    try
                    {
                       
                            pCatalogDetail.productName = sqloperationLogic.GetProducParentProductByProductID(item.Id).ProductName;
                       
                    }
                    catch (Exception)
                    {


                    }
                    pCatalogDetailList.Add(pCatalogDetail);
                    }
                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }

            catch (Exception ex)
            {

                pCatalogDetailList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }


        }


        #endregion

        #region Optimastion

        public BaseOutput GetDemandProductionDetailistForEValueId_OP(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetDemandProductionDetailistForEValueId_OP(ops);

                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    item.person = person;
                    item.foreignOrganization = operationLogic.GetForeign_OrganizationByUserId(item.userId);
                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByDemand_Production_Id(
           new tblProduction_Document
           {
               grup_Id = item.groupId
           });

                    item.productionDocumentList = productionDocumentList;
                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarDemandId(item.productionID);
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


        public BaseOutput GetDemandProductionDetailistForEValueId_OPC(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogic.GetDemandProductionDetailistForEValueId_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetAnnouncementDetailsByProductId_OP(BaseInput baseinput, long productID, int page, int pageSize, out List<AnnouncementDetail> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<AnnouncementDetail>();
            List<AnnouncementDetail> tblAnouncementlist = new List<AnnouncementDetail>();

            try
            {
                tblAnouncementlist = operationLogic.GetAnnouncementDetailsByProductId_OP(productID, page, pageSize);
                foreach (var obj in tblAnouncementlist)
                {
                    try
                    {
                        obj.parentName = sqloperationLogic.GetProducParentProductByProductID(productID).ProductName;
                    }
                    catch (Exception ex)
                    {

                    }

                    AnnouncementDetail item = new AnnouncementDetail();
                    item = obj;
                    try
                    {
                        item.productCatalogDocumentList = operationLogic.GetProductDocumentsByProductCatalogId((long)obj.announcement.product_id);
                    }
                    catch (Exception ex)
                    {


                    }

                    itemList.Add(item);
                }



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetOfferProductionDetailistForEValueId_OP(BaseInput baseinput,OfferProductionDetailSearch ops,out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetOfferProductionDetailistForEValueId_OP(ops);

                foreach (var item in itemList)
                {

                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    if (person != null)
                    {
                        item.person = person;

                    }


                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByOffer_Production_Id(
                      new tblProduction_Document
                      {
                          Offer_Production_Id = item.productionID
                      });
                    List<tblCommunication> comlist = operationLogic.GetCommunicationByPersonId(item.personID);
                    item.personcomList = comlist;
                    item.productDocumentList = operationLogic.GetProductDocumentsByProductCatalogId((long)item.productId);
                    item.productionDocumentList = productionDocumentList;
                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarOfferId(item.productionID);
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
        public BaseOutput GetOfferProductionDetailistForEValueId_OPC(BaseInput baseinput, OfferProductionDetailSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogic.GetOfferProductionDetailistForEValueId_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPotensialProductionDetailistForEValueId_OP(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetPotensialProductionDetailistForEValueId_OP(ops);

                foreach (var item in itemList)
                {
                    tblPerson person = operationLogic.GetPersonByUserId(item.userId);
                    if (person!=null)
                    {
                        item.person = person;
                    }
                    
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
        public BaseOutput GetPotensialProductionDetailistForEValueId_OPC(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogic.GetPotensialProductionDetailistForEValueId_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetDemandProductionDetailistForUser_OP(BaseInput baseinput, DemandProductionDetailistForUser ops, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetDemandProductionDetailistForUser_OP(ops);

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
                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarDemandId(item.productionID);
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
        public BaseOutput GetDemandProductionDetailistForUser_OPC(BaseInput baseinput, DemandProductionDetailistForUser ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogic.GetDemandProductionDetailistForUser_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPotensialUserList_OP(BaseInput baseinput, int page, int pageSize, out List<UserInfo> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPotensialUserList_OP(page, pageSize);

                foreach (var item in itemList)
                {
                    List<ProductCatalogDetail> list = new List<ProductCatalogDetail>();
                    GetProducListByUserID(baseinput, item.userID, out list);
                    item.productCatalogDetailList = list;
                }


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetPotensialUserList_OPC(BaseInput baseinput, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogic.GetPotensialUserList_OPC();


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPotensialUserForAdminUnitIdList_OP(BaseInput baseinput, PotensialUserForAdminUnitIdList ops, out List<UserInfo> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetPotensialUserForAdminUnitIdList_OP(ops);
                foreach (var item in itemList)
                {
                    List<ProductCatalogDetail> list = new List<ProductCatalogDetail>();
                    GetProducListByUserID(baseinput, item.userID, out list);
                    item.productCatalogDetailList = list;

                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetPotensialUserForAdminUnitIdList_OPC(BaseInput baseinput, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogic.GetPotensialUserForAdminUnitIdList_OPC();


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }



        public BaseOutput GetDemandProductDetailInfoForAccounting_OP(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, long year, long partOfYear, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetDemandProductDetailInfoForAccounting_OP(ops, year, partOfYear);

                foreach (var item in itemList)
                {
                    item.foreignOrganization = operationLogic.GetForeign_OrganizationByUserId(item.userId);

                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarDemandId(item.productionID);
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
        public BaseOutput GetDemandProductsForAccounting_OPC(BaseInput baseinput, DemandProductsForAccountingSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogic.GetDemandProductsForAccounting_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetDemandProductDetailInfoForAccounting_OPP(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, Int64 year, Int64 partOfYear, out float totalPrice)
        {
            BaseOutput baseOutput;
            totalPrice = 0;
            try
            {
                totalPrice = sqloperationLogic.GetDemandProductDetailInfoForAccounting_OPP(ops, year, partOfYear);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                totalPrice = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetDemandProductDetailInfoForAccounting_OPC(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, Int64 year, Int64 partOfYear, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogic.GetDemandProductDetailInfoForAccounting_OPC(ops, year, partOfYear);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetDemandProductsForAccounting_OP(BaseInput baseinput, DemandProductsForAccountingSearch ops, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetDemandProductsForAccounting_OP(ops);

                foreach (var item in itemList)
                {

                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarDemandId(item.productionID);
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
        public BaseOutput GetDemandProductDetailInfoForAccounting_Search(BaseInput baseinput, Int64 state_eV_Id, Int64 year, Int64 partOfYear ,string productID, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogic.GetDemandProductDetailInfoForAccounting_Search(state_eV_Id,year,partOfYear, productID);

                foreach (var item in itemList)
                {

                    item.productionCalendarList = sqloperationLogic.GetProductionCalendarDemandId(item.productionID);
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

       
        #endregion

        #region Yeni Hesabatlar

        public BaseOutput GetOfferGroupedProductionDetailistForAccounting(BaseInput baseinput,  out  List<OfferProductionDetail>  itemList)
        {
            BaseOutput baseOutput;
            List<OfferProductionDetail> objList = new List<OfferProductionDetail>();

            try
            {
                itemList = sqloperationLogic.GetOfferGroupedProductionDetailistForAccounting();
               
               
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetOfferGroupedProductionDetailistForAccountingAdmin_UnitID(BaseInput baseinput, Int64 addresID, out  List<OfferProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetOfferGroupedProductionDetailistForAccountingByAdmin_UnitID(addresID);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetOfferGroupedProductionDetailistForAccountingByProductId(BaseInput baseinput, Int64 productID, out  List<OfferProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetOfferGroupedProductionDetailistForAccountingByProductId(productID);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetOfferGroupedProductionDetailistForAccountingByRoleId(BaseInput baseinput, Int64 roleID, out  List<OfferProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetOfferGroupedProductionDetailistForAccountingByRoleId(roleID);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetOfferGroupedProductionDetailistForAccountingBySearch(BaseInput baseinput, OfferProductionDetailSearch ops, out  List<OfferProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = sqloperationLogic.GetOfferGroupedProductionDetailistForAccountingBySearch(ops);


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



