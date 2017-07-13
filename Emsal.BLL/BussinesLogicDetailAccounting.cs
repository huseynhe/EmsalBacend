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
        public BaseOutput GetDemandGovermentOrganisatinByAdminID(BaseInput baseinput, string adminIdList, out List<ForeignOrganization> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<ForeignOrganization>();


            try
            {
                itemList = sqloperationLogicAccounting.GetDemandGovermentOrganisatinByAdminID(adminIdList);
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
                    item.priceList = sqloperationLogicAccounting.GetUnitPriceByProdcutID(item.productId);
                    foreach (var citem in item.offerProductsList)
                    {
                        citem.comList = operationLogic.GetCommunicationByPersonId(citem.personID);
                        citem.contractTempList = operationLogic.GettblContractDetailTempByOfferId(citem.productionID);
                        citem.conUnitprice = sqloperationLogicAccounting.GetProductPriceByOfferID(citem.productionID, citem.productId);


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
                        item.priceList = sqloperationLogicAccounting.GetUnitPriceByProdcutID(item.productId);

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
                    item.conUnitprice = sqloperationLogicAccounting.GetProductPriceByOfferID(item.productionID, productID);


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
            Price price = new Price();

            try
            {
                itemList = sqloperationLogicAccounting.GetTotalOffer1(ops);

                foreach (var item in itemList)
                {

                    item.personcomList = operationLogic.GetCommunicationByPersonId(item.personID);
                  //  price = sqloperationLogicAccounting.GetOfferUnitPriceByProdcutID(item.productId);
                  //  if (price)
                  //  {
                        
                  //  }
                  item.priceList = sqloperationLogicAccounting.GetOfferUnitPriceByProdcutID(item.productId);
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

        public BaseOutput GetDemandTotalPriceByYearEvId(BaseInput baseinput, Int64 productId, Int64 partOfYear, out decimal count)
        {
            BaseOutput baseOutput;
            count = 0;
            decimal count1 = 0;
            Int64 count33 = 0;
            try
            {
                count = sqloperationLogicAccounting.GetDemandTotalPriceByYearEvId(productId, partOfYear);
               

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
      
        public BaseOutput GetDemandTotalPriceByProductID(BaseInput baseinput, Int64 productId, Int64 partOfYear, out decimal count)
        {
            BaseOutput baseOutput;
            count = 0;
            decimal count1 = 0;
            Int64 count33 = 0;
            try
            {
                count1 = sqloperationLogicAccounting.GetDemandTotalPriceByProductID(productId, partOfYear);
                count33 = sqloperationLogicAccounting.GetProductControl33_OPC(productId);
                if (count33 == 0)
                {
                   
                        count = sqloperationLogicAccounting.GetTotalPriceMarka(productId);
                    
                }
                else
                {
                    if (count1==0)
                    {
                        count = sqloperationLogicAccounting.GetTotalPriceMarkaNot(productId, partOfYear);
                    }
                    else
                    {
                        count = sqloperationLogicAccounting.GetDemandTotalPriceByProductID(productId, partOfYear);
                    }
                   
                }
                //if (count1 == 0)
                //{
                //    if (count33 == 0)
                //    {
                //        count = sqloperationLogicAccounting.GetTotalPriceMarka(productId);
                //    }
                //    else
                //    {
                //        count = sqloperationLogicAccounting.GetTotalPriceMarkaNot(productId, partOfYear);
                //    }

                //}
                //else
                //{
                //    count = count1;
                //}


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetDemandTotalPriceByProductIDList(BaseInput baseinput, Int64 productId, Int64 partOfYear, out Price count)
        {
            BaseOutput baseOutput;
            count = new Price();
            decimal count1 = 0;
            Int64 count33 = 0;
            try
            {
                count1 = sqloperationLogicAccounting.GetDemandTotalPriceByProductID(productId, partOfYear);
                count33 = sqloperationLogicAccounting.GetProductControl33_OPC(productId);
                if (count33 == 0)
                {

                    count.unit_price = sqloperationLogicAccounting.GetTotalPriceMarka(productId);

                }
                else
                {
                    if (count1 == 0)
                    {
                        count.unit_price = sqloperationLogicAccounting.GetTotalPriceMarkaNot(productId, partOfYear);
                    }
                    else
                    {
                        count.unit_price = sqloperationLogicAccounting.GetDemandTotalPriceByProductID(productId, partOfYear);
                    }

                }
                //if (count1 == 0)
                //{
                //    if (count33 == 0)
                //    {
                //        count = sqloperationLogicAccounting.GetTotalPriceMarka(productId);
                //    }
                //    else
                //    {
                //        count = sqloperationLogicAccounting.GetTotalPriceMarkaNot(productId, partOfYear);
                //    }

                //}
                //else
                //{
                //    count = count1;
                //}


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetDemandTotalPriceByYearEvId1(BaseInput baseinput, Int64 orgId, Int64 yearEvID, out List<Price> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<Price>();
            //Price item = new Price();
            Int64 countMovs = 0;
            List<Price> objList = new List<Price>();
            decimal count = 0;
            decimal count1 = 0;
            decimal sum = 0;
            decimal powr = 0;
            Int64 count33 = 0;
            decimal unitPrice = 0;
            try
            {
                
            

                objList = sqloperationLogicAccounting.GetDemandTotalPriceByYearEvId1(orgId, yearEvID);
                foreach (var item in objList)
                {
                    count1 = sqloperationLogicAccounting.GetDemandTotalPriceByProductID(item.productId, item.partOfYear);
                    count33 = sqloperationLogicAccounting.GetProductControl33_OPC(item.productId);
                    if (count33 == 0)
                    {

                        count = sqloperationLogicAccounting.GetTotalPriceMarka(item.productId);

                    }
                    else
                    {
                        if (count1 == 0)
                        {
                            count = sqloperationLogicAccounting.GetTotalPriceMarkaNot(item.productId, item.partOfYear);
                        }
                        else
                        {
                            count = sqloperationLogicAccounting.GetDemandTotalPriceByProductID(item.productId, item.partOfYear);
                        }

                    }
                    unitPrice = sqloperationLogicAccounting.GetDemandTotalPriceByProductID(item.productId, item.partOfYear);
                    item.unit_price = count;
                    powr=item.price * count;
                    sum = sum + powr;
                    item.totalPrice = item.price * count; 
                }
                itemList = objList;

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetDemandCalPriceByYearEvId(BaseInput baseinput, Int64 orgId, Int64 yearEvID, out List<DemandPrice> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<DemandPrice>();


            try
            {


                itemList = sqloperationLogicAccounting.GetDemandCalPriceByYearEvId(orgId,yearEvID);
               



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetOrganizationDetailist(BaseInput baseinput, DemandOfferProductsSearch ops, out List<OrganizationList> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<OrganizationList>();
            List<Organizations> org = new List<Organizations>();

            try
            {


                itemList = sqloperationLogicAccounting.GetOrganizationDetailist(ops);
               
                foreach (var item in itemList)
                {
                    item.productCount = sqloperationLogicAccounting.GetOrganizationCount(item.orgID);
               
                    item.orgList = sqloperationLogicAccounting.GetOrganizations(item.orgID);
                }



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
       
        public BaseOutput GetOrganizationDetailist_OPC(BaseInput baseinput, DemandOfferProductsSearch ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetOrganizationDetailist_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetEvaluation_OPC(BaseInput baseinput, EvaluationObjects ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetEvaluation_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetEvaluation_OP(BaseInput baseinput, EvaluationObjects ops, out List<EvaluationDetails> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<EvaluationDetails>();
          

            try
            {


                itemList = sqloperationLogicAccounting.GetEvaluation_OP(ops);

              


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetEvaluationAttachment_OPC(BaseInput baseinput, EvaluationObjects ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetEvaluationAttachment_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetEvaluationAttachment_OP(BaseInput baseinput, EvaluationObjects ops, out List<EvaluationAttachmentDetails> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<EvaluationAttachmentDetails>();


            try
            {


                itemList = sqloperationLogicAccounting.GetEvaluationAttachment_OP(ops);




                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetEvaluationResult_OPC(BaseInput baseinput, EvaluationObjects ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetEvaluationResult_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetEvaluationResult_OP(BaseInput baseinput, EvaluationObjects ops, out List<tblEvaluationResult> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<tblEvaluationResult>();


            try
            {


                itemList = sqloperationLogicAccounting.GetEvaluationResult_OP(ops);




                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetEvaluationResultQuestion_OPC(BaseInput baseinput, EvaluationObjects ops, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = sqloperationLogicAccounting.GetEvaluationResultQuestion_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetEvaluationResultQuestion_OP(BaseInput baseinput, EvaluationObjects ops, out List<tblEvaluationResultQuestion> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<tblEvaluationResultQuestion>();


            try
            {


                itemList = sqloperationLogicAccounting.GetEvaluationResultQuestion_OP(ops);




                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetEnumValueListByProductID(BaseInput baseinput, string productIdList, out List<tblEnumValue> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<tblEnumValue>();


            try
            {


                itemList = sqloperationLogicAccounting.GetEnumValueListByProductID(productIdList);




                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
    }
}

