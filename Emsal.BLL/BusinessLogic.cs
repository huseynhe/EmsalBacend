using Emsal.BLL.Helper;
using Emsal.DAL;
using Emsal.DAL.CodeObjects;
using Emsal.DAL.CustomObjects;
using Emsal.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emsal.Utility.UtilityObjects;
using Emsal.Utility.CustomObjects;
namespace Emsal.BLL
{
    public partial class BusinessLogic
    {
        OperationLogic operationLogic = new OperationLogic();
        public void StartDb()
        {
            EmsalDBContext dbContext = new EmsalDBContext();
            EmsalDBInitializer dbInitializer = new EmsalDBInitializer();
            dbInitializer.InitializeDatabase(dbContext);
        }
        int _lineNumber = 0;
        #region  tbl_Person

        public BaseOutput AddPerson(BaseInput baseinput, tblPerson person, out tblPerson personOut)
        {
            BaseOutput baseOutput;
           
           // string ip = baseinput.IpNumber.GetIpOrEmpty();
            string ipNumber = baseinput.IpNumber.GetIpOrEmpty();
            string _requestID = baseinput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseinput.ChannelId);
            personOut = null;
            try
            {

                person = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, person);
                personOut = operationLogic.AddPerson(person);

                
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseinput) + IOUtil.GetObjValue(person)), ipNumber, _requestID, _lineNumber, (ChannelEnum) channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                personOut = null;
              ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);
                
           
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
            finally
            {
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
               ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(personOut), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }

        }

        public BaseOutput DeletePerson(BaseInput baseinput, tblPerson Person)
        {
            BaseOutput baseOutput;
          
            try
            {
                Person = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, Person);
                operationLogic.DeletePerson(Person);


                
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
               
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
          
        }

        public BaseOutput GetPersons(BaseInput baseInput, out List<tblPerson> itemList)
        {
            BaseOutput baseOutput;
           
            try
            {
                itemList = operationLogic.GetPersons();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetPersonById(BaseInput baseinput, Int64 countryId, out tblPerson person)
        {
            BaseOutput baseOutput;
            person = null;
            string ipNumber = baseinput.IpNumber.GetIpOrEmpty();
            string _requestID = baseinput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseinput.ChannelId);
            try
            {
                person = operationLogic.GetPersonById(countryId);
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseinput) + IOUtil.GetObjValue(person)), ipNumber, _requestID, _lineNumber, (ChannelEnum)channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                person = null;
                ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
            finally
            {

                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(person), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }
        }

        public BaseOutput GetPersonByUserId(BaseInput baseInput, Int64 userID, out tblPerson person)
        {
            BaseOutput baseOutput;
            person = null;
            string ipNumber = baseInput.IpNumber.GetIpOrEmpty();
            string _requestID = baseInput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseInput.ChannelId);
            try
            {
                person = operationLogic.GetPersonByUserId(userID);
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseInput) + IOUtil.GetObjValue(person)), ipNumber, _requestID, _lineNumber, (ChannelEnum)channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                person = null;
                ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
            finally
            {
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(person), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }

        }

        public BaseOutput UpdatePerson(BaseInput baseinput, tblPerson person, out tblPerson personOut)
        {
            BaseOutput baseOutput;
            personOut = null;
            string ipNumber = baseinput.IpNumber.GetIpOrEmpty();
            string _requestID = baseinput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseinput.ChannelId);
            try
            {
                person = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, person);
                personOut = operationLogic.UpdatePerson(person);
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseinput) + IOUtil.GetObjValue(person)), ipNumber, _requestID, _lineNumber, (ChannelEnum)channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                personOut = null;
                ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
            finally
            {
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(personOut), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }
        }
        public BaseOutput GetPersonByPinNumber(BaseInput baseinput, string pinNumber, out tblPerson item)
        {
            BaseOutput baseOutput;
            item = null;
            string ipNumber = baseinput.IpNumber.GetIpOrEmpty();
            string _requestID = baseinput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseinput.ChannelId);
            try
            {
                item = operationLogic.GetPersonByPinNumber(pinNumber);
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseinput) + IOUtil.GetObjValue(item)), ipNumber, _requestID, _lineNumber, (ChannelEnum)channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
            finally
            {
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(item), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }
        }

        #endregion

        #region tbl Enum

        public BaseOutput AddEnumCategory(BaseInput baseinput, tblEnumCategory enumCategory, out tblEnumCategory enumCategoryOut)
        {

            BaseOutput baseOutput;

            try
            {
                Audit audit = new Audit();

                enumCategory = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, enumCategory);
                enumCategoryOut = operationLogic.AddEnumCategory(enumCategory);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                enumCategoryOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteEnumCategory(BaseInput baseinput, tblEnumCategory enumCategory)
        {
            BaseOutput baseOutput;

            try
            {
                enumCategory = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, enumCategory);
                operationLogic.DeleteEnumCategory(enumCategory);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput UpdateEnumCategory(BaseInput baseinput, tblEnumCategory inputItem, out tblEnumCategory updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateEnumCategory(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetEnumCategorys(BaseInput baseinput, out List<tblEnumCategory> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetEnumCategorys();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetEnumCategoryById(BaseInput baseinput, Int64 ID, out tblEnumCategory item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetEnumCategoryById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetEnumCategorysByName(BaseInput baseinput, string categoryName, out tblEnumCategory item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetEnumCategorysByName(categoryName);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetEnumCategorysForProduct(BaseInput baseinput, out List<tblEnumCategory> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetEnumCategorysForProduct();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput AddEnumValue(BaseInput baseinput, tblEnumValue enumValue, out tblEnumValue enumValueOut)
        {

            BaseOutput baseOutput;

            try
            {
                enumValue = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, enumValue);
                enumValueOut = operationLogic.AddEnumValue(enumValue);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                enumValueOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteEnumValue(BaseInput baseinput, tblEnumValue enumValue)
        {
            BaseOutput baseOutput;

            try
            {
                enumValue = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, enumValue);
                operationLogic.DeleteEnumValue(enumValue);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput UpdateEnumValue(BaseInput baseinput, tblEnumValue inputItem, out tblEnumValue updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {

                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateEnumValue(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetEnumValues(BaseInput baseinput, out List<tblEnumValue> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetEnumValues();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetEnumValueById(BaseInput baseinput, Int64 ID, out tblEnumValue item)
        {
            BaseOutput baseOutput;
            try
            {

                item = operationLogic.GetEnumValueById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetEnumValueByName(BaseInput baseinput, string name, out tblEnumValue item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetEnumValueByName(name);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }


        public BaseOutput GetEnumValuesByEnumCategoryId(BaseInput baseinput, Int64 categoryID, out List<tblEnumValue> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetEnumValuesByEnumCategoryId(categoryID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }


        public BaseOutput GetEnumValuesForProduct(BaseInput baseinput, out List<tblEnumValue> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetEnumValuesForProduct();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        #endregion

        #region tbl_PRM_AdminUnit
        public BaseOutput AddPRM_AdminUnit(BaseInput baseinput, tblPRM_AdminUnit PRM_AdminUnit)
        {
            BaseOutput baseOutput;
            try
            {
                operationLogic.AddPRM_AdminUnit(PRM_AdminUnit);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeletePRM_AdminUnit(BaseInput baseinput, tblPRM_AdminUnit PRM_AdminUnit)
        {
            BaseOutput baseOutput;
            try
            {
                operationLogic.DeletePRM_AdminUnit(PRM_AdminUnit);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetPRM_AdminUnits(BaseInput baseInput, out List<tblPRM_AdminUnit> PRM_CountryList)
        {
            BaseOutput baseOutput;
            try
            {
                PRM_CountryList = operationLogic.GetPRM_AdminUnits();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                PRM_CountryList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetPRM_AdminUnitById(BaseInput baseinput, Int64 countryId, out tblPRM_AdminUnit country)
        {
            BaseOutput baseOutput;
            try
            {
                country = operationLogic.GetPRM_AdminUnitById(countryId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                country = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPRM_AdminUnitsByParentId(BaseInput baseInput, Int64 parentID, out List<tblPRM_AdminUnit> adminUnitList)
        {
            BaseOutput baseOutput;
            try
            {
                adminUnitList = operationLogic.GetPRM_AdminUnitsByParentId(parentID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                adminUnitList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetPRM_AdminUnitByName(BaseInput baseinput, string countryName, out tblPRM_AdminUnit country)
        {
            BaseOutput baseOutput;
            try
            {
                country = operationLogic.GetPRM_AdminUnitByName(countryName);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                country = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdatePRM_AdminUnit(BaseInput baseinput, tblPRM_AdminUnit tblPRM_AdminUnit)
        {
            BaseOutput baseOutput;
            try
            {
                operationLogic.UpdatePRM_AdminUnit(tblPRM_AdminUnit);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GETPRM_AdminUnitsByChildId(BaseInput baseInput, tblPRM_AdminUnit adminUnit, out List<tblPRM_AdminUnit> adminUnitList)
        {
            BaseOutput baseOutput;
            try
            {
                adminUnitList = (List<tblPRM_AdminUnit>)(IList<tblPRM_AdminUnit>)operationLogic.GETPRM_AdminUnitsByChildId(adminUnit);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                adminUnitList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetPRM_AdminUnitByIamasId(BaseInput baseinput, Int64 iamasId, bool isCity, out tblPRM_AdminUnit country)
        {
            BaseOutput baseOutput;
            try
            {
                country = operationLogic.GetPRM_AdminUnitByIamasId(iamasId, isCity);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                country = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        #endregion

        #region tbl_PRM_Thoroghfares
        public BaseOutput AddPRM_Thoroughfare(BaseInput baseinput, tblPRM_Thoroughfare thoroughfare)
        {
            BaseOutput baseOutput;
            try
            {
                thoroughfare = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, thoroughfare);
                operationLogic.AddPRM_Thoroughfare(thoroughfare);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeletePRM_Thoroughfare(BaseInput baseinput, tblPRM_Thoroughfare thoroughfare)
        {
            BaseOutput baseOutput;
            try
            {
                thoroughfare = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, thoroughfare);
                operationLogic.DeletePRM_Thoroughfare(thoroughfare);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetPRM_Thoroughfares(BaseInput baseinput, out List<tblPRM_Thoroughfare> PRM_ThoroughfaresList)
        {
            BaseOutput baseOutput;
            try
            {
                PRM_ThoroughfaresList = operationLogic.GetPRM_Thoroughfares();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                PRM_ThoroughfaresList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetPRM_ThoroughfareById(BaseInput baseinput, Int64 ThoroughfareId, out tblPRM_Thoroughfare thoroughfare)
        {
            BaseOutput baseOutput;
            try
            {
                thoroughfare = operationLogic.GetPRM_ThoroughfareById(ThoroughfareId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                thoroughfare = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPRM_ThoroughfareByName(BaseInput baseinput, string thoroughfareName, out tblPRM_Thoroughfare thoroughfare)
        {
            BaseOutput baseOutput;
            try
            {
                thoroughfare = operationLogic.GetPRM_ThoroughfareByName(thoroughfareName);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                thoroughfare = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdatePRM_Thoroughfare(BaseInput baseinput, tblPRM_Thoroughfare tblPRM_Thoroughfare)
        {
            BaseOutput baseOutput;
            try
            {
                tblPRM_Thoroughfare = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, tblPRM_Thoroughfare);
                operationLogic.UpdatePRM_Thoroughfare(tblPRM_Thoroughfare);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetPRM_ThoroughfaresByAdminUnitId(BaseInput baseinput, Int64 AdminUnitID, out List<tblPRM_Thoroughfare> throughfareList)
        {
            BaseOutput baseOutput;
            try
            {
                throughfareList = operationLogic.GetPRM_ThoroughfaresByAdminUnitId(AdminUnitID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                throughfareList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        #endregion

        #region  tblAddresses logic
        public BaseOutput AddAddress(BaseInput baseinput, tblAddress address, out tblAddress addressOut)
        {
            BaseOutput baseOutput;
            try
            {
                address = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, address);
                addressOut = operationLogic.AddAddress(address);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                addressOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput DeleteAddress(BaseInput baseinput, tblAddress address)
        {
            BaseOutput baseOutput;
            try
            {
                address = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, address);
                operationLogic.DeleteAddress(address);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetAddresses(BaseInput baseinput, out List<tblAddress> addresseslist)
        {
            BaseOutput baseOutput;
            try
            {
                addresseslist = operationLogic.GetAddresses();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                addresseslist = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetAddressById(BaseInput baseinput, Int64 AddressId, out tblAddress address)
        {
            BaseOutput baseOutput;
            try
            {
                address = operationLogic.GetAddressById(AddressId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                address = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetAddressesByCountryId(BaseInput baseinput, Int64 CountryId, out List<tblAddress> addresses)
        {
            BaseOutput baseOutput;
            try
            {
                addresses = operationLogic.GetAddressesByCountryId(CountryId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                addresses = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetAddressesByVillageId(BaseInput baseinput, Int64 VillageId, out List<tblAddress> addresses)
        {
            BaseOutput baseOutput;
            try
            {
                addresses = operationLogic.GetAddressesByVillageId(VillageId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                addresses = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput UpdateAddress(BaseInput baseinput, tblAddress address)
        {
            BaseOutput baseOutput;
            try
            {
                address = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, address);
                operationLogic.UpdateAddress(address);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetAddressesByUserId(BaseInput baseinput, Int64 userID, out List<tblAddress> addresses)
        {
            BaseOutput baseOutput;
            try
            {
                addresses = operationLogic.GetAddressesByUserId(userID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                addresses = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }


        #endregion

        #region tblProductAddresses logic
        public BaseOutput AddProductAddress(BaseInput baseinput, tblProductAddress address, out tblProductAddress addressOut)
        {
            BaseOutput baseOutput;
            try
            {
                address = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, address);
                addressOut = operationLogic.AddProductAddress(address);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                addressOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput DeleteProductAddress(BaseInput baseinput, tblProductAddress address)
        {
            BaseOutput baseOutput;
            try
            {
                address = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, address);
                operationLogic.DeleteProductAddress(address);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProductAddresses(BaseInput baseinput, out List<tblProductAddress> addresseslist)
        {
            BaseOutput baseOutput;
            try
            {
                addresseslist = operationLogic.GetProductAddresses();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                addresseslist = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProductAddressById(BaseInput baseinput, Int64 AddressId, out tblProductAddress address)
        {
            BaseOutput baseOutput;
            try
            {
                address = operationLogic.GetProductAddressById(AddressId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                address = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetProductAddressesByAdminID(BaseInput baseinput, Int64 adminId, out List<tblProductAddress> addresses)
        {
            BaseOutput baseOutput;
            try
            {
                addresses = operationLogic.GetProductAddressesByAdminID(adminId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                addresses = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput UpdateProductAddress(BaseInput baseinput, tblProductAddress address, out tblProductAddress addressOut)
        {
            BaseOutput baseOutput;
            try
            {
                address = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, address);
                addressOut = operationLogic.UpdateProductAddress(address);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                addressOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion


        #region TblProducst


        public BaseOutput AddProductCatalog(BaseInput baseinput, tblProductCatalog productCatalog, out tblProductCatalog productCatalogOut)
        {

            BaseOutput baseOutput;

            try
            {
                productCatalog = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, productCatalog);
                productCatalogOut = operationLogic.AddProductCatalog(productCatalog);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                productCatalogOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteProductCatalog(BaseInput baseinput, tblProductCatalog productCatalog)
        {
            BaseOutput baseOutput;

            try
            {
                productCatalog = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, productCatalog);
                operationLogic.DeleteProductCatalog(productCatalog);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetProductCatalogs(BaseInput baseinput, out List<tblProductCatalog> pCatalogList)
        {
            BaseOutput baseOutput;
            try
            {
                pCatalogList = operationLogic.GetProductCatalogs();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                pCatalogList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetRootProductCatalogs(BaseInput baseinput, out List<tblProductCatalog> pCatalogList)
        {

            BaseOutput baseOutput;
            try
            {
                pCatalogList = operationLogic.GetRootProductCatalogs();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                pCatalogList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetProductCatalogsByParentId(BaseInput baseinput, int parentID, out List<tblProductCatalog> pCatalogList)
        {
            BaseOutput baseOutput;
            try
            {
                pCatalogList = operationLogic.GetProductCatalogsByParentId(parentID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                pCatalogList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetProductCatalogDetailsByParentId(BaseInput baseinput, int parentID, out List<ProductCatalogDetail> pCatalogDetailList)
        {
            BaseOutput baseOutput;
            List<tblProductCatalog> pCatalogList = new List<tblProductCatalog>();
            pCatalogDetailList = new List<ProductCatalogDetail>();
            try
            {
                pCatalogList = operationLogic.GetProductCatalogsByParentId(parentID);
                foreach (var item in pCatalogList)
                {
                    List<tblProduct_Document> productDocumentList = new List<tblProduct_Document>();
                    ProductCatalogDetail pcDetailObj = new ProductCatalogDetail();

                    productDocumentList = operationLogic.GetProductDocumentsByProductCatalogId(item.Id);

                    pcDetailObj.productCatalog = item;
                    pcDetailObj.productCatalogDocumentList = productDocumentList;
                    pCatalogDetailList.Add(pcDetailObj);
                }
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                pCatalogDetailList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }


        public BaseOutput GetProductCatalogDetailsById(BaseInput baseinput, int productID, out ProductCatalogDetail pCatalogDetail)
        {
            BaseOutput baseOutput;
           tblProductCatalog pCatalog = new tblProductCatalog();
            pCatalogDetail = new ProductCatalogDetail();
            try
            {
                pCatalog = operationLogic.GetProductCatalogsById(productID);
                pCatalogDetail.productCatalog = pCatalog;
                pCatalogDetail.parentProductCatalog = sqloperationLogic.GetProducParentProductByProductID(productID);

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                pCatalogDetail = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }


        public BaseOutput GetProductCatalogsById(BaseInput baseinput, int productID, out tblProductCatalog pCatalog)
        {
           
            BaseOutput baseOutput;
            try
            {
                pCatalog = operationLogic.GetProductCatalogsById(productID);
               
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                pCatalog = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput UpdateProductCatalog(BaseInput baseinput, tblProductCatalog productCatalog, out tblProductCatalog updatedproductCatalog)
        {
            BaseOutput baseOutput;
            updatedproductCatalog = null;
            try
            {
                productCatalog = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, productCatalog);
                updatedproductCatalog = operationLogic.UpdateProductCatalog(productCatalog);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        #endregion

        #region PhoneConfirmation

        //public BaseOutput SendConfirmationMessage(BaseInput baseinput, tblConfirmationMessage message)
        //{
        //    BaseOutput baseOutput;
        //    try
        //    {
        //        //message = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, message);
        //        operationLogic.SendConfirmationMessage(message);
        //        return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        return baseOutput = new BaseOutput(true, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
        //    }
        //}
        public BaseOutput SendConfirmationMessageNew(BaseInput baseinput, tblConfirmationMessage message, out tblConfirmationMessage messageOut)
        {

            BaseOutput baseOutput;

            try
            {

                message = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, message);
                messageOut = operationLogic.SendConfirmationMessageNew(message);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                messageOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetConfirmationMessages(BaseInput baseinput, out List<tblConfirmationMessage> confirmationMessages)
        {
            BaseOutput baseOutput;
            try
            {
                confirmationMessages = operationLogic.GetConfirmationMessages();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                confirmationMessages = null;
                return baseOutput = new BaseOutput(true, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        #endregion

        #region tblCommunication
        public BaseOutput AddCommunication(BaseInput baseinput, tblCommunication communication, out tblCommunication CommunicationOut)
        {

            BaseOutput baseOutput;

            try
            {

                communication = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, communication);
                CommunicationOut = operationLogic.AddCommunication(communication);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                CommunicationOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteCommunication(BaseInput baseinput, tblCommunication communication)
        {
            BaseOutput baseOutput;

            try
            {
                communication = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, communication);
                operationLogic.DeleteCommunication(communication);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput UpdateCommunication(BaseInput baseinput, tblCommunication inputItem, out tblCommunication updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateCommunication(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetCommunications(BaseInput baseinput, out List<tblCommunication> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetCommunications();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetCommunicationById(BaseInput baseinput, Int64 ID, out tblCommunication item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetCommunicationById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetCommunicationByPersonId(BaseInput baseinput, Int64 personID, out List<tblCommunication> item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetCommunicationByPersonId(personID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion
        #region tblOffer_Production

        public BaseOutput AddOffer_Production(BaseInput baseinput, tblOffer_Production offer_Production, out tblOffer_Production Offer_ProductionOut)
        {


            BaseOutput baseOutput;

            try
            {
                offer_Production = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, offer_Production);
                Offer_ProductionOut = operationLogic.AddOffer_Production(offer_Production);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                Offer_ProductionOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteOffer_Production(BaseInput baseinput, tblOffer_Production offer_Production)
        {
            BaseOutput baseOutput;

            try
            {
                offer_Production = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, offer_Production);
                operationLogic.DeleteOffer_Production(offer_Production);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput UpdateOffer_Production(BaseInput baseinput, tblOffer_Production inputItem, out tblOffer_Production updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateOffer_Production(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }


        }

        public BaseOutput GetOffer_ProductionById(BaseInput baseinput, Int64 ID, out tblOffer_Production item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetOffer_ProductionById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }


        }
        public BaseOutput GetOffer_ProductionByProductIdandStateEVId(BaseInput baseinput, Int64 productId, Int64 state_Ev_Id, out Int64 count)
        {
            BaseOutput baseOutput;
            try
            {
               count = operationLogic.GetOffer_ProductionByProductIdandStateEVId(productId,state_Ev_Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }


        }

        public BaseOutput GetOffer_Productions(BaseInput baseinput, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOffer_Productions();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetOffer_ProductionsByUserId(BaseInput baseinput, Int64 UserId, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOffer_ProductionsByUserId(UserId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetOffer_ProductionsByUserId1(BaseInput baseinput, Int64 UserId, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOffer_ProductionsByUserId1(UserId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetOffAirOffer_ProductionsByUserId(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOffAirOffer_ProductionsByUserId(Offer);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetOnAirOffer_ProductionsByUserId(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOnAirOffer_ProductionsByUserId(Offer);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetOnAirOfferCount_ProductionsByUserId(BaseInput baseinput, tblOffer_Production Offer,out List<tblOffer_Production> OfferProductionList)
        {
            BaseOutput baseOutput;
            try
            {
                OfferProductionList = operationLogic.GetOnAirOfferCount_ProductionsByUserId(Offer);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                OfferProductionList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetOnAirOffer_ProductionsByUserIDSortedForDate(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOnAirOffer_ProductionsByUserIDSortedForDate(Offer);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetOnAirOffer_ProductionsByUserIDSortedForDateDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOnAirOffer_ProductionsByUserIDSortedForDateDes(Offer);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetOnAirOffer_ProductionsByUserIDSortedForPriceAsc(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOnAirOffer_ProductionsByUserIDSortedForPriceAsc(Offer);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetOnAirOffer_ProductionsByUserIDSortedForPriceDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOnAirOffer_ProductionsByUserIDSortedForPriceDes(Offer);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetOffAirOffer_ProductionsByUserIDSortedForDate(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOffAirOffer_ProductionsByUserIDSortedForDate(Offer);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetOffAirOffer_ProductionsByUserIDSortedForDateDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOffAirOffer_ProductionsByUserIDSortedForDateDes(Offer);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetOffAirOffer_ProductionsByUserIDSortedForPriceAsc(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOffAirOffer_ProductionsByUserIDSortedForPriceAsc(Offer);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }


        public BaseOutput GetOffAirOffer_ProductionsByUserIDSortedForPriceDes(BaseInput baseinput, tblOffer_Production Offer, out List<tblOffer_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOffAirOffer_ProductionsByUserIDSortedForPriceDes(Offer);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
      
        public BaseOutput GetOffer_ProductionsByContractId(BaseInput baseinput, Int64 contractId, out List<tblOffer_Production> offerOut)
        {
            BaseOutput baseOutput;
            try
            {
                offerOut = operationLogic.GetOffer_ProductionsByContractId(contractId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                offerOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        #endregion
        //burda qalmisam
        #region tblPotential_Production
        public BaseOutput AddPotential_Production(BaseInput baseinput, tblPotential_Production potential_Production, out tblPotential_Production Potential_ProductionOut)
        {


            BaseOutput baseOutput;

            try
            {
                potential_Production = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, potential_Production);
                Potential_ProductionOut = operationLogic.AddPotential_Production(potential_Production);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                Potential_ProductionOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput DeletePotential_Production(BaseInput baseinput, tblPotential_Production potential_Production)
        {
            BaseOutput baseOutput;

            try
            {
                potential_Production = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, potential_Production);
                operationLogic.DeletePotential_Production(potential_Production);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput UpdatePotential_Production(BaseInput baseinput, tblPotential_Production input, out tblPotential_Production updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                input = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, input);
                updatedItem = operationLogic.UpdatePotential_Production(input);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetPotential_ProductionById(BaseInput baseinput, Int64 ID, out tblPotential_Production item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetPotential_ProductionById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPotential_Productions(BaseInput baseinput, out List<tblPotential_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetPotential_Productions();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput UpdatePotential_ProductionForUserID(tblPotential_Production item, out List<tblPotential_Production> itemList)
        {

            BaseOutput baseOutput;
            itemList = null;
            try
            {
                itemList = operationLogic.UpdatePotential_ProductionForUserID(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetConfirmedPotential_ProductionsByUserId(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetConfirmedPotential_ProductionsByUserID(potentialProduction);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPotential_ProductionsByUserID(BaseInput baseinput, Int64 UserId, out List<tblPotential_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetPotential_ProductionsByUserID(UserId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetConfirmedPotential_ProductionsByStateAndUserId(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetConfirmedPotential_ProductionsByStateAndUserId(potentialProduction);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetConfirmedPotential_ProductionsByStateAndUserIdForPriceDes(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetConfirmedPotential_ProductionsByStateAndUserIdForPriceDes(potentialProduction);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetConfirmedPotential_ProductionsByStateAndUserIdForPriceAsc(BaseInput baseinput, tblPotential_Production potentialProduction, out List<tblPotential_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetConfirmedPotential_ProductionsByStateAndUserIdForPriceAsc(potentialProduction);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        #endregion
        #region tblDemand_Production
        public BaseOutput AddDemand_Production(BaseInput baseinput, tblDemand_Production demand_Production, out tblDemand_Production Demand_ProductionOut)
        {

            BaseOutput baseOutput;

            try
            {
                demand_Production = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, demand_Production);
                Demand_ProductionOut = operationLogic.AddDemand_Production(demand_Production);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                Demand_ProductionOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteDemand_Production(BaseInput baseinput, tblDemand_Production demand_Production)
        {
            BaseOutput baseOutput;

            try
            {
                demand_Production = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, demand_Production);
                operationLogic.DeleteDemand_Production(demand_Production);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateDemand_Production(BaseInput baseinput, tblDemand_Production inputItem, out tblDemand_Production updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateDemand_Production(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetOnAirDemand_ProductionsByUserId(BaseInput baseinput, tblDemand_Production demand, out List<tblDemand_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOnAirDemand_ProductionsByUserId(demand);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetOnAirDemandCount_ProductionsByUserId(BaseInput baseinput, tblDemand_Production demand, out List<tblDemand_Production> demandProductionList)
        {
            BaseOutput baseOutput;
            try
            {
                demandProductionList = operationLogic.GetOnAirDemandCount_ProductionsByUserId(demand);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                demandProductionList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetDemand_ProductionById(BaseInput baseinput, Int64 ID, out tblDemand_Production item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetDemand_ProductionById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetDemand_Productions(BaseInput baseinput, out List<tblDemand_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetDemand_Productions();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput UpdateDemand_ProductionForUserID(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList)
        {
            BaseOutput baseOutput;
            itemList = null;
            try
            {
                itemList = operationLogic.UpdateDemand_ProductionForUserID(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetDemandProductionForUserId(BaseInput baseinput,Int64 userId, out List<tblDemand_Production> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetDemandProductionForUserId(userId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput UpdateDemand_ProductionForStartAndEndDate(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList)
        {
            BaseOutput baseOutput;
            itemList = null;
            try
            {
                item = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, item);
                itemList = operationLogic.UpdateDemand_ProductionForStartAndEndDate(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetDemand_ProductionsByStateAndUserID(BaseInput baseinput, tblDemand_Production item, out List<tblDemand_Production> itemList)
        {
            BaseOutput baseOutput;
            itemList = null;
            try
            {
                itemList = operationLogic.GetDemand_ProductionsByStateAndUserID(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        #endregion
        #region tblAnnouncement

        public BaseOutput AddAnnouncement(BaseInput baseinput, tblAnnouncement announcement, out tblAnnouncement AnnouncementOut)
        {

            BaseOutput baseOutput;

            try
            {
                announcement = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, announcement);
                AnnouncementOut = operationLogic.AddAnnouncement(announcement);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                AnnouncementOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteAnnouncement(BaseInput baseinput, tblAnnouncement announcement)
        {
            BaseOutput baseOutput;

            try
            {
                announcement = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, announcement);
                operationLogic.DeleteAnnouncement(announcement);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateAnnouncement(BaseInput baseinput, tblAnnouncement inputItem, out tblAnnouncement updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateAnnouncement(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput UpdateAnnouncementPrice(BaseInput baseinput, tblAnnouncement inputItem, out tblAnnouncement updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateAnnouncement(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetAnnouncementById(BaseInput baseinput, Int64 ID, out tblAnnouncement item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetAnnouncementById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetAnnouncements(BaseInput baseinput, out List<AnnouncementDetail> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<AnnouncementDetail>();
            List<tblAnnouncement> tblAnouncementlist = new List<tblAnnouncement>();

            try
            {

                tblAnouncementlist = operationLogic.GetAnnouncements();

                foreach (var obj in tblAnouncementlist)
                {
                    AnnouncementDetail item = new AnnouncementDetail();
                    item.announcement = obj;
                    try
                    {
                        item.parentName = sqloperationLogic.GetProducParentProductByProductID((long)obj.product_id).ProductName;

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
        public BaseOutput GetAnnouncementsByProductId(BaseInput baseinput, Int64 productID, out List<tblAnnouncement> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetAnnouncementsByProductId(productID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }


        public BaseOutput GetAnnouncementDetailsByProductId(BaseInput baseinput, Int64 productID, out List<AnnouncementDetail> itemList)
        {

            BaseOutput baseOutput;
            itemList = new List<AnnouncementDetail>();
            List<AnnouncementDetail> tblAnouncementlist = new List<AnnouncementDetail>();

            try
            {
                tblAnouncementlist = operationLogic.GetAnnouncementDetailsByProductId(productID);
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
        public BaseOutput GetAnnouncementDetailById(BaseInput baseinput, Int64 ID, out AnnouncementDetail item)
        {
            BaseOutput baseOutput;
            item = new AnnouncementDetail();
            tblAnnouncement tblAnouncementObj = new tblAnnouncement();
            try
            {
                tblAnouncementObj = operationLogic.GetAnnouncementDetailsById(ID);
                item.announcement = tblAnouncementObj;

                try
                {
                    item.parentName = sqloperationLogic.GetProducParentProductByProductID((long)tblAnouncementObj.product_id).ProductName;
                    item.parentName = sqloperationLogic.GetProducParentProductByProductID((long)tblAnouncementObj.product_id).ProductName;
                }
                catch (Exception ex)
                {

                }

                item.productCatalogDocumentList = operationLogic.GetProductDocumentsByProductCatalogId((long)tblAnouncementObj.product_id);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }




        public BaseOutput GetAnnouncementDetails(BaseInput baseinput, out List<AnnouncementDetail> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<AnnouncementDetail>();
            List<AnnouncementDetail> tblAnouncementlist = new List<AnnouncementDetail>();
            try
            {
                tblAnouncementlist = operationLogic.GetAnnouncementDetails();
                foreach (var obj in tblAnouncementlist)
                {
                    AnnouncementDetail item = new AnnouncementDetail();
                    item = obj;
                    try
                    {
                        item.parentName = sqloperationLogic.GetProducParentProductByProductID((long)obj.announcement.product_id).ProductName;


                    }
                    catch (Exception ex)
                    {

                    }


                    item.productCatalogDocumentList = operationLogic.GetProductDocumentsByProductCatalogId((long)obj.announcement.product_id);

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

        public BaseOutput GetAnnouncementDetailsCount(BaseInput baseinput, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try 
	      {	        
		
	       count=operationLogic.GetAnnouncementDetailsCount();



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }


        public BaseOutput GetAnnouncementsByYearAndPartOfYear(BaseInput baseinput, string year, string month, out List<tblAnnouncement> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetAnnouncementsByYearAndPartOfYear(year, month);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        #endregion
        #region tblEmployee
        public BaseOutput AddEmployee(BaseInput baseinput, tblEmployee employee, out tblEmployee EmployeeOut)
        {

            BaseOutput baseOutput;

            try
            {
                employee = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, employee);
                EmployeeOut = operationLogic.AddEmployee(employee);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                EmployeeOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteEmployee(BaseInput baseinput, tblEmployee employee)
        {
            BaseOutput baseOutput;

            try
            {
                employee = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, employee);
                operationLogic.DeleteEmployee(employee);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateEmployee(BaseInput baseinput, tblEmployee inputItem, out tblEmployee updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateEmployee(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetEmployeeById(BaseInput baseinput, Int64 ID, out tblEmployee item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetEmployeeById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetEmployees(BaseInput baseinput, out List<tblEmployee> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetEmployees();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        #endregion
        #region tblUser
        public BaseOutput AddUser(BaseInput baseinput, tblUser user, out tblUser UserOut)
        {

            BaseOutput baseOutput;

            try
            {
                user = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, user);
                UserOut = operationLogic.AddUser(user);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                UserOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteUser(BaseInput baseinput, tblUser user)
        {
            BaseOutput baseOutput;

            try
            {
                user = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, user);
                operationLogic.DeleteUser(user);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput UpdateUser(BaseInput baseinput, tblUser inputItem, out tblUser updatedItem)
        {
            BaseOutput baseOutput;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateUser(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                updatedItem = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetUserById(BaseInput baseinput, Int64 ID, out tblUser item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetUserById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetUserByUserEmail(BaseInput baseinput, string email, out tblUser item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetUserByUserEmail(email);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetUsers(BaseInput baseinput, out List<tblUser> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetUsers();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetUserByUserName(BaseInput baseinput, string UserName, out tblUser item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetUserByUserName(UserName);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetUsersByUserType(BaseInput baseinput, long userTypeID, out List<tblUser> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetUsersByUserType(userTypeID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        #endregion
        #region tblRole
        public BaseOutput AddRole(BaseInput baseinput, tblRole role, out tblRole RoleOut)
        {

            BaseOutput baseOutput;

            try
            {
                role = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, role);
                RoleOut = operationLogic.AddRole(role);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                RoleOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteRole(BaseInput baseinput, tblRole role)
        {
            BaseOutput baseOutput;

            try
            {
                role = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, role);
                operationLogic.DeleteRole(role);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateRole(BaseInput baseinput, tblRole inputItem, out tblRole updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateRole(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetRoleById(BaseInput baseinput, Int64 ID, out tblRole item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetRoleById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetRoles(BaseInput baseinput, out List<tblRole> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetRoles();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetRoleByName(BaseInput baseinput, string name, out tblRole item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetRoleByName(name);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        #endregion
        #region tblExpertise
        public BaseOutput AddExpertise(BaseInput baseinput, tblExpertise expertise, out tblExpertise ExpertiseOut)
        {

            BaseOutput baseOutput;

            try
            {
                expertise = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, expertise);
                ExpertiseOut = operationLogic.AddExpertise(expertise);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                ExpertiseOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteExpertise(BaseInput baseinput, tblExpertise expertise)
        {
            BaseOutput baseOutput;

            try
            {
                expertise = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, expertise);
                operationLogic.DeleteExpertise(expertise);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateExpertise(BaseInput baseinput, tblExpertise inputItem, out tblExpertise updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateExpertise(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetExpertiseById(BaseInput baseinput, Int64 ID, out tblExpertise item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetExpertiseById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetExpertises(BaseInput baseinput, out List<tblExpertise> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetExpertises();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        #endregion
        #region tblTitle
        public BaseOutput AddTitle(BaseInput baseinput, tblTitle title, out tblTitle TitleOut)
        {

            BaseOutput baseOutput;

            try
            {
                title = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, title);
                TitleOut = operationLogic.AddTitle(title);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                TitleOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteTitle(BaseInput baseinput, tblTitle title)
        {
            BaseOutput baseOutput;

            try
            {
                title = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, title);
                operationLogic.DeleteTitle(title);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateTitle(BaseInput baseinput, tblTitle inputItem, out tblTitle updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateTitle(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetTitleById(BaseInput baseinput, Int64 ID, out tblTitle item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetTitleById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetTitles(BaseInput baseinput, out List<tblTitle> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetTitles();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        #endregion
        #region tblParty
        public BaseOutput AddParty(BaseInput baseinput, tblParty party, out tblParty PartyOut)
        {

            BaseOutput baseOutput;

            try
            {
                party = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, party);
                PartyOut = operationLogic.AddParty(party);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                PartyOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteParty(BaseInput baseinput, tblParty party)
        {
            BaseOutput baseOutput;

            try
            {
                party = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, party);
                operationLogic.DeleteParty(party);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateParty(BaseInput baseinput, tblParty inputItem, out tblParty updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateParty(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetPartyById(BaseInput baseinput, Int64 ID, out tblParty item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetPartyById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetParties(BaseInput baseinput, out List<tblParty> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetParties();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        #endregion
        #region  tblOrganization
        public BaseOutput AddOrganization(BaseInput baseinput, tblOrganization organization, out tblOrganization OrganizationOut)
        {

            BaseOutput baseOutput;

            try
            {
                organization = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, organization);
                OrganizationOut = operationLogic.AddOrganization(organization);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                OrganizationOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteOrganization(BaseInput baseinput, tblOrganization organization)
        {
            BaseOutput baseOutput;

            try
            {
                organization = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, organization);
                operationLogic.DeleteOrganization(organization);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateOrganization(BaseInput baseinput, tblOrganization inputItem, out tblOrganization updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateOrganization(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetOrganizationById(BaseInput baseinput, Int64 ID, out tblOrganization item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetOrganizationById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetOrganizations(BaseInput baseinput, out List<tblOrganization> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetOrganizations();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        #endregion
        #region  tblConfirmationMessage
        public BaseOutput AddConfirmationMessage(BaseInput baseinput, tblConfirmationMessage confirmationMessage, out tblConfirmationMessage ConfirmationMessageOut)
        {

            BaseOutput baseOutput;

            try
            {

                ConfirmationMessageOut = operationLogic.AddConfirmationMessage(confirmationMessage);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                ConfirmationMessageOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteConfirmationMessage(BaseInput baseinput, tblConfirmationMessage confirmationMessage)
        {
            BaseOutput baseOutput;

            try
            {
                confirmationMessage = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, confirmationMessage);
                operationLogic.DeleteConfirmationMessage(confirmationMessage);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateConfirmationMessage(BaseInput baseinput, tblConfirmationMessage inputItem, out tblConfirmationMessage updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateConfirmationMessage(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetConfirmationMessageById(BaseInput baseinput, Int64 ID, out tblConfirmationMessage item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetConfirmationMessageById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        #endregion
        //bunda
        #region  tblForeign_Organization
        public BaseOutput AddForeign_Organization(BaseInput baseinput, tblForeign_Organization foreign_Organization, out tblForeign_Organization Foreign_OrganizationOut)
        {

            BaseOutput baseOutput;

            try
            {
                foreign_Organization = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, foreign_Organization);
                Foreign_OrganizationOut = operationLogic.AddForeign_Organization(foreign_Organization);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                Foreign_OrganizationOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteForeign_Organization(BaseInput baseinput, tblForeign_Organization foreign_Organization)
        {
            BaseOutput baseOutput;

            try
            {
                foreign_Organization = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, foreign_Organization);
                operationLogic.DeleteForeign_Organization(foreign_Organization);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateForeign_Organization(BaseInput baseinput, tblForeign_Organization inputItem, out tblForeign_Organization updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateForeign_Organization(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetForeign_OrganizationById(BaseInput baseinput, Int64 ID, out tblForeign_Organization item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetForeign_OrganizationById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetForeign_Organizations(BaseInput baseinput, out List<tblForeign_Organization> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetForeign_Organizations();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetForeign_OrganizationByUserId(BaseInput baseinput, Int64 userId, out tblForeign_Organization item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetForeign_OrganizationByUserId(userId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {
                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetForeign_OrganizationByVoen(BaseInput baseinput, string voen, out tblForeign_Organization item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetForeign_OrganizationByVoen(voen);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }


        public BaseOutput GetForeign_OrganisationsByParentId(BaseInput baseinput, long Id, out List<tblForeign_Organization> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetForeign_OrganisationsByParentId(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }

        }


        public BaseOutput WS_GetForeignOrganizationListByUserId(BaseInput baseinput, long userId, out List<tblForeign_Organization> itemList)
        {
            BaseOutput baseOutput;
            itemList = null;
            List<tblForeign_Organization> list = new List<tblForeign_Organization>();
            tblForeign_Organization item;
            try
            {
                item = operationLogic.GetForeign_OrganizationByUserId(userId);


                list = operationLogic.GetForeign_OrganisationsByParentId(item.Id);

                itemList = list;

                foreach (tblForeign_Organization fo in itemList)
                {
                    List<tblForeign_Organization> list1 = operationLogic.GetForeign_OrganisationsByParentId(item.Id);
                    itemList.Concat(list1);
                    foreach (var i1 in list1)
                    {
                        List<tblForeign_Organization> list2 = operationLogic.GetForeign_OrganisationsByParentId(i1.Id);
                        itemList.Concat(list2);
                        foreach (var i2 in list2)
                        {
                            List<tblForeign_Organization> list3 = operationLogic.GetForeign_OrganisationsByParentId(i2.Id);
                            itemList.Concat(list3);
                            foreach (var i3 in list3)
                            {
                                List<tblForeign_Organization> list4 = operationLogic.GetForeign_OrganisationsByParentId(i3.Id);
                                itemList.Concat(list4);
                            }
                        }

                    }
                }
                itemList.Add(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }

        }
        #endregion


        #region ProductCatalogControlOperation


        public BaseOutput AddProductCatalogControl(BaseInput baseinput, tblProductCatalogControl item, out tblProductCatalogControl itemOut)
        {

            BaseOutput baseOutput;

            try
            {
                item = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, item);
                itemOut = operationLogic.AddProductCatalogControl(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteProductCatalogControl(BaseInput baseinput, tblProductCatalogControl item)
        {
            BaseOutput baseOutput;

            try
            {
                item = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, item);
                operationLogic.DeleteProductCatalogControl(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetProductCatalogControls(BaseInput baseinput, out List<tblProductCatalogControl> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetProductCatalogControls();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetProductCatalogControlById(BaseInput baseinput, Int64 Id, out tblProductCatalogControl item)
        {

            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetProductCatalogControlById(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetProductCatalogControlsByProductID(BaseInput baseinput, Int64 productId, out List<tblProductCatalogControl> itemList)
        {

            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetProductCatalogControlsByProductID(productId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }


        public BaseOutput GetProductCatalogControlsByECategoryID(BaseInput baseinput, Int64 enumCategoryID, out List<tblProductCatalogControl> itemList)
        {

            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetProductCatalogControlsByECategoryID(enumCategoryID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetProductCatalogControlsByEValueID(BaseInput baseinput, Int64 enumValueID, out List<tblProductCatalogControl> itemList)
        {

            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetProductCatalogControlsByEValueID(enumValueID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }


        public BaseOutput UpdateProductCatalogControl(BaseInput baseinput, tblProductCatalogControl item, out tblProductCatalogControl updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                item = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, item);
                updatedItem = operationLogic.UpdateProductCatalogControl(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput AddProductCatalogControlList(BaseInput baseinput, List<tblProductCatalogControl> itemList, out List<tblProductCatalogControl> itemListOut)
        {

            BaseOutput baseOutput;
            itemListOut = new List<tblProductCatalogControl>();
            try
            {
                foreach (var item in itemList)
                {

                    tblProductCatalogControl obj = new tblProductCatalogControl();
                    obj = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, item);
                    itemListOut.Add(operationLogic.AddProductCatalogControl(obj));
                }
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                itemListOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteAllProductCatalogControlByProductID(BaseInput baseinput, Int64 productID)
        {
            BaseOutput baseOutput;

            try
            {
                List<tblProductCatalogControl> itemList = new List<tblProductCatalogControl>();
                itemList = operationLogic.GetProductCatalogControlsByProductID(productID);

                tblProductCatalogControl obj = new tblProductCatalogControl();
                foreach (var item in itemList)
                {
                    obj = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, item);
                    operationLogic.DeleteProductCatalogControl(obj);
                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }


        #endregion

        #region tblProductionDocument

        public BaseOutput AddProductionDocument(BaseInput baseinput, tblProduction_Document productionDocument, out tblProduction_Document productionDocumentOut)
        {
            BaseOutput baseOutput;
            try
            {
                productionDocument = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, productionDocument);
                productionDocumentOut = operationLogic.AddProductionDocument(productionDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                productionDocumentOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteProductionDocument(BaseInput baseinput, tblProduction_Document productionDocument)
        {
            BaseOutput baseOutput;
            try
            {
                productionDocument = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, productionDocument);
                operationLogic.DeleteProductionDocument(productionDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProductionDocuments(BaseInput baseinput, out List<tblProduction_Document> ProductionDocuments)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionDocuments = operationLogic.GetProductionDocuments();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionDocuments = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProductionDocumentById(BaseInput baseinput, Int64 Id, out tblProduction_Document ProductionDocument)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionDocument = operationLogic.GetProductionDocumentById(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionDocument = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetProductionDocumentsByDemand_Production_Id(BaseInput baseinput, tblProduction_Document ProductionDocument, out List<tblProduction_Document> ProductionDocumentList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionDocumentList = operationLogic.GetProductionDocumentsByDemand_Production_Id(ProductionDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionDocumentList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProductionDocumentsByOffer_Production_Id(BaseInput baseinput, tblProduction_Document ProductionDocument, out List<tblProduction_Document> ProductionDocumentList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionDocumentList = operationLogic.GetProductionDocumentsByOffer_Production_Id(ProductionDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionDocumentList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProductionDocumentsByPotential_Production_Id(BaseInput baseinput, tblProduction_Document ProductionDocument, out List<tblProduction_Document> ProductionDocumentList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionDocumentList = operationLogic.GetProductionDocumentsByPotential_Production_Id(ProductionDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionDocumentList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProductionDocumentsByGroupId(BaseInput baseinput, string GroupId, out List<tblProduction_Document> ProductionDocumentList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionDocumentList = operationLogic.GetProductionDocumentsByGroupId(GroupId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionDocumentList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateProductionDocument(BaseInput baseinput, tblProduction_Document productionDocument, out tblProduction_Document productionDocumentOut)
        {
            BaseOutput baseOutput;
            try
            {
                productionDocument = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, productionDocument);
                productionDocumentOut = operationLogic.UpdateProductionDocument(productionDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                productionDocumentOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }


        public BaseOutput UpdateProductionDocumentForGroupID(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> productionDocumentList)
        {
            BaseOutput baseOutput;
            try
            {
                productionDocument = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, productionDocument);
                productionDocumentList = operationLogic.UpdateProductionDocumentForGroupID(productionDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                productionDocumentList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProductionDocumentsByGroupIdAndPotential_Production_Id(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> ProductionDocumentList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionDocumentList = operationLogic.GetProductionDocumentsByGroupIdAndPotential_Production_Id(productionDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionDocumentList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProductionDocumentsByGroupIdAndOffer_Production_Id(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> ProductionDocumentList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionDocumentList = operationLogic.GetProductionDocumentsByGroupIdAndOffer_Production_Id(productionDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionDocumentList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProductionDocumentsByGroupIdAndDemand_Production_Id(BaseInput baseinput, tblProduction_Document productionDocument, out List<tblProduction_Document> ProductionDocumentList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionDocumentList = operationLogic.GetProductionDocumentsByGroupIdAndDemand_Production_Id(productionDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionDocumentList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion


        #region tblProductDocument
        public BaseOutput AddProductDocument(BaseInput baseinput, tblProduct_Document productDocument, out tblProduct_Document productDocumentOut)
        {
            BaseOutput baseOutput;

            try
            {
                productDocument = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, productDocument);
                productDocumentOut = operationLogic.AddProductDocument(productDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                productDocumentOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteProductDocument(BaseInput baseinput, tblProduct_Document ProductDocument)
        {
            BaseOutput baseOutput;
            try
            {
                ProductDocument = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, ProductDocument);
                operationLogic.DeleteProductDocument(ProductDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput DeleteProductDocumentByProductID(BaseInput baseinput, tblProduct_Document ProductDocument, out List<tblProduct_Document> productDocumentListOut)
        {
            BaseOutput baseOutput;
            try
            {
                ProductDocument = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, ProductDocument);
                productDocumentListOut = operationLogic.DeleteProductDocumentByProductID(ProductDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                productDocumentListOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProductDocuments(BaseInput baseInput, out List<tblProduct_Document> ProductDocuments)
        {
            BaseOutput baseOutput;
            try
            {
                ProductDocuments = operationLogic.GetProductDocuments();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductDocuments = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProductDocumentById(BaseInput baseinput, Int64 Id, out tblProduct_Document ProductDocument)
        {
            BaseOutput baseOutput;
            try
            {
                ProductDocument = operationLogic.GetProductDocumentById(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductDocument = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetProductDocumentsByProductCatalogId(BaseInput baseinput, Int64 productCatalogId, out List<tblProduct_Document> ProductDocumentList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductDocumentList = operationLogic.GetProductDocumentsByProductCatalogId(productCatalogId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductDocumentList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }


        public BaseOutput UpdateProductDocument(BaseInput baseinput, tblProduct_Document ProductDocument, out tblProduct_Document ProductDocumentOut)
        {
            BaseOutput baseOutput;
            try
            {
                ProductDocument = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, ProductDocument);
                ProductDocumentOut = operationLogic.UpdateProductDocument(ProductDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                ProductDocumentOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        #endregion
        #region tblProductionControl
        public BaseOutput AddProductionControl(BaseInput baseinput, tblProductionControl productionControl, out tblProductionControl productionControltOut)
        {
            BaseOutput baseOutput;

            try
            {
                productionControl = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, productionControl);
                productionControltOut = operationLogic.AddProductionControl(productionControl);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                productionControltOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteProductionControl(BaseInput baseinput, tblProductionControl ProductionControl)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionControl = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, ProductionControl);
                operationLogic.DeleteProductionControl(ProductionControl);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProductionControls(BaseInput baseInput, out List<tblProductionControl> ProductionControl)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionControl = operationLogic.GetProductionControls();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionControl = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProductionControlById(BaseInput baseinput, Int64 Id, out tblProductionControl ProductionControl)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionControl = operationLogic.GetProductionControlById(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionControl = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetProductionControlsByPotentialProductionId(BaseInput baseinput, Int64 potentialProductionId, out List<tblProductionControl> ProductionControlList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionControlList = operationLogic.GetProductionControlsPotentialProductionId(potentialProductionId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionControlList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetProductionControlsByOfferProductionId(BaseInput baseinput, Int64 offerProductionId, out List<tblProductionControl> ProductionControlList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionControlList = operationLogic.GetProductionControlsByOfferProductionId(offerProductionId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionControlList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetProductionControlsByDemandProductionId(BaseInput baseinput, Int64 demandProductionId, out List<tblProductionControl> ProductionControlList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionControlList = operationLogic.GetProductionControlsByDemandProductionId(demandProductionId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionControlList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput UpdateProductionControl(BaseInput baseinput, tblProductionControl ProductionControl, out tblProductionControl ProductionControlOut)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionControl = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, ProductionControl);
                ProductionControlOut = operationLogic.UpdateProductionControl(ProductionControl);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                ProductionControlOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput DeleteProductionControlsByProduction_Type_ev_Id(BaseInput baseinput, Int64 production_Type_ev_ID)
        {
            BaseOutput baseOutput;
            try
            {
                operationLogic.DeleteProductionControlsByProduction_Type_ev_Id(production_Type_ev_ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion
        #region tblProduction_Calendar
        public BaseOutput AddProduction_Calendar(BaseInput baseinput, tblProduction_Calendar ProductionCalendar, out tblProduction_Calendar productionCalendarOut)
        {
            BaseOutput baseOutput;

            try
            {
                ProductionCalendar = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, ProductionCalendar);
                productionCalendarOut = operationLogic.AddProduction_Calendar(ProductionCalendar);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                productionCalendarOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteProduction_Calendar(BaseInput baseinput, tblProduction_Calendar ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, ProductionCalendar);
                operationLogic.DeleteProduction_Calendar(ProductionCalendar);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProduction_Calendar(BaseInput baseInput, out List<tblProduction_Calendar> ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = operationLogic.GetProduction_Calendar();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendar = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProduction_CalendarById(BaseInput baseinput, Int64 Id, out tblProduction_Calendar ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = operationLogic.GetProduction_CalendarById(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendar = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetProductionCalendarByProductionId(BaseInput baseinput, Int64 ProductionId, Int64 productionType_eV_Id, out tblProduction_Calendar ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = operationLogic.GetProductionCalendarByProductionId(ProductionId, productionType_eV_Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendar = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetProductionCalendarProductiontypeeVId(BaseInput baseinput, Int64 production_type_eV_Id, out List<tblProduction_Calendar> ProductionCalendarlList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendarlList = operationLogic.GetProductionCalendarProductiontypeeVId(production_type_eV_Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendarlList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetProductionCalendartransportationeVId(BaseInput baseinput, Int64 t_eV_Id, Int64 Year, out List<tblProduction_Calendar> ProductionCalendarlList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendarlList = operationLogic.GetProductionCalendartransportationeVId(t_eV_Id, Year);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendarlList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput UpdateProductionCalendar(BaseInput baseinput, tblProduction_Calendar ProductionCalendar, out tblProduction_Calendar ProductionCalendarOut)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, ProductionCalendar);
                ProductionCalendarOut = operationLogic.UpdateProductionCalendar(ProductionCalendar);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                ProductionCalendarOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProductionCalendarByInstance(BaseInput baseinput, tblProductionCalendar item, out tblProductionCalendar ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = operationLogic.GetProductionCalendarByInstance(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendar = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        #endregion


        #region Jira Request

        public BaseOutput GetDocumentSizebyGroupId(string groupID, Int64 production_type_eVId, out Int64 totalSize)
        {

            BaseOutput baseOutput;
            try
            {
                totalSize = operationLogic.GetDocumentSizebyGroupId(groupID, production_type_eVId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                totalSize = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetListOfPotensialProductionByUserId(string userID, out List<ProductionDetail> list)
        {
            BaseOutput baseOutput;
            try
            {
                list = operationLogic.GetListOfPotensialProductionByUserId(userID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                list = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetDocumentSizeByGroupIdAndPotentialProductionID(tblProduction_Document production_Document, out Int64 totalSize)
        {

            BaseOutput baseOutput;
            try
            {
                totalSize = operationLogic.GetDocumentSizeByGroupIdAndPotentialProductionID(production_Document);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                totalSize = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetDocumentSizeByGroupIdAndOfferProductionID(tblProduction_Document production_Document, out Int64 totalSize)
        {

            BaseOutput baseOutput;
            try
            {
                totalSize = operationLogic.GetDocumentSizeByGroupIdAndPotentialProductionID(production_Document);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                totalSize = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetDocumentSizeByGroupIdAndDemandProductionID(tblProduction_Document production_Document, out Int64 totalSize)
        {

            BaseOutput baseOutput;
            try
            {
                totalSize = operationLogic.GetDocumentSizeByGroupIdAndPotentialProductionID(production_Document);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                totalSize = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        #endregion

        #region TblComMessage

        public BaseOutput AddComMessage(BaseInput baseinput, tblComMessage item, out tblComMessage itemOut)
        {
            BaseOutput baseOutput;

            try
            {
                item = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, item);
                itemOut = operationLogic.AddComMessage(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteComMessage(BaseInput baseinput, tblComMessage item)
        {
            BaseOutput baseOutput;
            try
            {
                item = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, item);
                operationLogic.DeleteComMessage(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput UpdateComMessage(BaseInput baseinput, tblComMessage item, out tblComMessage itemOut)
        {
            BaseOutput baseOutput;
            try
            {
                item = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, item);
                itemOut = operationLogic.UpdateComMessage(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetComMessageById(BaseInput baseInput, Int64 ID, out tblComMessage itemOut)
        {
            BaseOutput baseOutput;
            try
            {
                itemOut = operationLogic.GetComMessageById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetComMessagesyByGroupId(BaseInput baseInput, Int64 groupId, out List<tblComMessage> itemListOut)
        {
            BaseOutput baseOutput;
            try
            {
                itemListOut = operationLogic.GetComMessagesyByGroupId(groupId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemListOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }


        public BaseOutput GetComMessagesyByToUserId(BaseInput baseInput, Int64 toUserId, out List<tblComMessage> itemListOut)
        {
            BaseOutput baseOutput;
            try
            {
                itemListOut = operationLogic.GetComMessagesyByToUserId(toUserId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemListOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }


        public BaseOutput GetComMessagesyByFromUserId(BaseInput baseInput, Int64 fromUserId, out List<tblComMessage> itemListOut)
        {
            BaseOutput baseOutput;
            try
            {
                itemListOut = operationLogic.GetComMessagesyByFromUserId(fromUserId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemListOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetComMessagesyByFromUserIDToUserId(BaseInput baseInput, Int64 fromUserId, Int64 toUserId, out List<tblComMessage> itemListOut)
        {
            BaseOutput baseOutput;
            try
            {
                itemListOut = operationLogic.GetComMessagesyByFromUserIDToUserId(fromUserId, toUserId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemListOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }


        //ferid
        public BaseOutput GetComMessagesyByToUserIdSortedForDateAsc(BaseInput baseInput, Int64 toUserId, out List<tblComMessage> itemListOut)
        {
            BaseOutput baseOutput;
            try
            {
                itemListOut = operationLogic.GetComMessagesyByToUserIdSortedForDateAsc(toUserId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemListOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetComMessagesyByToUserIdSortedForDateDes(BaseInput baseInput, Int64 toUserId, out List<tblComMessage> itemListOut)
        {
            BaseOutput baseOutput;
            try
            {
                itemListOut = operationLogic.GetComMessagesyByToUserIdSortedForDateDes(toUserId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemListOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetComMessagesyByFromUserIdSortedForDateAsc(BaseInput baseInput, Int64 fromUserId, out List<tblComMessage> itemListOut)
        {
            BaseOutput baseOutput;
            try
            {
                itemListOut = operationLogic.GetComMessagesyByFromUserIdSortedForDateAsc(fromUserId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemListOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetComMessagesyByFromUserIdSortedForDateDes(BaseInput baseInput, Int64 fromUserId, out List<tblComMessage> itemListOut)
        {
            BaseOutput baseOutput;
            try
            {
                itemListOut = operationLogic.GetComMessagesyByFromUserIdSortedForDateDes(fromUserId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemListOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetNotReadComMessagesByToUserId(BaseInput baseInput, Int64 toUserId, out List<tblComMessage> itemListOut)
        {
            BaseOutput baseOutput;
            try
            {
                itemListOut = operationLogic.GetNotReadComMessagesByToUserId(toUserId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemListOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }


        //////////////////
        #endregion

        #region tblUserRole
        public BaseOutput AddUserRole(BaseInput baseinput, tblUserRole userRole, out tblUserRole UserRoleOut)
        {

            BaseOutput baseOutput;

            try
            {
                userRole = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, userRole);
                UserRoleOut = operationLogic.AddUserRole(userRole);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                UserRoleOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteUserRole(BaseInput baseinput, tblUserRole userRole)
        {
            BaseOutput baseOutput;

            try
            {
                userRole = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, userRole);
                operationLogic.DeleteUserRole(userRole);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateUserRole(BaseInput baseinput, tblUserRole inputItem, out tblUserRole updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateUserRole(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetUserRoleById(BaseInput baseinput, Int64 ID, out tblUserRole item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetUserRoleById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetUserRoles(BaseInput baseinput, out List<tblUserRole> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetUserRoles();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetUserRolesByUserId(BaseInput baseinput, long userId, out List<tblUserRole> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetUserRoleByUserId(userId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }



        #endregion
        #region tblProductPrice
        public BaseOutput AddProductPrice(BaseInput baseinput, tblProductPrice productionPrice, out tblProductPrice productionPricetOut)
        {
            BaseOutput baseOutput;
            try
            {
                productionPrice = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, productionPrice);
                productionPricetOut = operationLogic.AddProductPrice(productionPrice);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
                /*
                productionDocument = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, productionDocument);
                productionDocumentOut = operationLogic.AddProductionDocument(productionDocument);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");*/
            }
            catch (Exception ex)
            {
                productionPricetOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetProductPriceById(BaseInput baseinput, Int64 ID, out ProductPriceDetail item)
        {
            BaseOutput baseOutput;
            item = new ProductPriceDetail();
            try
            {
                tblProductPrice price = new tblProductPrice();
                price = operationLogic.GetProductPriceById(ID);
                item.priceID = price.Id;
                item.productID = (long)price.productId;
                item.unit_price = (long)price.unit_price;
                item.year = (long)price.year;
                item.partOfYear = (long)price.partOfYear;
                try
                {
                    item.productName = operationLogic.GetProductCatalogsById((int)ID).ProductName;
                }
                catch (Exception)
                {


                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput DeleteProductPrice(BaseInput baseinput, tblProductPrice productionPrice)
        {
            BaseOutput baseOutput;
            try
            {
                productionPrice = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, productionPrice);
                operationLogic.DeleteProductPrice(productionPrice);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateProductPrice(BaseInput baseinput, tblProductPrice inputItem, out tblProductPrice updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateProductPrice(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetProductPriceByProductId(BaseInput baseinput, Int64 producId, out ProductPriceDetail item)
        {
            BaseOutput baseOutput;
            item = new ProductPriceDetail();
            try
            {
                item.productPriceList = operationLogic.GetProductPriceByProductId(producId);
                try
                {
                    item.productName = operationLogic.GetProductCatalogsById((int)producId).ProductName;
                }
                catch (Exception)
                {


                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetProductPriceByYearId(BaseInput baseinput, Int64 yearId, out ProductPriceDetail item)
        {
            BaseOutput baseOutput;
            item = new ProductPriceDetail();
            try
            {
                item.productPriceList = operationLogic.GetProductPriceByYearId(yearId);
                try
                {
                    item.productName = operationLogic.GetProductCatalogsById((int)item.productPriceList[0].productId).ProductName;
                }
                catch (Exception ex)
                {


                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetProductPriceByYearAndProductIdAndPartOfYear(BaseInput baseinput, Int64 productID, Int64 year, Int64 partOfYear, out ProductPriceDetail item)
        {
            BaseOutput baseOutput;
            item = new ProductPriceDetail();
            item.productPriceList = new List<tblProductPrice>();
            try
            {
                tblProductPrice price = new tblProductPrice();
                price = operationLogic.GetProductPriceByYearAndProductIdAndPartOfYear(productID, year, partOfYear);
                item.priceID = price.Id;
                item.productID = (long)price.productId;
                item.unit_price = (long)price.unit_price;
                item.year = (long)price.year;
                item.partOfYear = (long)price.partOfYear;
                try
                {
                    item.productName = operationLogic.GetProductCatalogsById((int)productID).ProductName;
                }
                catch (Exception ex)
                {


                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetProductPriceByYearIdAndProductId(BaseInput baseinput, Int64 productID, Int64 year, out ProductPriceDetail item)
        {
            BaseOutput baseOutput;
            item = new ProductPriceDetail();
            item.productPriceList = new List<tblProductPrice>();
            try
            {
                item.productPriceList = operationLogic.GetProductPriceByYearIdAndProductId(productID, year);
                try
                {
                    item.productName = operationLogic.GetProductCatalogsById((int)productID).ProductName;
                }
                catch (Exception ex)
                {


                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        #endregion



        #region tblAuthenticatedPart
        public BaseOutput GetAuthenticatedPartByName(BaseInput baseinput, string name, out tblAuthenticatedPart item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetAuthenticatedPartByName(name);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetAuthenticatedParts(BaseInput baseinput, out List<tblAuthenticatedPart> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetAuthenticatedParts();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetAuthenticatedPartById(BaseInput baseinput, long Id, out tblAuthenticatedPart item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetAuthenticatedPartById(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        #endregion

        #region tblPrivilegedRole
        public BaseOutput GetPrivilegedRolesByAuthenticatedPartId(BaseInput baseinput, long authenticatedPartId, out List<tblPrivilegedRole> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetPrivilegedRolesByAuthenticatedPartId(authenticatedPartId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPrivilegedRoles(BaseInput baseinput, out List<tblPrivilegedRole> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetPrivilegedRoles();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetPrivilegedRoleById(BaseInput baseinput, long Id, out tblPrivilegedRole item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetPrivilegedRoleById(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput DeletePrivilegedRole(BaseInput baseinput, tblPrivilegedRole privilegedRole)
        {
            BaseOutput baseOutput;

            try
            {
                privilegedRole = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, privilegedRole);
                operationLogic.DeletePrivilegedRole(privilegedRole);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput AddPrivilegedRole(BaseInput baseinput, tblPrivilegedRole privilegedRole, out tblPrivilegedRole PrivilegedRoleOut)
        {

            BaseOutput baseOutput;

            try
            {
                privilegedRole = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, privilegedRole);
                PrivilegedRoleOut = operationLogic.AddPrivilegedRole(privilegedRole);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                PrivilegedRoleOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }


        #endregion
        #region tblProductProfileImage
        public BaseOutput AddProductProfileImage(BaseInput baseinput, tblProductProfileImage productProfileImage, out tblProductProfileImage ProductProfileImageOut)
        {

            BaseOutput baseOutput;

            try
            {
                productProfileImage = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, productProfileImage);
                ProductProfileImageOut = operationLogic.AddProductProfileImage(productProfileImage);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                ProductProfileImageOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput DeleteProductProfileImage(BaseInput baseinput, tblProductProfileImage productProfileImage)
        {
            BaseOutput baseOutput;

            try
            {
                productProfileImage = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, productProfileImage);
                operationLogic.DeleteProductProfileImage(productProfileImage);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateProductProfileImage(BaseInput baseinput, tblProductProfileImage inputItem, out tblProductProfileImage updatedItem)
        {
            BaseOutput baseOutput;
            updatedItem = null;
            try
            {
                inputItem = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, inputItem);
                updatedItem = operationLogic.UpdateProductProfileImage(inputItem);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetProductProfileImageById(BaseInput baseinput, Int64 ID, out tblProductProfileImage item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetProductProfileImageById(ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetProductProfileImages(BaseInput baseinput, out List<tblProductProfileImage> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetProductProfileImages();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }
        public BaseOutput GetProductProfileImageByProductId(BaseInput baseinput, Int64 productId, out tblProductProfileImage item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetProductProfileImageByProductId(productId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        #endregion
        #region tblPRM_ASCBranch
        public BaseOutput AddPRM_ASCBranch(BaseInput baseinput, tblPRM_ASCBranch branch, out tblPRM_ASCBranch branchOut)
        {
            BaseOutput baseOutput;
            try
            {
                branch = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, branch);
                branchOut = operationLogic.AddPRM_ASCBranch(branch);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                branchOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeletePRM_ASCBranch(BaseInput baseinput, tblPRM_ASCBranch branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, branch);
                operationLogic.DeletePRM_ASCBranch(branch);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetPRM_ASCBranches(BaseInput baseinput, out List<tblPRM_ASCBranch> PRM_ASCBranchList)
        {
            BaseOutput baseOutput;
            try
            {
                PRM_ASCBranchList = operationLogic.GetPRM_ASCBranches();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                PRM_ASCBranchList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetPRM_ASCBranchById(BaseInput baseinput, Int64 branchId, out tblPRM_ASCBranch branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = operationLogic.GetPRM_ASCBranchById(branchId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                branch = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPRM_ASCBranchByName(BaseInput baseinput, string branchName, out tblPRM_ASCBranch branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = operationLogic.GetPRM_ASCBranchByName(branchName);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                branch = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdatePRM_ASCBranch(BaseInput baseinput, tblPRM_ASCBranch branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, branch);
                operationLogic.UpdatePRM_ASCBranch(branch);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion
        #region tblPRM_KTNBranch
        public BaseOutput AddPRM_KTNBranch(BaseInput baseinput, tblPRM_KTNBranch branch, out tblPRM_KTNBranch branchOut)
        {
            BaseOutput baseOutput;
            try
            {
                branch = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, branch);
                branchOut = operationLogic.AddPRM_KTNBranch(branch);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                branchOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeletePRM_KTNBranch(BaseInput baseinput, tblPRM_KTNBranch branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, branch);
                operationLogic.DeletePRM_KTNBranch(branch);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetPRM_KTNBranches(BaseInput baseinput, out List<tblPRM_KTNBranch> PRM_KTNBranchList)
        {
            BaseOutput baseOutput;
            try
            {
                PRM_KTNBranchList = operationLogic.GetPRM_KTNBranches();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                PRM_KTNBranchList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetPRM_KTNBranchById(BaseInput baseinput, Int64 branchId, out tblPRM_KTNBranch branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = operationLogic.GetPRM_KTNBranchById(branchId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                branch = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPRM_KTNBranchByName(BaseInput baseinput, string branchName, out tblPRM_KTNBranch branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = operationLogic.GetPRM_KTNBranchByName(branchName);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                branch = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdatePRM_KTNBranch(BaseInput baseinput, tblPRM_KTNBranch branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, branch);
                operationLogic.UpdatePRM_KTNBranch(branch);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion
        #region tblBranchResponsibility
        public BaseOutput AddBranchResponsibility(BaseInput baseinput, tblBranchResponsibility branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, branch);
                operationLogic.AddBranchResponsibility(branch);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteBranchResponsibility(BaseInput baseinput, tblBranchResponsibility branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, branch);
                operationLogic.DeleteBranchResponsibility(branch);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetBranchResponsibilities(BaseInput baseinput, out List<tblBranchResponsibility> BranchResponsibilityList)
        {
            BaseOutput baseOutput;
            try
            {
                BranchResponsibilityList = operationLogic.GetBranchResponsibilities();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                BranchResponsibilityList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetBranchResponsibilityById(BaseInput baseinput, Int64 branchId, out tblBranchResponsibility branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = operationLogic.GetBranchResponsibilityById(branchId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                branch = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetBranchResponsibilityForBranch(BaseInput baseinput, tblBranchResponsibility item, out List<tblBranchResponsibility> branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = operationLogic.GetBranchResponsibilityForBranch(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                branch = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateBranchResponsibility(BaseInput baseinput, tblBranchResponsibility branch)
        {
            BaseOutput baseOutput;
            try
            {
                branch = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, branch);
                operationLogic.UpdateBranchResponsibility(branch);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion


        #region tblProductionCalendar
        public BaseOutput AddProductionCalendar(BaseInput baseinput, tblProductionCalendar ProductionCalendar, out tblProductionCalendar productionCalendarOut)
        {
            BaseOutput baseOutput;

            try
            {
                ProductionCalendar = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, ProductionCalendar);
                productionCalendarOut = operationLogic.AddProductionCalendar(ProductionCalendar);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                productionCalendarOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteProductionCalendar(BaseInput baseinput, tblProductionCalendar ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, ProductionCalendar);
                operationLogic.DeleteProductionCalendar(ProductionCalendar);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProductionCalendar(BaseInput baseInput, out List<tblProductionCalendar> ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = operationLogic.GetProductionCalendar();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendar = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProductionCalendarById(BaseInput baseinput, Int64 Id, out tblProductionCalendar ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = operationLogic.GetProductionCalendarById(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendar = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }


        public BaseOutput GetProductionCalendarPrice(BaseInput baseinput, decimal price, out tblProductionCalendar ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = operationLogic.GetProductionCalendarPrice(price);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendar = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetProductionCalendarQuantity(BaseInput baseinput, decimal quantity, out tblProductionCalendar ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = operationLogic.GetProductionCalendarQuantity(quantity);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendar = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetProductionCalendarBymonthseVId(BaseInput baseinput, Int64 months_eV_Id, out List<tblProductionCalendar> ProductionCalendarlList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendarlList = operationLogic.GetProductionCalendarBymonthseVId(months_eV_Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendarlList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetProductionCalendarDay(BaseInput baseinput, Int64 day, out List<tblProductionCalendar> ProductionCalendarlList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendarlList = operationLogic.GetProductionCalendarDay(day);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendarlList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetProductionCalendarOclock(BaseInput baseinput, Int64 oclock, out List<tblProductionCalendar> ProductionCalendarlList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendarlList = operationLogic.GetProductionCalendarOclock(oclock);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendarlList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetProductionCalendartransportationeVId2(BaseInput baseinput, Int64 type_eV_Id, Int64 year_id, out List<tblProductionCalendar> ProductionCalendarlList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendarlList = operationLogic.GetProductionCalendartransportationeVId2(type_eV_Id, year_id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendarlList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }


        public BaseOutput GetProductionCalendarpartOfytypeeVId(BaseInput baseinput, Int64 partOfyear_eV_Id, out List<tblProductionCalendar> ProductionCalendarlList)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendarlList = operationLogic.GetProductionCalendarpartOfytypeeVId(partOfyear_eV_Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendarlList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput UpdateProductionCalendar2(BaseInput baseinput, tblProductionCalendar ProductionCalendar, out tblProductionCalendar ProductionCalendarOut)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, ProductionCalendar);
                ProductionCalendarOut = operationLogic.UpdateProductionCalendar2(ProductionCalendar);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                ProductionCalendarOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        //public BaseOutput GetProductionCalendarOfferID(BaseInput baseinput, Int64 offer_Id, out List<tblProductionCalendar> ProductionCalendar)
        //{
        //    BaseOutput baseOutput;
        //    try
        //    {
        //        ProductionCalendar = operationLogic.GetProductionCalendarOfferID(offer_Id);
        //        return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        ProductionCalendar = null;
        //        return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

        //    }
        //}
        public BaseOutput GeProductionCalendarProductionId2(BaseInput baseinput, Int64 Production_Id, Int64 Production_type_eV_Id, out List<tblProductionCalendar> ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = operationLogic.GeProductionCalendarProductionId2(Production_Id,Production_type_eV_Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendar = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetProductionCalendarProductiontypeEVId2(BaseInput baseinput, Int64 production_type_eV_Id, out List<tblProductionCalendar> ProductionCalendar)
        {
            BaseOutput baseOutput;
            try
            {
                ProductionCalendar = operationLogic.GetProductionCalendarProductiontypeEVId2(production_type_eV_Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                ProductionCalendar = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        #endregion



        #region tblComMessageAttachment
        public BaseOutput AddComMessageAttachment(BaseInput baseinput, tblComMessageAttachment comMessageAttachment, out tblComMessageAttachment comMessageAttachmentOut)
        {
            BaseOutput baseOutput;

            try
            {
                comMessageAttachment = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, comMessageAttachment);
                comMessageAttachmentOut = operationLogic.AddComMessageAttachment(comMessageAttachment);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                comMessageAttachmentOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteComMessageAttachment(BaseInput baseinput, tblComMessageAttachment comMessageAttachment)
        {
            BaseOutput baseOutput;
            try
            {
                comMessageAttachment = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, comMessageAttachment);
                operationLogic.DeleteComMessageAttachment(comMessageAttachment);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetComMessageAttachment(BaseInput baseInput, out List<tblComMessageAttachment> comMessageAttachment)
        {
            BaseOutput baseOutput;
            try
            {
                comMessageAttachment = operationLogic.GetComMessageAttachment();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                comMessageAttachment = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetComMessageAttachmentById(BaseInput baseinput, Int64 Id, out tblComMessageAttachment comMessageAttachment)
        {
            BaseOutput baseOutput;
            try
            {
                comMessageAttachment = operationLogic.GetComMessageAttachmentById(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                comMessageAttachment = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }


       
        public BaseOutput UpdateComMessageAttachment(BaseInput baseinput, tblComMessageAttachment comMessageAttachment, out tblComMessageAttachment comMessageAttachmentOut)
        {
            BaseOutput baseOutput;
            try
            {
                comMessageAttachment = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, comMessageAttachment);
                comMessageAttachmentOut = operationLogic.UpdateComMessageAttachment(comMessageAttachment);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                comMessageAttachmentOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetByComMessageAttachmentId(BaseInput baseinput, Int64 Id, out List<tblComMessageAttachment> comMessageAttachment)
        {
            BaseOutput baseOutput;
            try
            {
                comMessageAttachment = operationLogic.GetByComMessageAttachmentId(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                comMessageAttachment = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        #endregion
        #region tblContract
        public BaseOutput AddContract(BaseInput baseinput, tblContract item, out tblContract contractOut)
        {
            BaseOutput baseOutput;

            try
            {
                item = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, item);
                contractOut = operationLogic.AddContract(item);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                contractOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput DeleteContract(BaseInput baseinput, tblContract contract)
        {
            BaseOutput baseOutput;
            try
            {
                contract = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, contract);
                operationLogic.DeleteContract(contract);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetContract(BaseInput baseInput, out List<tblContract> contractOut)
        {
            BaseOutput baseOutput;
            try
            {
                contractOut = operationLogic.GetContract();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                contractOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateContract(BaseInput baseinput, tblContract contract, out tblContract contractOut)
        {
            BaseOutput baseOutput;
            try
            {
                contract = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, contract);
                contractOut = operationLogic.UpdateContract(contract);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                contractOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetContractById(BaseInput baseinput, Int64 Id, out tblContract contract)
        {
            BaseOutput baseOutput;
            try
            {
                contract = operationLogic.GetContractById(Id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                contract = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetContractBySupplierOrganisationID(BaseInput baseinput, Int64 organisationID, out List<tblContract> contractOut)
        {
            BaseOutput baseOutput;
            try
            {
                contractOut = operationLogic.GetContractBySupplierOrganisationID(organisationID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                contractOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetContractBySupplierUserID(BaseInput baseinput, Int64 supplierUserID, out List<tblContract> contractOut)
        {
            BaseOutput baseOutput;
            try
            {
                contractOut = operationLogic.GetContractBySupplierUserID(supplierUserID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                contractOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetContractByAgentUserID(BaseInput baseinput, Int64 agentUserID, out List<tblContract> contractOut)
        {
            BaseOutput baseOutput;
            try
            {
                contractOut = operationLogic.GetContractBySupplierOrganisationID(agentUserID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                contractOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        #endregion

        public tblProductionCalendar[] GetProductionCalendar1(BaseInput baseInput)
        {
            try
            {
              return operationLogic.GetProductionCalendar().ToArray();
               // return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
              return  null;
               // return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #region Optimastion         
        public BaseOutput GetAnnouncementDetailsByProductId_OPC(BaseInput baseinput, Int64 productID, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = operationLogic.GetAnnouncementDetailsByProductId_OPC(productID);


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }

        public BaseOutput GetAnnouncementDetails_OP(BaseInput baseinput, int page, int pageSize, out List<AnnouncementDetail> itemList)
        {
            BaseOutput baseOutput;
            itemList = new List<AnnouncementDetail>();
            List<AnnouncementDetail> tblAnouncementlist = new List<AnnouncementDetail>();
            try
            {
                tblAnouncementlist = operationLogic.GetAnnouncementDetails_OP(page,pageSize);
                foreach (var obj in tblAnouncementlist)
                {
                    AnnouncementDetail item = new AnnouncementDetail();
                    item = obj;
                    try
                    {
                        item.parentName = sqloperationLogic.GetProducParentProductByProductID((long)obj.announcement.product_id).ProductName;

                    }
                    catch (Exception ex)
                    {

                    }


                    item.productCatalogDocumentList = operationLogic.GetProductDocumentsByProductCatalogId((long)obj.announcement.product_id);

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
        public BaseOutput GetAnnouncementDetails_OPC(BaseInput baseinput, out Int64 count)
        {
            BaseOutput baseOutput;
            count = 0;
            try
            {
                count = operationLogic.GetAnnouncementDetails_OPC();


                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                count = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        #endregion
        #region  tblPropertyDetail

        public BaseOutput AddPropertyDetail(BaseInput baseinput, tblPropertyDetail detail, out tblPropertyDetail detailOut)
        {
            BaseOutput baseOutput;

            // string ip = baseinput.IpNumber.GetIpOrEmpty();
            string ipNumber = baseinput.IpNumber.GetIpOrEmpty();
            string _requestID = baseinput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseinput.ChannelId);
            detailOut = null;
            try
            {

                detail = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, detail);
                detailOut = operationLogic.AddPropertyDetail(detail);


                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseinput) + IOUtil.GetObjValue(detail)), ipNumber, _requestID, _lineNumber, (ChannelEnum)channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                detailOut = null;
                ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);


                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
            finally
            {
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(detailOut), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }

        }

        public BaseOutput DeletePropertyDetail(BaseInput baseinput, tblPropertyDetail item)
        {
            BaseOutput baseOutput;

            try
            {
                item = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, item);
                operationLogic.DeletePropertyDetail(item);



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }

        }

        public BaseOutput GetPropertyDetails(BaseInput baseInput, out List<tblPropertyDetail> itemList)
        {
            BaseOutput baseOutput;

            try
            {
                itemList = operationLogic.GetPropertyDetails();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetPropertyDetailById(BaseInput baseinput, Int64 Id, out tblPropertyDetail Detail)
        {
            BaseOutput baseOutput;
            Detail = null;
            string ipNumber = baseinput.IpNumber.GetIpOrEmpty();
            string _requestID = baseinput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseinput.ChannelId);
            try
            {
                Detail = operationLogic.GetPropertyDetailById(Id);
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseinput) + IOUtil.GetObjValue(Detail)), ipNumber, _requestID, _lineNumber, (ChannelEnum)channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                Detail = null;
                ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
            finally
            {

                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(Detail), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }
        }

        public BaseOutput GetPropertyDetailArea(BaseInput baseInput, decimal area, out tblPropertyDetail detail)
        {
            BaseOutput baseOutput;
            detail = null;
            string ipNumber = baseInput.IpNumber.GetIpOrEmpty();
            string _requestID = baseInput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseInput.ChannelId);
            try
            {
                detail = operationLogic.GetPropertyDetailArea(area);
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseInput) + IOUtil.GetObjValue(detail)), ipNumber, _requestID, _lineNumber, (ChannelEnum)channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                detail = null;
                ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
            finally
            {
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(detail), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }

        }

        public BaseOutput UpdatePropertyDetail(BaseInput baseinput, tblPropertyDetail detail, out tblPropertyDetail detailOut)
        {
            BaseOutput baseOutput;
            detailOut = null;
            string ipNumber = baseinput.IpNumber.GetIpOrEmpty();
            string _requestID = baseinput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseinput.ChannelId);
            try
            {
                detail = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, detail);
                detailOut = operationLogic.UpdatePropertyDetail(detail);
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseinput) + IOUtil.GetObjValue(detail)), ipNumber, _requestID, _lineNumber, (ChannelEnum)channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                detailOut = null;
                ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
            finally
            {
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(detailOut), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }
        }

        public BaseOutput GetPropertyDetailByProperty_type_ID(BaseInput baseinput, Int64 Property_type_ID, out List<tblPropertyDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetPropertyDetailByProperty_type_ID(Property_type_ID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPropertyDetailByCapacity_measuriment_evID(BaseInput baseinput, Int64 Capacity_measuriment_evID, out List<tblPropertyDetail> itemList)
        {
            BaseOutput baseOutput;
            try
            {
                itemList = operationLogic.GetPropertyDetailByCapacity_measuriment_evID(Capacity_measuriment_evID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPropertyDetailCapacity(BaseInput baseinput, decimal capacity, out tblPropertyDetail item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetPropertyDetailCapacity(capacity);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        public BaseOutput GetPropertyDetailByAddressId(BaseInput baseinput, Int64 adsressID, out tblPropertyDetail item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetPropertyDetailByAddressId(adsressID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
        #endregion
        #region  tblPropertyType

        public BaseOutput AddPropertyTypes(BaseInput baseinput, tblPropertyType detail, out tblPropertyType detailOut)
        {
            BaseOutput baseOutput;

            // string ip = baseinput.IpNumber.GetIpOrEmpty();
            string ipNumber = baseinput.IpNumber.GetIpOrEmpty();
            string _requestID = baseinput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseinput.ChannelId);
            detailOut = null;
            try
            {

                detail = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Insert, detail);
                detailOut = operationLogic.AddPropertyTypes(detail);


                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseinput) + IOUtil.GetObjValue(detail)), ipNumber, _requestID, _lineNumber, (ChannelEnum)channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                detailOut = null;
                ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);


                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
            finally
            {
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(detailOut), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }

        }

        public BaseOutput DeletePropertyType(BaseInput baseinput, tblPropertyType item)
        {
            BaseOutput baseOutput;

            try
            {
                item = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Delete, item);
                operationLogic.DeletePropertyType(item);



                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }

        }

        public BaseOutput GetPropertyTypes(BaseInput baseInput, out List<tblPropertyType> itemList)
        {
            BaseOutput baseOutput;

            try
            {
                itemList = operationLogic.GetPropertyTypes();
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                itemList = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }

        }

        public BaseOutput GetPropertyTypeById(BaseInput baseinput, Int64 Id, out tblPropertyType Detail)
        {
            BaseOutput baseOutput;
            Detail = null;
            string ipNumber = baseinput.IpNumber.GetIpOrEmpty();
            string _requestID = baseinput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseinput.ChannelId);
            try
            {
                Detail = operationLogic.GetPropertyTypeById(Id);
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseinput) + IOUtil.GetObjValue(Detail)), ipNumber, _requestID, _lineNumber, (ChannelEnum)channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                Detail = null;
                ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
            finally
            {

                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(Detail), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }
        }

       

        public BaseOutput UpdatePropertyType(BaseInput baseinput, tblPropertyType detail, out tblPropertyType detailOut)
        {
            BaseOutput baseOutput;
            detailOut = null;
            string ipNumber = baseinput.IpNumber.GetIpOrEmpty();
            string _requestID = baseinput.TransactionId.GetTransactionId();
            Int64 channelId = Convert.ToInt64(baseinput.ChannelId);
            try
            {
                detail = AuditingLogic.SetAuditingInfo(baseinput, (int)CRUD.Update, detail);
                detailOut = operationLogic.UpdatePropertyType(detail);
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveInputInformation((IOUtil.GetObjValue(baseinput) + IOUtil.GetObjValue(detail)), ipNumber, _requestID, _lineNumber, (ChannelEnum)channelId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                detailOut = null;
                ExceptionHandlingOperation.HandleException(ex, ipNumber, _requestID, ChannelEnum.Emsal);
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
            finally
            {
                _lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber() + 1;
                ExceptionHandlingOperation.SaveOutputInformation(IOUtil.GetObjValue(detailOut), ipNumber, _requestID, _lineNumber, ChannelEnum.Emsal);
            }
        }

       
        public BaseOutput GetPropertyTypeByAddressId(BaseInput baseinput, Int64 adressId, out tblPropertyType item)
        {
            BaseOutput baseOutput;
            try
            {
                item = operationLogic.GetPropertyTypeByAddressId(adressId);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
            }
            catch (Exception ex)
            {
                item = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);

            }
        }
    
        #endregion


      
    }
}

