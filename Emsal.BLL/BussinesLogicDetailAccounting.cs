using Emsal.DAL;
using Emsal.DAL.CodeObjects;
using Emsal.DAL.CustomObjects;
using Emsal.DAL.Enum;
using Emsal.DAL.SearchObject;
using Emsal.Utility.UtilityObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.BLL
{
    public partial class BusinessLogic
    {

        SqlOperationLogicAccounting sqloperationLogicAccounting = new SqlOperationLogicAccounting();
        public BaseOutput GetOfferProductionDetailistForMonitoringEVId_OP(BaseInput baseinput, OfferProductionDetailSearch opds, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            List<tblCommunication> comlist = new List<tblCommunication>();
            try
            {
                itemList = sqloperationLogicAccounting.GetOfferProductionDetailistForMonitoringEVId_OP(opds);

                foreach (var item in itemList)
                {
                    if (item.userType_eV_ID == 50)
                    {
                        //tblPerson person = operationLogic.GetPersonByUserId(item.managerId);
                        //item.person = person;
                       // List<PersonInformation> person1 = new List<PersonInformation>();
                     PersonInformation person1 = sqloperationLogic.GetPersonInformationuserIDNew(item.userId);
                       // person1 = sqloperationLogic.GetPersonInformationuserIDNew(item.userId);
                        item.personInformation = person1;
                    }
                    else
                    {
                        PersonInformation person1 = sqloperationLogic.GetPersonInformationuserIDNew(item.userId);
                        item.personInformation = person1;
                    }

                    //PersonInformation person1 = sqloperationLogic.GetPersonInformationUserID(item.userId);
                    //item.personInformation = person1;

                    List<tblProduction_Document> productionDocumentList = operationLogic.GetProductionDocumentsByOffer_Production_Id(
                      new tblProduction_Document
                      {
                          Offer_Production_Id = item.productionID
                      });
                    /*
                      foreach (var item in itemList)
                {
                    List<tblCommunication> comlist = operationLogic.GetCommunicationByPersonId(item.personId);
                    item.personcomList = comlist;
                }*/

                    comlist = operationLogic.GetCommunicationByPersonId(item.personID);
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
        public BaseOutput GetOfferProductionDetailistForMonitoringEVId_OPC(BaseInput baseinput, OfferProductionDetailSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetOfferProductionDetailistForMonitoringEVId_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetOfferProductionDetailistForStateEVId_OP(BaseInput baseinput, OfferProductionDetailSearch opds, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            List<ProductionDetail> objlist = new List<ProductionDetail>();
            try
            {
                itemList = sqloperationLogicAccounting.GetOfferProductionDetailistForStateEVId_OP(opds);
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
        public BaseOutput GetOfferProductionDetailistForStateEVId_OPC(BaseInput baseinput, OfferProductionDetailSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetOfferProductionDetailistForStateEVId_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetAnnouncementDetails_Search(BaseInput baseinput, OfferProductionDetailSearch ops, out List<AnnouncementDetail> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<AnnouncementDetail>();


            try
            {
                itemList = sqloperationLogicAccounting.GetAnnouncementDetails_Search(ops);
                foreach (var item in itemList)
                {
                    item.announcement = operationLogic.GetAnnouncementById(item.announcementID);
                    item.productCatalogDocumentList = operationLogic.GetProductDocumentsByProductCatalogId((long)item.announcement.product_id);
                }


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetOfferProductionDetailistForEValueId_OP1(BaseInput baseinput, OfferProductionDetailSearch ops, out List<GetOfferProductionDetailistForEValueId> itemList)
        {
            BaseOutput baseOutput;
            List<GetOfferProductionDetailistForEValueId> objlist = new List<GetOfferProductionDetailistForEValueId>();
            try
            {
                itemList = sqloperationLogicAccounting.GetOfferProductionDetailistForEValueId_OP1(ops);

                foreach (var item in itemList)
                {




                    List<tblCommunication> comlist = operationLogic.GetCommunicationByPersonId(item.personID);

                    item.personcomList = comlist;


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
        public BaseOutput GetOfferProductionDetailistForEValueId_OPC1(BaseInput baseinput, OfferProductionDetailSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetOfferProductionDetailistForEValueId_OPC1(ops);
              


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetUserDetailInfoForOffers_OP(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch1 ops, out List<PersonDetail> itemList)
        {
            BaseOutput baseOutput;
            // itemList = new List<PersonDetail>();
            List<PersonDetail> objlist = new List<PersonDetail>();

            try
            {
                itemList = sqloperationLogicAccounting.GetUserDetailInfoForOffers_OP(ops);

                //foreach (var item in itemList)
                //{


                //    if (ops.contractStatus == true && item.contractID > 0)
                //    {


                //        tblContract contract = operationLogic.GetContractById(item.contractID);
                //        item.contractList = contract;


                //    }
                //    else if (ops.contractStatus == false || item.contractID == 0)
                //    {

                //        item.contractList = null;

                //    }
                //    objlist.Add(item);
                //}
                //itemList = objlist;

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetUserDetailInfoForOffers_OPC(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch1 ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetUserDetailInfoForOffers_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetGovermentOrganisatinByAdminID(BaseInput baseinput, string adminIdList, out List<ForeignOrganization> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<ForeignOrganization>();


            try
            {
                itemList = sqloperationLogicAccounting.GetGovermentOrganisatinByAdminID(adminIdList);
                //foreach (var item in itemList)
                //{
                //    item.adminUnitIdList = sqloperationLogicAccounting.GetPRM_AdminUnitByAdminID(adminId);
                //}


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPRM_AdminUnitByAdminID(BaseInput baseinput, Int64 adminId, out List<AdminUnitRegion> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<AdminUnitRegion>();


            try
            {
                itemList = sqloperationLogicAccounting.GetPRM_AdminUnitByAdminID(adminId);



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetTotalDemandOffers(BaseInput baseinput, DemandOfferProductsSearch ops, out List<DemanProductionGroup> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<DemanProductionGroup>();


            try
            {
                itemList = sqloperationLogicAccounting.GetTotalDemandOffers(ops);

                foreach (var item in itemList)
                {
                    item.offerProductsList = sqloperationLogicAccounting.GetTotalOffersbyProductID(item.productId, ops);
              
                    foreach (var citem in item.offerProductsList)
                    {
                        citem.comList = operationLogic.GetCommunicationByPersonId(citem.personID);
                        citem.contractTempList = operationLogic.GettblContractDetailTempByOfferId(citem.productionID);
                    
                     
                    }
                }



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetTotalDemandOffersPA(BaseInput baseinput, DemandOfferProductsSearch ops, out List<DemanProductionGroup> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<DemanProductionGroup>();


            try
            {
                itemList = sqloperationLogicAccounting.GetTotalDemandOffers(ops);

                try
                {
                    foreach (var item in itemList)
                    {
                        List<OfferProducts> oproduct = sqloperationLogicAccounting.GetTotalOffer(item.productId, ops.monthID);

                        foreach (var item1 in oproduct)
                        {


                            item.totalOfferQuantity = item1.totalQuantity;
                            item.totalOfferPrice = item1.totalPrice;
                         
                        }      

                       

                    }
                }
                catch (Exception ex)
                {
                    
                    
                }
              


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetTotalOffersbyProductID(BaseInput baseinput, Int64 productID, DemandOfferProductsSearch ops, out List<DemanOfferProduction> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<DemanOfferProduction>();
        

            try
            {

            
                itemList = sqloperationLogicAccounting.GetTotalOffersbyProductID(productID, ops);
                foreach (var item in itemList)
                {
                    item.comList = operationLogic.GetCommunicationByPersonId(item.personID);
                    item.contractTempList = operationLogic.GettblContractDetailTempByOfferId(item.productionID);
                

                }



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetTotalDemandOffers_OPC(BaseInput baseinput, DemandOfferProductsSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetTotalDemandOffers_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetTotalOffersbyProductID_OPC(BaseInput baseinput, Int64 productID, DemandOfferProductsSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetTotalOffersbyProductID_OPC(productID, ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetTotalDemandOffersRegion(BaseInput baseinput, DemandOfferProductsSearch ops, out List<DemandDetail> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<DemandDetail>();


            try
            {
                itemList = sqloperationLogicAccounting.GetTotalDemandOffersRegion(ops);
                foreach (var item in itemList)
                {
                    item.offerList = sqloperationLogicAccounting.GetTotalOfferRole(item.productID);
                }



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetAnnouncementDetails_Search_OPC(BaseInput baseinput, OfferProductionDetailSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetAnnouncementDetails_Search_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetTotalDemandOffersRegion_OPC(BaseInput baseinput, DemandOfferProductsSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetTotalDemandOffersRegion_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetTotalOffer1(BaseInput baseinput, OfferProductionDetailSearch ops, out List<ProductionDetail> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<ProductionDetail>();
            decimal unitprice = 0;

            try
            {
                itemList = sqloperationLogicAccounting.GetTotalOffer1(ops);

                foreach (var item in itemList)
                {
                   
                   item.personcomList = operationLogic.GetCommunicationByPersonId(item.personID);;
                   // item.unitPrice = sqloperationLogicAccounting.GetOfferPriceCount(item.productionID, item.productId);
                   // 
                }



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetOfferPersonByProductID(BaseInput baseinput, Int64 productID, out List<OfferPerson> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<OfferPerson>();
          

            try
            {

               
                itemList = sqloperationLogicAccounting.GetOfferPersonByProductID(productID);
                foreach (var item in itemList)
                {
                    item.comList = operationLogic.GetCommunicationByPersonId(item.personID);
                  
                    item.unit_price = sqloperationLogicAccounting.GetOfferPriceCount(item.productionID, productID);
                 

                }



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetTotalOffer1_OPC(BaseInput baseinput, OfferProductionDetailSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetTotalOffer1_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
    }
}

