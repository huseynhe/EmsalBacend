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

namespace Emsal.WebSrv
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IEmsalService
    {

        #region CreateDB

        [OperationContract]
        void WS_createDb();

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
        BaseOutput WS_GetProductCatalogsById(BaseInput baseinput, int productID, out tblProductCatalog pCatalog);

        [OperationContract]
        BaseOutput WS_UpdateProductCatalog(BaseInput baseinput, tblProductCatalog productCatalog, out tblProductCatalog updatedproductCatalog);
        #endregion

        //TODO: Add your service operations here

        #region PhoneConfirmation
        [OperationContract]
        BaseOutput WS_SendConfirmationMessage(BaseInput baseinput, tblConfirmationMessage message);

        [OperationContract]
        BaseOutput WS_GetConfirmationMessages(BaseInput baseinput, out List<tblConfirmationMessage> confirmationMessages);


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
        BaseOutput WS_AddOffer_Production(BaseInput baseinput, tblOffer_Production offer_Production, out tblOffer_Production Offer_ProductionOut);
        [OperationContract]
        BaseOutput WS_DeleteOffer_Production(BaseInput baseinput, tblOffer_Production offer_Production);
        [OperationContract]
        BaseOutput WS_UpdateOffer_Production(BaseInput baseinput, tblOffer_Production inputItem, out tblOffer_Production updatedItem);
        [OperationContract]
        BaseOutput WS_GetOffer_ProductionById(BaseInput baseinput, Int64 ID, out tblOffer_Production item);
        [OperationContract]
        BaseOutput WS_GetOffer_Productions(BaseInput baseinput, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOffer_ProductionsByUserID(BaseInput baseinput, Int64 UserID, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOffAirOffer_ProductionsByUserID(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
        [OperationContract]
        BaseOutput WS_GetOnAirOffer_ProductionsByUserID(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList);
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
        BaseOutput WS_GetOfferProductionDetailistForMonitoringEVId(BaseInput baseinput, Int64 userID, long monintoring_eV_Id, out List<ProductionDetail> itemList);
         [OperationContract]
         BaseOutput WS_GetOfferProductionDetailistForStateEVId(BaseInput baseinput, Int64 userID, long state_eV_Id, out List<ProductionDetail> itemList);
       
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
        BaseOutput WS_UpdatePotential_ProductionForUserID(tblPotential_Production item, out  List<tblPotential_Production> itemList);
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
        BaseOutput WS_GetDemand_ProductionById(BaseInput baseinput, Int64 ID, out tblDemand_Production item);
        [OperationContract]
        BaseOutput WS_GetDemand_Productions(BaseInput baseinput, out List<tblDemand_Production> itemList);
        [OperationContract]
        BaseOutput WS_UpdateDemand_ProductionForUserID(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList);

        [OperationContract]
        BaseOutput WS_UpdateDemand_ProductionForStartAndEndDate(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList);

        [OperationContract]
        BaseOutput WS_GetDemand_ProductionsByStateAndUserID(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList);
        #endregion
        #region WS_Announcement
        [OperationContract]
        BaseOutput WS_AddAnnouncement(BaseInput baseinput, tblAnnouncement announcement, out tblAnnouncement AnnouncementOut);
        [OperationContract]
        BaseOutput WS_DeleteAnnouncement(BaseInput baseinput, tblAnnouncement announcement);
        [OperationContract]
        BaseOutput WS_UpdateAnnouncement(BaseInput baseinput, tblAnnouncement inputItem, out tblAnnouncement updatedItem);
        [OperationContract]
        BaseOutput WS_GetAnnouncementById(BaseInput baseinput, Int64 ID, out tblAnnouncement item);
        [OperationContract]
        BaseOutput WS_GetAnnouncements(BaseInput baseinput, out List<tblAnnouncement> itemList);
        [OperationContract]
        BaseOutput WS_GetAnnouncementsByProductId(BaseInput baseinput, Int64 productID, out List<tblAnnouncement> itemList);
        [OperationContract]
        BaseOutput WS_GetAnnouncementDetailsByProductId(BaseInput baseinput, Int64 productID,  out List<AnnouncementDetail> itemList);
        [OperationContract]
        BaseOutput WS_GetAnnouncementDetailById(BaseInput baseinput, Int64 ID,  out AnnouncementDetail item);
        [OperationContract]
        BaseOutput WS_GetAnnouncementDetails(BaseInput baseinput, out List<AnnouncementDetail> itemList);
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
        BaseOutput WS_GetListOfPotensialProductionByUserId(string userID, out  List<ProductionDetail> list);

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
        BaseOutput WS_GetAdminUnitListForID(BaseInput baseinput, Int64 ID, out List<tblPRM_AdminUnit> itemList);

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
        BaseOutput WS_GetProductPriceById(BaseInput baseinput, Int64 ID, out  ProductPriceDetail item);

        [OperationContract]
        BaseOutput WS_GetProductPriceByProductId(BaseInput baseinput, Int64 productId, out ProductPriceDetail item);
        [OperationContract]
        BaseOutput WS_GetProductPriceByYearId(BaseInput baseinput, Int64 yearId, out ProductPriceDetail item);

        [OperationContract]
        BaseOutput WS_GetProductPriceByYearIdAndProductId(BaseInput baseinput, Int64 productID, Int64 year, out ProductPriceDetail item);

        [OperationContract]
        BaseOutput WS_GetProductPriceByYearAndProductIdAndPartOfYear(BaseInput baseinput, Int64 productID, Int64 year, Int64 partOfYear, out ProductPriceDetail item);
        [OperationContract]
        BaseOutput WS_GetProductPriceList(BaseInput baseinput, Int64 year, Int64 partOfYear, out  List<ProductPriceDetail> itemList);


        [OperationContract]
        BaseOutput WS_GetProductPriceListNotPrice(BaseInput baseinput, Int64 year, Int64 partOfYear, out  List<ProductPriceDetail> itemList);

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
         BaseOutput WS_GetOfferDetailByAmdminID(BaseInput baseinput, long adminID, out List<DemandOfferDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetDemanDetailByAdminID(BaseInput baseinput, long adminID, out List<DemandOfferDetail> itemList);

        [OperationContract]
        BaseOutput WS_GetPotentialClientCount(BaseInput baseinput, out List<PotentialClientDetail> itemList);
        #endregion

    }
}

