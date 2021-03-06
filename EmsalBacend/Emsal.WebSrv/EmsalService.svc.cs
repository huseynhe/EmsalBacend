﻿using Emsal.BLL;
using Emsal.DAL;
using Emsal.DAL.CodeObjects;
using Emsal.DAL.CustomObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Mvc;

namespace Emsal.WebSrv
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class EmsalService : IEmsalService
    {

        BusinessLogic businessLogic = new BusinessLogic();

        #region DB Create

        public void WS_createDb()
        {
            BusinessLogic businessLogic = new BusinessLogic();
            businessLogic.StartDb();
        }
        #endregion

        #region Enum Category

        public BaseOutput WS_AddEnumCategory(BaseInput baseinput, tblEnumCategory enumCategory, out tblEnumCategory enumCategoryOut)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.AddEnumCategory(baseinput, enumCategory, out enumCategoryOut);
        }

        public BaseOutput WS_DeleteEnumCategory(BaseInput baseinput, tblEnumCategory enumCategory)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.DeleteEnumCategory(baseinput, enumCategory);
        }

        public BaseOutput WS_UpdateEnumCategory(BaseInput baseinput, tblEnumCategory inputItem, out tblEnumCategory updatedItem)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.UpdateEnumCategory(baseinput, inputItem, out updatedItem);
        }

        public BaseOutput WS_GetEnumCategorys(BaseInput baseinput, out List<tblEnumCategory> itemList)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetEnumCategorys(baseinput, out itemList);
        }

        public BaseOutput WS_GetEnumCategoryById(BaseInput baseinput, long ID, out tblEnumCategory item)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetEnumCategoryById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetEnumCategorysByName(BaseInput baseinput, string categoryName, out tblEnumCategory item)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetEnumCategorysByName(baseinput, categoryName, out item);
        }


        public BaseOutput WS_GetEnumCategorysForProduct(BaseInput baseinput, out List<tblEnumCategory> itemList)
        {

            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetEnumCategorysForProduct(baseinput, out itemList);
        }

        public BaseOutput WS_AddEnumValue(BaseInput baseinput, tblEnumValue enumValue, out tblEnumValue enumValueOut)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.AddEnumValue(baseinput, enumValue, out enumValueOut);
        }


        public BaseOutput WS_DeleteEnumValue(BaseInput baseinput, tblEnumValue enumValue)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.DeleteEnumValue(baseinput, enumValue);
        }

        public BaseOutput WS_UpdateEnumValue(BaseInput baseinput, tblEnumValue inputItem, out tblEnumValue updatedItem)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.UpdateEnumValue(baseinput, inputItem, out updatedItem);
        }

        public BaseOutput WS_GetEnumValues(BaseInput baseinput, out List<tblEnumValue> itemList)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetEnumValues(baseinput, out itemList);
        }

        public BaseOutput WS_GetEnumValueById(BaseInput baseinput, long ID, out tblEnumValue item)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetEnumValueById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetEnumValuesByEnumCategoryId(BaseInput baseinput, long categoryID, out List<tblEnumValue> itemList)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetEnumValuesByEnumCategoryId(baseinput, categoryID, out itemList);
        }

        public BaseOutput WS_GetEnumValuesForProduct(BaseInput baseinput, out List<tblEnumValue> itemList)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetEnumValuesForProduct(baseinput, out itemList);
        }

        public BaseOutput WS_GetEnumValueByName(BaseInput baseinput, string name, out tblEnumValue item)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetEnumValueByName(baseinput, name, out item);
        }
        #endregion

        #region  PRM_AdminUnit
        public BaseOutput WS_AddPRM_AdminUnit(BaseInput baseinput, tblPRM_AdminUnit adminUnit)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.AddPRM_AdminUnit(baseinput, adminUnit);
        }

        public BaseOutput WS_DeletePRM_AdminUnit(BaseInput baseinput, tblPRM_AdminUnit adminUnit)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.DeletePRM_AdminUnit(baseinput, adminUnit);
        }

        public BaseOutput WS_UpdatePRM_AdminUnit(BaseInput baseinput, tblPRM_AdminUnit adminUnit)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.UpdatePRM_AdminUnit(baseinput, adminUnit);
        }

        public BaseOutput WS_GetPRM_AdminUnits(BaseInput baseinput, out List<tblPRM_AdminUnit> adminUnits)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetPRM_AdminUnits(baseinput, out adminUnits);
        }

        public BaseOutput WS_GetPRM_AdminUnitById(BaseInput baseinput, Int64 ID, out tblPRM_AdminUnit adminUnit)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetPRM_AdminUnitById(baseinput, ID, out adminUnit);
        }

        public BaseOutput WS_GetAdminUnitsByParentId(BaseInput baseinput, int parentID, out List<tblPRM_AdminUnit> adminUnitList)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetPRM_AdminUnitsByParentId(baseinput, parentID, out adminUnitList);
        }


        public BaseOutput WS_GetPRM_AdminUnitByName(BaseInput baseinput, string countryName, out tblPRM_AdminUnit adminUnit)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetPRM_AdminUnitByName(baseinput, countryName, out adminUnit);
        }

        public BaseOutput WS_GETPRM_AdminUnitsByChildId(BaseInput baseinput, tblPRM_AdminUnit adminUnit, out List<tblPRM_AdminUnit> adminUnitList)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GETPRM_AdminUnitsByChildId(baseinput, adminUnit, out adminUnitList);
        }
        #endregion

        #region  PRM_Thoroughfares
        public BaseOutput WS_AddPRM_Thoroughfare(BaseInput baseinput, tblPRM_Thoroughfare Thoroughfare)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.AddPRM_Thoroughfare(baseinput, Thoroughfare);
        }

        public BaseOutput WS_DeletePRM_Thoroughfare(BaseInput baseinput, tblPRM_Thoroughfare Thoroughfare)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.DeletePRM_Thoroughfare(baseinput, Thoroughfare);
        }

        public BaseOutput WS_UpdatePRM_Thoroughfare(BaseInput baseinput, tblPRM_Thoroughfare Thoroughfare)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.UpdatePRM_Thoroughfare(baseinput, Thoroughfare);
        }

        public BaseOutput WS_GetPRM_Thoroughfares(BaseInput baseinput, out List<tblPRM_Thoroughfare> Thoroughfares)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetPRM_Thoroughfares(baseinput, out Thoroughfares);
        }

        public BaseOutput WS_GetPRM_ThoroughfareById(BaseInput baseinput, Int64 ID, out tblPRM_Thoroughfare Thoroughfare)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetPRM_ThoroughfareById(baseinput, ID, out Thoroughfare);
        }

        public BaseOutput WS_GetPRM_ThoroughfaresByAdminUnitId(BaseInput baseinput, Int64 adminUnitID, out List<tblPRM_Thoroughfare> ThoroughfareList)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetPRM_ThoroughfaresByAdminUnitId(baseinput, adminUnitID, out ThoroughfareList);
        }

        public BaseOutput WS_GetPRM_ThoroughfareByName(BaseInput baseinput, string ThoroughfareName, out tblPRM_Thoroughfare Thoroughfare)
        {
            BusinessLogic businessLogic = new BusinessLogic();

            return businessLogic.GetPRM_ThoroughfareByName(baseinput, ThoroughfareName, out Thoroughfare);
        }
        #endregion

        //burdan
        #region  Addresses

        public BaseOutput WS_AddAddress(BaseInput baseinput, tblAddress address, out tblAddress addressOut)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.AddAddress(baseinput, address, out addressOut);
        }

        public BaseOutput WS_DeleteAddress(BaseInput baseinput, tblAddress address)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.DeleteAddress(baseinput, address);
        }

        public BaseOutput WS_UpdateAddress(BaseInput baseinput, tblAddress address)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.UpdateAddress(baseinput, address);
        }

        public BaseOutput WS_GetAddresses(BaseInput baseinput, out List<tblAddress> addresses)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetAddresses(baseinput, out addresses);
        }

        public BaseOutput WS_GetAddressById(BaseInput baseinput, Int64 addressId, out tblAddress address)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetAddressById(baseinput, addressId, out address);
        }

        public BaseOutput WS_GetAddressesByCountryId(BaseInput baseinput, Int64 countryId, out List<tblAddress> addresses)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetAddressesByCountryId(baseinput, countryId, out addresses);
        }

        public BaseOutput WS_GetAddressesByVillageId(BaseInput baseinput, Int64 villageId, out List<tblAddress> addresses)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetAddressesByVillageId(baseinput, villageId, out addresses);
        }

        public BaseOutput WS_GetAddressesByUserId(BaseInput baseinput, Int64 userID, out List<tblAddress> addresses)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetAddressesByUserId(baseinput, userID, out addresses);
        }


        #endregion////burdan

        #region  ProductAddresses

        public BaseOutput WS_AddProductAddress(BaseInput baseinput, tblProductAddress address, out tblProductAddress addressOut)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.AddProductAddress(baseinput, address, out addressOut);
        }

        public BaseOutput WS_DeleteProductAddress(BaseInput baseinput, tblProductAddress address)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.DeleteProductAddress(baseinput, address);
        }

        public BaseOutput WS_UpdateProductAddress(BaseInput baseinput, tblProductAddress address, out tblProductAddress addressOut)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.UpdateProductAddress(baseinput, address, out addressOut);
        }

        public BaseOutput WS_GetProductAddresses(BaseInput baseinput, out List<tblProductAddress> addresses)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetProductAddresses(baseinput, out addresses);
        }

        public BaseOutput WS_GetProductAddressById(BaseInput baseinput, Int64 addressId, out tblProductAddress address)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetProductAddressById(baseinput, addressId, out address);
        }

        public BaseOutput WS_GetProductAddressesByAdminID(BaseInput baseinput, Int64 adminId, out List<tblProductAddress> addresses)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetProductAddressesByAdminID(baseinput, adminId, out addresses);
        }




        #endregion

        #region ProductCatalog
        public BaseOutput WS_DeleteProductCatalog(BaseInput baseinput, tblProductCatalog productCatalog)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.DeleteProductCatalog(baseinput, productCatalog);
        }

        public BaseOutput WS_AddProductCatalog(BaseInput baseinput, tblProductCatalog productCatalog, out tblProductCatalog productCatalogOut)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.AddProductCatalog(baseinput, productCatalog, out productCatalogOut);
        }

        public BaseOutput WS_GetProductCatalogs(BaseInput baseinput, out List<tblProductCatalog> pCatalogList)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetProductCatalogs(baseinput, out pCatalogList);
        }

        public BaseOutput WS_GetRootProductCatalogs(BaseInput baseinput, out List<tblProductCatalog> pCatalogList)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetRootProductCatalogs(baseinput, out pCatalogList);
        }

        public BaseOutput WS_GetProductCatalogsByParentId(BaseInput baseinput, int parentID, out List<tblProductCatalog> pCatalogList)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetProductCatalogsByParentId(baseinput, parentID, out pCatalogList);
        }
        public BaseOutput WS_GetProductCatalogDetailsByParentId(BaseInput baseinput, int parentID, out List<ProductCatalogDetail> pCatalogDetailList)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetProductCatalogDetailsByParentId(baseinput, parentID, out pCatalogDetailList);
        
        }
        public BaseOutput WS_GetProductCatalogsById(BaseInput baseinput, int productID, out tblProductCatalog pCatalog)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetProductCatalogsById(baseinput, productID, out pCatalog);

        }

        public BaseOutput WS_UpdateProductCatalog(BaseInput baseinput, tblProductCatalog productCatalog, out tblProductCatalog updatedproductCatalog)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.UpdateProductCatalog(baseinput, productCatalog, out updatedproductCatalog);

        }
        #endregion

        #region PhoneConfirmation
        public BaseOutput WS_SendConfirmationMessage(BaseInput baseinput, tblConfirmationMessage message)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.SendConfirmationMessage(baseinput, message);
        }
        public BaseOutput WS_GetConfirmationMessages(BaseInput baseinput, out List<tblConfirmationMessage> confirmationMessages)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetConfirmationMessages(baseinput, out confirmationMessages);
        }

        #endregion

        #region WS_Commucition



        public BaseOutput WS_AddCommunication(BaseInput baseinput, tblCommunication communication, out tblCommunication communicationOut)
        {

            return businessLogic.AddCommunication(baseinput, communication, out communicationOut);
        }


        public BaseOutput WS_DeleteCommunication(BaseInput baseinput, tblCommunication communication)
        {

            return businessLogic.DeleteCommunication(baseinput, communication);
        }

        public BaseOutput WS_UpdateCommunication(BaseInput baseinput, tblCommunication inputItem, out tblCommunication updatedItem)
        {
            return businessLogic.UpdateCommunication(baseinput, inputItem, out updatedItem);
        }

        public BaseOutput WS_GetCommunications(BaseInput baseinput, out List<tblCommunication> itemList)
        {
            return businessLogic.GetCommunications(baseinput, out itemList);
        }

        public BaseOutput WS_GetCommunicationById(BaseInput baseinput, long ID, out tblCommunication item)
        {
            return businessLogic.GetCommunicationById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetCommunicationByPersonId(BaseInput baseinput, Int64 personId, out List<tblCommunication> communication)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            return businessLogic.GetCommunicationByPersonId(baseinput, personId, out communication);
        }
        #endregion

        #region WS_Offer_Production

        public BaseOutput WS_AddOffer_Production(BaseInput baseinput, tblOffer_Production offer_Production, out tblOffer_Production Offer_ProductionOut)
        {
            return businessLogic.AddOffer_Production(baseinput, offer_Production, out Offer_ProductionOut);
        }

        public BaseOutput WS_DeleteOffer_Production(BaseInput baseinput, tblOffer_Production offer_Production)
        {

            return businessLogic.DeleteOffer_Production(baseinput, offer_Production);
        }

        public BaseOutput WS_UpdateOffer_Production(BaseInput baseinput, tblOffer_Production inputItem, out tblOffer_Production updatedItem)
        {
            return businessLogic.UpdateOffer_Production(baseinput, inputItem, out updatedItem);
        }

        public BaseOutput WS_GetOffer_ProductionById(BaseInput baseinput, long ID, out tblOffer_Production item)
        {
            return businessLogic.GetOffer_ProductionById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetOffer_Productions(BaseInput baseinput, out List<tblOffer_Production> itemList)
        {
            return businessLogic.GetOffer_Productions(baseinput, out itemList);
        }
        public BaseOutput WS_GetOffer_ProductionsByUserID(BaseInput baseinput, Int64 UserID, out List<tblOffer_Production> OfferProductionList)
        {

            return businessLogic.GetOffer_ProductionsByUserId(baseinput, UserID, out OfferProductionList);
        }
        public BaseOutput WS_GetOffAirOffer_ProductionsByUserID(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> OfferProductionList)
        {
            return businessLogic.GetOffAirOffer_ProductionsByUserId(baseinput, Offer, out OfferProductionList);
        }
        public BaseOutput WS_GetOnAirOffer_ProductionsByUserID(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> OfferProductionList)
        {
            return businessLogic.GetOnAirOffer_ProductionsByUserId(baseinput, Offer, out OfferProductionList);
        }
        public BaseOutput WS_GetOnAirOffer_ProductionsByUserIDSortedForDate(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> OfferProductionList)
        {
            return businessLogic.GetOnAirOffer_ProductionsByUserIDSortedForDate(baseinput, Offer, out OfferProductionList);
        }
        public BaseOutput WS_GetOnAirOffer_ProductionsByUserIDSortedForDateDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> OfferProductionList)
        {
            return businessLogic.GetOnAirOffer_ProductionsByUserIDSortedForDateDes(baseinput, Offer, out OfferProductionList);
        }
        public BaseOutput WS_GetOnAirOffer_ProductionsByUserIDSortedForPriceAsc(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> OfferProductionList)
        {
            return businessLogic.GetOnAirOffer_ProductionsByUserIDSortedForPriceAsc(baseinput, Offer, out OfferProductionList);
        }
        public BaseOutput WS_GetOnAirOffer_ProductionsByUserIDSortedForPriceDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> OfferProductionList)
        {
            return businessLogic.GetOnAirOffer_ProductionsByUserIDSortedForPriceDes(baseinput, Offer, out OfferProductionList);
        }
        public BaseOutput WS_GetOffAirOffer_ProductionsByUserIDSortedForDate(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> OfferProductionList)
        {
            return businessLogic.GetOffAirOffer_ProductionsByUserIDSortedForDate(baseinput, Offer, out OfferProductionList);
        }
        public BaseOutput WS_GetOffAirOffer_ProductionsByUserIDSortedForDateDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> OfferProductionList)
        {
            return businessLogic.GetOffAirOffer_ProductionsByUserIDSortedForDateDes(baseinput, Offer, out OfferProductionList);
        }
        public BaseOutput WS_GetOffAirOffer_ProductionsByUserIDSortedForPriceAsc(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> OfferProductionList)
        {
            return businessLogic.GetOffAirOffer_ProductionsByUserIDSortedForPriceAsc(baseinput, Offer, out OfferProductionList);
        }
        public BaseOutput WS_GetOffAirOffer_ProductionsByUserIDSortedForPriceDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> OfferProductionList)
        {
            return businessLogic.GetOffAirOffer_ProductionsByUserIDSortedForPriceDes(baseinput, Offer, out OfferProductionList);
        }

        #endregion
        #region WS_Potential_Production
        public BaseOutput WS_AddPotential_Production(BaseInput baseinput, tblPotential_Production potential_Production, out tblPotential_Production Potential_ProductionOut)
        {
            return businessLogic.AddPotential_Production(baseinput, potential_Production, out Potential_ProductionOut);
        }

        public BaseOutput WS_DeletePotential_Production(BaseInput baseinput, tblPotential_Production potential_Production)
        {
            return businessLogic.DeletePotential_Production(baseinput, potential_Production);
        }

        public BaseOutput WS_UpdatePotential_Production(BaseInput baseinput, tblPotential_Production inputItem, out tblPotential_Production updatedItem)
        {
            return businessLogic.UpdatePotential_Production(baseinput, inputItem, out updatedItem);
        }

        public BaseOutput WS_GetPotential_ProductionById(BaseInput baseinput, long ID, out tblPotential_Production item)
        {
            return businessLogic.GetPotential_ProductionById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetPotential_Productions(BaseInput baseinput, out List<tblPotential_Production> itemList)
        {
            return businessLogic.GetPotential_Productions(baseinput, out itemList);
        }

        public BaseOutput WS_UpdatePotential_ProductionForUserID(tblPotential_Production item, out List<tblPotential_Production> itemList)
        {
            return businessLogic.UpdatePotential_ProductionForUserID(item, out itemList);
        }

        public BaseOutput WS_GetConfirmedPotential_ProductionsByUserId(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList)
        {
            return businessLogic.GetConfirmedPotential_ProductionsByUserId(baseinput, potentialProduction, out itemList);
        }

        public BaseOutput WS_GetPotential_ProductionsByUserId(BaseInput baseinput, long userId, out List<tblPotential_Production> itemList)
        {
            return businessLogic.GetPotential_ProductionsByUserID(baseinput, userId, out itemList);
        }
        public BaseOutput WS_GetConfirmedPotential_ProductionsByStateAndUserId(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList)
        {
            return businessLogic.GetConfirmedPotential_ProductionsByStateAndUserId(baseinput, potentialProduction, out itemList);
        }
        public BaseOutput WS_GetConfirmedPotential_ProductionsByStateAndUserIdForPriceDes(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList)
        {
            return businessLogic.GetConfirmedPotential_ProductionsByStateAndUserIdForPriceDes(baseinput, potentialProduction, out itemList);
        }
        public BaseOutput WS_GetConfirmedPotential_ProductionsByStateAndUserIdForPriceAsc(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList)
        {
            return businessLogic.GetConfirmedPotential_ProductionsByStateAndUserIdForPriceAsc(baseinput, potentialProduction, out itemList);
        }
        #endregion
        #region WS_Demand_Production


        public BaseOutput WS_AddDemand_Production(BaseInput baseinput, tblDemand_Production demand_Production, out tblDemand_Production Demand_ProductionOut)
        {
            return businessLogic.AddDemand_Production(baseinput, demand_Production, out Demand_ProductionOut);
        }

        public BaseOutput WS_DeleteDemand_Production(BaseInput baseinput, tblDemand_Production demand_Production)
        {
            return businessLogic.DeleteDemand_Production(baseinput, demand_Production);
        }

        public BaseOutput WS_UpdateDemand_Production(BaseInput baseinput, tblDemand_Production inputItem, out tblDemand_Production updatedItem)
        {
            return businessLogic.UpdateDemand_Production(baseinput, inputItem, out updatedItem);
        }

        public BaseOutput WS_GetDemand_ProductionById(BaseInput baseinput, long ID, out tblDemand_Production item)
        {
            return businessLogic.GetDemand_ProductionById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetDemand_Productions(BaseInput baseinput, out List<tblDemand_Production> itemList)
        {
            return businessLogic.GetDemand_Productions(baseinput, out itemList);
        }
        public BaseOutput WS_UpdateDemand_ProductionForUserID(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList)
        {
            return businessLogic.UpdateDemand_ProductionForUserID(baseinput, item, out itemList);
        }

        public BaseOutput WS_UpdateDemand_ProductionForStartAndEndDate(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList)
        {
            return businessLogic.UpdateDemand_ProductionForStartAndEndDate(baseinput, item, out itemList);
        }
        public BaseOutput WS_GetDemand_ProductionsByStateAndUserID(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList)
        {
            return businessLogic.GetDemand_ProductionsByStateAndUserID(baseinput, item, out itemList);
        }

        #endregion
        #region  WS_Announcement


        public BaseOutput WS_AddAnnouncement(BaseInput baseinput, tblAnnouncement announcement, out tblAnnouncement AnnouncementOut)
        {
            return businessLogic.AddAnnouncement(baseinput, announcement, out AnnouncementOut);
        }

        public BaseOutput WS_DeleteAnnouncement(BaseInput baseinput, tblAnnouncement announcement)
        {
            return businessLogic.DeleteAnnouncement(baseinput, announcement);
        }

        public BaseOutput WS_UpdateAnnouncement(BaseInput baseinput, tblAnnouncement inputItem, out tblAnnouncement updatedItem)
        {
            return businessLogic.UpdateAnnouncement(baseinput, inputItem, out updatedItem);
        }

        public BaseOutput WS_GetAnnouncementById(BaseInput baseinput, long ID, out tblAnnouncement item)
        {
            return businessLogic.GetAnnouncementById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetAnnouncements(BaseInput baseinput, out List<tblAnnouncement> itemList)
        {
            return businessLogic.GetAnnouncements(baseinput, out itemList);
        }
        public BaseOutput WS_GetAnnouncementsByProductId(BaseInput baseinput, long productID, out List<tblAnnouncement> itemList)
        {
            return businessLogic.GetAnnouncementsByProductId(baseinput, productID, out itemList);
        }

        public BaseOutput WS_GetAnnouncementDetailsByProductId(BaseInput baseinput, long productID,  out List<AnnouncementDetail> itemList)
        {
            return businessLogic.GetAnnouncementDetailsByProductId(baseinput, productID, out itemList);
        }

        public BaseOutput WS_GetAnnouncementDetailById(BaseInput baseinput, long ID, out AnnouncementDetail item)
        {
            return businessLogic.GetAnnouncementDetailById(baseinput, ID, out item);
        }
        public BaseOutput WS_GetAnnouncementDetails(BaseInput baseinput,  out List<AnnouncementDetail> itemList)
        {
            return businessLogic.GetAnnouncementDetails(baseinput, out itemList);
        }
        public BaseOutput WS_GetAnnouncementsByYearAndPartOfYear(BaseInput baseinput, string year, string month, out List<tblAnnouncement> itemList)
        {
            return businessLogic.GetAnnouncementsByYearAndPartOfYear(baseinput, year,month, out itemList);
        }
        #endregion
        #region WS_Employee


        public BaseOutput WS_AddEmployee(BaseInput baseinput, tblEmployee employee, out tblEmployee EmployeeOut)
        {
            return businessLogic.AddEmployee(baseinput, employee, out EmployeeOut);
        }

        public BaseOutput WS_DeleteEmployee(BaseInput baseinput, tblEmployee employee)
        {
            return businessLogic.DeleteEmployee(baseinput, employee);
        }

        public BaseOutput WS_UpdateEmployee(BaseInput baseinput, tblEmployee inputItem, out tblEmployee updatedItem)
        {
            return businessLogic.UpdateEmployee(baseinput, inputItem, out updatedItem);
        }

        public BaseOutput WS_GetEmployeeById(BaseInput baseinput, long ID, out tblEmployee item)
        {
            return businessLogic.GetEmployeeById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetEmployees(BaseInput baseinput, out List<tblEmployee> itemList)
        {
            return businessLogic.GetEmployees(baseinput, out itemList);
        }
        #endregion

        #region WS_Person
        public BaseOutput WS_AddPerson(BaseInput baseinput, tblPerson person, out tblPerson PersonOut)
        {
            return businessLogic.AddPerson(baseinput, person, out PersonOut);
        }

        public BaseOutput WS_DeletePerson(BaseInput baseinput, tblPerson person)
        {
            return businessLogic.DeletePerson(baseinput, person);
        }

        public BaseOutput WS_UpdatePerson(BaseInput baseinput, tblPerson person, out tblPerson updatedItem)
        {
            return businessLogic.UpdatePerson(baseinput, person, out updatedItem);
        }

        public BaseOutput WS_GetPersonById(BaseInput baseinput, long ID, out tblPerson item)
        {
            return businessLogic.GetPersonById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetPersonByUserId(BaseInput baseinput, long userID, out tblPerson item)
        {
            return businessLogic.GetPersonByUserId(baseinput, userID, out item);
        }

        public BaseOutput WS_GetPersons(BaseInput baseinput, out List<tblPerson> itemList)
        {
            return businessLogic.GetPersons(baseinput, out itemList);
        }
        public BaseOutput WS_GetPersonByPinNumber(BaseInput baseinput, string pinNumber, out tblPerson item) 
        {
            return businessLogic.GetPersonByPinNumber(baseinput, pinNumber, out item);
        }
        #endregion

        #region WS_User


        public BaseOutput WS_AddUser(BaseInput baseinput, tblUser user, out tblUser UserOut)
        {
            return businessLogic.AddUser(baseinput, user, out UserOut);
        }

        public BaseOutput WS_DeleteUser(BaseInput baseinput, tblUser user)
        {
            return businessLogic.DeleteUser(baseinput, user);
        }

        public BaseOutput WS_UpdateUser(BaseInput baseinput, tblUser user, out tblUser updatedItem)
        {
            return businessLogic.UpdateUser(baseinput, user, out updatedItem);
        }

        public BaseOutput WS_GetUserById(BaseInput baseinput, long ID, out tblUser item)
        {
            return businessLogic.GetUserById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetUsers(BaseInput baseinput, out List<tblUser> itemList)
        {
            return businessLogic.GetUsers(baseinput, out itemList);
        }
        public BaseOutput WS_GetUserByUserName(BaseInput baseinput, string UserName, out tblUser item)
        {
            return businessLogic.GetUserByUserName(baseinput, UserName, out item);
        }
        public BaseOutput WS_GetGovernmentOrganisations(BaseInput baseinput, long govErnmentRoleEnum, out List<tblUser> itemList)
        {
            return businessLogic.GetGovernmentOrganisations(baseinput, govErnmentRoleEnum, out itemList);
        }

        public BaseOutput WS_GetOrganisationTypeUsers(BaseInput baseinput, long govRoleEnum, long userType, out List<tblUser> itemList)
        {
            return businessLogic.GetOrganisationTypeUsers(baseinput, govRoleEnum, userType, out itemList);
        }
        public BaseOutput WS_GetUsersByUserType(BaseInput baseinput, long userTypeID, out List<tblUser> itemList)
        {
            return businessLogic.GetUsersByUserType(baseinput, userTypeID, out itemList);
        }

        #endregion
        #region WS_Role
        public BaseOutput WS_AddRole(BaseInput baseinput, tblRole role, out tblRole RoleOut)
        {
            return businessLogic.AddRole(baseinput, role, out RoleOut);
        }

        public BaseOutput WS_DeleteRole(BaseInput baseinput, tblRole role)
        {
            return businessLogic.DeleteRole(baseinput, role);

        }

        public BaseOutput WS_UpdateRole(BaseInput baseinput, tblRole role, out tblRole item)
        {
            return businessLogic.UpdateRole(baseinput, role, out item);
        }

        public BaseOutput WS_GetRoleById(BaseInput baseinput, long ID, out tblRole item)
        {
            return businessLogic.GetRoleById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetRoles(BaseInput baseinput, out List<tblRole> itemList)
        {
            return businessLogic.GetRoles(baseinput, out itemList);
        }

        public BaseOutput WS_GetRoleByName(BaseInput baseinput, string name, out tblRole item)
        {
            return businessLogic.GetRoleByName(baseinput, name, out item);
        }

        public BaseOutput WS_GetRolesNotOwnedByUser(BaseInput baseinput, long userId, out List<tblRole> itemList)
        {
            return businessLogic.GetRolesNotOwnedByUser(baseinput, userId, out itemList);
        }

        public BaseOutput WS_GetRolesNotAllowedInPage(BaseInput baseinput, long pageId, out List<tblRole> itemList)
        {
            return businessLogic.GetRolesNotAllowedInPage(baseinput, pageId, out itemList);
        }

        #endregion


        #region WS_Expertise


        public BaseOutput WS_AddExpertise(BaseInput baseinput, tblExpertise expertise, out tblExpertise ExpertiseOut)
        {
            return businessLogic.AddExpertise(baseinput, expertise, out ExpertiseOut);
        }

        public BaseOutput WS_DeleteExpertise(BaseInput baseinput, tblExpertise expertise)
        {
            return businessLogic.DeleteExpertise(baseinput, expertise);
        }

        public BaseOutput WS_UpdateExpertise(BaseInput baseinput, tblExpertise expertise, out tblExpertise item)
        {
            return businessLogic.UpdateExpertise(baseinput, expertise, out item);
        }

        public BaseOutput WS_GetExpertiseById(BaseInput baseinput, long ID, out tblExpertise item)
        {
            return businessLogic.GetExpertiseById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetExpertises(BaseInput baseinput, out List<tblExpertise> itemList)
        {
            return businessLogic.GetExpertises(baseinput, out itemList);
        }
        #endregion

        #region WS_Title


        public BaseOutput WS_AddTitle(BaseInput baseinput, tblTitle title, out tblTitle TitleOut)
        {
            return businessLogic.AddTitle(baseinput, title, out TitleOut);
        }

        public BaseOutput WS_DeleteTitle(BaseInput baseinput, tblTitle title)
        {
            return businessLogic.DeleteTitle(baseinput, title);
        }

        public BaseOutput WS_UpdateTitle(BaseInput baseinput, tblTitle title, out tblTitle item)
        {
            return businessLogic.UpdateTitle(baseinput, title, out item);
        }

        public BaseOutput WS_GetTitleById(BaseInput baseinput, long ID, out tblTitle item)
        {
            return businessLogic.GetTitleById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetTitles(BaseInput baseinput, out List<tblTitle> itemList)
        {
            return businessLogic.GetTitles(baseinput, out itemList);
        }
        #endregion
        #region WS_Party


        public BaseOutput WS_AddParty(BaseInput baseinput, tblParty party, out tblParty PartyOut)
        {
            return businessLogic.AddParty(baseinput, party, out PartyOut);
        }

        public BaseOutput WS_DeleteParty(BaseInput baseinput, tblParty party)
        {
            return businessLogic.DeleteParty(baseinput, party);
        }

        public BaseOutput WS_UpdateParty(BaseInput baseinput, tblParty party, out tblParty item)
        {
            return businessLogic.UpdateParty(baseinput, party, out item);
        }

        public BaseOutput WS_GetPartyById(BaseInput baseinput, long ID, out tblParty item)
        {
            return businessLogic.GetPartyById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetParties(BaseInput baseinput, out List<tblParty> itemList)
        {
            return businessLogic.GetParties(baseinput, out itemList);
        }
        #endregion
        #region WS_Organization


        public BaseOutput WS_AddOrganization(BaseInput baseinput, tblOrganization organization, out tblOrganization OrganizationOut)
        {
            return businessLogic.AddOrganization(baseinput, organization, out OrganizationOut);
        }

        public BaseOutput WS_DeleteOrganization(BaseInput baseinput, tblOrganization organization)
        {
            return businessLogic.DeleteOrganization(baseinput, organization);
        }

        public BaseOutput WS_UpdateOrganization(BaseInput baseinput, tblOrganization organization, out tblOrganization item)
        {
            return businessLogic.UpdateOrganization(baseinput, organization, out item);
        }

        public BaseOutput WS_GetOrganizationById(BaseInput baseinput, long ID, out tblOrganization item)
        {
            return businessLogic.GetOrganizationById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetOrganizations(BaseInput baseinput, out List<tblOrganization> itemList)
        {
            return businessLogic.GetOrganizations(baseinput, out itemList);
        }
        #endregion
        #region  WS_Foreign_Organization




        public BaseOutput WS_AddForeign_Organization(BaseInput baseinput, tblForeign_Organization foreign_Organization, out tblForeign_Organization Foreign_OrganizationOut)
        {
            return businessLogic.AddForeign_Organization(baseinput, foreign_Organization, out Foreign_OrganizationOut);
        }

        public BaseOutput WS_DeleteForeign_Organization(BaseInput baseinput, tblForeign_Organization foreign_Organization)
        {
            return businessLogic.DeleteForeign_Organization(baseinput, foreign_Organization);
        }

        public BaseOutput WS_UpdateForeign_Organization(BaseInput baseinput, tblForeign_Organization foreign_Organization, out tblForeign_Organization item)
        {
            return businessLogic.UpdateForeign_Organization(baseinput, foreign_Organization, out item);
        }

        public BaseOutput WS_GetForeign_OrganizationById(BaseInput baseinput, long ID, out tblForeign_Organization item)
        {
            return businessLogic.GetForeign_OrganizationById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetForeign_Organizations(BaseInput baseinput, out List<tblForeign_Organization> itemList)
        {
            return businessLogic.GetForeign_Organizations(baseinput, out itemList);
        }


        public BaseOutput WS_GetForeign_OrganizationByUserId(BaseInput baseinput, Int64 userId, out tblForeign_Organization item)
        {
            return businessLogic.GetForeign_OrganizationByUserId(baseinput, userId, out item);

        }
        public BaseOutput WS_GetForeign_OrganizationByVoen(BaseInput baseinput, string voen, out tblForeign_Organization item) 
        {
            return businessLogic.GetForeign_OrganizationByVoen(baseinput, voen, out item);
        }

        public BaseOutput WS_GetForeign_OrganisationsByParentId(BaseInput baseInput, long Id, out List<tblForeign_Organization>itemList)
        {
            return businessLogic.GetForeign_OrganisationsByParentId(baseInput, Id, out itemList);
        }
        #endregion

        #region WS_ConfirmationMessage

        public BaseOutput WS_AddConfirmationMessage(BaseInput baseinput, tblConfirmationMessage confirmationMessage, out tblConfirmationMessage ConfirmationMessageOut)
        {
            return businessLogic.AddConfirmationMessage(baseinput, confirmationMessage, out ConfirmationMessageOut);
        }

        public BaseOutput WS_DeleteConfirmationMessage(BaseInput baseinput, tblConfirmationMessage confirmationMessage)
        {
            return businessLogic.DeleteConfirmationMessage(baseinput, confirmationMessage);
        }

        public BaseOutput WS_UpdateConfirmationMessage(BaseInput baseinput, tblConfirmationMessage confirmationMessage, out tblConfirmationMessage item)
        {
            return businessLogic.UpdateConfirmationMessage(baseinput, confirmationMessage, out item);
        }
        public BaseOutput WS_GetConfirmationMessageById(BaseInput baseinput, long ID, out tblConfirmationMessage item)
        {
            return businessLogic.GetConfirmationMessageById(baseinput, ID, out item);
        }
        #endregion


        #region ProductCatalogControl

        public BaseOutput WS_AddProductCatalogControl(BaseInput baseinput, tblProductCatalogControl item, out tblProductCatalogControl itemOut)
        {
            return businessLogic.AddProductCatalogControl(baseinput, item, out itemOut);
        }

        public BaseOutput WS_DeleteProductCatalogControl(BaseInput baseinput, tblProductCatalogControl item)
        {
            return businessLogic.DeleteProductCatalogControl(baseinput, item);
        }

        public BaseOutput WS_GetProductCatalogControls(BaseInput baseinput, out List<tblProductCatalogControl> itemList)
        {
            return businessLogic.GetProductCatalogControls(baseinput, out itemList);
        }

        public BaseOutput WS_GetProductCatalogControlById(BaseInput baseinput, long Id, out tblProductCatalogControl item)
        {
            return businessLogic.GetProductCatalogControlById(baseinput, Id, out item);
        }

        public BaseOutput WS_GetProductCatalogControlsByProductID(BaseInput baseinput, long productId, out List<tblProductCatalogControl> itemList)
        {
            return businessLogic.GetProductCatalogControlsByProductID(baseinput, productId, out itemList);

        }

        public BaseOutput WS_GetProductCatalogControlsByECategoryID(BaseInput baseinput, long enumCategoryID, out List<tblProductCatalogControl> itemList)
        {
            return businessLogic.GetProductCatalogControlsByECategoryID(baseinput, enumCategoryID, out itemList);
        }

        public BaseOutput WS_GetProductCatalogControlsByEValueID(BaseInput baseinput, long enumValueID, out List<tblProductCatalogControl> itemList)
        {
            return businessLogic.GetProductCatalogControlsByEValueID(baseinput, enumValueID, out itemList);
        }

        public BaseOutput WS_UpdateProductCatalogControl(BaseInput baseinput, tblProductCatalogControl item, out tblProductCatalogControl updatedItem)
        {
            return businessLogic.UpdateProductCatalogControl(baseinput, item, out  updatedItem);
        }

        public BaseOutput WS_AddProductCatalogControlList(BaseInput baseinput, List<tblProductCatalogControl> itemList, out List<tblProductCatalogControl> itemListOut)
        {
            return businessLogic.AddProductCatalogControlList(baseinput, itemList, out itemListOut);
        }


        public BaseOutput WS_DeleteAllProductCatalogControlByProductID(BaseInput baseinput, long productID)
        {
            return businessLogic.DeleteAllProductCatalogControlByProductID(baseinput, productID);
        }
        #endregion

        #region WS_ProductionDocument

        public BaseOutput WS_AddProductionDocument(BaseInput baseinput, tblProduction_Document ProductionDocument, out tblProduction_Document ProductionDocumentOut)
        {
            return businessLogic.AddProductionDocument(baseinput, ProductionDocument, out ProductionDocumentOut);
        }

        public BaseOutput WS_DeleteProductionDocument(BaseInput baseinput, tblProduction_Document ProductionDocument)
        {
            return businessLogic.DeleteProductionDocument(baseinput, ProductionDocument);
        }

        public BaseOutput WS_UpdateProductionDocument(BaseInput baseinput, tblProduction_Document ProductionDocument, out tblProduction_Document updatedProductionDocument)
        {
            return businessLogic.UpdateProductionDocument(baseinput, ProductionDocument, out updatedProductionDocument);
        }

        public BaseOutput WS_GetProductionDocumentById(BaseInput baseinput, long ID, out tblProduction_Document item)
        {
            return businessLogic.GetProductionDocumentById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetProductionDocumentsByDemand_Production_Id(BaseInput baseinput, tblProduction_Document item, out List<tblProduction_Document> itemList)
        {
            return businessLogic.GetProductionDocumentsByDemand_Production_Id(baseinput, item, out itemList);
        }

        public BaseOutput WS_GetProductionDocumentsByOffer_Production_Id(BaseInput baseinput, tblProduction_Document item, out List<tblProduction_Document> itemList)
        {
            return businessLogic.GetProductionDocumentsByOffer_Production_Id(baseinput, item, out itemList);
        }

        public BaseOutput WS_GetProductionDocumentsByPotential_Production_Id(BaseInput baseinput, tblProduction_Document item, out List<tblProduction_Document> itemList)
        {
            return businessLogic.GetProductionDocumentsByPotential_Production_Id(baseinput, item, out itemList);
        }
        public BaseOutput WS_GetProductionDocumentsByGroupId(BaseInput baseinput, string GroupID, out List<tblProduction_Document> itemList)
        {
            return businessLogic.GetProductionDocumentsByGroupId(baseinput, GroupID, out itemList);
        }


        public BaseOutput WS_GetProductionDocuments(BaseInput baseinput, out List<tblProduction_Document> itemList)
        {
            return businessLogic.GetProductionDocuments(baseinput, out itemList);
        }
        public BaseOutput WS_GetProductionDocumentsByGroupIdAndPotential_Production_Id(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> itemList)
        {
            return businessLogic.GetProductionDocumentsByGroupIdAndPotential_Production_Id(baseinput, productionDocument, out itemList);
        }
        public BaseOutput WS_GetProductionDocumentsByGroupIdAndOffer_Production_Id(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> itemList)
        {
            return businessLogic.GetProductionDocumentsByGroupIdAndOffer_Production_Id(baseinput, productionDocument, out itemList);
        }
        public BaseOutput WS_GetProductionDocumentsByGroupIdAndDemand_Production_Id(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> itemList)
        {
            return businessLogic.GetProductionDocumentsByGroupIdAndDemand_Production_Id(baseinput, productionDocument, out itemList);
        }
        public BaseOutput WS_GetDocumentSizeByGroupIdAndPotentialProductionID(tblProduction_Document production_Document, out Int64 totalSize)
        {
            return businessLogic.GetDocumentSizeByGroupIdAndPotentialProductionID(production_Document, out totalSize);
        }
        public BaseOutput WS_GetDocumentSizeByGroupIdAndOfferProductionID(tblProduction_Document production_Document, out Int64 totalSize)
        {
            return businessLogic.GetDocumentSizeByGroupIdAndOfferProductionID(production_Document, out totalSize);
        }
        public BaseOutput WS_GetDocumentSizeByGroupIdAndDemandProductionID(tblProduction_Document production_Document, out Int64 totalSize)
        {
            return businessLogic.GetDocumentSizeByGroupIdAndDemandProductionID(production_Document, out totalSize);
        }
        public BaseOutput WS_UpdateProductionDocumentForGroupID(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> productionDocumentList)
        {
            return businessLogic.UpdateProductionDocumentForGroupID(baseinput, productionDocument, out productionDocumentList);
        }
        #endregion

        #region WS_ProductDocument

        public BaseOutput WS_AddProductDocument(BaseInput baseinput, tblProduct_Document ProductDocument, out tblProduct_Document ProductDocumentOut)
        {
            return businessLogic.AddProductDocument(baseinput, ProductDocument, out ProductDocumentOut);
        }

        public BaseOutput WS_DeleteProductDocument(BaseInput baseinput, tblProduct_Document ProductDocument)
        {
            return businessLogic.DeleteProductDocument(baseinput, ProductDocument);
        }

        public BaseOutput WS_UpdateProductDocument(BaseInput baseinput, tblProduct_Document ProductDocument, out tblProduct_Document updatedProductDocument)
        {
            return businessLogic.UpdateProductDocument(baseinput, ProductDocument, out updatedProductDocument);
        }

        public BaseOutput WS_GetProductDocumentById(BaseInput baseinput, long ID, out tblProduct_Document item)
        {
            return businessLogic.GetProductDocumentById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetProductDocumentsByProductCatalogId(BaseInput baseinput, Int64 productCatalogId, out List<tblProduct_Document> itemList)
        {
            return businessLogic.GetProductDocumentsByProductCatalogId(baseinput, productCatalogId, out itemList);
        }

        public BaseOutput WS_GetProductDocuments(BaseInput baseinput, out List<tblProduct_Document> itemList)
        {
            return businessLogic.GetProductDocuments(baseinput, out itemList);
        }
        
        public BaseOutput WS_DeleteProductDocumentByProductID(BaseInput baseinput, tblProduct_Document ProductDocument, out List<tblProduct_Document> productDocumentListOut)
        {
            return businessLogic.DeleteProductDocumentByProductID(baseinput, ProductDocument, out productDocumentListOut);
        }
       
        #endregion
        #region Ws_ProductionControl

        public BaseOutput WS_AddProductionControl(BaseInput baseinput, tblProductionControl productionControl, out tblProductionControl productionControltOut)
        {
            return businessLogic.AddProductionControl(baseinput, productionControl, out productionControltOut);
        }

        public BaseOutput WS_DeleteProductionControl(BaseInput baseinput, tblProductionControl ProductionControl)
        {
            return businessLogic.DeleteProductionControl(baseinput, ProductionControl);
        }

        public BaseOutput WS_UpdateProductionControl(BaseInput baseinput, tblProductionControl ProductionControl, out tblProductionControl ProductionControlOut)
        {
            return businessLogic.UpdateProductionControl(baseinput, ProductionControl, out ProductionControlOut);
        }

        public BaseOutput WS_GetProductionControlById(BaseInput baseinput, Int64 ID, out tblProductionControl ProductionControl)
        {
            return businessLogic.GetProductionControlById(baseinput, ID, out ProductionControl);
        }

        public BaseOutput WS_GetProductionControlsByPotentialProductionId(BaseInput baseinput, Int64 potentialProductionId, out List<tblProductionControl> ProductionControlList)
        {
            return businessLogic.GetProductionControlsByPotentialProductionId(baseinput, potentialProductionId, out ProductionControlList);
        }

        public BaseOutput WS_GetProductionControlsByOfferProductionId(BaseInput baseinput, Int64 offerProductionId, out List<tblProductionControl> ProductionControlList)
        {
            return businessLogic.GetProductionControlsByOfferProductionId(baseinput, offerProductionId, out ProductionControlList);
        }
        public BaseOutput WS_GetProductionControlsByDemandProductionId(BaseInput baseinput, Int64 demandProductionId, out List<tblProductionControl> ProductionControlList)
        {
            return businessLogic.GetProductionControlsByDemandProductionId(baseinput, demandProductionId, out ProductionControlList);
        }

        public BaseOutput WS_GetProductionControls(BaseInput baseinput, out List<tblProductionControl> ProductionControl)
        {
            return businessLogic.GetProductionControls(baseinput, out ProductionControl);
        }

        public BaseOutput WS_DeleteProductionControlsByProduction_Type_ev_Id(BaseInput baseinput, Int64 production_Type_ev_ID)
        {
            return businessLogic.DeleteProductionControlsByProduction_Type_ev_Id(baseinput, production_Type_ev_ID);
        }
        #endregion
        #region WS_Production_Calendar

        public BaseOutput WS_AddProduction_Calendar(BaseInput baseinput, tblProduction_Calendar ProductionCalendar, out tblProduction_Calendar productionCalendarOut)
        {
            return businessLogic.AddProduction_Calendar(baseinput, ProductionCalendar, out productionCalendarOut);
        }

        public BaseOutput WS_DeleteProduction_Calendar(BaseInput baseinput, tblProduction_Calendar ProductionCalendar)
        {
            return businessLogic.DeleteProduction_Calendar(baseinput, ProductionCalendar);
        }

        public BaseOutput WS_GetProduction_Calendar(BaseInput baseInput, out List<tblProduction_Calendar> ProductionCalendar)
        {
            return businessLogic.GetProduction_Calendar(baseInput, out ProductionCalendar);
        }

        public BaseOutput WS_GetProduction_CalendarById(BaseInput baseinput, Int64 Id, out tblProduction_Calendar ProductionCalendar)
        {
            return businessLogic.GetProduction_CalendarById(baseinput, Id, out ProductionCalendar);
        }

        public BaseOutput WS_GetProductionCalendarByProductionId(BaseInput baseinput, Int64 ProductionId,Int64 productionType_eV_Id ,out tblProduction_Calendar ProductionCalendar)
        {
            return businessLogic.GetProductionCalendarByProductionId(baseinput, ProductionId,productionType_eV_Id, out ProductionCalendar);
        }

        public BaseOutput WS_GetProductionCalendarProductiontypeeVId(BaseInput baseinput, Int64 production_type_eV_Id, out List<tblProduction_Calendar> ProductionCalendarlList)
        {
            return businessLogic.GetProductionCalendarProductiontypeeVId(baseinput, production_type_eV_Id, out ProductionCalendarlList);
        }

        public BaseOutput WS_GetProductionCalendartransportationeVId(BaseInput baseinput, Int64 t_eV_Id, Int64 Year, out List<tblProduction_Calendar> ProductionCalendarlList)
        {
            return businessLogic.GetProductionCalendartransportationeVId(baseinput, t_eV_Id, Year, out ProductionCalendarlList);
        }

        public BaseOutput WS_UpdateProductionCalendar(BaseInput baseinput, tblProduction_Calendar ProductionCalendar, out tblProduction_Calendar ProductionCalendarOut)
        {
            return businessLogic.UpdateProductionCalendar(baseinput, ProductionCalendar, out ProductionCalendarOut);
        }

        #endregion

        #region Jira Request

        public BaseOutput WS_GetDocumentSizebyGroupId(string groupID, long production_type_eVId, out long totalSize)
        {
            return businessLogic.GetDocumentSizebyGroupId(groupID, production_type_eVId, out totalSize);
        }

        public BaseOutput WS_GetListOfPotensialProductionByUserId(string userID, out List<ProductionDetail> list)
        {
            return businessLogic.GetListOfPotensialProductionByUserId(userID, out list);
        }
        #endregion


        #region TblComMessage



        public BaseOutput WS_AddComMessage(BaseInput baseinput, tblComMessage item, out tblComMessage itemOut)
        {
            return businessLogic.AddComMessage(baseinput, item, out itemOut);
        }

        public BaseOutput WS_DeleteComMessage(BaseInput baseinput, tblComMessage item)
        {
            return businessLogic.DeleteComMessage(baseinput, item);
        }
        public BaseOutput WS_UpdateComMessage(BaseInput baseinput, tblComMessage item, out tblComMessage itemOut)
        {
            return businessLogic.UpdateComMessage(baseinput, item, out itemOut);
        }
        public BaseOutput WS_GetComMessageById(BaseInput baseInput, long ID, out tblComMessage itemOut)
        {
            return businessLogic.GetComMessageById(baseInput, ID, out itemOut);
        }

        public BaseOutput WS_GetComMessagesyByGroupId(BaseInput baseInput, long groupId, out List<tblComMessage> itemListOut)
        {
            return businessLogic.GetComMessagesyByGroupId(baseInput, groupId, out itemListOut);
        }

        public BaseOutput WS_GetComMessagesyByToUserId(BaseInput baseInput, long toUserId, out List<tblComMessage> itemListOut)
        {
            return businessLogic.GetComMessagesyByToUserId(baseInput, toUserId, out itemListOut);
        }

        public BaseOutput WS_GetComMessagesyByFromUserId(BaseInput baseInput, long fromUserId, out List<tblComMessage> itemListOut)
        {
            return businessLogic.GetComMessagesyByFromUserId(baseInput, fromUserId, out itemListOut);
        }

        public BaseOutput WS_GetComMessagesyByFromUserIDToUserId(BaseInput baseInput, long fromUserId, long toUserId, out List<tblComMessage> itemListOut)
        {
            return businessLogic.GetComMessagesyByFromUserIDToUserId(baseInput, fromUserId, toUserId, out itemListOut);
        }
        //ferid
        public BaseOutput WS_GetComMessagesyByToUserIdSortedForDateAsc(BaseInput baseInput, long toUserId, out List<tblComMessage> itemListOut)
        {
            return businessLogic.GetComMessagesyByToUserIdSortedForDateAsc(baseInput, toUserId, out itemListOut);
        }
        public BaseOutput WS_GetComMessagesyByToUserIdSortedForDateDes(BaseInput baseInput, long toUserId, out List<tblComMessage> itemListOut)
        {
            return businessLogic.GetComMessagesyByToUserIdSortedForDateDes(baseInput, toUserId, out itemListOut);
        }
        public BaseOutput WS_GetComMessagesyByFromUserIdSortedForDateAsc(BaseInput baseInput, long fromUserId, out List<tblComMessage> itemListOut)
        {
            return businessLogic.GetComMessagesyByFromUserIdSortedForDateAsc(baseInput, fromUserId, out itemListOut);
        }
        public BaseOutput WS_GetComMessagesyByFromUserIdSortedForDateDes(BaseInput baseInput, long fromUserId, out List<tblComMessage> itemListOut)
        {
            return businessLogic.GetComMessagesyByFromUserIdSortedForDateDes(baseInput, fromUserId, out itemListOut);
        }

        public BaseOutput WS_GetNotReadComMessagesByToUserId(BaseInput baseInput, Int64 toUserId, out List<tblComMessage> itemListOut)
        {
            return businessLogic.GetNotReadComMessagesByToUserId(baseInput, toUserId, out itemListOut);

        }

        ///
        #endregion

        #region ProductionDetails
        public BaseOutput WS_GetProductionDetailist(BaseInput baseinput, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetProductionDetailist(baseinput, out itemList);
        }

        #endregion

        #region Sql Operation
        public BaseOutput WS_GetAdminUnitListForID(BaseInput baseinput, long ID, out List<tblPRM_AdminUnit> itemList)
        {
            return businessLogic.GetAdminUnitListForID(baseinput, ID, out itemList);
        }
     
        public BaseOutput WS_GetProductCatalogListForID(BaseInput baseinput, long ID, out List<tblProductCatalog> itemList)
        {
            return businessLogic.GetProductCatalogListForID(baseinput, ID, out itemList);
        }


        public BaseOutput WS_GetPotensialProductionDetailistForUser(BaseInput baseinput, long userID, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetPotensialProductionDetailistForUser(baseinput, userID, out itemList);
        }


        public BaseOutput WS_GetPotensialProductionDetailistForCreateUser(BaseInput baseinput, string createdUser, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetPotensialProductionDetailistForCreateUser(baseinput, createdUser, out itemList);
        }
        public BaseOutput WS_GetPotensialProductionDetailistForEValueId(BaseInput baseinput, long state_eV_Id, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetPotensialProductionDetailistForEValueId(baseinput, state_eV_Id, out itemList);
        }


        public BaseOutput WS_GetPotensialProductionDetailistForMonitoringEVId(BaseInput baseinput,Int64 userID, long monintoring_eV_Id, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetPotensialProductionDetailistForMonitoringEVId(baseinput,userID, monintoring_eV_Id, out itemList);
        }


        public BaseOutput WS_GetPotensialProductionDetailistForStateEVId(BaseInput baseinput, long userID, long state_eV_Id, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetPotensialProductionDetailistForStateEVId(baseinput, userID, state_eV_Id, out itemList);
        }
        public BaseOutput WS_GetOfferProductionDetailistForUser(BaseInput baseinput, long userID, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetOfferProductionDetailistForUser(baseinput, userID, out itemList);
        }

        public BaseOutput WS_GetOfferProductionDetailistForEValueId(BaseInput baseinput, long state_eV_Id, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetOfferProductionDetailistForEValueId(baseinput, state_eV_Id, out itemList);
        }

        public BaseOutput WS_GetOfferProductionDetailistForMonitoringEVId(BaseInput baseinput,Int64 userID, long monintoring_eV_Id, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetOfferProductionDetailistForMonitoringEVId(baseinput, userID,monintoring_eV_Id, out itemList);
        }
        public BaseOutput WS_GetOfferProductionDetailistForStateEVId(BaseInput baseinput, long userID, long state_eV_Id, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetOfferProductionDetailistForStateEVId(baseinput, userID, state_eV_Id, out itemList);
        }
        public BaseOutput WS_GetDemandProductionDetailistForUser(BaseInput baseinput, long userID, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetDemandProductionDetailistForUser(baseinput, userID, out itemList);
        }

        public BaseOutput WS_GetDemandProductionDetailistForEValueId(BaseInput baseinput, long state_eV_Id, out List<ProductionDetail> itemList)
        {
            return businessLogic.GetDemandProductionDetailistForEValueId(baseinput, state_eV_Id, out itemList);
        }

        #endregion

        #region WS_UserRole
        public BaseOutput WS_AddUserRole(BaseInput baseinput, tblUserRole userRole, out tblUserRole UserRoleOut)
        {
            return businessLogic.AddUserRole(baseinput, userRole, out UserRoleOut);
        }

        public BaseOutput WS_DeleteUserRole(BaseInput baseinput, tblUserRole userRole)
        {
            return businessLogic.DeleteUserRole(baseinput, userRole);
        }

        public BaseOutput WS_UpdateUserRole(BaseInput baseinput, tblUserRole userRole, out tblUserRole item)
        {
            return businessLogic.UpdateUserRole(baseinput, userRole, out item);
        }

        public BaseOutput WS_GetUserRoleById(BaseInput baseinput, Int64 ID, out tblUserRole item)
        {
            return businessLogic.GetUserRoleById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetUserRoles(BaseInput baseinput, out List<tblUserRole> itemList)
        {
            return businessLogic.GetUserRoles(baseinput, out itemList);
        }
        public BaseOutput WS_GetUserRolesByUserId(BaseInput baseinput, Int64 userId, out List<tblUserRole> itemList)
        {
            return businessLogic.GetUserRolesByUserId(baseinput, userId, out itemList);
        }
        #endregion

        #region Elanlarla bagli servisler

        public BaseOutput WS_GetDemanProductionGroupList(BaseInput baseinput, long startDate, long endDate,Int64 year ,Int64 partOfYear, out List<DemanProductionGroup> itemList)
        {
            return businessLogic.GetDemanProductionGroupList(baseinput, startDate, endDate,year,partOfYear, out itemList);
        }
        #endregion

        #region Potensial Istehsalcilar
        public BaseOutput WS_GetPotensialUserList(BaseInput baseinput, out List<UserInfo> itemList)
        {
            return businessLogic.GetPotensialUserList(baseinput, out itemList);
        }
        public BaseOutput WS_GetPotensialUserForAdminUnitIdList(BaseInput baseinput, long adminUnitId, out List<UserInfo> itemList)
        {
            return businessLogic.GetPotensialUserForAdminUnitIdList(baseinput, adminUnitId, out itemList);
        }
        public BaseOutput WS_GetPotensialUserListByName(BaseInput baseinput, string name, out List<UserInfo> itemList)
        {
            return businessLogic.GetPotensialUserListByName(baseinput, name, out itemList);

        }
        public BaseOutput WS_GetPotensialUserListBySurname(BaseInput baseinput, string surname, out List<UserInfo> itemList)
        {
            return businessLogic.GetPotensialUserListBySurname(baseinput, surname, out itemList);
        }

        public BaseOutput WS_GetPotensialUserListByAdminUnitName(BaseInput baseinput, string adminUnitName, out List<UserInfo> itemList)
        {
            return businessLogic.GetPotensialUserListByAdminUnitName(baseinput, adminUnitName, out itemList);
        }
        #endregion

        #region WS_ProductPrice
        public BaseOutput WS_AddProductPrice(BaseInput baseinput, tblProductPrice productPrice, out tblProductPrice productPriceOut)
        {
            return businessLogic.AddProductPrice(baseinput, productPrice, out productPriceOut);
        }

        public BaseOutput WS_DeleteProductPrice(BaseInput baseinput, tblProductPrice productPrice)
        {
            return businessLogic.DeleteProductPrice(baseinput, productPrice);
        }

        public BaseOutput WS_UpdateProductPrice(BaseInput baseinput, tblProductPrice productPrice, out tblProductPrice item)
        {
            return businessLogic.UpdateProductPrice(baseinput, productPrice, out item);
        }

        public BaseOutput WS_GetProductPriceById(BaseInput baseinput, Int64 ID, out ProductPriceDetail item)
        {
            return businessLogic.GetProductPriceById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetProductPriceByProductId(BaseInput baseinput, Int64 productId, out ProductPriceDetail item)
        {
            return businessLogic.GetProductPriceByProductId(baseinput, productId, out item);
        }
        public BaseOutput WS_GetProductPriceByYearId(BaseInput baseinput, Int64 yearId, out ProductPriceDetail item)
        {
            return businessLogic.GetProductPriceByYearId(baseinput, yearId, out item);
        }
        public BaseOutput WS_GetProductPriceByYearIdAndProductId(BaseInput baseinput, Int64 productID, Int64 year, out ProductPriceDetail item)
         {
              return businessLogic.GetProductPriceByYearIdAndProductId(baseinput, productID, year, out item);
         }

        public BaseOutput WS_GetProductPriceByYearAndProductIdAndPartOfYear(BaseInput baseinput, long productID, long year, long partOfYear, out ProductPriceDetail item)
        {
            return businessLogic.GetProductPriceByYearAndProductIdAndPartOfYear(baseinput, productID, year, partOfYear, out item);
        }

        public BaseOutput WS_GetProductPriceList(BaseInput baseinput, long year, long partOfYear, out List<ProductPriceDetail> itemList)
        {
            return businessLogic.GetProductPriceList(baseinput, year, partOfYear, out itemList);
        }

        public BaseOutput WS_GetProductPriceListNotPrice(BaseInput baseinput, long year, long partOfYear, out List<ProductPriceDetail> itemList)
        {
            return businessLogic.GetProductPriceListNotPrice(baseinput, year, partOfYear, out itemList);
        }
        #endregion

        #region  WS_AuthenticatedPart
        public BaseOutput WS_GetAuthenticatedPartById(BaseInput baseinput, Int64 ID, out tblAuthenticatedPart item)
        {
            return businessLogic.GetAuthenticatedPartById(baseinput, ID, out item);
        }
        public BaseOutput WS_GetAuthenticatedParts(BaseInput baseinput, out List<tblAuthenticatedPart> itemList)
        {
            return businessLogic.GetAuthenticatedParts(baseinput, out itemList);
        }
        public BaseOutput WS_GetAuthenticatedPartByName(BaseInput baseinput, string AuthenticatedPartName, out tblAuthenticatedPart item)
        {
            return businessLogic.GetAuthenticatedPartByName(baseinput, AuthenticatedPartName, out item);
        }

        #endregion

        #region WS_PrivilegedRoles

        public BaseOutput WS_DeletePrivilegedRole(BaseInput baseinput, tblPrivilegedRole privilegedRole)
        {
            return businessLogic.DeletePrivilegedRole(baseinput, privilegedRole);
        }

        public BaseOutput WS_GetPrivilegedRoleById(BaseInput baseinput, Int64 ID, out tblPrivilegedRole item)
        {
            return businessLogic.GetPrivilegedRoleById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetPrivilegedRoles(BaseInput baseinput, out List<tblPrivilegedRole> itemList)
        {
            return businessLogic.GetPrivilegedRoles(baseinput, out itemList);
        }
        public BaseOutput WS_GetPrivilegedRolesByAuthenticatedPartId(BaseInput baseinput, Int64 authenticatedPartId, out List<tblPrivilegedRole> itemList)
        {
            return businessLogic.GetPrivilegedRolesByAuthenticatedPartId(baseinput, authenticatedPartId, out itemList);
        }

        public BaseOutput WS_AddPrivilegedRole(BaseInput baseinput, tblPrivilegedRole privilegedRole, out tblPrivilegedRole privilegedRoleOut)
        {
            return businessLogic.AddPrivilegedRole(baseinput, privilegedRole, out privilegedRoleOut);
        }


        #endregion



        #region WS_tblProductProfileImage

        public BaseOutput WS_AddProductProfileImage(BaseInput baseinput, tblProductProfileImage productProfileImage, out tblProductProfileImage ProductProfileImageOut)
        {
            return businessLogic.AddProductProfileImage(baseinput, productProfileImage, out ProductProfileImageOut);
        }

        public BaseOutput WS_DeleteProductProfileImage(BaseInput baseinput, tblProductProfileImage productProfileImage)
        {
            return businessLogic.DeleteProductProfileImage(baseinput, productProfileImage);
        }

        public BaseOutput WS_UpdateProductProfileImage(BaseInput baseinput, tblProductProfileImage inputItem, out tblProductProfileImage updatedItem)
        {
            return businessLogic.UpdateProductProfileImage(baseinput, inputItem, out updatedItem);
        }

        public BaseOutput WS_GetProductProfileImageById(BaseInput baseinput, Int64 ID, out tblProductProfileImage item)
        {
            return businessLogic.GetProductProfileImageById(baseinput, ID, out item);
        }

        public BaseOutput WS_GetProductProfileImages(BaseInput baseinput, out List<tblProductProfileImage> itemList)
        {
            return businessLogic.GetProductProfileImages(baseinput, out itemList);
        }

        public BaseOutput WS_GetProductProfileImageByProductId(BaseInput baseinput, Int64 productId, out tblProductProfileImage item)
        {
            return businessLogic.GetProductProfileImageByProductId(baseinput, productId, out item);
        }


        #endregion



        #region WS_tblPRM_ASCBranch

        public BaseOutput WS_AddPRM_ASCBranch(BaseInput baseinput, tblPRM_ASCBranch branch, out tblPRM_ASCBranch branchOut) 
        {
            return businessLogic.AddPRM_ASCBranch(baseinput, branch, out branchOut);
        }

        public BaseOutput WS_DeletePRM_ASCBranch(BaseInput baseinput, tblPRM_ASCBranch branch) 
        {
            return businessLogic.DeletePRM_ASCBranch(baseinput, branch);
        }

        public BaseOutput WS_GetPRM_ASCBranches(BaseInput baseinput, out List<tblPRM_ASCBranch> PRM_ASCBranchList) 
        {
            return businessLogic.GetPRM_ASCBranches(baseinput, out PRM_ASCBranchList);
        }

        public BaseOutput WS_GetPRM_ASCBranchById(BaseInput baseinput, Int64 branchId, out tblPRM_ASCBranch branch) 
        {
            return businessLogic.GetPRM_ASCBranchById(baseinput, branchId, out branch);
        }

        public BaseOutput WS_GetPRM_ASCBranchByName(BaseInput baseinput, string branchName, out tblPRM_ASCBranch branch) 
        {
            return businessLogic.GetPRM_ASCBranchByName(baseinput, branchName, out branch);
        }

        public BaseOutput WS_UpdatePRM_ASCBranch(BaseInput baseinput, tblPRM_ASCBranch branch) 
        {
            return businessLogic.UpdatePRM_ASCBranch(baseinput, branch);
        }

        #endregion


        #region WS_tblPRM_KTNBranch
        public BaseOutput WS_AddPRM_KTNBranch(BaseInput baseinput, tblPRM_KTNBranch branch,  out tblPRM_KTNBranch branchOut)
        {
            return businessLogic.AddPRM_KTNBranch(baseinput, branch,out  branchOut);
        }
        public BaseOutput WS_DeletePRM_KTNBranch(BaseInput baseinput, tblPRM_KTNBranch branch)
        {
            return businessLogic.DeletePRM_KTNBranch(baseinput, branch);
        }
        public BaseOutput WS_GetPRM_KTNBranches(BaseInput baseinput, out List<tblPRM_KTNBranch> PRM_KTNBranchList)
        {
            return businessLogic.GetPRM_KTNBranches(baseinput, out PRM_KTNBranchList);
        }
        public BaseOutput WS_GetPRM_KTNBranchById(BaseInput baseinput, Int64 branchId, out tblPRM_KTNBranch branch)
        {
            return businessLogic.GetPRM_KTNBranchById(baseinput, branchId, out branch);
        }
        public BaseOutput WS_GetPRM_KTNBranchByName(BaseInput baseinput, string branchName, out tblPRM_KTNBranch branch)
        {
            return businessLogic.GetPRM_KTNBranchByName(baseinput, branchName, out branch);
        }
        public BaseOutput WS_UpdatePRM_KTNBranch(BaseInput baseinput, tblPRM_KTNBranch branch)
        {
            return businessLogic.UpdatePRM_KTNBranch(baseinput, branch);
        }
        #endregion


        #region WS_tblBranchResponsibility
        public BaseOutput WS_AddBranchResponsibility(BaseInput baseinput, tblBranchResponsibility branch)
        {
            return businessLogic.AddBranchResponsibility(baseinput, branch);
        }
        public BaseOutput WS_DeleteBranchResponsibility(BaseInput baseinput, tblBranchResponsibility branch)
        {
            return businessLogic.DeleteBranchResponsibility(baseinput, branch);
        }
        public BaseOutput WS_GetBranchResponsibilities(BaseInput baseinput, out List<tblBranchResponsibility> BranchResponsibilityList)
        {
            return businessLogic.GetBranchResponsibilities(baseinput, out BranchResponsibilityList);
        }
        public BaseOutput WS_GetBranchResponsibilityById(BaseInput baseinput, Int64 branchId, out tblBranchResponsibility branch)
        {
            return businessLogic.GetBranchResponsibilityById(baseinput, branchId, out branch);
        }
        public BaseOutput WS_GetBranchResponsibilityForBranch(BaseInput baseinput, tblBranchResponsibility item, out List<tblBranchResponsibility> branch)
        {
            return businessLogic.GetBranchResponsibilityForBranch(baseinput, item, out branch);
        }
        public BaseOutput WS_UpdateBranchResponsibility(BaseInput baseinput, tblBranchResponsibility branch)
        {
            return businessLogic.UpdateBranchResponsibility(baseinput, branch);
        }
        #endregion




        #region Report

        public BaseOutput WS_GetDemandOfferDetailID(BaseInput baseinput, long adminID, out List<DemandOfferDetail> itemList)
        {
            return businessLogic.GetDemandOfferDetailID(baseinput, adminID, out itemList);
        }

        public BaseOutput WS_GetDemandOfferDetailByProductID(BaseInput baseinput, out List<DemandOfferDetail> itemList)
        {
            return businessLogic.GetDemandOfferDetailByProductID(baseinput, out itemList);
        }

        public BaseOutput WS_GetOfferDetailByAmdminID(BaseInput baseinput, long adminID, out List<DemandOfferDetail> itemList)
        {
            return businessLogic.GetOfferDetailByAmdminID(baseinput, adminID, out itemList);
        }

        public BaseOutput WS_GetDemanDetailByAdminID(BaseInput baseinput, long adminID, out List<DemandOfferDetail> itemList)
        {
            return businessLogic.GetDemanDetailByAdminID(baseinput, adminID, out itemList);
        }

        public BaseOutput WS_GetPotentialClientCount(BaseInput baseinput, out List<PotentialClientDetail> itemList)
        {
            return businessLogic.GetPotentialClientCount(baseinput, out itemList);
        }
        #endregion










       
    }
}