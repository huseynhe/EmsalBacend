using Emsal.DAL;
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
                        PersonInformation person1 = sqloperationLogic.GetPersonInformationUserID(item.managerId);
                        item.personInformation = person1;
                    }
                    else
                    {
                        PersonInformation person1 = sqloperationLogic.GetPersonInformationUserID(item.userId);
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
                   
                      comlist=  operationLogic.GetCommunicationByPersonId(item.personID);
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
                    item.announcement =operationLogic.GetAnnouncementById(item.announcementID);
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
                count = sqloperationLogic.GetOfferProductionDetailistForEValueId_OPC(ops);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetUserDetailInfoForOffers_OP(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, out List<PersonDetail> itemList)
        {
            BaseOutput baseOutput;
           // itemList = new List<PersonDetail>();
            List<PersonDetail> objlist = new List<PersonDetail>();

            try
            {
                itemList = sqloperationLogicAccounting.GetUserDetailInfoForOffers_OP(ops);

                foreach (var item in itemList)
                {
                    tblContract contract = operationLogic.GetContractById(item.contractID);
                    //tblContract contract1 = operationLogic.GetContractById(item.contractID);
                    if (item.contractStatus != 0)
                    {
                        if (contract != null)
                        {
                            item.contractList = contract;
                        }

                    }
                    else if (item.contractStatus == 0)
                    {
                        if (contract == null)
                        {
                            item.contractList = contract;
                        }
                    }
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
        public BaseOutput GetUserDetailInfoForOffers_OPC(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, out Int64 count)
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
    }
}

