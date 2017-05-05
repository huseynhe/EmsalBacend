using Emsal.DAL;
using Emsal.DAL.CodeObjects;
using Emsal.DAL.CustomObjects;
using Emsal.DAL.SearchObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Emsal.WebSrv
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]

    public interface IEmsalService
    {

        #region CreateDB

        //[OperationContract]
        //void WS_createDb();

        #endregion

        #region Enum Service
        [OperationContract]
        BaseOutput WS_AddEnumCategory(BaseInput baseinput, tblEnumCategory enumCategory, out tblEnumCategory enumCategoryOut);

        [OperationContract]
        BaseOutput WS_DeleteEnumCategory(BaseInput baseinput, tblEnumCategory enumCategory);

        [OperationContract]
        BaseOutput WS_UpdateEnumCategory(BaseInput baseinput, tblEnumCategory inputItem, out tblEnumCategory updatedItem);

        [OperationContract]
        BaseOutput WS_GetEnumCategorys(BaseInput baseinput, out List<tblEnumCategory> itemList);

        [OperationContract]
        BaseOutput WS_GetEnumCategoryById(BaseInput baseinput, Int64 ID, out tblEnumCategory item);

        [OperationContract]
        BaseOutput WS_GetEnumCategorysByName(BaseInput baseinput, string categoryName, out tblEnumCategory item);

        [OperationContract]
        BaseOutput WS_GetEnumCategorysForProduct(BaseInput baseinput, out List<tblEnumCategory> itemList);

        [OperationContract]
        BaseOutput WS_AddEnumValue(BaseInput baseinput, tblEnumValue enumValue, out tblEnumValue enumValueOut);

        [OperationContract]
        BaseOutput WS_DeleteEnumValue(BaseInput baseinput, tblEnumValue enumValue);

        [OperationContract]
        BaseOutput WS_UpdateEnumValue(BaseInput baseinput, tblEnumValue inputItem, out tblEnumValue updatedItem);

        [OperationContract]
        BaseOutput WS_GetEnumValues(BaseInput baseinput, out List<tblEnumValue> itemList);

        [OperationContract]
        BaseOutput WS_GetEnumValueById(BaseInput baseinput, Int64 ID, out tblEnumValue item);

        [OperationContract]
        BaseOutput WS_GetEnumValuesByEnumCategoryId(BaseInput baseinput, Int64 categoryID, out List<tblEnumValue> itemList);

        [OperationContract]
        BaseOutput WS_GetEnumValuesForProduct(BaseInput baseinput, out List<tblEnumValue> itemList);

        [OperationContract]
        BaseOutput WS_GetEnumValueByName(BaseInput baseinput, string name, out tblEnumValue item);
        #endregion

        #region PRM_AdminUnit Service
        [OperationContract]
        BaseOutput WS_AddPRM_AdminUnit(BaseInput baseinput, tblPRM_AdminUnit adminUnit);

        [OperationContract]
        BaseOutput WS_DeletePRM_AdminUnit(BaseInput baseinput, tblPRM_AdminUnit adminUnit);

        [OperationContract]
        BaseOutput WS_UpdatePRM_AdminUnit(BaseInput baseinput, tblPRM_AdminUnit adminUnit);

        [OperationContract]
        BaseOutput WS_GetPRM_AdminUnits(BaseInput baseinput, out List<tblPRM_AdminUnit> adminUnit);

        [OperationContract]
        BaseOutput WS_GetPRM_AdminUnitById(BaseInput baseinput, Int64 ID, out tblPRM_AdminUnit adminUnit);

        [OperationContract]
        BaseOutput WS_GetPRM_AdminUnitByName(BaseInput baseinput, string countryName, out tblPRM_AdminUnit adminUnit);

        [OperationContract]
        BaseOutput WS_GetAdminUnitsByParentId(BaseInput baseinput, int parentID, out List<tblPRM_AdminUnit> adminUnitList);

        [OperationContract]
        BaseOutput WS_GETPRM_AdminUnitsByChildId(BaseInput baseinput, tblPRM_AdminUnit adminUnit, out List<tblPRM_AdminUnit> adminUnitList);
        [OperationContract]
        BaseOutput WS_GetPRM_AdminUnitByIamasId(BaseInput baseinput, Int64 iamasId, bool isCity, out tblPRM_AdminUnit adminUnit);
        #endregion

        #region WS_Person
        [OperationContract]
        BaseOutput WS_AddPerson(BaseInput baseinput, tblPerson person, out tblPerson PersonOut);
        [OperationContract]
        BaseOutput WS_DeletePerson(BaseInput baseinput, tblPerson person);
        [OperationContract]
        BaseOutput WS_UpdatePerson(BaseInput baseinput, tblPerson person, out tblPerson updatedItem);
        [OperationContract]
        BaseOutput WS_GetPersonById(BaseInput baseinput, Int64 ID, out tblPerson item);
        [OperationContract]
        BaseOutput WS_GetPersonByUserId(BaseInput baseinput, Int64 userId, out tblPerson item);
        [OperationContract]
        BaseOutput WS_GetPersons(BaseInput baseinput, out List<tblPerson> itemList);
        [OperationContract]
        BaseOutput WS_GetPersonByPinNumber(BaseInput baseinput, string pinNumber, out tblPerson item);
        #endregion

        #region PRM_Thoroughfares Service
        [OperationContract]
        BaseOutput WS_AddPRM_Thoroughfare(BaseInput baseinput, tblPRM_Thoroughfare Thoroughfare);

        [OperationContract]
        BaseOutput WS_DeletePRM_Thoroughfare(BaseInput baseinput, tblPRM_Thoroughfare Thoroughfare);

        [OperationContract]
        BaseOutput WS_UpdatePRM_Thoroughfare(BaseInput baseinput, tblPRM_Thoroughfare Thoroughfare);

        [OperationContract]
        BaseOutput WS_GetPRM_Thoroughfares(BaseInput baseinput, out List<tblPRM_Thoroughfare> Thoroughfares);

        [OperationContract]
        BaseOutput WS_GetPRM_ThoroughfareById(BaseInput baseinput, Int64 ID, out tblPRM_Thoroughfare Thoroughfare);

        [OperationContract]
        BaseOutput WS_GetPRM_ThoroughfareByName(BaseInput baseinput, string ThoroughfareName, out tblPRM_Thoroughfare Thoroughfare);

        [OperationContract]
        BaseOutput WS_GetPRM_ThoroughfaresByAdminUnitId(BaseInput baseinput, Int64 adminUnitID, out List<tblPRM_Thoroughfare> ThoroughfareList);

        #endregion

        #region  Addresses Service

        [OperationContract]
        BaseOutput WS_AddAddress(BaseInput baseinput, tblAddress address, out tblAddress addressOut);

        [OperationContract]
        BaseOutput WS_DeleteAddress(BaseInput baseinput, tblAddress address);

        [OperationContract]
        BaseOutput WS_UpdateAddress(BaseInput baseinput, tblAddress address);

        [OperationContract]
        BaseOutput WS_GetAddresses(BaseInput baseinput, out List<tblAddress> addresses);

        [OperationContract]
        BaseOutput WS_GetAddressById(BaseInput baseinput, Int64 addressId, out tblAddress address);

        [OperationContract]
        BaseOutput WS_GetAddressesByCountryId(BaseInput baseinput, Int64 countryId, out List<tblAddress> addresses);

        [OperationContract]
        BaseOutput WS_GetAddressesByVillageId(BaseInput baseinput, Int64 villageId, out List<tblAddress> addresses);

        [OperationContract]
        BaseOutput WS_GetAddressesByUserId(BaseInput baseinput, Int64 userID, out List<tblAddress> addresses);


        #endregion

        #region ProductAddressServices
        [OperationContract]
        BaseOutput WS_AddProductAddress(BaseInput baseinput, tblProductAddress address, out tblProductAddress addressOut);

        [OperationContract]
        BaseOutput WS_DeleteProductAddress(BaseInput baseinput, tblProductAddress address);

        [OperationContract]
        BaseOutput WS_UpdateProductAddress(BaseInput baseinput, tblProductAddress addressout, out tblProductAddress addressOut);

        [OperationContract]
        BaseOutput WS_GetProductAddresses(BaseInput baseinput, out List<tblProductAddress> addresses);

        [OperationContract]
        BaseOutput WS_GetProductAddressById(BaseInput baseinput, Int64 addressId, out tblProductAddress address);

        [OperationContract]
        BaseOutput WS_GetProductAddressesByAdminID(BaseInput baseinput, Int64 adminId, out List<tblProductAddress> addresses);

        #endregion

        #region ProductCatalog
        [OperationContract]
        BaseOutput WS_GetProductCatalogsOffer(BaseInput baseinput, out List<ProductCatalogDetail> pCatalogDetailList);
        [OperationContract]
        BaseOutput WS_GetProductCatalogsDemand(BaseInput baseinput, out List<ProductCatalogDetail> pCatalogDetailList);

        [OperationContract]
        BaseOutput WS_DeleteProductCatalog(BaseInput baseinput, tblProductCatalog productCatalog);

        [OperationContract]
        BaseOutput WS_AddProductCatalog(BaseInput baseinput, tblProductCatalog productCatalog, out tblProductCatalog productCatalogOut);

        [OperationContract]
        BaseOutput WS_GetProductCatalogs(BaseInput baseinput, out List<tblProductCatalog> pCatalogList);

        [OperationContract]
        BaseOutput WS_GetRootProductCatalogs(BaseInput baseinput, out List<tblProductCatalog> pCatalogList);

        [OperationContract]
        BaseOutput WS_GetProductCatalogsByParentId(BaseInput baseinput, int parentID, out List<tblProductCatalog> pCatalogList);
        [OperationContract]
        BaseOutput WS_GetProductCatalogDetailsByParentId(BaseInput baseinput, int parentID, out List<ProductCatalogDetail> pCatalogDetailList);
        [OperationContract]
        BaseOutput WS_GetProductCatalogDetailsById(BaseInput baseinput, int productID, out ProductCatalogDetail pCatalogDetail);


        [OperationContract]
        BaseOutput WS_GetProductCatalogsById(BaseInput baseinput, int productID, out tblProductCatalog pCatalog);

        [OperationContract]
        BaseOutput WS_UpdateProductCatalog(BaseInput baseinput, tblProductCatalog productCatalog, out tblProductCatalog updatedproductCatalog);
        #endregion

        //TODO: Add your service operations here

        #region PhoneConfirmation
        //[OperationContract]
        //BaseOutput WS_SendConfirmationMessage(BaseInput baseinput, tblConfirmationMessage message);

        [OperationContract]
        BaseOutput WS_GetConfirmationMessages(BaseInput baseinput, out List<tblConfirmationMessage> confirmationMessages);
        [OperationContract]
        BaseOutput WS_SendConfirmationMessageNew(BaseInput baseinput, tblConfirmationMessage message, out tblConfirmationMessage messagenOut);


        #endregion

        #region WS_Commucition
        [OperationContract]
        BaseOutput WS_AddCommunication(BaseInput baseinput, tblCommunication communication, out tblCommunication CommunicationOut);
        [OperationContract]
        BaseOutput WS_DeleteCommunication(BaseInput baseinput, tblCommunication communication);
        [OperationContract]
        BaseOutput WS_UpdateCommunication(BaseInput baseinput, tblCommunication inputItem, out tblCommunication updatedItem);
        [OperationContract]
        BaseOutput WS_GetCommunications(BaseInput baseinput, out List<tblCommunication> itemList);
        [OperationContract]
        BaseOutput WS_GetCommunicationById(BaseInput baseinput, Int64 ID, out tblCommunication item);
        [OperationContract]
        BaseOutput WS_GetCommunicationByPersonId(BaseInput baseinput, Int64 personId, out List<tblCommunication> communication);
        #endregion
        #region WS_Offer_Production
        [OperationContract]
        BaseOutput WS_UpdateOffer_ProductionForUserID(BaseInput baseinput, tblOffer_Production item, out List<tblOffer_Production> itemList);
       
        [OperationContract]
        BaseOutput WS_GetOrderProducts(BaseInput baseinput, out List<OfferProducts> itemList);
        [OperationContract]
        BaseOutput WS_AddOffer_Production(BaseInput baseinput, tblOffer_Production offer_Production, out tblOffer_Production Offer_ProductionOut);
        [OperationContract]
        BaseOutput WS_DeleteOffer_Production(BaseInput baseinput, tblOffer_Production offer_Production);
        [OperationContract]
        BaseOutput WS_UpdateOffer_Production(BaseInput baseinput, tblOffer_Production inputItem, out tblOffer_Production updatedItem);
        [OperationContract]
        BaseOutput WS_GetOffer_ProductionById(BaseInput baseinput, Int64 ID, out tblOffer_Production item);
        [OperationContract]
        BaseOutput WS_GetOffer_ProductionByProductIdandStateEVId(BaseInput baseinput, Int64 productId, Int64 state_Ev_Id, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetOffer_Productions(BaseInput baseinput, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOffer_ProductionsByUserID_OP(BaseInput baseinput, Int64 UserID,int page ,int page_size, out List<OfferDetails> itemList);
        [OperationContract]
        BaseOutput WS_GetOffer_ProductionsByUserID_OPC(BaseInput baseinput, Int64 UserID,  out Int64 count);
        [OperationContract]
        BaseOutput WS_GetOffer_ProductionsByUserID1(BaseInput baseinput, Int64 UserID, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOffer_ProductionsByUserID(BaseInput baseinput, Int64 UserID, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOffAirOffer_ProductionsByUserID(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOnAirOffer_ProductionsByUserID(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]

        BaseOutput WS_GetOnAirOfferCount_ProductionsByUserId(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOnAirOffer_ProductionsByUserIDSortedForDate(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOnAirOffer_ProductionsByUserIDSortedForDateDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOnAirOffer_ProductionsByUserIDSortedForPriceAsc(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOnAirOffer_ProductionsByUserIDSortedForPriceDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOffAirOffer_ProductionsByUserIDSortedForDate(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOffAirOffer_ProductionsByUserIDSortedForDateDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOffAirOffer_ProductionsByUserIDSortedForPriceAsc(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOffAirOffer_ProductionsByUserIDSortedForPriceDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetConfirmedPotential_ProductionsByStateAndUserIdForPriceDes(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetConfirmedPotential_ProductionsByStateAndUserIdForPriceAsc(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForStateEVId(BaseInput baseinput, Int64 userID, long state_eV_Id, out List<ProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetOffer_ProductionsByContractId(BaseInput baseinput, Int64 contractId, out List<tblOffer_Production> offerOut);

        #endregion
        #region WS_Potential_Production
        [OperationContract]
        BaseOutput WS_AddPotential_Production(BaseInput baseinput, tblPotential_Production potential_Production, out tblPotential_Production Potential_ProductionOut);
        [OperationContract]
        BaseOutput WS_DeletePotential_Production(BaseInput baseinput, tblPotential_Production potential_Production);
        [OperationContract]
        BaseOutput WS_UpdatePotential_Production(BaseInput baseinput, tblPotential_Production inputItem, out tblPotential_Production updatedItem);
        [OperationContract]
        BaseOutput WS_GetPotential_ProductionById(BaseInput baseinput, Int64 ID, out tblPotential_Production item);
        [OperationContract]
        BaseOutput WS_GetPotential_Productions(BaseInput baseinput, out List<tblPotential_Production> itemList);
        [OperationContract]
        BaseOutput WS_UpdatePotential_ProductionForUserID(tblPotential_Production item, out List<tblPotential_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetConfirmedPotential_ProductionsByUserId(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetPotential_ProductionsByUserId(BaseInput baseinput, Int64 userId, out List<tblPotential_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetConfirmedPotential_ProductionsByStateAndUserId(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetPotensialProductionDetailistForMonitoringEVId(BaseInput baseinput, Int64 userID, long monintoring_eV_Id, out List<ProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetPotensialProductionDetailistForStateEVId(BaseInput baseinput, Int64 userID, long state_eV_Id, out List<ProductionDetail> itemList);

        #endregion
        #region WS_Demand_Production
        [OperationContract]
        BaseOutput WS_AddDemand_Production(BaseInput baseinput, tblDemand_Production demand_Production, out tblDemand_Production Demand_ProductionOut);
        [OperationContract]
        BaseOutput WS_DeleteDemand_Production(BaseInput baseinput, tblDemand_Production demand_Productionn);
        [OperationContract]
        BaseOutput WS_UpdateDemand_Production(BaseInput baseinput, tblDemand_Production inputItem, out tblDemand_Production updatedItem);
        [OperationContract]
        BaseOutput WS_GetOnAirDemand_ProductionsByUserId(BaseInput baseinput, tblDemand_Production demand, out List<tblDemand_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOnAirDemandCount_ProductionsByUserId(BaseInput baseinput, tblDemand_Production demand, out List<tblDemand_Production> demandProductionList);
        [OperationContract]
        BaseOutput WS_GetDemand_ProductionById(BaseInput baseinput, Int64 ID, out tblDemand_Production item);
        [OperationContract]
        BaseOutput WS_GetDemand_Productions(BaseInput baseinput, out List<tblDemand_Production> itemList);
        [OperationContract]
        BaseOutput WS_UpdateDemand_ProductionForUserID(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList);

        [OperationContract]
        BaseOutput WS_UpdateDemand_ProductionForStartAndEndDate(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList);

        [OperationContract]
        BaseOutput WS_GetDemand_ProductionsByStateAndUserID_OP(BaseInput baseinput, DemandProductsForAccountingSearch ops, out List<DemandDetails> itemList);
        [OperationContract]
        BaseOutput WS_GetOffer_ProductionsByStateAndUserID_OP(BaseInput baseinput, DemandProductsForAccountingSearch ops, out List<DemandDetails> itemList);
        [OperationContract]
        BaseOutput WS_GetDemand_ProductionsByStateAndUserID_OPC(BaseInput baseinput, DemandProductsForAccountingSearch ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetOffer_ProductionsByStateAndUserID_OPC(BaseInput baseinput, DemandProductsForAccountingSearch ops, out Int64 count);
       
        [OperationContract]
        BaseOutput WS_GetDemand_ProductionsByStateAndUserID(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetDemandProductionForUserId(BaseInput baseinput, Int64 userId, out List<tblDemand_Production> itemList);

        #endregion
        #region WS_Announcement
        [OperationContract]
        BaseOutput UpdateAnnouncementPrice(BaseInput baseinput, tblAnnouncement inputItem, out tblAnnouncement updatedItem);

        [OperationContract]
        BaseOutput WS_AddAnnouncement(BaseInput baseinput, tblAnnouncement announcement, out tblAnnouncement AnnouncementOut);
        [OperationContract]
        BaseOutput WS_DeleteAnnouncement(BaseInput baseinput, tblAnnouncement announcement);
        [OperationContract]
        BaseOutput WS_UpdateAnnouncement(BaseInput baseinput, tblAnnouncement inputItem, out tblAnnouncement updatedItem);
        [OperationContract]
        BaseOutput WS_GetAnnouncementById(BaseInput baseinput, Int64 ID, out tblAnnouncement item);
        [OperationContract]
        BaseOutput WS_GetAnnouncements(BaseInput baseinput, out List<AnnouncementDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetAnnouncementsByProductId(BaseInput baseinput, Int64 productID, out List<tblAnnouncement> itemList);
        [OperationContract]

        BaseOutput WS_GetAnnouncementDetailsByProductId(BaseInput baseinput, Int64 productID, out List<AnnouncementDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetAnnouncementDetailById(BaseInput baseinput, Int64 ID, out AnnouncementDetail item);


        [OperationContract]
        BaseOutput WS_GetAnnouncementDetails(BaseInput baseinput, out List<AnnouncementDetail> itemlist);
        [OperationContract]
        BaseOutput GetAnnouncementDetailsCount(BaseInput baseinput, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetAnnouncementsByYearAndPartOfYear(BaseInput baseinput, string year, string month, out List<tblAnnouncement> itemList);
        #endregion
        #region WS_Employee
        [OperationContract]
        BaseOutput WS_AddEmployee(BaseInput baseinput, tblEmployee employee, out tblEmployee EmployeeOut);
        [OperationContract]
        BaseOutput WS_DeleteEmployee(BaseInput baseinput, tblEmployee employee);
        [OperationContract]
        BaseOutput WS_UpdateEmployee(BaseInput baseinput, tblEmployee inputItem, out tblEmployee updatedItem);
        [OperationContract]
        BaseOutput WS_GetEmployeeById(BaseInput baseinput, Int64 ID, out tblEmployee item);
        [OperationContract]
        BaseOutput WS_GetEmployees(BaseInput baseinput, out List<tblEmployee> itemList);
        #endregion
        #region WS_User
        [OperationContract]
        BaseOutput WS_AddUser(BaseInput baseinput, tblUser user, out tblUser UserOut);
        [OperationContract]
        BaseOutput WS_DeleteUser(BaseInput baseinput, tblUser user);
        [OperationContract]
        BaseOutput WS_UpdateUser(BaseInput baseinput, tblUser user, out tblUser updatedItem);
        [OperationContract]
        BaseOutput WS_GetUserById(BaseInput baseinput, Int64 ID, out tblUser item);
        [OperationContract]
        BaseOutput WS_GetUsers(BaseInput baseinput, out List<tblUser> itemList);
        [OperationContract]
        BaseOutput WS_GetUserByUserName(BaseInput baseinput, string UserName, out tblUser item);

        [OperationContract]
        BaseOutput WS_GetGovernmentOrganisations(BaseInput baseinput, long govErnmentRoleEnum, out List<tblUser> itemList);

        [OperationContract]
        BaseOutput WS_GetOrganisationTypeUsers(BaseInput baseinput, long govRoleEnum, long userType, out List<tblUser> itemList);
        [OperationContract]
        BaseOutput WS_GetOrganisationTypeUsers_OP(BaseInput baseinput,UserDetailSearch ops, out List<UserDetails> itemList);
        [OperationContract]
        BaseOutput WS_GetOrganisationTypeUsers_OPC(BaseInput baseinput, UserDetailSearch ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetGovernmentOrganizationTypeUsers_OP(BaseInput baseinput, UserDetailSearch ops, out List<UserDetails> itemList);
        [OperationContract]
        BaseOutput WS_GetGovernmentOrganizationTypeUsers_OPC(BaseInput baseinput, UserDetailSearch ops, out Int64 count);
      
        [OperationContract]
        BaseOutput WS_GetUsersByUserType_OP(BaseInput baseinput,UserDetailSearch ops, out List<UserDetails> itemList);
        [OperationContract]
        BaseOutput WS_GetUsersByUserType_OPC(BaseInput baseinput,UserDetailSearch ops, out Int64 count);
       
        [OperationContract]
        BaseOutput WS_GetUsersByUserType(BaseInput baseinput, long userTypeID, out List<tblUser> itemList);
        #endregion
        #region WS_Role
        [OperationContract]
        BaseOutput WS_AddRole(BaseInput baseinput, tblRole role, out tblRole RoleOut);
        [OperationContract]
        BaseOutput WS_DeleteRole(BaseInput baseinput, tblRole role);
        [OperationContract]
        BaseOutput WS_UpdateRole(BaseInput baseinput, tblRole role, out tblRole item);
        [OperationContract]
        BaseOutput WS_GetRoleById(BaseInput baseinput, Int64 ID, out tblRole item);
        [OperationContract]
        BaseOutput WS_GetRoles(BaseInput baseinput, out List<tblRole> itemList);
        [OperationContract]
        BaseOutput WS_GetRoleByName(BaseInput baseinput, string name, out tblRole item);
        [OperationContract]
        BaseOutput WS_GetRolesNotOwnedByUser(BaseInput baseinput, long userId, out List<tblRole> itemList);
        [OperationContract]
        BaseOutput WS_GetRoles1(BaseInput baseinput, out List<tblRole> itemList);

        [OperationContract]
        BaseOutput WS_GetRolesNotAllowedInPage(BaseInput baseinput, long pageId, out List<tblRole> itemList);
        #endregion
        #region WS_Expertise
        [OperationContract]
        BaseOutput WS_AddExpertise(BaseInput baseinput, tblExpertise expertise, out tblExpertise ExpertiseOut);
        [OperationContract]
        BaseOutput WS_DeleteExpertise(BaseInput baseinput, tblExpertise expertise);
        [OperationContract]
        BaseOutput WS_UpdateExpertise(BaseInput baseinput, tblExpertise expertise, out tblExpertise item);
        [OperationContract]
        BaseOutput WS_GetExpertiseById(BaseInput baseinput, Int64 ID, out tblExpertise item);
        [OperationContract]
        BaseOutput WS_GetExpertises(BaseInput baseinput, out List<tblExpertise> itemList);
        #endregion
        #region WS_Title
        [OperationContract]
        BaseOutput WS_AddTitle(BaseInput baseinput, tblTitle title, out tblTitle TitleOut);
        [OperationContract]
        BaseOutput WS_DeleteTitle(BaseInput baseinput, tblTitle title);
        [OperationContract]
        BaseOutput WS_UpdateTitle(BaseInput baseinput, tblTitle title, out tblTitle item);
        [OperationContract]
        BaseOutput WS_GetTitleById(BaseInput baseinput, Int64 ID, out tblTitle item);
        [OperationContract]
        BaseOutput WS_GetTitles(BaseInput baseinput, out List<tblTitle> itemList);
        #endregion
        #region WS_Party
        [OperationContract]
        BaseOutput WS_AddParty(BaseInput baseinput, tblParty party, out tblParty PartyOut);
        [OperationContract]
        BaseOutput WS_DeleteParty(BaseInput baseinput, tblParty party);
        [OperationContract]
        BaseOutput WS_UpdateParty(BaseInput baseinput, tblParty party, out tblParty item);
        [OperationContract]
        BaseOutput WS_GetPartyById(BaseInput baseinput, Int64 ID, out tblParty item);
        [OperationContract]
        BaseOutput WS_GetParties(BaseInput baseinput, out List<tblParty> itemList);
        #endregion
        #region WS_Organization
        [OperationContract]
        BaseOutput WS_AddOrganization(BaseInput baseinput, tblOrganization organization, out tblOrganization OrganizationOut);
        [OperationContract]
        BaseOutput WS_DeleteOrganization(BaseInput baseinput, tblOrganization organization);
        [OperationContract]
        BaseOutput WS_UpdateOrganization(BaseInput baseinput, tblOrganization organization, out tblOrganization item);
        [OperationContract]
        BaseOutput WS_GetOrganizationById(BaseInput baseinput, Int64 ID, out tblOrganization item);
        [OperationContract]
        BaseOutput WS_GetOrganizations(BaseInput baseinput, out List<tblOrganization> itemList);
        #endregion
        #region WS_ConfirmationMessage
        [OperationContract]
        BaseOutput WS_AddConfirmationMessage(BaseInput baseinput, tblConfirmationMessage confirmationMessage, out tblConfirmationMessage ConfirmationMessageOut);
        [OperationContract]
        BaseOutput WS_DeleteConfirmationMessage(BaseInput baseinput, tblConfirmationMessage confirmationMessage);
        [OperationContract]
        BaseOutput WS_UpdateConfirmationMessage(BaseInput baseinput, tblConfirmationMessage confirmationMessage, out tblConfirmationMessage item);
        [OperationContract]
        BaseOutput WS_GetConfirmationMessageById(BaseInput baseinput, Int64 ID, out tblConfirmationMessage item);

        #endregion
        #region WS_Foreign_Organization
        [OperationContract]
        BaseOutput WS_AddForeign_Organization(BaseInput baseinput, tblForeign_Organization foreign_Organization, out tblForeign_Organization Foreign_OrganizationOut);
        [OperationContract]
        BaseOutput WS_DeleteForeign_Organization(BaseInput baseinput, tblForeign_Organization foreign_Organization);
        [OperationContract]
        BaseOutput WS_UpdateForeign_Organization(BaseInput baseinput, tblForeign_Organization foreign_Organization, out tblForeign_Organization item);
        [OperationContract]
        BaseOutput WS_GetForeign_OrganizationById(BaseInput baseinput, Int64 ID, out tblForeign_Organization item);
        [OperationContract]
        BaseOutput WS_GetForeign_Organizations(BaseInput baseinput, out List<tblForeign_Organization> itemList);
        [OperationContract]
        BaseOutput WS_GetForeign_OrganizationByUserId(BaseInput baseinput, Int64 userId, out tblForeign_Organization item);
        [OperationContract]
        BaseOutput WS_GetForeign_OrganizationByVoen(BaseInput baseinput, string voen, out tblForeign_Organization item);
        [OperationContract]
        BaseOutput WS_GetForeign_OrganisationsByParentId(BaseInput baseInput, long Id, out List<tblForeign_Organization> itemList);
        [OperationContract]
        BaseOutput WS_GetForeignOrganizationListByUserId(BaseInput baseinput, Int64 userId, out List<tblForeign_Organization> itemList);

        #endregion


        #region ProductCatalogControl
        [OperationContract]
        BaseOutput WS_AddProductCatalogControl(BaseInput baseinput, tblProductCatalogControl item, out tblProductCatalogControl itemOut);
        [OperationContract]
        BaseOutput WS_DeleteProductCatalogControl(BaseInput baseinput, tblProductCatalogControl item);
        [OperationContract]
        BaseOutput WS_GetProductCatalogControls(BaseInput baseinput, out List<tblProductCatalogControl> itemList);
        [OperationContract]
        BaseOutput WS_GetProductCatalogControlById(BaseInput baseinput, Int64 Id, out tblProductCatalogControl item);
        [OperationContract]
        BaseOutput WS_GetProductCatalogControlsByProductID(BaseInput baseinput, Int64 productId, out List<tblProductCatalogControl> itemList);
        [OperationContract]
        BaseOutput WS_GetProductCatalogControlsByECategoryID(BaseInput baseinput, Int64 enumCategoryID, out List<tblProductCatalogControl> itemList);
        [OperationContract]
        BaseOutput WS_GetProductCatalogControlsByEValueID(BaseInput baseinput, Int64 enumValueID, out List<tblProductCatalogControl> itemList);
        [OperationContract]
        BaseOutput WS_UpdateProductCatalogControl(BaseInput baseinput, tblProductCatalogControl item, out tblProductCatalogControl updatedItem);
        [OperationContract]
        BaseOutput WS_AddProductCatalogControlList(BaseInput baseinput, List<tblProductCatalogControl> itemList, out List<tblProductCatalogControl> itemListOut);
        [OperationContract]
        BaseOutput WS_DeleteAllProductCatalogControlByProductID(BaseInput baseinput, Int64 productID);
        #endregion

        #region WS_Production_Document
        [OperationContract]
        BaseOutput WS_AddProductionDocument(BaseInput baseinput, tblProduction_Document ProductionDocument, out tblProduction_Document ProductionDocumentOut);
        [OperationContract]
        BaseOutput WS_DeleteProductionDocument(BaseInput baseinput, tblProduction_Document ProductionDocument);
        [OperationContract]
        BaseOutput WS_UpdateProductionDocument(BaseInput baseinput, tblProduction_Document ProductionDocument, out tblProduction_Document updatedItem);
        [OperationContract]
        BaseOutput WS_GetProductionDocumentById(BaseInput baseinput, Int64 ID, out tblProduction_Document item);
        [OperationContract]
        BaseOutput WS_GetProductionDocumentsByDemand_Production_Id(BaseInput baseinput, tblProduction_Document ProductionDocument, out List<tblProduction_Document> items);
        [OperationContract]
        BaseOutput WS_GetProductionDocumentsByOffer_Production_Id(BaseInput baseinput, tblProduction_Document ProductionDocument, out List<tblProduction_Document> items);
        [OperationContract]
        BaseOutput WS_GetProductionDocumentsByPotential_Production_Id(BaseInput baseinput, tblProduction_Document ProductionDocument, out List<tblProduction_Document> items);
        [OperationContract]
        BaseOutput WS_GetProductionDocuments(BaseInput baseinput, out List<tblProduction_Document> itemList);
        [OperationContract]
        BaseOutput WS_UpdateProductionDocumentForGroupID(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> productionDocumentList);
        [OperationContract]
        BaseOutput WS_GetProductionDocumentsByGroupId(BaseInput baseinput, string GroupID, out List<tblProduction_Document> itemList);
        [OperationContract]
        BaseOutput WS_GetProductionDocumentsByGroupIdAndPotential_Production_Id(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> itemList);
        [OperationContract]
        BaseOutput WS_GetProductionDocumentsByGroupIdAndOffer_Production_Id(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> itemList);
        [OperationContract]
        BaseOutput WS_GetProductionDocumentsByGroupIdAndDemand_Production_Id(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> itemList);

        #endregion

        #region WS_Product_Document
        [OperationContract]
        BaseOutput WS_AddProductDocument(BaseInput baseinput, tblProduct_Document ProductDocument, out tblProduct_Document ProductDocumentOut);
        [OperationContract]
        BaseOutput WS_DeleteProductDocument(BaseInput baseinput, tblProduct_Document ProductDocument);
        [OperationContract]
        BaseOutput WS_DeleteProductDocumentByProductID(BaseInput baseinput, tblProduct_Document ProductDocument, out List<tblProduct_Document> productDocumentListOut);
        [OperationContract]
        BaseOutput WS_UpdateProductDocument(BaseInput baseinput, tblProduct_Document ProductDocument, out tblProduct_Document updatedItem);
        [OperationContract]
        BaseOutput WS_GetProductDocumentById(BaseInput baseinput, Int64 ID, out tblProduct_Document item);
        [OperationContract]
        BaseOutput WS_GetProductDocumentsByProductCatalogId(BaseInput baseinput, Int64 productCatalogId, out List<tblProduct_Document> itemList);
        [OperationContract]
        BaseOutput WS_GetProductDocuments(BaseInput baseinput, out List<tblProduct_Document> itemList);
        #endregion

        #region Ws_ProductionControl
        [OperationContract]
        BaseOutput WS_AddProductionControl(BaseInput baseinput, tblProductionControl productionControl, out tblProductionControl productionControltOut);
        [OperationContract]
        BaseOutput WS_DeleteProductionControl(BaseInput baseinput, tblProductionControl ProductionControl);
        [OperationContract]
        BaseOutput WS_UpdateProductionControl(BaseInput baseinput, tblProductionControl ProductionControl, out tblProductionControl ProductionControlOut);
        [OperationContract]
        BaseOutput WS_GetProductionControlById(BaseInput baseinput, Int64 ID, out tblProductionControl ProductionControl);
        [OperationContract]
        BaseOutput WS_GetProductionControlsByPotentialProductionId(BaseInput baseinput, Int64 potentialProductionId, out List<tblProductionControl> ProductionControlList);
        [OperationContract]
        BaseOutput WS_GetProductionControls(BaseInput baseinput, out List<tblProductionControl> ProductionControl);
        [OperationContract]
        BaseOutput WS_GetProductionControlsByDemandProductionId(BaseInput baseinput, Int64 demandProductionId, out List<tblProductionControl> ProductionControlList);
        [OperationContract]
        BaseOutput WS_GetProductionControlsByOfferProductionId(BaseInput baseinput, Int64 offerProductionId, out List<tblProductionControl> ProductionControlList);
        [OperationContract]
        BaseOutput WS_DeleteProductionControlsByProduction_Type_ev_Id(BaseInput baseinput, Int64 production_Type_ev_ID);



        #endregion
        #region WS_Production_Calendar
        [OperationContract]
        BaseOutput WS_AddProduction_Calendar(BaseInput baseinput, tblProduction_Calendar ProductionCalendar, out tblProduction_Calendar productionCalendarOut);
        [OperationContract]
        BaseOutput WS_DeleteProduction_Calendar(BaseInput baseinput, tblProduction_Calendar ProductionCalendar);
        [OperationContract]
        BaseOutput WS_GetProduction_Calendar(BaseInput baseInput, out List<tblProduction_Calendar> ProductionCalendar);
        [OperationContract]
        BaseOutput WS_GetProduction_CalendarById(BaseInput baseinput, Int64 Id, out tblProduction_Calendar ProductionCalendar);
        [OperationContract]
        BaseOutput WS_GetProductionCalendarByProductionId(BaseInput baseinput, Int64 ProductionId, Int64 productionType_eV_Id, out tblProduction_Calendar ProductionCalendar);
        [OperationContract]
        BaseOutput WS_GetProductionCalendarProductiontypeeVId(BaseInput baseinput, Int64 production_type_eV_Id, out List<tblProduction_Calendar> ProductionCalendarlList);
        [OperationContract]
        BaseOutput WS_GetProductionCalendartransportationeVId(BaseInput baseinput, Int64 t_eV_Id, Int64 Year, out List<tblProduction_Calendar> ProductionCalendarlList);
        [OperationContract]
        BaseOutput WS_UpdateProductionCalendar(BaseInput baseinput, tblProduction_Calendar ProductionCalendar, out tblProduction_Calendar ProductionCalendarOut);

        #endregion

        #region Jira Request

        [OperationContract]
        BaseOutput WS_GetDocumentSizebyGroupId(string groupID, Int64 production_type_eVId, out Int64 totalSize);

        [OperationContract]
        BaseOutput WS_GetListOfPotensialProductionByUserId(string userID, out List<ProductionDetail> list);

        [OperationContract]
        BaseOutput WS_GetDocumentSizeByGroupIdAndPotentialProductionID(tblProduction_Document production_Document, out Int64 totalSize);
        [OperationContract]
        BaseOutput WS_GetDocumentSizeByGroupIdAndOfferProductionID(tblProduction_Document production_Document, out Int64 totalSize);
        [OperationContract]
        BaseOutput WS_GetDocumentSizeByGroupIdAndDemandProductionID(tblProduction_Document production_Document, out Int64 totalSize);
        #endregion


        #region TblComMessage

        [OperationContract]
        BaseOutput WS_AddComMessage(BaseInput baseinput, tblComMessage item, out tblComMessage itemOut);

        [OperationContract]
        BaseOutput WS_DeleteComMessage(BaseInput baseinput, tblComMessage item);

        [OperationContract]
        BaseOutput WS_UpdateComMessage(BaseInput baseinput, tblComMessage item, out tblComMessage itemOut);

        [OperationContract]
        BaseOutput WS_GetComMessageById(BaseInput baseInput, Int64 ID, out tblComMessage itemOut);


        [OperationContract]
        BaseOutput WS_GetComMessagesyByGroupId(BaseInput baseInput, Int64 groupId, out List<tblComMessage> itemListOut);

        [OperationContract]
        BaseOutput WS_GetComMessagesyByToUserId(BaseInput baseInput, Int64 toUserId, out List<tblComMessage> itemListOut);

        [OperationContract]
        BaseOutput WS_GetComMessagesyByFromUserId(BaseInput baseInput, Int64 fromUserId, out List<tblComMessage> itemListOut);

        [OperationContract]
        BaseOutput WS_GetComMessagesyByFromUserIDToUserId(BaseInput baseInput, Int64 fromUserId, Int64 toUserId, out List<tblComMessage> itemListOut);

        //ferid
        [OperationContract]
        BaseOutput WS_GetComMessagesyByToUserIdSortedForDateAsc(BaseInput baseInput, Int64 toUserId, out List<tblComMessage> itemListOut);

        [OperationContract]
        BaseOutput WS_GetComMessagesyByToUserIdSortedForDateDes(BaseInput baseInput, Int64 toUserId, out List<tblComMessage> itemListOut);


        [OperationContract]
        BaseOutput WS_GetComMessagesyByFromUserIdSortedForDateAsc(BaseInput baseInput, Int64 fromUserId, out List<tblComMessage> itemListOut);


        [OperationContract]
        BaseOutput WS_GetComMessagesyByFromUserIdSortedForDateDes(BaseInput baseInput, Int64 fromUserId, out List<tblComMessage> itemListOut);
        /////
        [OperationContract]
        BaseOutput WS_GetNotReadComMessagesByToUserId(BaseInput baseInput, Int64 toUserId, out List<tblComMessage> itemListOut);

        #endregion

        #region ProductionDetails
        [OperationContract]
        BaseOutput WS_GetProductionDetailist(BaseInput baseinput, out List<ProductionDetail> itemList);
        #endregion

        #region Sql Operation
        [OperationContract]
        BaseOutput WS_GetDemandProductionAmountOfEachProduct(BaseInput baseinput,PriceOfEachProductSearch ops, out List<DemandOfferDetail> itemList);


        [OperationContract]
        BaseOutput WS_GetPersonalinformationByRoleId(BaseInput baseinput, PotensialUserForAdminUnitIdList ops, out List<UserInfo> itemList);
        [OperationContract]
        BaseOutput WS_GetPersonalinformationByRoleId_OPC(BaseInput baseinput, PotensialUserForAdminUnitIdList ops, out Int64 count);

        [OperationContract]
        BaseOutput WS_GetDemandOfferProductionTotal(BaseInput baseinput, Int64 addressID,Int64 startDate,Int64 endDate, out List<DemandOfferDetail> itemList);
        //[OperationContract]
        //BaseOutput WS_GetDemandOfferProductionTotal(BaseInput baseinput, Int64 addressID, out List<DemandOfferDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetDemandProductsForAccounting(BaseInput baseinput, Int64 state_eV_Id, out List<ProductionDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetDemandProductDetailInfoForAccounting(BaseInput baseinput, Int64 state_eV_Id, Int64 year, Int64 partOfYear, out List<ProductionDetail> itemList);


        [OperationContract]
        BaseOutput WS_GetAdminUnitListForID(BaseInput baseinput, Int64 ID, out List<tblPRM_AdminUnit> itemList);
        [OperationContract]

        BaseOutput WS_GetForeign_OrganizationsListForID(BaseInput baseinput, Int64 Id, out List<tblForeign_Organization> itemList);

        [OperationContract]
        BaseOutput WS_GetProductCatalogListForID(BaseInput baseinput, Int64 ID, out List<tblProductCatalog> itemList);

        [OperationContract]
        BaseOutput WS_GetPotensialProductionDetailistForUser(BaseInput baseinput, Int64 userID, out List<ProductionDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetPotensialProductionDetailistForCreateUser(BaseInput baseinput, string createdUser, out List<ProductionDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetPotensialProductionDetailistForEValueId(BaseInput baseinput, Int64 state_eV_Id, out List<ProductionDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForUser(BaseInput baseinput, Int64 userID, out List<ProductionDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForEValueId(BaseInput baseinput, Int64 state_eV_Id, out List<ProductionDetail> itemList);


        [OperationContract]
        BaseOutput WS_GetDemandProductionDetailistForUser(BaseInput baseinput, Int64 userID, out List<ProductionDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetDemandProductionDetailistForEValueId(BaseInput baseinput, Int64 state_eV_Id, out List<ProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailById(BaseInput baseinput, Int64 offer, out ProductionDetail itemList);
        #endregion

        #region WS_UserRole
        [OperationContract]
        BaseOutput WS_AddUserRole(BaseInput baseinput, tblUserRole userRole, out tblUserRole UserRoleOut);
        [OperationContract]
        BaseOutput WS_DeleteUserRole(BaseInput baseinput, tblUserRole userRole);
        [OperationContract]
        BaseOutput WS_UpdateUserRole(BaseInput baseinput, tblUserRole userRole, out tblUserRole item);
        [OperationContract]
        BaseOutput WS_GetUserRoleById(BaseInput baseinput, Int64 ID, out tblUserRole item);
        [OperationContract]
        BaseOutput WS_GetUserRoles(BaseInput baseinput, out List<tblUserRole> itemList);
        [OperationContract]
        BaseOutput WS_GetUserRolesByUserId(BaseInput baseinput, Int64 userId, out List<tblUserRole> itemList);
        [OperationContract]
        BaseOutput WS_GetUserByUserEmail(BaseInput baseinput, string email, out tblUser item);

        #endregion

        #region Elanlarla bagli servisler

        [OperationContract]
        BaseOutput WS_GetDemanProductionGroupList(BaseInput baseinput, Int64 startDate, Int64 endDate, Int64 year, Int64 partOfYear, out List<DemanProductionGroup> itemList);
        #endregion

        #region Potensial Istehsalcilar

        [OperationContract]
        BaseOutput WS_GetPotensialUserList(BaseInput baseinput, out List<UserInfo> itemList);
        //[OperationContract]
        //BaseOutput WS_GetPotensialUserForAdminUnitIdList(BaseInput baseinput, Int64 adminUnitId, out  List<UserInfo> itemList);

        [OperationContract]
        BaseOutput WS_GetPotensialUserForAdminUnitIdList(BaseInput baseinput, long adminUnitId, out List<UserInfo> itemList);
        [OperationContract]
        BaseOutput WS_GetPotensialUserListByName(BaseInput baseinput, string name, out List<UserInfo> itemList);
        [OperationContract]
        BaseOutput WS_GetPotensialUserListBySurname(BaseInput baseinput, string surname, out List<UserInfo> itemList);
        [OperationContract]
        BaseOutput WS_GetPotensialUserListByAdminUnitName(BaseInput baseinput, string adminUnitName, out List<UserInfo> itemList);

        #endregion

        #region WS_ProductPrice
        [OperationContract]
        BaseOutput WS_AddProductPrice(BaseInput baseinput, tblProductPrice productPrice, out tblProductPrice productPriceOut);

        [OperationContract]
        BaseOutput WS_DeleteProductPrice(BaseInput baseinput, tblProductPrice productPrice);

        [OperationContract]
        BaseOutput WS_UpdateProductPrice(BaseInput baseinput, tblProductPrice productPrice, out tblProductPrice item);

        [OperationContract]
        BaseOutput WS_GetProductPriceById(BaseInput baseinput, Int64 ID, out ProductPriceDetail item);

        [OperationContract]
        BaseOutput WS_GetProductPriceByProductId(BaseInput baseinput, Int64 productId, out ProductPriceDetail item);
        [OperationContract]
        BaseOutput WS_GetProductPriceByYearId(BaseInput baseinput, Int64 yearId, out ProductPriceDetail item);

        [OperationContract]
        BaseOutput WS_GetProductPriceByYearIdAndProductId(BaseInput baseinput, Int64 productID, Int64 year, out ProductPriceDetail item);

        [OperationContract]
        BaseOutput WS_GetProductPriceByYearAndProductIdAndPartOfYear(BaseInput baseinput, Int64 productID, Int64 year, Int64 partOfYear, out ProductPriceDetail item);
        [OperationContract]
        BaseOutput WS_GetProductPriceList(BaseInput baseinput, Int64 year, Int64 partOfYear, out List<ProductPriceDetail> itemList);


        [OperationContract]
        BaseOutput WS_GetProductPriceListNotPrice(BaseInput baseinput, Int64 year, Int64 partOfYear, out List<ProductPriceDetail> itemList);

        #endregion

        #region WS_AuthenticatedParts

        [OperationContract]
        BaseOutput WS_GetAuthenticatedPartById(BaseInput baseinput, Int64 ID, out tblAuthenticatedPart item);

        [OperationContract]
        BaseOutput WS_GetAuthenticatedParts(BaseInput baseinput, out List<tblAuthenticatedPart> itemList);
        [OperationContract]
        BaseOutput WS_GetAuthenticatedPartByName(BaseInput baseinput, string AuthenticatedPartName, out tblAuthenticatedPart item);

        #endregion

        #region PrivilegedRoles
        [OperationContract]
        BaseOutput WS_AddPrivilegedRole(BaseInput baseinput, tblPrivilegedRole privilegedRole, out tblPrivilegedRole privilegedRoleOut);

        [OperationContract]
        BaseOutput WS_DeletePrivilegedRole(BaseInput baseinput, tblPrivilegedRole privilegedRole);

        [OperationContract]
        BaseOutput WS_GetPrivilegedRoles(BaseInput baseinput, out List<tblPrivilegedRole> itemList);

        [OperationContract]
        BaseOutput WS_GetPrivilegedRoleById(BaseInput baseinput, Int64 ID, out tblPrivilegedRole item);

        [OperationContract]
        BaseOutput WS_GetPrivilegedRolesByAuthenticatedPartId(BaseInput baseinput, long authenticatedPartId, out List<tblPrivilegedRole> itemList);
        #endregion
        #region WS_tblProductProfileImage
        [OperationContract]
        BaseOutput WS_AddProductProfileImage(BaseInput baseinput, tblProductProfileImage productProfileImage, out tblProductProfileImage ProductProfileImageOut);
        [OperationContract]
        BaseOutput WS_DeleteProductProfileImage(BaseInput baseinput, tblProductProfileImage productProfileImage);
        [OperationContract]
        BaseOutput WS_UpdateProductProfileImage(BaseInput baseinput, tblProductProfileImage inputItem, out tblProductProfileImage updatedItem);
        [OperationContract]
        BaseOutput WS_GetProductProfileImageById(BaseInput baseinput, Int64 ID, out tblProductProfileImage item);
        [OperationContract]
        BaseOutput WS_GetProductProfileImages(BaseInput baseinput, out List<tblProductProfileImage> itemList);
        [OperationContract]
        BaseOutput WS_GetProductProfileImageByProductId(BaseInput baseinput, Int64 productId, out tblProductProfileImage item);


        #endregion
        #region WS_tblPRM_ASCBranch
        [OperationContract]
        BaseOutput WS_AddPRM_ASCBranch(BaseInput baseinput, tblPRM_ASCBranch branch, out tblPRM_ASCBranch branchOut);
        [OperationContract]
        BaseOutput WS_DeletePRM_ASCBranch(BaseInput baseinput, tblPRM_ASCBranch branch);
        [OperationContract]
        BaseOutput WS_GetPRM_ASCBranches(BaseInput baseinput, out List<tblPRM_ASCBranch> PRM_ASCBranchList);
        [OperationContract]
        BaseOutput WS_GetPRM_ASCBranchById(BaseInput baseinput, Int64 branchId, out tblPRM_ASCBranch branch);
        [OperationContract]
        BaseOutput WS_GetPRM_ASCBranchByName(BaseInput baseinput, string branchName, out tblPRM_ASCBranch branch);
        [OperationContract]
        BaseOutput WS_UpdatePRM_ASCBranch(BaseInput baseinput, tblPRM_ASCBranch branch);

        #endregion
        #region WS_tblPRM_KTNBranch
        [OperationContract]
        BaseOutput WS_AddPRM_KTNBranch(BaseInput baseinput, tblPRM_KTNBranch branch, out tblPRM_KTNBranch branchOut);
        [OperationContract]
        BaseOutput WS_DeletePRM_KTNBranch(BaseInput baseinput, tblPRM_KTNBranch branch);
        [OperationContract]
        BaseOutput WS_GetPRM_KTNBranches(BaseInput baseinput, out List<tblPRM_KTNBranch> PRM_ASCBranchList);
        [OperationContract]
        BaseOutput WS_GetPRM_KTNBranchById(BaseInput baseinput, Int64 branchId, out tblPRM_KTNBranch branch);
        [OperationContract]
        BaseOutput WS_GetPRM_KTNBranchByName(BaseInput baseinput, string branchName, out tblPRM_KTNBranch branch);
        [OperationContract]
        BaseOutput WS_UpdatePRM_KTNBranch(BaseInput baseinput, tblPRM_KTNBranch branch);

        #endregion
        #region WS_tblBranchResponsibility
        [OperationContract]
        BaseOutput WS_AddBranchResponsibility(BaseInput baseinput, tblBranchResponsibility branch);
        [OperationContract]
        BaseOutput WS_DeleteBranchResponsibility(BaseInput baseinput, tblBranchResponsibility branch);
        [OperationContract]
        BaseOutput WS_GetBranchResponsibilities(BaseInput baseinput, out List<tblBranchResponsibility> BranchResponsibilityList);
        [OperationContract]
        BaseOutput WS_GetBranchResponsibilityById(BaseInput baseinput, Int64 branchId, out tblBranchResponsibility branch);
        [OperationContract]
        BaseOutput WS_GetBranchResponsibilityForBranch(BaseInput baseinput, tblBranchResponsibility item, out List<tblBranchResponsibility> branch);
        [OperationContract]
        BaseOutput WS_UpdateBranchResponsibility(BaseInput baseinput, tblBranchResponsibility branch);

        #endregion

        #region Report
        [OperationContract]
        BaseOutput WS_GetDemandOfferDetailID(BaseInput baseinput, long adminID, out List<DemandOfferDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetDemandOfferDetailByProductID(BaseInput baseinput, out List<DemandOfferDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetProductionCalendarDemand(BaseInput baseinput, out List<ProductionCalendarDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetProductionCalendarDemandId(BaseInput baseinput, Int64 demnad, out List<ProductionCalendarDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferDetailByAmdminID(BaseInput baseinput, long adminID, out List<DemandOfferDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetDemanDetailByAdminID(BaseInput baseinput, long adminID, out List<DemandOfferDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetPotentialClientCount(BaseInput baseinput, out List<PotentialClientDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetProductionCalendarOfferId(BaseInput baseinput, Int64 offer_id, out List<ProductionCalendarDetail> itemList);
        #endregion

        #region WS_tblProductionCalendar
        [OperationContract]
        BaseOutput WS_AddProductionCalendar(BaseInput baseinput, tblProductionCalendar ProductionCalendar, out tblProductionCalendar productionCalendarOut);
        [OperationContract]

        BaseOutput WS_DeleteProductionCalendar(BaseInput baseinput, tblProductionCalendar ProductionCalendar);
        [OperationContract]

        BaseOutput WS_GetProductionCalendar(BaseInput baseInput, out List<tblProductionCalendar> ProductionCalendar);
        [OperationContract]

        BaseOutput WS_GetProductionCalendarById(BaseInput baseinput, Int64 Id, out tblProductionCalendar ProductionCalendar);
        [OperationContract]

        BaseOutput WS_GetProductionCalendarPrice(BaseInput baseinput, decimal price, out tblProductionCalendar ProductionCalendar);

        BaseOutput WS_GetProductionCalendarQuantity(BaseInput baseinput, decimal quantity, out tblProductionCalendar ProductionCalendar);
        [OperationContract]

        BaseOutput WS_GetProductionCalendarBymonthseVId(BaseInput baseinput, Int64 months_eV_Id, out List<tblProductionCalendar> ProductionCalendarlList);
        [OperationContract]

        BaseOutput WS_GetProductionCalendarDay(BaseInput baseinput, Int64 day, out List<tblProductionCalendar> ProductionCalendarlList);
        [OperationContract]
        BaseOutput WS_GetProductionCalendarOclock(BaseInput baseinput, Int64 oclock, out List<tblProductionCalendar> ProductionCalendarlList);
        [OperationContract]
        BaseOutput WS_GetProductionCalendartransportationeVId2(BaseInput baseinput, Int64 type_eV_Id, Int64 year_id, out List<tblProductionCalendar> ProductionCalendarlList);
        [OperationContract]

        BaseOutput WS_GetProductionCalendarpartOfytypeeVId(BaseInput baseinput, Int64 partOfyear_eV_Id, out List<tblProductionCalendar> ProductionCalendarlList);
        [OperationContract]
        BaseOutput WS_UpdateProductionCalendar2(BaseInput baseinput, tblProductionCalendar ProductionCalendar, out tblProductionCalendar ProductionCalendarOut);
        [OperationContract]


        BaseOutput WS_GetProductionCalendarProductionId2(BaseInput baseinput, Int64 Production_Id, Int64 Production_type_eV_Id, out List<tblProductionCalendar> ProductionCalendar);

        [OperationContract]
        BaseOutput WS_GetProductionCalendarProductiontypeEVId2(BaseInput baseinput, Int64 production_type_eV_Id, out List<tblProductionCalendar> ProductionCalendar);



        [OperationContract]
        BaseOutput WS_GetProducParentProductByProductID(BaseInput baseinput, Int64 prodcutID, out tblProductCatalog pCatalog);



        [OperationContract]
        BaseOutput WS_GetProducListByUserID(BaseInput baseinput, Int64 userID,Int64 productID, out List<ProductCatalogDetail> pCatalogDetailList);



        #endregion
        #region WS_tblComMessageAttachment

        [OperationContract]
        BaseOutput WS_AddComMessageAttachment(BaseInput baseinput, tblComMessageAttachment comMessageAttachment, out tblComMessageAttachment comMessageAttachmentOut);
        [OperationContract]
        BaseOutput WS_DeleteComMessageAttachment(BaseInput baseinput, tblComMessageAttachment comMessageAttachment);


        [OperationContract]
        BaseOutput WS_GetComMessageAttachment(BaseInput baseInput, out List<tblComMessageAttachment> comMessageAttachment);
        [OperationContract]
        BaseOutput WS_GetComMessageAttachmentById(BaseInput baseinput, Int64 Id, out tblComMessageAttachment comMessageAttachment);




        [OperationContract]
        BaseOutput WS_UpdateComMessageAttachment(BaseInput baseinput, tblComMessageAttachment comMessageAttachment, out tblComMessageAttachment comMessageAttachmentOut);
        [OperationContract]
        BaseOutput WS_GetByComMessageAttachmentId(BaseInput baseinput, Int64 Id, out List<tblComMessageAttachment> comMessageAttachment);

        #endregion
        #region WS_tblContract
        [OperationContract]
        BaseOutput WS_AddContract(BaseInput baseinput, tblContract item, out tblContract contractOut);
        [OperationContract]

        BaseOutput WS_DeleteContract(BaseInput baseinput, tblContract contract);
        [OperationContract]

        BaseOutput WS_GetContract(BaseInput baseInput, out List<tblContract> contractOut);
        [OperationContract]
        BaseOutput WS_UpdateContract(BaseInput baseinput, tblContract contract, out tblContract contractOut);
        [OperationContract]
        BaseOutput WS_GetContractById(BaseInput baseinput, Int64 Id, out tblContract contract);
        [OperationContract]
        BaseOutput WS_GetContractBySupplierOrganisationID(BaseInput baseinput, Int64 organisationID, out List<tblContract> contractOut);
        [OperationContract]
        BaseOutput WS_GetContractBySupplierUserID(BaseInput baseinput, Int64 supplierUserID, out List<tblContract> contractOut);
        [OperationContract]
        BaseOutput WS_GetContractByAgentUserID(BaseInput baseinput, Int64 agentUserID, out List<tblContract> contractOut);

        #endregion
        [OperationContract]
        BaseOutput WS_GetPersonInformationUserID(BaseInput baseinput, Int64 userId, out PersonInformation item);
        [OperationContract]
        BaseOutput WS_GetPersonInformationByPinNumber(BaseInput baseinput, string PinNumber, out List<PersonInformation> itemList);

        [OperationContract]
        BaseOutput GetProductCatalogsWithParent(BaseInput baseinput, out List<ProductCatalogDetail> pCatalogDetailList);

        [OperationContract]
        tblProductionCalendar[] WS_GetProductionCalendar1(BaseInput baseInput);

        [OperationContract]
        BaseOutput WS_GetProductionCalendarByInstance(BaseInput baseinput, tblProductionCalendar item, out tblProductionCalendar ProductionCalendar);

        [OperationContract]
        BaseOutput WS_GetDemandProductionListNotPrice(BaseInput baseinput, Int64 year, Int64 partOfYear, out List<ProductPriceDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForMonitoringEVId(BaseInput baseinput, Int64 monintoring_eV_Id, long userID, out List<ProductionDetail> itemList);


        #region Optimisation public 
        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForUser_OP(BaseInput baseinput, DemandProductionDetailistForUser ops, out List<ProductionDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetUserDetailInfoForOffers_OP(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch1 ops, out List<PersonDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetUserDetailInfoForOffers_OPC(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch1 ops, out Int64 count);

        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForEValueId_OP1(BaseInput baseinput, OfferProductionDetailSearch ops, out List<GetOfferProductionDetailistForEValueId> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForEValueId_OPC1(BaseInput baseinput, OfferProductionDetailSearch ops, out Int64 count);

        [OperationContract]
        BaseOutput WS_GetDemandProductDetailInfoForAccounting_Search(BaseInput baseinput, Int64 state_eV_Id, Int64 year, Int64 partOfYear, string productID, out List<ProductionDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetDemandProductDetailInfoForAccounting_OP(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, Int64 year, Int64 partOfYear, out List<ProductionDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetDemandProductDetailInfoForAccounting_OPP(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, Int64 year, Int64 partOfYear, out float totalPrice);

        [OperationContract]
        BaseOutput WS_GetDemandProductionDetailistForEValueId_OP(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, out List<ProductionDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetDemandProductionDetailistForEValueId_OPC(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForStateEVId_OPC(BaseInput baseinput, OfferProductionDetailSearch ops, out Int64 count);

        [OperationContract]
        BaseOutput WS_GetAnnouncementDetailsByProductId_OP(BaseInput baseinput, int productID, int page, int pageSize, out List<AnnouncementDetail> itemList);

        [OperationContract]

        BaseOutput WS_GetOfferProductionDetailistForEValueId_OP(BaseInput baseinput, OfferProductionDetailSearch1 ops, out List<ProductionDetail> itemList);

        [OperationContract]

        BaseOutput WS_GetOfferProductionDetailistForEValueId_OPC(BaseInput baseinput, OfferProductionDetailSearch1 ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetPotensialProductionDetailistForEValueId_OP(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch1 ops, out List<ProductionDetail> itemList);
        [OperationContract]

        BaseOutput WS_GetPotensialProductionDetailistForEValueId_OPC(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch1 ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetDemandProductionDetailistForUser_OP(BaseInput baseinput, DemandProductionDetailistForUser ops, out List<ProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetDemandProductionDetailistForUser_OPC(BaseInput baseinput, DemandProductionDetailistForUser ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForUser_OPC(BaseInput baseinput, DemandProductionDetailistForUser ops, out Int64 count);
       
        [OperationContract]
        BaseOutput WS_GetAnnouncementDetailsByProductId_OPC(BaseInput baseinput, Int64 productID, out Int64 count);
        //[OperationContract]
        //BaseOutput WS_GetPotensialUserList_OP(BaseInput baseinput, PotensialUserForAdminUnitIdList ops, out List<UserInfo> itemList);
        [OperationContract]
        BaseOutput WS_GetPotensialUserList_OP(BaseInput baseinput, PotensialUserForAdminUnitIdList1 ops, out List<UserInfo> itemList);
        [OperationContract]
        BaseOutput WS_GetPotensialUserList_OPDila(BaseInput baseinput, PotensialUserForAdminUnitIdList ops, out List<UserInfo> itemList);

        [OperationContract]
        BaseOutput WS_GetPotensialUserList_OPC(BaseInput baseinput, PotensialUserForAdminUnitIdList1 ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetAnnouncementDetails_OP(BaseInput baseinput, int page, int pageSize, out List<AnnouncementDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetAnnouncementDetails_OPC(BaseInput baseinput, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetDemandProductsForAccounting_OPC(BaseInput baseinput, DemandProductsForAccountingSearch ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetDemandProductDetailInfoForAccounting_OPC(BaseInput baseinput, GetDemandProductionDetailistForEValueIdSearch ops, Int64 year, Int64 partOfYear, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetDemandProductsForAccounting_OP(BaseInput baseinput, DemandProductsForAccountingSearch ops, out List<ProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetDemandProductionDetailistForEValueId_OPA(BaseInput baseinput, Int64 stateEvId, int page, int pagesize, out List<DemandDetialOPA> itemList);
        [OperationContract]
        BaseOutput WS_GetPotensialUserForAdminUnitIdList_OP(BaseInput baseinput, PotensialUserForAdminUnitIdList ops, out List<UserInfo> itemList);
        [OperationContract]
        BaseOutput WS_GetPotensialUserForAdminUnitIdList_OPC(BaseInput baseinput, PotensialUserForAdminUnitIdList ops, out Int64 count);

        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForMonitoringEVId_OP(BaseInput baseinput, OfferProductionDetailSearch opds, out List<ProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForStateEVId_OP(BaseInput baseinput, OfferProductionDetailSearch opds, out List<ProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferProductionDetailistForMonitoringEVId_OPC(BaseInput baseinput, OfferProductionDetailSearch ops, out Int64 count);
        #endregion

        #region  tblPropertyDetail
        [OperationContract]
        BaseOutput WS_AddPropertyDetail(BaseInput baseinput, tblPropertyDetail detail, out tblPropertyDetail detailOut);
        [OperationContract]

        BaseOutput WS_DeletePropertyDetail(BaseInput baseinput, tblPropertyDetail item);
        [OperationContract]
        BaseOutput WS_GetPropertyDetails(BaseInput baseInput, out List<tblPropertyDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetPropertyDetailById(BaseInput baseinput, Int64 Id, out tblPropertyDetail detailOut);
        [OperationContract]

        BaseOutput WS_GetPropertyDetailArea(BaseInput baseInput, decimal area, out tblPropertyDetail detailOut);

        [OperationContract]
        BaseOutput WS_UpdatePropertyDetail(BaseInput baseinput, tblPropertyDetail detail, out tblPropertyDetail detailOut);

        [OperationContract]
        BaseOutput WS_GetPropertyDetailByProperty_type_ID(BaseInput baseinput, Int64 Property_type_ID, out List<tblPropertyDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetPropertyDetailByCapacity_measuriment_evID(BaseInput baseinput, Int64 Capacity_measuriment_evID, out List<tblPropertyDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetPropertyDetailCapacity(BaseInput baseinput, decimal capacity, out tblPropertyDetail item);
        [OperationContract]
        BaseOutput WS_GetPropertyDetailByAddressId(BaseInput baseinput, Int64 adsressID, out tblPropertyDetail item);

        #endregion
        #region  tblPropertyType
        [OperationContract]
        BaseOutput WS_AddPropertyTypes(BaseInput baseinput, tblPropertyType item, out tblPropertyType itemOut);
        [OperationContract]

        BaseOutput WS_DeletePropertyType(BaseInput baseinput, tblPropertyType item);
        [OperationContract]

        BaseOutput WS_GetPropertyTypes(BaseInput baseInput, out List<tblPropertyType> itemList);

        [OperationContract]
        BaseOutput WS_GetPropertyTypeById(BaseInput baseinput, Int64 Id, out tblPropertyType Detail);


        [OperationContract]

        BaseOutput WS_UpdatePropertyType(BaseInput baseinput, tblPropertyType type, out tblPropertyType typeOut);


        [OperationContract]
        BaseOutput WS_GetPropertyTypeByAddressId(BaseInput baseinput, Int64 adressId, out tblPropertyType item);


        #endregion
        #region tblContractDetailTemp
        [OperationContract]
        BaseOutput WS_AddtblContractDetailTemp(BaseInput baseinput, tblContractDetailTemp contractDetail, out tblContractDetailTemp contractDetailOut);
        [OperationContract]
        BaseOutput WS_UpdatetblContractDetailTemp(BaseInput baseinput, tblContractDetailTemp detail, out tblContractDetailTemp detailOut);
        [OperationContract]
        BaseOutput WS_GettblContractDetailTempByOfferId(BaseInput baseinput, Int64 OfferId, out List<tblContractDetailTemp> Detail);
        [OperationContract]
        BaseOutput WS_DeletetblContractDetailTemp(BaseInput baseinput, tblContractDetailTemp item);
        [OperationContract]
        BaseOutput WS_GettblContractDetailTempById(BaseInput baseinput, Int64 ID, out List<tblContractDetailTemp> Detail);
       
        #endregion
        #region YeniHesabatlar
        [OperationContract]
        BaseOutput WS_GetOfferGroupedProductionDetailistForAccounting(BaseInput baseinput, out  List<OfferProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferGroupedProductionDetailistForAccountingByAdmin_UnitID(BaseInput baseinput, Int64 addresID, out  List<OfferProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferGroupedProductionDetailistForAccountingByProductId(BaseInput baseinput, Int64 productID, out  List<OfferProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferGroupedProductionDetailistForAccountingByRoleId(BaseInput baseinput,Int64 RoleId, out  List<OfferProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferGroupedProductionDetailistForAccountingBySearch(BaseInput baseinput, OfferProductionDetailSearch ops, out  List<OfferProductionDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetDemandByForganistion_OPC(BaseInput baseinput, DemandForegnOrganization1 ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetDemandByForganistion_OP(BaseInput baseinput, DemandForegnOrganization1 ops, out  List<OrganizationDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetTotalDemandOffers(BaseInput baseinput,  DemandOfferProductsSearch ops, out List<DemanProductionGroup> itemList);
        [OperationContract]
        BaseOutput WS_GetTotalDemandOffersPA(BaseInput baseinput,  DemandOfferProductsSearch ops, out List<DemanProductionGroup> itemList);
        [OperationContract]
        BaseOutput WS_GetTotalOffersbyProductID(BaseInput baseinput, Int64 productID, DemandOfferProductsSearch ops, out List<DemanOfferProduction> itemList);
        [OperationContract]
        BaseOutput WS_GetTotalDemandOffers_OPC(BaseInput baseinput, DemandOfferProductsSearch ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetTotalOffersbyProductID_OPC(BaseInput baseinput, Int64 productID, DemandOfferProductsSearch ops, out Int64 count);


        #endregion
        [OperationContract]
        BaseOutput WS_GetAnnouncementDetails_Search(BaseInput baseinput, OfferProductionDetailSearch ops, out List<AnnouncementDetail> itemList);
         [OperationContract]
        BaseOutput WS_GetAnnouncementDetails_Search_OPC(BaseInput baseinput, OfferProductionDetailSearch ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetDemand_ProductionsByStateAndUserID1(BaseInput baseinput, Int64 userID, Int64 state_Ev_Id, out List<DemandDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetGovermentOrganisatinByAdminID(BaseInput baseinput, string adminIdList, out List<ForeignOrganization> itemList);
        [OperationContract]
        BaseOutput WS_GetPRM_AdminUnitByAdminID(BaseInput baseinput, Int64 adminId, out List<AdminUnitRegion> itemList);
        [OperationContract]
        BaseOutput WS_GetPRM_AdminUnitRegionByAddressId(BaseInput baseinput, Int64 adminId, out List<tblPRM_AdminUnit> itemList);
        [OperationContract]

        BaseOutput WS_GetPRM_AdminUnitRegionList(BaseInput baseinput, out List<AdminUnitRegion> itemList);
        [OperationContract]
        BaseOutput WS_GetTotalDemandOffersRegion(BaseInput baseinput, DemandOfferProductsSearch ops, out List<DemandDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetTotalDemandOffersRegion_OPC(BaseInput baseinput, DemandOfferProductsSearch ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetProductCatalogsOfferWitoutTypeOfEV(BaseInput baseinput,Int64 userID,  out List<tblProductCatalog> itemList);
        [OperationContract]
        BaseOutput GetAnnouncementDetailsByProductIdOPC(BaseInput baseinput, int ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetTotalOffer1(BaseInput baseinput, OfferProductionDetailSearch ops, out List<ProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetTotalOffer1_OPC(BaseInput baseinput, OfferProductionDetailSearch ops, out Int64 count);
        [OperationContract]
        BaseOutput WS_GetOfferGroupedProductionDetailistForAccountingByProductId_OP(BaseInput baseinput, OfferProductionDetailSearch1 ops, out  List<OfferProductionDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetOfferGroupedProductionDetailistForAccountingByProductId_OPC(BaseInput baseinput, OfferProductionDetailSearch1 ops, out  Int64 count);
        
        
       
      
    }
}