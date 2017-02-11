using Emsal.DAL.CustomObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Emsal.Utility.CustomObjects;


namespace Emsal.DAL
{
    public class OperationLogic
    {

        public void StartDb()
        {

            EmsalDBContext dbContext = new EmsalDBContext();
            EmsalDBInitializer dbInitializer = new EmsalDBInitializer();
            dbInitializer.InitializeDatabase(dbContext);
        }

        #region Person
        public tblPerson GetPersonByPinNumber(string pinNumber)
        {

            try
            {

                using (var context = new EmsalDBEntities())
                {


                    var person = (from p in context.tblPersons
                                  where p.PinNumber == pinNumber && p.Status == 1
                                  select p).FirstOrDefault();

                    return person;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblPerson AddPerson(tblPerson person)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblPersons.Add(person);
                    context.SaveChanges();
                    return person;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeletePerson(tblPerson item)
        {

            try
            {
                tblPerson oldItem;
                using (var context = new EmsalDBEntities())
                {

                    oldItem = (from p in context.tblPersons
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.LastUpdatedStatus = item.LastUpdatedStatus;
                        oldItem.updatedDate = item.updatedDate;
                        oldItem.updatedUser = item.updatedUser;
                        context.tblPersons.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblPerson> GetPersons()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var persons = (from p in context.tblPersons
                                   where p.Status == 1 && p.Status == 1
                                   select p);

                    return persons.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public tblPerson GetPersonById(Int64 Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var person = (from p in context.tblPersons
                                  where p.Id == Id && p.Status == 1
                                  select p).FirstOrDefault();

                    return person;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblPerson GetPersonByUserId(Int64 UserId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var person = (from p in context.tblPersons
                                  where p.UserId == UserId && p.Status == 1
                                  select p).FirstOrDefault();

                    return person;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblPerson UpdatePerson(tblPerson item)
        {
            try
            {
                tblPerson oldItem;
                using (var context = new EmsalDBEntities())
                {
                    oldItem = (from p in context.tblPersons
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        item.createdDate = oldItem.createdDate;
                        item.createdUser = oldItem.createdUser;
                        item.Status = oldItem.Status;
                        item.address_Id = oldItem.address_Id;
                        item.UserId = oldItem.UserId;
                        oldItem = item;

                        context.tblPersons.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        #region Enum Operation

        public tblEnumCategory AddEnumCategory(tblEnumCategory enumCategory)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblEnumCategories.Add(enumCategory);
                    context.SaveChanges();
                    return enumCategory;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeleteEnumCategory(tblEnumCategory enumCategory)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    var entry = context.Entry(enumCategory);
                    if (entry.State == EntityState.Detached)
                        context.tblEnumCategories.Attach(enumCategory);
                    context.tblEnumCategories.Remove(enumCategory);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblEnumCategory UpdateEnumCategory(tblEnumCategory enumCategory)
        {

            try
            {
                tblEnumCategory oldenumCategory;
                using (var context = new EmsalDBEntities())
                {





                    oldenumCategory = (from p in context.tblEnumCategories
                                       where p.Id == enumCategory.Id && p.Status == 1
                                       select p).FirstOrDefault();

                }

                if (oldenumCategory != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldenumCategory = enumCategory;

                        context.tblEnumCategories.Attach(oldenumCategory);
                        context.Entry(oldenumCategory).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldenumCategory;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblEnumCategory GetEnumCategoryById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblEnumCategories
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblEnumCategory> GetEnumCategorys()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblEnumCategories
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public tblEnumCategory GetEnumCategorysByName(string categoryName)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalog = (from p in context.tblEnumCategories
                                    where p.name == categoryName && p.Status == 1
                                    select p).FirstOrDefault();

                    return ecatalog;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblEnumCategory> GetEnumCategorysForProduct()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblEnumCategories
                                     where p.isProductDescibe == 1 && p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public tblEnumValue AddEnumValue(tblEnumValue enumValue)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    //    tblEnumCategory enumCategory = (from p in context
                    //                                    where p.enumCategoryId == enumValue.tblEnumCategory.enumCategoryId
                    //                                    select p).SingleOrDefault();
                    //    if (enumCategory != null)
                    //    {
                    //        enumValue.e = enumCategory;
                    //    }

                    context.tblEnumValues.Add(enumValue);
                    context.SaveChanges();
                    return enumValue;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeleteEnumValue(tblEnumValue item)
        {

            try
            {
                tblEnumValue oldItem;
                using (var context = new EmsalDBEntities())
                {





                    oldItem = (from p in context.tblEnumValues
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.LastUpdatedStatus = item.LastUpdatedStatus;
                        oldItem.updatedDate = item.updatedDate;
                        oldItem.updatedUser = item.updatedUser;
                        context.tblEnumValues.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblEnumValue UpdateEnumValue(tblEnumValue enumValue)
        {

            try
            {
                tblEnumValue oldItem;
                using (var context = new EmsalDBEntities())
                {





                    oldItem = (from p in context.tblEnumValues
                               where p.Id == enumValue.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem = enumValue;

                        context.tblEnumValues.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblEnumValue> GetEnumValues()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblEnumValues
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public tblEnumValue GetEnumValueById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblEnumValues
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblEnumValue> GetEnumValuesByEnumCategoryId(Int64 enumCategoryId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblEnumValues
                                     where p.enumCategory_enumCategoryId == enumCategoryId && p.Status == 1
                                     select p);

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        public List<tblEnumValue> GetEnumValuesForProduct()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    var enumValues = (from eC in context.tblEnumCategories
                                      join eV in context.tblEnumValues on eC.Id equals eV.enumCategory_enumCategoryId
                                      where eV.Status==1
                                      select eV);

                    return enumValues.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblEnumValue GetEnumValueByName(string name)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var enumValue = (from p in context.tblEnumValues
                                     where p.name == name && p.Status == 1
                                     select p).First();

                    return enumValue;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        #region PRMCountriesOperation
        public tblPRM_AdminUnit AddPRM_AdminUnit(tblPRM_AdminUnit PRM_adminUnit)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblPRM_AdminUnit.Add(PRM_adminUnit);
                    context.SaveChanges();
                    return PRM_adminUnit;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeletePRM_AdminUnit(tblPRM_AdminUnit PRM_AdminUnit)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var entry = context.Entry(PRM_AdminUnit);
                    if (entry.State == EntityState.Detached)
                        context.tblPRM_AdminUnit.Attach(PRM_AdminUnit);
                    context.tblPRM_AdminUnit.Remove(PRM_AdminUnit);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public tblPRM_AdminUnit UpdatePRM_AdminUnit(tblPRM_AdminUnit PRM_AdminUnit)
        {

            try
            {
                tblPRM_AdminUnit oldPRM_AdminUnit;
                using (var context = new EmsalDBEntities())
                {
                    oldPRM_AdminUnit = (from p in context.tblPRM_AdminUnit
                                        where p.Id == PRM_AdminUnit.Id && p.Status == 1
                                        select p).FirstOrDefault();

                }

                if (oldPRM_AdminUnit != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldPRM_AdminUnit = PRM_AdminUnit;

                        context.tblPRM_AdminUnit.Attach(oldPRM_AdminUnit);
                        context.Entry(oldPRM_AdminUnit).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldPRM_AdminUnit;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir record yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblPRM_AdminUnit GetPRM_AdminUnitById(Int64 ID)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var country = (from p in context.tblPRM_AdminUnit
                                   where p.Id == ID && p.Status == 1
                                   select p).First();
                    return country;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<tblPRM_AdminUnit> GetPRM_AdminUnits()
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var countries = (from p in context.tblPRM_AdminUnit
                                     where p.Status == 1
                                     select p);
                    return countries.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblPRM_AdminUnit> GetPRM_AdminUnitsByParentId(Int64 parentID)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var adminUnits = (from p in context.tblPRM_AdminUnit
                                      where p.ParentID == parentID && p.Status == 1
                                      select p);

                    return adminUnits.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tblPRM_AdminUnit GetPRM_AdminUnitByName(string adminUnitName)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var country = (from p in context.tblPRM_AdminUnit
                                   where p.Name == adminUnitName && p.Status == 1
                                   select p).First();
                    return country;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //bu işlemir
        public IQueryable<tblPRM_AdminUnit> GETPRM_AdminUnitsByChildId(tblPRM_AdminUnit AdminUnit)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    return from adminUnit in context.tblPRM_AdminUnit
                           join parent in context.tblPRM_AdminUnit on adminUnit.ParentID equals parent.Id
                           where parent.Id == AdminUnit.Id
                           select parent;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblPRM_AdminUnit GetPRM_AdminUnitByIamasId(Int64 iamasId,bool isCity)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var country = (from p in context.tblPRM_AdminUnit
                                   where p.iamasId == iamasId && p.Status == 1 && p.isCity==isCity
                                   select p).First();
                    return country;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region PRMThoroughfareOperation

        public tblPRM_Thoroughfare AddPRM_Thoroughfare(tblPRM_Thoroughfare thoroughfare)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblPRM_Thoroughfare.Add(thoroughfare);
                    context.SaveChanges();
                    return thoroughfare;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeletePRM_Thoroughfare(tblPRM_Thoroughfare thoroughfare)
        {
            try
            {
                tblPRM_Thoroughfare oldPRM_Thoroughfare;
                using (var context = new EmsalDBEntities())
                {


                    oldPRM_Thoroughfare = (from p in context.tblPRM_Thoroughfare
                                           where p.Id == thoroughfare.Id && p.Status == 1
                                           select p).FirstOrDefault();

                }

                if (oldPRM_Thoroughfare != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldPRM_Thoroughfare.Status = 0;

                        context.tblPRM_Thoroughfare.Attach(oldPRM_Thoroughfare);
                        context.Entry(oldPRM_Thoroughfare).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblPRM_Thoroughfare UpdatePRM_Thoroughfare(tblPRM_Thoroughfare thoroughfare)
        {

            try
            {
                tblPRM_Thoroughfare oldPRM_Thoroughfare;
                using (var context = new EmsalDBEntities())
                {
                    oldPRM_Thoroughfare = (from p in context.tblPRM_Thoroughfare
                                           where p.Id == thoroughfare.Id && p.Status == 1
                                           select p).FirstOrDefault();

                }

                if (oldPRM_Thoroughfare != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldPRM_Thoroughfare = thoroughfare;

                        context.tblPRM_Thoroughfare.Attach(oldPRM_Thoroughfare);
                        context.Entry(oldPRM_Thoroughfare).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldPRM_Thoroughfare;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir record yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblPRM_Thoroughfare GetPRM_ThoroughfareById(Int64 ID)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var Thoroughfare = (from p in context.tblPRM_Thoroughfare
                                        where p.Id == ID && p.Status == 1
                                        select p).First();
                    return Thoroughfare;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblPRM_Thoroughfare> GetPRM_Thoroughfares()
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var Thoroughfares = (from p in context.tblPRM_Thoroughfare
                                         where p.Status == 1
                                         select p);
                    return Thoroughfares.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tblPRM_Thoroughfare GetPRM_ThoroughfareByName(string ThoroughfareName)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var Thoroughfare = (from p in context.tblPRM_Thoroughfare
                                        where p.Name == ThoroughfareName && p.Status == 1
                                        select p).First();
                    return Thoroughfare;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblPRM_Thoroughfare> GetPRM_ThoroughfaresByAdminUnitId(Int64 adminUnitId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    var pcatalogList = (from p in context.tblPRM_Thoroughfare
                                        where p.AdminUnitID == adminUnitId && p.Status == 1
                                        select p);

                    return pcatalogList.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        #region Addresses Operation

        public tblAddress AddAddress(tblAddress address)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblAddresses.Add(address);
                    context.SaveChanges();
                    return address;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAddress(tblAddress address)
        {
            try
            {
                tblAddress oldAddress;
                using (var context = new EmsalDBEntities())
                {
                    oldAddress = (from p in context.tblAddresses
                                  where p.Id == address.Id && p.Status == 1
                                  select p).FirstOrDefault();
                }

                if (oldAddress != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldAddress.Status = 0;

                        context.tblAddresses.Attach(oldAddress);
                        context.Entry(oldAddress).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblAddress UpdateAddress(tblAddress address)
        {
            try
            {
                tblAddress oldAddress;
                using (var context = new EmsalDBEntities())
                {
                    oldAddress = (from p in context.tblAddresses
                                  where p.Id == address.Id && p.Status == 1
                                  select p).FirstOrDefault();

                }

                if (oldAddress != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldAddress = address;

                        context.tblAddresses.Attach(oldAddress);
                        context.Entry(oldAddress).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldAddress;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir record yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblAddress> GetAddresses()
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var addresses = (from p in context.tblAddresses
                                     where p.Status == 1
                                     select p);
                    return addresses.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tblAddress GetAddressById(Int64 Id)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var address = (from p in context.tblAddresses
                                   where p.Id == Id && p.Status == 1
                                   select p).First();

                    return address;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblAddress> GetAddressesByCountryId(Int64 countryId)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var addresses = (from p in context.tblAddresses
                                     where p.adminUnit_Id == countryId && p.Status == 1
                                     select p);
                    return addresses.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblAddress> GetAddressesByVillageId(Int64 villageId)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var addresses = (from p in context.tblAddresses
                                     where p.adminUnit_Id == villageId && p.Status == 1
                                     select p);
                    return addresses.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblAddress> GetAddressesByUserId(Int64 userID)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var addresses = (from p in context.tblAddresses
                                     where p.user_Id == userID && p.Status == 1
                                     select p);
                    return addresses.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region ProductAddresses(offerADDRESSes) Operation

        public tblProductAddress AddProductAddress(tblProductAddress address)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblProductAddresses.Add(address);
                    context.SaveChanges();
                    return address;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteProductAddress(tblProductAddress address)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var entry = context.Entry(address);
                    if (entry.State == EntityState.Detached)
                        context.tblProductAddresses.Attach(address);
                    context.tblProductAddresses.Remove(address);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblProductAddress UpdateProductAddress(tblProductAddress address)
        {
            try
            {
                tblProductAddress oldAddress;
                using (var context = new EmsalDBEntities())
                {
                    oldAddress = (from p in context.tblProductAddresses
                                  where p.Id == address.Id && p.Status == 1
                                  select p).FirstOrDefault();

                }

                if (oldAddress != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldAddress = address;

                        context.tblProductAddresses.Attach(oldAddress);
                        context.Entry(oldAddress).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldAddress;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir record yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblProductAddress> GetProductAddresses()
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerAddresses = (from p in context.tblProductAddresses
                                          where p.Status == 1
                                          select p);
                    return offerAddresses.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tblProductAddress GetProductAddressById(Int64 offerId)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var address = (from p in context.tblProductAddresses
                                   where p.Id == offerId && p.Status == 1
                                   select p).First();

                    return address;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblProductAddress> GetProductAddressesByAdminID(Int64 id)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var addresses = (from p in context.tblProductAddresses
                                     where p.adminUnit_Id == id && p.Status == 1
                                     select p);
                    return addresses.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region ProductCatalogOperation
        public tblProductCatalog AddProductCatalog(tblProductCatalog productCatalog)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblProductCatalogs.Add(productCatalog);
                    context.SaveChanges();
                    return productCatalog;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteProductCatalog(tblProductCatalog productCatalog)
        {

            try
            {
                tblProductCatalog oldProduct;
                using (var context = new EmsalDBEntities())
                {





                    oldProduct = (from p in context.tblProductCatalogs
                                  where p.Id == productCatalog.Id && p.Status == 1
                                  select p).FirstOrDefault();

                }

                if (oldProduct != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldProduct = productCatalog;
                        oldProduct.Status = 0;
                        context.tblProductCatalogs.Attach(oldProduct);
                        context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProductCatalog> GetProductCatalogs()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblProductCatalogs
                              
                                     where p.Status == 1
                                     select p).Distinct();

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tblProductCatalog> GetProductCatalogsOffer()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblProductCatalogs
                                     join ev in context.tblOffer_Production on p.Id equals ev.product_Id
                                     where p.Status == 1 && ev.Status == 1
                                     select p).Distinct();

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tblProductCatalog> GetProductCatalogsDemand()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblProductCatalogs
                                     join ev in context.tblDemand_Production on p.Id equals ev.product_Id
                                     where p.Status == 1 && ev.Status == 1
                                     select p).Distinct();

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tblProductCatalog> GetRootProductCatalogs()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblProductCatalogs
                                     where p.ProductCatalogParentID == 0 && p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProductCatalog> GetProductCatalogsByParentId(int parentID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblProductCatalogs
                                     where p.ProductCatalogParentID == parentID && p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblProductCatalog GetProductCatalogsById(int productID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalog = (from p in context.tblProductCatalogs
                                 
                                    where p.Id == productID && p.Status == 1
                                    select 
                                    
                                    p).FirstOrDefault();

                    return pcatalog;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblProductCatalog UpdateProductCatalog(tblProductCatalog product)
        {

            try
            {
                tblProductCatalog oldProduct;
                using (var context = new EmsalDBEntities())
                {
                    oldProduct = (from p in context.tblProductCatalogs
                                  where p.Id == product.Id && p.Status == 1
                                  select p).FirstOrDefault();

                }

                if (oldProduct != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldProduct = product;

                        context.tblProductCatalogs.Attach(oldProduct);
                        context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldProduct;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion


        #region PhoneConfirmationOperation
        //public bool SendConfirmationMessage(tblConfirmationMessage message)
        //{
        //    try
        //    {
        //        using (var context = new EmsalDBContext())
        //        {
        //            //tesdiq mesajını silirik
        //            context.Database.ExecuteSqlCommand("Delete from tblConfirmationMessage");

        //            context.tblConfirmationMessages.Add(message);

        //            context.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}

        public List<tblConfirmationMessage> GetConfirmationMessages()
        {
            try
            {
                using (var context = new EmsalDBContext())
                {


                    var messages = (from mes in context.tblConfirmationMessages
                                    select mes);

                    return messages.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public tblConfirmationMessage SendConfirmationMessageNew(tblConfirmationMessage message)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblConfirmationMessages.Add(message);

                    context.SaveChanges();
                    return message;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion


        #region tblCommunication

        public tblCommunication AddCommunication(tblCommunication communication)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblCommunications.Add(communication);
                    context.SaveChanges();
                    return communication;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeleteCommunication(tblCommunication communication)
        {

            try
            {
                tblCommunication oldCommunication;
                using (var context = new EmsalDBEntities())
                {


                    oldCommunication = (from p in context.tblCommunications
                                        where p.Id == communication.Id && p.Status == 1
                                        select p).FirstOrDefault();

                }

                if (oldCommunication != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldCommunication.Status = 0;

                        context.tblCommunications.Attach(oldCommunication);
                        context.Entry(oldCommunication).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblCommunication UpdateCommunication(tblCommunication communication)
        {

            try
            {
                tblCommunication oldCommunication;
                using (var context = new EmsalDBEntities())
                {





                    oldCommunication = (from p in context.tblCommunications
                                        where p.Id == communication.Id && p.Status == 1
                                        select p).FirstOrDefault();

                }

                if (oldCommunication != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldCommunication = communication;

                        context.tblCommunications.Attach(oldCommunication);
                        context.Entry(oldCommunication).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldCommunication;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblCommunication GetCommunicationById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblCommunications
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblCommunication> GetCommunications()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblCommunications
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<tblCommunication> GetCommunicationByPersonId(Int64 personId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var ecatalogs = (from p in context.tblCommunications
                                     where p.PersonId == personId && p.Status == 1
                                     select p);

                    return ecatalogs.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region tblOffer_Production
        public tblOffer_Production AddOffer_Production(tblOffer_Production offer_Production)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblOffer_Production.Add(offer_Production);
                    context.SaveChanges();
                    return offer_Production;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeleteOffer_Production(tblOffer_Production offer_Production)
        {

            try
            {
                tblOffer_Production oldOffer_Production;
                using (var context = new EmsalDBEntities())
                {


                    oldOffer_Production = (from p in context.tblOffer_Production
                                           where p.Id == offer_Production.Id && p.Status == 1
                                           select p).FirstOrDefault();

                }

                if (oldOffer_Production != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldOffer_Production.Status = 0;

                        context.tblOffer_Production.Attach(oldOffer_Production);
                        context.Entry(oldOffer_Production).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblOffer_Production UpdateOffer_Production(tblOffer_Production offer_Production)
        {

            try
            {
                tblOffer_Production oldOffer_Production;
                using (var context = new EmsalDBEntities())
                {





                    oldOffer_Production = (from p in context.tblOffer_Production
                                           where p.Id == offer_Production.Id && p.Status == 1
                                           select p).FirstOrDefault();

                }

                if (oldOffer_Production != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldOffer_Production = offer_Production;

                        context.tblOffer_Production.Attach(oldOffer_Production);
                        context.Entry(oldOffer_Production).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldOffer_Production;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public Int64 GetOffer_ProductionByProductIdandStateEVId(Int64 productId, Int64 state_Ev_Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblOffer_Production
                                     where p.product_Id == productId && p.Status == 1 && p.state_eV_Id == state_Ev_Id
                                     select p).Count();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblOffer_Production GetOffer_ProductionById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblOffer_Production
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblOffer_Production> GetOffer_Productions()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblOffer_Production
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<tblOffer_Production> GetOffer_ProductionsByUserId(Int64 UserId)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            join ev in context.tblEnumValues on p.state_eV_Id equals ev.Id
                                            where p.user_Id == UserId && p.Status == 1 && ev.Status==1 
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<tblOffer_Production> GetOffer_ProductionsByUserId1(Int64 UserId)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            join ev in context.tblEnumValues on p.state_eV_Id equals ev.Id
                                            where p.user_Id == UserId && p.Status == 1  && ev.Status == 1 && ev.Id != 41 && ev.Id != 1
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblOffer_Production> GetOffAirOffer_ProductionsByUserId(tblOffer_Production Offer)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            where p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id && p.Status == 1
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblOffer_Production> GetOnAirOffer_ProductionsByUserId(tblOffer_Production Offer)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    if (Offer.Status == 2 )
                    {
                        var offerProductions = (from p in context.tblOffer_Production
                                                where (p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id) || (p.monitoring_eV_Id == Offer.monitoring_eV_Id) && p.Status == 1
                                                select p);
                        return offerProductions.ToList();
                    }
                    else
                    {
                        var offerProductions = (from p in context.tblOffer_Production
                                                where (p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id) && (p.monitoring_eV_Id != Offer.monitoring_eV_Id) && p.Status == 1
                                                select p);
                        return offerProductions.ToList();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public List<tblOffer_Production> GetOnAirOfferCount_ProductionsByUserId(tblOffer_Production Offer)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    if (Offer.Status == 2)
                    {
                        var offerProductions = (from p in context.tblOffer_Production
                                                where (p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id) || (p.monitoring_eV_Id == Offer.monitoring_eV_Id) && p.Status == 1 && p.isNew == 1

                                                select p).ToList();
                        return offerProductions;
                    }
                    else
                    {
                        var offerProductions = (from p in context.tblOffer_Production
                                                where (p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id) && (p.monitoring_eV_Id != Offer.monitoring_eV_Id) && p.Status == 1 && p.isNew == 1
                                                select p).ToList();
                        return offerProductions;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    

        public List<tblOffer_Production> GetOnAirOffer_ProductionsByUserIDSortedForDate(tblOffer_Production Offer)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            where p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id && p.Status == 1
                                            orderby p.endDate
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public List<tblOffer_Production> GetOnAirOffer_ProductionsByUserIDSortedForDateDes(tblOffer_Production Offer)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            where p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id && p.Status == 1
                                            orderby p.endDate descending
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblOffer_Production> GetOnAirOffer_ProductionsByUserIDSortedForPriceAsc(tblOffer_Production Offer)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            where p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id && p.Status == 1
                                            orderby (p.unit_price * p.quantity)
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblOffer_Production> GetOnAirOffer_ProductionsByUserIDSortedForPriceDes(tblOffer_Production Offer)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            where p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id && p.Status == 1
                                            orderby (p.unit_price * p.quantity) descending
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblOffer_Production> GetOffAirOffer_ProductionsByUserIDSortedForDate(tblOffer_Production Offer)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            where p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id && p.Status == 1
                                            orderby p.endDate
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblOffer_Production> GetOffAirOffer_ProductionsByUserIDSortedForDateDes(tblOffer_Production Offer)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            where p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id && p.Status == 1
                                            orderby p.endDate descending
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblOffer_Production> GetOffAirOffer_ProductionsByUserIDSortedForPriceAsc(tblOffer_Production Offer)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            where p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id && p.Status == 1
                                            orderby (p.unit_price * p.quantity)
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblOffer_Production> GetOffAirOffer_ProductionsByUserIDSortedForPriceDes(tblOffer_Production Offer)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            where p.user_Id == Offer.user_Id && p.state_eV_Id == Offer.state_eV_Id && p.Status == 1
                                            orderby (p.unit_price * p.quantity) descending
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //dila
        public List<tblOffer_Production> GetOffer_ProductionsByContractId(Int64 contractId)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var offerProductions = (from p in context.tblOffer_Production
                                            where p.contractId == contractId && p.Status == 1
                                            select p);

                    return offerProductions.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        #region tblPotential_Production
        public tblPotential_Production AddPotential_Production(tblPotential_Production potential_Production)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblPotential_Production.Add(potential_Production);
                    context.SaveChanges();
                    return potential_Production;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeletePotential_Production(tblPotential_Production potential_Production)
        {

            try
            {
                tblPotential_Production oldPotential_Production;
                using (var context = new EmsalDBEntities())
                {


                    oldPotential_Production = (from p in context.tblPotential_Production
                                               where p.Id == potential_Production.Id && p.Status == 1
                                               select p).FirstOrDefault();

                }

                if (oldPotential_Production != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldPotential_Production.Status = 0;

                        context.tblPotential_Production.Attach(oldPotential_Production);
                        context.Entry(oldPotential_Production).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblPotential_Production UpdatePotential_Production(tblPotential_Production potential_Production)
        {

            try
            {
                tblPotential_Production oldPotential_Production;
                using (var context = new EmsalDBEntities())
                {





                    oldPotential_Production = (from p in context.tblPotential_Production
                                               where p.Id == potential_Production.Id && p.Status == 1
                                               select p).FirstOrDefault();

                }

                if (oldPotential_Production != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldPotential_Production = potential_Production;

                        context.tblPotential_Production.Attach(oldPotential_Production);
                        context.Entry(oldPotential_Production).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldPotential_Production;

                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblPotential_Production GetPotential_ProductionById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblPotential_Production
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblPotential_Production> GetPotential_Productions()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblPotential_Production
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tblPotential_Production> UpdatePotential_ProductionForUserID(tblPotential_Production item)
        {

            try
            {
                List<tblPotential_Production> oldItemListList = new List<tblPotential_Production>();
                List<tblPotential_Production> newItemListList = new List<tblPotential_Production>();

                tblPotential_Production oldItem;
                using (var context = new EmsalDBEntities())
                {





                    oldItemListList = (from p in context.tblPotential_Production
                                       where p.user_Id == item.user_Id
                                                     && p.isSelected == true && p.Status == 1
                                       select p).ToList();

                }
                foreach (tblPotential_Production obj in oldItemListList)
                {
                    if (obj != null)
                    {
                        using (var context = new EmsalDBEntities())
                        {
                            obj.isSelected = false;
                            obj.grup_Id = item.grup_Id;
                            obj.state_eV_Id = item.state_eV_Id;
                            context.tblPotential_Production.Attach(obj);
                            context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            newItemListList.Add(obj);


                        }
                    }


                }
                return newItemListList;

            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblPotential_Production> GetConfirmedPotential_ProductionsByUserID(tblPotential_Production Production)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var ecatalogs = (from p in context.tblPotential_Production
                                     where p.user_Id == Production.user_Id && p.state_eV_Id == Production.state_eV_Id && p.Status == 1
                                     select p);

                    return ecatalogs.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblPotential_Production> GetPotential_ProductionsByUserID(Int64 UserId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var ecatalogs = (from p in context.tblPotential_Production
                                     where p.user_Id == UserId && p.Status == 1
                                     select p);

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblPotential_Production> GetConfirmedPotential_ProductionsByStateAndUserId(tblPotential_Production Production)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var ecatalogs = (from p in context.tblPotential_Production
                                     where p.user_Id == Production.user_Id && p.state_eV_Id == Production.state_eV_Id && p.Status == 1
                                     select p);

                    return ecatalogs.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblPotential_Production> GetConfirmedPotential_ProductionsByStateAndUserIdForPriceDes(tblPotential_Production Production)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var ecatalogs = (from p in context.tblPotential_Production
                                     where p.user_Id == Production.user_Id && p.state_eV_Id == Production.state_eV_Id && p.Status == 1
                                     orderby (p.unit_price * p.quantity) descending

                                     select p);

                    return ecatalogs.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblPotential_Production> GetConfirmedPotential_ProductionsByStateAndUserIdForPriceAsc(tblPotential_Production Production)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var ecatalogs = (from p in context.tblPotential_Production
                                     where p.user_Id == Production.user_Id && p.state_eV_Id == Production.state_eV_Id && p.Status == 1
                                     orderby (p.unit_price * p.quantity)

                                     select p);

                    return ecatalogs.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion
        #region tblDemand_Production
        public List<tblDemand_Production> GetOnAirDemand_ProductionsByUserId(tblDemand_Production demand)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    if (demand.Status == 2)
                    {
                        var demandProductions = (from p in context.tblDemand_Production
                                                 where (p.user_Id == demand.user_Id && p.state_eV_Id == demand.state_eV_Id) || (p.monitoring_eV_Id == demand.monitoring_eV_Id) && p.Status == 1 && p.isNew == 1

                                                 select p).ToList();
                        return demandProductions;
                    }
                    else
                    {
                        var demandProductions = (from p in context.tblDemand_Production
                                                 where (p.user_Id == demand.user_Id && p.state_eV_Id == demand.state_eV_Id) && (p.monitoring_eV_Id != demand.monitoring_eV_Id) && p.Status == 1 && p.isNew == 1
                                                 select p).ToList();
                        return demandProductions;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tblDemand_Production AddDemand_Production(tblDemand_Production demand_Production)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblDemand_Production.Add(demand_Production);
                    context.SaveChanges();
                    return demand_Production;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteDemand_Production(tblDemand_Production demand_Productionn)
        {

            try
            {
                tblDemand_Production oldDemand_Production;
                using (var context = new EmsalDBEntities())
                {


                    oldDemand_Production = (from p in context.tblDemand_Production
                                            where p.Id == demand_Productionn.Id && p.Status == 1
                                            select p).FirstOrDefault();

                }

                if (oldDemand_Production != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldDemand_Production.Status = 0;

                        context.tblDemand_Production.Attach(oldDemand_Production);
                        context.Entry(oldDemand_Production).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblDemand_Production UpdateDemand_Production(tblDemand_Production demand_Production)
        {

            try
            {
                tblDemand_Production oldDemand_Production;
                using (var context = new EmsalDBEntities())
                {





                    oldDemand_Production = (from p in context.tblDemand_Production
                                            where p.Id == demand_Production.Id && p.Status == 1
                                            select p).FirstOrDefault();

                }

                if (oldDemand_Production != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldDemand_Production = demand_Production;

                        context.tblDemand_Production.Attach(oldDemand_Production);
                        context.Entry(oldDemand_Production).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldDemand_Production;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblDemand_Production> GetOnAirDemandCount_ProductionsByUserId(tblDemand_Production demand)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    if (demand.Status == 2)
                    {
                        var demandProductions = (from p in context.tblDemand_Production
                                                where (p.user_Id == demand.user_Id && p.state_eV_Id == demand.state_eV_Id) || (p.monitoring_eV_Id == demand.monitoring_eV_Id) && p.Status == 1 && p.isNew == 1

                                                select p).ToList();
                        return demandProductions;
                    }
                    else
                    {
                        var demandProductions = (from p in context.tblDemand_Production
                                                where (p.user_Id == demand.user_Id && p.state_eV_Id == demand.state_eV_Id) && (p.monitoring_eV_Id != demand.monitoring_eV_Id) && p.Status == 1 && p.isNew == 1
                                                select p).ToList();
                        return demandProductions;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tblDemand_Production GetDemand_ProductionById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblDemand_Production
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblDemand_Production> GetDemand_Productions()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblDemand_Production
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tblDemand_Production> UpdateDemand_ProductionForUserID(tblDemand_Production item)
        {

            try
            {
                List<tblDemand_Production> oldItemListList = new List<tblDemand_Production>();
                List<tblDemand_Production> newItemListList = new List<tblDemand_Production>();

                tblDemand_Production oldItem;
                using (var context = new EmsalDBEntities())
                {

                    oldItemListList = (from p in context.tblDemand_Production
                                       where p.user_Id == item.user_Id
                                                     && p.isSelected == true && p.Status == 1
                                       select p).ToList();

                }
                foreach (tblDemand_Production obj in oldItemListList)
                {
                    if (obj != null)
                    {
                        using (var context = new EmsalDBEntities())
                        {
                            obj.isSelected = false;
                            obj.grup_Id = item.grup_Id;
                            obj.state_eV_Id = item.state_eV_Id;

                            context.tblDemand_Production.Attach(obj);
                            context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            newItemListList.Add(obj);


                        }
                    }


                }
                return newItemListList;

            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblDemand_Production> UpdateDemand_ProductionForStartAndEndDate(tblDemand_Production item)
        {

            try
            {
                List<tblDemand_Production> oldItemListList = new List<tblDemand_Production>();
                List<tblDemand_Production> newItemListList = new List<tblDemand_Production>();

                using (var context = new EmsalDBEntities())
                {

                    oldItemListList = (from p in context.tblDemand_Production
                                       where p.Status == 1 && p.createdDate >= item.startDate && p.createdDate <= item.endDate && p.isSelected == false

                                       select p).ToList();

                }
                foreach (tblDemand_Production obj in oldItemListList)
                {
                    if (obj != null)
                    {
                        using (var context = new EmsalDBEntities())
                        {
                            obj.isAnnouncement = true;
                            obj.updatedDate = item.updatedDate;
                            obj.updatedUser = item.updatedUser;
                            context.tblDemand_Production.Attach(obj);
                            context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            newItemListList.Add(obj);


                        }
                    }


                }
                return newItemListList;

            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblDemand_Production> GetDemand_ProductionsByStateAndUserID(tblDemand_Production item)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var ecatalogs = (from p in context.tblDemand_Production
                                     where p.user_Id == item.user_Id && p.Status == 1 && p.state_eV_Id == item.state_eV_Id
                                     select p);

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblDemand_Production> GetDemandProductionForUserId(Int64 userID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblDemand_Production
                                     where p.user_Id == userID && p.Status==1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
        #region tblAnnouncement
        public tblAnnouncement AddAnnouncement(tblAnnouncement announcement)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblAnnouncements.Add(announcement);
                    context.SaveChanges();
                    return announcement;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteAnnouncement(tblAnnouncement announcement)
        {

            try
            {
                tblAnnouncement oldAnnouncement;
                using (var context = new EmsalDBEntities())
                {


                    oldAnnouncement = (from p in context.tblAnnouncements
                                       where p.Id == announcement.Id && p.Status == 1
                                       select p).FirstOrDefault();

                }

                if (oldAnnouncement != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldAnnouncement.Status = 0;

                        context.tblAnnouncements.Attach(oldAnnouncement);
                        context.Entry(oldAnnouncement).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblAnnouncement UpdateAnnouncement(tblAnnouncement announcement)
        {

            try
            {
                tblAnnouncement oldAnnouncement;
                using (var context = new EmsalDBEntities())
                {





                    oldAnnouncement = (from p in context.tblAnnouncements
                                       where p.Id == announcement.Id && p.Status == 1
                                       select p).FirstOrDefault();

                }

                if (oldAnnouncement != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldAnnouncement = announcement;

                        context.tblAnnouncements.Attach(oldAnnouncement);
                        context.Entry(oldAnnouncement).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldAnnouncement;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblAnnouncement UpdateAnnouncementPrice(tblAnnouncement announcement)
        {

            try
            {
                tblAnnouncement oldAnnouncement;
                using (var context = new EmsalDBEntities())
                {





                    oldAnnouncement = (from p in context.tblAnnouncements
                                       where p.Id == announcement.Id && p.Status == 1
                                       select p).FirstOrDefault();

                }

                if (oldAnnouncement != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldAnnouncement.unit_price = announcement.unit_price;

                        context.tblAnnouncements.Attach(oldAnnouncement);
                        context.Entry(oldAnnouncement).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldAnnouncement;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblAnnouncement GetAnnouncementById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblAnnouncements
                                     where p.Id == ID && p.Status == 1
                                     select p).FirstOrDefault();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblAnnouncement> GetAnnouncements()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblAnnouncements

                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tblAnnouncement> GetAnnouncementsByProductId(Int64 productId)
        {
            Int64 curentDate = DateTime.Now.Ticks;
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblAnnouncements
                                     where p.product_id == productId && p.Status == 1
                                        && p.startDate <= curentDate && p.endDate <= curentDate
                                     select p);

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblAnnouncement> GetAnnouncementsByYearAndPartOfYear(string year, string month)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblAnnouncements
                                     where p.createdDate.Value.ToString().Substring(0, 4) == year && p.Status == 1
                                     && p.createdDate.Value.ToString().Substring(4, 2) == month
                                     select p);

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblAnnouncement GetAnnouncementDetailsById(Int64 ID)
        {
            Int64 curentDate = DateTime.Now.Ticks;
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblAnnouncements
                                     where p.Id == ID && p.Status == 1 
                                     && p.startDate <= curentDate && p.endDate <= curentDate
                                     select p).FirstOrDefault();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        public List<AnnouncementDetail> GetAnnouncementDetailsByProductId(Int64 productId)
        {
            Int64 curentDate = DateTime.Now.getInt64ShortDate();

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblAnnouncements
                                     join pc in context.tblProductCatalogs on p.product_id equals pc.Id
                                     join ev  in context.tblEnumValues on p.quantity_type_Name equals ev.name into sr
                                     from x in sr.DefaultIfEmpty()
                                     where p.product_id == productId && p.Status == 1 
                                    // && p.startDate <= curentDate 
                                     &&
                                     p.endDate >= curentDate
                                     select
                                    new AnnouncementDetail
                                    {
                                       
                                        announcement = p,
                                        description=x.description,
                                        productName=pc.ProductName,
                       
                 

                                    });

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

       

        public List<AnnouncementDetail> GetAnnouncementDetailsOld()
        {
            Int64 curentDate = DateTime.Now.getInt64ShortDate();
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblAnnouncements
                                     join pc in context.tblProductCatalogs on p.product_id equals pc.Id

                                     where p.Status == 1 && p.startDate <= curentDate && p.endDate >= curentDate

                                     select new AnnouncementDetail
                                     {
                                         announcement = p,



                                     }).ToList();

                    return pcatalogs;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<AnnouncementDetail> GetAnnouncementDetails()
        {
            Int64 curentDate = DateTime.Now.getInt64ShortDate();
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblAnnouncements
                                     join pc in context.tblProductCatalogs on p.product_id equals pc.Id
                              join ev in context.tblEnumValues on p.quantity_type_Name equals ev.name 
                               into sr
                                     from x in sr.DefaultIfEmpty()
                                     where p.Status == 1 && //p.startDate <= curentDate &&
                                     p.endDate >= curentDate
                                   

                                     select new AnnouncementDetail
                                     {
                                         announcement = p,
                                         productName=pc.ProductName,
                                         description=x.description


                                     }).ToList();

                    return pcatalogs;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Int64 GetAnnouncementDetailsCount()
        {
            Int64 curentDate = DateTime.Now.getInt64ShortDate();
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblAnnouncements
                                     join pc in context.tblProductCatalogs on p.product_id equals pc.Id
                                     join ev in context.tblEnumValues on p.quantity_type_Name equals ev.name
                                      into sr
                                     from x in sr.DefaultIfEmpty()
                                     where p.Status == 1 && p.startDate <= curentDate && p.endDate >= curentDate
                                     && x.Status==1

                                     select new AnnouncementDetail
                                     {
                                         announcement = p,
                                         productName = pc.ProductName,
                                         description = x.description


                                     }).Count();

                    return pcatalogs;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
     
        #endregion
        #region tblEmployee
        public tblEmployee AddEmployee(tblEmployee employee)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblEmployees.Add(employee);
                    context.SaveChanges();
                    return employee;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteEmployee(tblEmployee employee)
        {

            try
            {
                tblEmployee oldEmployee;
                using (var context = new EmsalDBEntities())
                {


                    oldEmployee = (from p in context.tblEmployees
                                   where p.Id == employee.Id && p.Status == 1
                                   select p).FirstOrDefault();

                }

                if (oldEmployee != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldEmployee.Status = 0;

                        context.tblEmployees.Attach(oldEmployee);
                        context.Entry(oldEmployee).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblEmployee UpdateEmployee(tblEmployee employee)
        {
            try
            {
                tblEmployee oldEmployee;
                using (var context = new EmsalDBEntities())
                {

                    oldEmployee = (from p in context.tblEmployees
                                   where p.Id == employee.Id && p.Status == 1
                                   select p).FirstOrDefault();
                }

                if (oldEmployee != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldEmployee = employee;

                        context.tblEmployees.Attach(oldEmployee);
                        context.Entry(oldEmployee).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldEmployee;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblEmployee GetEmployeeById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblEmployees
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblEmployee> GetEmployees()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblEmployees
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region tblUser
        public tblUser AddUser(tblUser user)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblUsers.Add(user);
                    context.SaveChanges();
                    return user;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteUser(tblUser user)
        {

            try
            {
                tblUser oldUser;
                using (var context = new EmsalDBEntities())
                {


                    oldUser = (from p in context.tblUsers
                               where p.Id == user.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldUser != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldUser.Status = 0;

                        context.tblUsers.Attach(oldUser);
                        context.Entry(oldUser).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblUser UpdateUser(tblUser user)
        {
            try
            {
                tblUser oldUser;
                using (var context = new EmsalDBEntities())
                {
                    oldUser = (from p in context.tblUsers
                               where p.Id == user.Id && p.Status == 1
                               select p).FirstOrDefault();
                }

                if (oldUser != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        user.Status = oldUser.Status;
                        if (user.Password == null)
                        {
                            user.Password = oldUser.Password;
                        }
                        oldUser = user;

                        context.tblUsers.Attach(oldUser);
                        context.Entry(oldUser).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldUser;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblUser GetUserById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var ecatalogs = (from p in context.tblUsers
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tblUser GetUserByUserEmail(string email)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var ecatalogs = (from p in context.tblUsers
                                     where p.Email == email && p.Status == 1
                                     select p).FirstOrDefault();

                    return ecatalogs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tblUser> GetUsers()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblUsers
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tblUser GetUserByUserName(string UserName)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblUsers
                                     where p.Username == UserName && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblUser> GetUsersByUserType(long userTypeID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblUsers
                                     where p.userType_eV_ID == userTypeID && p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
        #region tblRole
        public tblRole AddRole(tblRole role)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblRoles.Add(role);
                    context.SaveChanges();
                    return role;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteRole(tblRole role)
        {

            try
            {
                tblRole oldRole;
                using (var context = new EmsalDBEntities())
                {


                    oldRole = (from p in context.tblRoles
                               where p.Id == role.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldRole != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldRole.Status = 0;

                        context.tblRoles.Attach(oldRole);
                        context.Entry(oldRole).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblRole UpdateRole(tblRole role)
        {

            try
            {
                tblRole oldRole;
                using (var context = new EmsalDBEntities())
                {





                    oldRole = (from p in context.tblRoles
                               where p.Id == role.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldRole != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldRole = role;

                        context.tblRoles.Attach(oldRole);
                        context.Entry(oldRole).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldRole;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblRole GetRoleById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblRoles
                                     where p.Id == ID && p.Status == 1
                                     select p).FirstOrDefault();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblRole> GetRoles()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblRoles
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tblRole> GetRoles1()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblRoles
                                     where p.Status == 1 &&( p.Id==11 || p.Id==15)
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tblRole GetRoleByName(string name)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblRoles
                                     where p.Name == name && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion
        #region tblExpertise
        public tblExpertise AddExpertise(tblExpertise expertise)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblExpertises.Add(expertise);
                    context.SaveChanges();
                    return expertise;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteExpertise(tblExpertise expertise)
        {

            try
            {
                tblExpertise oldExpertise;
                using (var context = new EmsalDBEntities())
                {


                    oldExpertise = (from p in context.tblExpertises
                                    where p.Id == expertise.Id && p.Status == 1
                                    select p).FirstOrDefault();

                }

                if (oldExpertise != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldExpertise.Status = 0;

                        context.tblExpertises.Attach(oldExpertise);
                        context.Entry(oldExpertise).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblExpertise UpdateExpertise(tblExpertise expertise)
        {

            try
            {
                tblExpertise oldExpertise;
                using (var context = new EmsalDBEntities())
                {





                    oldExpertise = (from p in context.tblExpertises
                                    where p.Id == expertise.Id && p.Status == 1
                                    select p).FirstOrDefault();

                }

                if (oldExpertise != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldExpertise = expertise;

                        context.tblExpertises.Attach(oldExpertise);
                        context.Entry(oldExpertise).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldExpertise;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblExpertise GetExpertiseById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblExpertises
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblExpertise> GetExpertises()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblExpertises
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region tblTitle
        public tblTitle AddTitle(tblTitle title)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblTitles.Add(title);
                    context.SaveChanges();
                    return title;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteTitle(tblTitle title)
        {

            try
            {
                tblTitle oldTitle;
                using (var context = new EmsalDBEntities())
                {


                    oldTitle = (from p in context.tblTitles
                                where p.Id == title.Id && p.Status == 1
                                select p).FirstOrDefault();

                }

                if (oldTitle != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldTitle.Status = 0;

                        context.tblTitles.Attach(oldTitle);
                        context.Entry(oldTitle).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblTitle UpdateTitle(tblTitle title)
        {

            try
            {
                tblTitle oldTitle;
                using (var context = new EmsalDBEntities())
                {





                    oldTitle = (from p in context.tblTitles
                                where p.Id == title.Id && p.Status == 1
                                select p).FirstOrDefault();

                }

                if (oldTitle != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldTitle = title;

                        context.tblTitles.Attach(oldTitle);
                        context.Entry(oldTitle).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldTitle;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblTitle GetTitleById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblTitles
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblTitle> GetTitles()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblTitles
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region tblParty

        public tblParty AddParty(tblParty party)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblParties.Add(party);
                    context.SaveChanges();
                    return party;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteParty(tblParty party)
        {

            try
            {
                tblParty oldParty;
                using (var context = new EmsalDBEntities())
                {


                    oldParty = (from p in context.tblParties
                                where p.Id == party.Id && p.Status == 1
                                select p).FirstOrDefault();

                }

                if (oldParty != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldParty.Status = 0;

                        context.tblParties.Attach(oldParty);
                        context.Entry(oldParty).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblParty UpdateParty(tblParty party)
        {

            try
            {
                tblParty oldParty;
                using (var context = new EmsalDBEntities())
                {





                    oldParty = (from p in context.tblParties
                                where p.Id == party.Id && p.Status == 1
                                select p).FirstOrDefault();

                }

                if (oldParty != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldParty = party;

                        context.tblParties.Attach(oldParty);
                        context.Entry(oldParty).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldParty;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblParty GetPartyById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblParties
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblParty> GetParties()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblParties
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
        #region tblOrganization

        public tblOrganization AddOrganization(tblOrganization organization)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblOrganizations.Add(organization);
                    context.SaveChanges();
                    return organization;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteOrganization(tblOrganization organization)
        {

            try
            {
                tblOrganization oldOrganization;
                using (var context = new EmsalDBEntities())
                {


                    oldOrganization = (from p in context.tblOrganizations
                                       where p.Id == organization.Id && p.Status == 1
                                       select p).FirstOrDefault();

                }

                if (oldOrganization != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldOrganization.Status = 0;

                        context.tblOrganizations.Attach(oldOrganization);
                        context.Entry(oldOrganization).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblOrganization UpdateOrganization(tblOrganization organization)
        {

            try
            {
                tblOrganization oldOrganization;
                using (var context = new EmsalDBEntities())
                {





                    oldOrganization = (from p in context.tblOrganizations
                                       where p.Id == organization.Id && p.Status == 1
                                       select p).FirstOrDefault();

                }

                if (oldOrganization != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldOrganization = organization;

                        context.tblOrganizations.Attach(oldOrganization);
                        context.Entry(oldOrganization).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldOrganization;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblOrganization GetOrganizationById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblOrganizations
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblOrganization> GetOrganizations()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblOrganizations
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
        #region tblConfirmationMessage

        public tblConfirmationMessage AddConfirmationMessage(tblConfirmationMessage confirmationMessage)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblConfirmationMessages.Add(confirmationMessage);
                    context.SaveChanges();
                    return confirmationMessage;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteConfirmationMessage(tblConfirmationMessage confirmationMessage)
        {



            try
            {
                tblConfirmationMessage oldConfirmationMessage;
                using (var context = new EmsalDBEntities())
                {


                    oldConfirmationMessage = (from p in context.tblConfirmationMessages
                                              where p.Id == confirmationMessage.Id
                                              select p).FirstOrDefault();

                }

                if (oldConfirmationMessage != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        //status yoxdur
                        // oldConfirmationMessage. = 0;

                        context.tblConfirmationMessages.Attach(oldConfirmationMessage);
                        context.Entry(oldConfirmationMessage).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblConfirmationMessage UpdateConfirmationMessage(tblConfirmationMessage confirmationMessage)
        {

            try
            {
                tblConfirmationMessage oldConfirmationMessage;
                using (var context = new EmsalDBEntities())
                {





                    oldConfirmationMessage = (from p in context.tblConfirmationMessages
                                              where p.Id == confirmationMessage.Id
                                              select p).FirstOrDefault();

                }

                if (oldConfirmationMessage != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldConfirmationMessage = confirmationMessage;

                        context.tblConfirmationMessages.Attach(oldConfirmationMessage);
                        context.Entry(oldConfirmationMessage).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldConfirmationMessage;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblConfirmationMessage GetConfirmationMessageById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblConfirmationMessages
                                     where p.Id == ID
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion
        #region tblForeign_Organization
        public tblForeign_Organization GetForeign_OrganizationByVoen(string voen)
        {

            try
            {

                using (var context = new EmsalDBEntities())
                {


                    var foreign_Organization = (from p in context.tblForeign_Organization
                                                where p.voen == voen && p.Status == 1
                                                select p).FirstOrDefault();

                    return foreign_Organization;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblForeign_Organization AddForeign_Organization(tblForeign_Organization foreign_organization)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblForeign_Organization.Add(foreign_organization);
                    if (foreign_organization.parent_Id == null)
                    {
                        foreign_organization.parent_Id = 0;
                    }
                    context.SaveChanges();
                    return foreign_organization;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteForeign_Organization(tblForeign_Organization foreign_organization)
        {

            try
            {
                tblForeign_Organization oldForeign_organization;
                using (var context = new EmsalDBEntities())
                {


                    oldForeign_organization = (from p in context.tblForeign_Organization
                                               where p.Id == foreign_organization.Id && p.Status == 1
                                               select p).FirstOrDefault();

                }

                if (oldForeign_organization != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldForeign_organization.Status = 0;

                        context.tblForeign_Organization.Attach(oldForeign_organization);
                        context.Entry(oldForeign_organization).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblForeign_Organization UpdateForeign_Organization(tblForeign_Organization foreign_organization)
        {

            try
            {
                tblForeign_Organization oldForeign_organization;
                using (var context = new EmsalDBEntities())
                {





                    oldForeign_organization = (from p in context.tblForeign_Organization
                                               where p.Id == foreign_organization.Id && p.Status == 1
                                               select p).FirstOrDefault();

                }

                if (oldForeign_organization != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldForeign_organization = foreign_organization;

                        context.tblForeign_Organization.Attach(oldForeign_organization);
                        context.Entry(oldForeign_organization).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldForeign_organization;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblForeign_Organization GetForeign_OrganizationById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblForeign_Organization
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblForeign_Organization> GetForeign_Organizations()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblForeign_Organization
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public tblForeign_Organization GetForeign_OrganizationByUserId(Int64 userId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    var ecatalogs = (from p in context.tblForeign_Organization
                                     where p.userId == userId && p.Status == 1
                                     select p).FirstOrDefault();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblForeign_Organization> GetForeign_OrganisationsByParentId(Int64 ID)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var organisations = (from p in context.tblForeign_Organization
                                         where p.parent_Id == ID && p.Status == 1
                                         select p);
                    return organisations.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        ///dila GetPRM_AdminUnitRegionByAddressId
        //public List<tblPRM_AdminUnit> GetPRM_AdminUnitRegionList(Int64 id)
        //{
        //    try
        //    {
        //        using (var context = new EmsalDBEntities())
        //        {

        //            var organisations = (from p in context.tblPRM_AdminUnit
        //                                 where p.Id == id && p.Status == 1
        //                                 select p);
        //            return organisations.ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public List<tblPRM_AdminUnit> GetPRM_AdminUnitRegionByAddressId(Int64 id)
        {
           
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    if (id!=0)
                    {
                       var  organisations = (from p in context.tblPRM_AdminUnit
                                             where p.ParentRegionID == id && p.Status == 1
                                             select p);
                       return organisations.ToList();
                    }
                    else if(id==0)
                    {
                       var organisations1 = (from p in context.tblPRM_AdminUnit
                                         where p.ParentRegionID != 0 && p.Status == 1
                                         select p);
                       return organisations1.ToList();
                    }

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region tblProductCatalogControlOperation
        public tblProductCatalogControl AddProductCatalogControl(tblProductCatalogControl item)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblProductCatalogControls.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteProductCatalogControl(tblProductCatalogControl item)
        {

            try
            {
                tblProductCatalogControl oldProduct;
                using (var context = new EmsalDBEntities())
                {





                    oldProduct = (from p in context.tblProductCatalogControls
                                  where p.Id == item.Id && p.Status == 1
                                  select p).FirstOrDefault();

                }

                if (oldProduct != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldProduct.Status = 0;
                        oldProduct.LastUpdatedStatus = item.LastUpdatedStatus;
                        oldProduct.updateDate = item.updateDate;
                        oldProduct.updateUser = item.updateUser;
                        context.tblProductCatalogControls.Attach(oldProduct);
                        context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProductCatalogControl> GetProductCatalogControls()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblProductCatalogControls
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public tblProductCatalogControl GetProductCatalogControlById(Int64 controlID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalog = (from p in context.tblProductCatalogControls
                                    where p.Id == controlID && p.Status == 1
                                    select p).FirstOrDefault();

                    return pcatalog;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProductCatalogControl> GetProductCatalogControlsByProductID(Int64 productId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblProductCatalogControls
                                     where p.ProductId == productId && p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProductCatalogControl> GetProductCatalogControlsByECategoryID(Int64 enumCategoryID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblProductCatalogControls
                                     where p.EnumCategoryId == enumCategoryID && p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProductCatalogControl> GetProductCatalogControlsByEValueID(Int64 enumValueID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblProductCatalogControls
                                     where p.EnumValueId == enumValueID && p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public tblProductCatalogControl UpdateProductCatalogControl(tblProductCatalogControl item)
        {

            try
            {
                tblProductCatalogControl oldProduct;
                using (var context = new EmsalDBEntities())
                {
                    oldProduct = (from p in context.tblProductCatalogControls
                                  where p.Id == item.Id && p.Status == 1
                                  select p).FirstOrDefault();

                }

                if (oldProduct != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        item.createDate = oldProduct.createDate;
                        item.createUser = oldProduct.createUser;
                        item.Status = oldProduct.Status;

                        oldProduct = item;

                        context.tblProductCatalogControls.Attach(oldProduct);
                        context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldProduct;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        #region tblProductionDocument
        public tblProduction_Document AddProductionDocument(tblProduction_Document productionDocument)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblProduction_Document.Add(productionDocument);
                    context.SaveChanges();
                    return productionDocument;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeleteProductionDocument(tblProduction_Document productionDocument)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    var entry = context.Entry(productionDocument);
                    if (entry.State == EntityState.Detached)
                        context.tblProduction_Document.Attach(productionDocument);
                    context.tblProduction_Document.Remove(productionDocument);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblProduction_Document UpdateProductionDocument(tblProduction_Document productionDocument)
        {

            try
            {
                tblProduction_Document oldproductionDocument;
                using (var context = new EmsalDBEntities())
                {
                    oldproductionDocument = (from p in context.tblProduction_Document
                                             where p.Id == productionDocument.Id && p.Status == 1
                                             select p).FirstOrDefault();

                }

                if (oldproductionDocument != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldproductionDocument = productionDocument;

                        context.tblProduction_Document.Attach(oldproductionDocument);
                        context.Entry(oldproductionDocument).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldproductionDocument;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblProduction_Document GetProductionDocumentById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productionDocument = (from p in context.tblProduction_Document
                                              where p.Id == ID && p.Status == 1
                                              select p).First();

                    return productionDocument;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProduction_Document> GetProductionDocumentsByDemand_Production_Id(tblProduction_Document prodDoc)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productionDocumentList = (from p in context.tblProduction_Document
                                                  where p.grup_Id == prodDoc.grup_Id && p.Status == 1
                                                  select p);

                    return productionDocumentList.ToList(); ;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProduction_Document> GetProductionDocumentsByOffer_Production_Id(tblProduction_Document prodDoc)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var productionDocumentList = (from p in context.tblProduction_Document
                                                  where p.Offer_Production_Id == prodDoc.Offer_Production_Id && p.Status == 1
                                                  select p);
                    return productionDocumentList.ToList(); ;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProduction_Document> GetProductionDocumentsByPotential_Production_Id(tblProduction_Document prodDoc)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productionDocumentList = (from p in context.tblProduction_Document
                                                  where p.Potential_Production_Id == prodDoc.Potential_Production_Id && p.Status == 1
                                                  select p);

                    return productionDocumentList.ToList(); ;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProduction_Document> GetProductionDocumentsByGroupId(string GroupId)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productionDocumentList = (from p in context.tblProduction_Document
                                                  where p.grup_Id == GroupId && p.Status == 1
                                                  select p);

                    return productionDocumentList.ToList(); ;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public List<tblProduction_Document> GetProductionDocuments()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var documents = (from p in context.tblProduction_Document
                                     where p.Status == 1
                                     select p);

                    return documents.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<tblProduction_Document> UpdateProductionDocumentForGroupID(tblProduction_Document productionDocument)
        {

            try
            {
                List<tblProduction_Document> oldproductionDocumentList = new List<tblProduction_Document>();
                List<tblProduction_Document> newproductionDocumentList = new List<tblProduction_Document>();
                using (var context = new EmsalDBEntities())
                {
                    oldproductionDocumentList = (from p in context.tblProduction_Document
                                                 where p.grup_Id == productionDocument.grup_Id && p.Status == 1
                                                 select p).ToList();

                }

                foreach (tblProduction_Document oldproductionDocument in oldproductionDocumentList)
                {

                    if (oldproductionDocument != null)
                    {
                        using (var context = new EmsalDBEntities())
                        {
                            oldproductionDocument.Offer_Production_Id = productionDocument.Offer_Production_Id;
                            oldproductionDocument.Potential_Production_Id = productionDocument.Potential_Production_Id;
                            oldproductionDocument.Demand_Production_Id = productionDocument.Demand_Production_Id;

                            context.tblProduction_Document.Attach(oldproductionDocument);
                            context.Entry(oldproductionDocument).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            newproductionDocumentList.Add(oldproductionDocument);

                        }
                    }
                    else
                    {
                        Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                        throw ex;
                    }

                }

                return newproductionDocumentList;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProduction_Document> GetProductionDocumentsByGroupIdAndPotential_Production_Id(tblProduction_Document productionDocument)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productionDocumentList = (from p in context.tblProduction_Document
                                                  where p.grup_Id == productionDocument.grup_Id && p.Potential_Production_Id == productionDocument.Potential_Production_Id && p.Status == 1
                                                  select p);

                    return productionDocumentList.ToList(); ;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProduction_Document> GetProductionDocumentsByGroupIdAndOffer_Production_Id(tblProduction_Document productionDocument)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productionDocumentList = (from p in context.tblProduction_Document
                                                  where p.grup_Id == productionDocument.grup_Id && p.Offer_Production_Id == productionDocument.Offer_Production_Id && p.Status == 1
                                                  select p);

                    return productionDocumentList.ToList(); ;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProduction_Document> GetProductionDocumentsByGroupIdAndDemand_Production_Id(tblProduction_Document productionDocument)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productionDocumentList = (from p in context.tblProduction_Document
                                                  where p.grup_Id == productionDocument.grup_Id && p.Demand_Production_Id == productionDocument.Demand_Production_Id && p.Status == 1
                                                  select p);

                    return productionDocumentList.ToList(); ;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        #region tblProductDocument
        public tblProduct_Document AddProductDocument(tblProduct_Document productDocument)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblProduct_Document.Add(productDocument);
                    context.SaveChanges();
                    return productDocument;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeleteProductDocument(tblProduct_Document productDocument)
        {

            try
            {
                tblProduct_Document oldItem;
                using (var context = new EmsalDBEntities())
                {


                    oldItem = (from p in context.tblProduct_Document
                               where p.Id == productDocument.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.updatedUser = productDocument.updatedUser;
                        oldItem.updatedDate = productDocument.updatedDate;

                        context.tblProduct_Document.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public List<tblProduct_Document> DeleteProductDocumentByProductID(tblProduct_Document itemObj)
        {

            try
            {
                List<tblProduct_Document> oldItemList = new List<tblProduct_Document>();
                List<tblProduct_Document> newItemList = new List<tblProduct_Document>();
                using (var context = new EmsalDBEntities())
                {


                    oldItemList = (from p in context.tblProduct_Document
                                   where p.Product_catalog_Id == itemObj.Product_catalog_Id && p.Status == 1
                                   select p).ToList();

                }

                if (oldItemList != null)
                {
                    foreach (var item in oldItemList)
                    {

                        using (var context = new EmsalDBEntities())
                        {
                            item.Status = 0;
                            item.updatedDate = itemObj.updatedDate;
                            item.updatedUser = itemObj.updatedUser;
                            context.tblProduct_Document.Attach(item);
                            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            newItemList.Add(item);

                        }
                    }
                    return newItemList;

                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public tblProduct_Document UpdateProductDocument(tblProduct_Document productDocument)
        {

            try
            {
                tblProduct_Document oldproductDocument;
                using (var context = new EmsalDBEntities())
                {
                    oldproductDocument = (from p in context.tblProduct_Document
                                          where p.Id == productDocument.Id && p.Status == 1
                                          select p).FirstOrDefault();

                }

                if (oldproductDocument != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldproductDocument = productDocument;

                        context.tblProduct_Document.Attach(oldproductDocument);
                        context.Entry(oldproductDocument).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldproductDocument;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblProduct_Document GetProductDocumentById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productDocument = (from p in context.tblProduct_Document
                                           where p.Id == ID && p.Status == 1
                                           select p).First();

                    return productDocument;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProduct_Document> GetProductDocumentsByProductCatalogId(Int64 productCatalogId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var productDocuments = (from p in context.tblProduct_Document
                                            where p.Product_catalog_Id == productCatalogId && p.Status == 1
                                            select p);

                    return productDocuments.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProduct_Document> GetProductDocuments()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var documents = (from p in context.tblProduct_Document
                                     where p.Status == 1
                                     select p);

                    return documents.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region tblProductionControlOperation
        public tblProductionControl AddProductionControl(tblProductionControl item)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblProductionControls.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteProductionControl(tblProductionControl item)
        {

            try
            {
                tblProductionControl oldItem;
                using (var context = new EmsalDBEntities())
                {

                    oldItem = (from p in context.tblProductionControls
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.LastUpdatedStatus = item.LastUpdatedStatus;
                        oldItem.updateDate = item.updateDate;
                        oldItem.updateUser = item.updateUser;
                        context.tblProductionControls.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProductionControl> GetProductionControls()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productionControl = (from p in context.tblProductionControls
                                             where p.Status == 1
                                             select p);

                    return productionControl.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tblProductionControl GetProductionControlById(Int64 controlID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcontrol = (from p in context.tblProductionControls
                                    where p.Id == controlID && p.Status == 1
                                    select p).FirstOrDefault();

                    return pcontrol;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProductionControl> GetProductionControlsPotentialProductionId(Int64 potentialProductionId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcontrol = (from p in context.tblProductionControls
                                    where p.Potential_Production_Id == potentialProductionId && p.Status == 1
                                    select p);

                    return pcontrol.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProductionControl> GetProductionControlsByOfferProductionId(Int64 offerProductionId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcontrol = (from p in context.tblProductionControls
                                    where p.Offer_Production_Id == offerProductionId && p.Status == 1
                                    select p);

                    return pcontrol.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProductionControl> GetProductionControlsByDemandProductionId(Int64 demandProductionId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcontrol = (from p in context.tblProductionControls
                                    where p.Demand_Production_Id == demandProductionId && p.Status == 1
                                    select p);

                    return pcontrol.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProductionControl> GetProductionControlsByEValueID(Int64 enumValueID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcontrols = (from p in context.tblProductionControls
                                     where p.EnumValueId == enumValueID && p.Status == 1
                                     select p);

                    return pcontrols.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProductionControl> GetProductionControlsByECategoryID(Int64 enumCategoryID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcontrols = (from p in context.tblProductionControls
                                     where p.EnumCategoryId == enumCategoryID && p.Status == 1
                                     select p);

                    return pcontrols.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblProductionControl UpdateProductionControl(tblProductionControl item)
        {

            try
            {
                tblProductionControl oldProduct;
                using (var context = new EmsalDBEntities())
                {
                    oldProduct = (from p in context.tblProductionControls
                                  where p.Id == item.Id && p.Status == 1
                                  select p).FirstOrDefault();

                }

                if (oldProduct != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        item.createDate = oldProduct.createDate;
                        item.createUser = oldProduct.createUser;
                        item.Status = oldProduct.Status;

                        oldProduct = item;

                        context.tblProductionControls.Attach(oldProduct);
                        context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldProduct;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeleteProductionControlsByProduction_Type_ev_Id(Int64 production_Type_ev_Id)
        {

            try
            {
                List<tblProductionControl> productionControls;
                using (var context = new EmsalDBEntities())
                {
                    productionControls = (from p in context.tblProductionControls
                                          where p.Production_type_eV_Id == production_Type_ev_Id && p.Status == 1
                                          select p).ToList();

                }

                if (productionControls.Count != 0)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        foreach (var item in productionControls)
                        {
                            item.Status = 0;
                            context.tblProductionControls.Attach(item);
                            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                        }
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion
        #region tblProduction_Calendar
        public tblProduction_Calendar AddProduction_Calendar(tblProduction_Calendar item)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblProduction_Calendar.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteProduction_Calendar(tblProduction_Calendar item)
        {

            try
            {
                tblProduction_Calendar oldItem;
                using (var context = new EmsalDBEntities())
                {





                    oldItem = (from p in context.tblProduction_Calendar
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.LastUpdatedStatus = item.LastUpdatedStatus;
                        oldItem.updatedDate = item.updatedDate;
                        oldItem.updatedUser = item.updatedUser;
                        context.tblProduction_Calendar.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProduction_Calendar> GetProduction_Calendar()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var production_Calendar = (from p in context.tblProduction_Calendar
                                               where p.Status == 1
                                               select p);

                    return production_Calendar.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tblProduction_Calendar GetProduction_CalendarById(Int64 controlID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcalendar = (from p in context.tblProduction_Calendar
                                     where p.Id == controlID && p.Status == 1
                                     select p).FirstOrDefault();

                    return pcalendar;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //burda list yox tek element qayıtmalıdı ona göre deyişirem ferid
        // GetProductionCalendarByProductionId
        public tblProduction_Calendar GetProductionCalendarByProductionId(Int64 productionId, Int64 productionType_eV_Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcalendar = (from p in context.tblProduction_Calendar
                                     where p.Production_Id == productionId && p.Status == 1 && p.Production_type_eV_Id == productionType_eV_Id
                                     select p).FirstOrDefault();

                    return pcalendar;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProduction_Calendar> GetProductionCalendarProductiontypeeVId(Int64 production_type_eV_Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcalendar = (from p in context.tblProduction_Calendar
                                     where p.Production_type_eV_Id == production_type_eV_Id && p.Status == 1
                                     select p);

                    return pcalendar.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProduction_Calendar> GetProductionCalendartransportationeVId(Int64 t_eV_Id, Int64 year)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcalendar = (from p in context.tblProduction_Calendar
                                     where p.Transportation_eV_Id == t_eV_Id && p.Status == 1 && p.Year == year
                                     select p);

                    return pcalendar.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public tblProduction_Calendar UpdateProductionCalendar(tblProduction_Calendar item)
        {

            try
            {
                tblProduction_Calendar oldProduct;
                using (var context = new EmsalDBEntities())
                {
                    oldProduct = (from p in context.tblProduction_Calendar
                                  where p.Id == item.Id && p.Status == 1
                                  select p).FirstOrDefault();

                }

                if (oldProduct != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        item.createdDate = oldProduct.createdDate;
                        item.createdUser = oldProduct.createdUser;
                        item.Status = oldProduct.Status;

                        oldProduct = item;

                        context.tblProduction_Calendar.Attach(oldProduct);
                        context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldProduct;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
        #region Jira Request


        public Int64 GetDocumentSizebyGroupId(string groupID, Int64 production_type_eVId)
        {
            List<tblProduction_Document> list = new List<tblProduction_Document>();
            Int64 totalSize = 0;
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    list = (from p in context.tblProduction_Document
                            where p.grup_Id == groupID && p.Status == 1 && p.Production_type_eV_Id == production_type_eVId
                            select p).ToList();

                    foreach (tblProduction_Document item in list)
                    {
                        totalSize = totalSize + (long)item.documentSize;
                    }

                }
                return totalSize;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




        public List<ProductionDetail> GetListOfPotensialProductionByUserId(string userID)
        {
            List<ProductionDetail> list = new List<ProductionDetail>();

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    list = (from p in context.tblPersons
                            join pp in context.tblPotential_Production on p.UserId equals pp.user_Id
                            join pc in context.tblProductCatalogs on pp.product_Id equals pc.Id
                            join pa in context.tblProductAddresses on pp.productAddress_Id equals pa.Id
                            join pca in context.tblProduction_Calendar on pp.product_Id equals pca.Production_Id
                            join ev in context.tblEnumValues on pp.quantity_type_eV_Id equals ev.Id
                            select new ProductionDetail()
                            {
                                productName = pc.ProductName,
                                unitPrice = (decimal)pp.unit_price,
                                enumValueName = ev.name,
                            }).ToList();



                }
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Int64 GetDocumentSizeByGroupIdAndPotentialProductionID(tblProduction_Document production_Document)
        {
            List<tblProduction_Document> list = new List<tblProduction_Document>();
            Int64 totalSize = 0;
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    list = (from p in context.tblProduction_Document
                            where p.grup_Id == production_Document.grup_Id && p.Potential_Production_Id == production_Document.Potential_Production_Id && p.Status == 1 && p.Production_type_eV_Id == production_Document.Production_type_eV_Id
                            select p).ToList();

                    foreach (tblProduction_Document item in list)
                    {
                        totalSize = totalSize + (long)item.documentSize;
                    }

                }
                return totalSize;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Int64 GetDocumentSizeByGroupIdAndOfferProductionID(tblProduction_Document production_Document)
        {
            List<tblProduction_Document> list = new List<tblProduction_Document>();
            Int64 totalSize = 0;
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    list = (from p in context.tblProduction_Document
                            where p.grup_Id == production_Document.grup_Id && p.Offer_Production_Id == production_Document.Offer_Production_Id && p.Status == 1 && p.Production_type_eV_Id == production_Document.Production_type_eV_Id
                            select p).ToList();

                    foreach (tblProduction_Document item in list)
                    {
                        totalSize = totalSize + (long)item.documentSize;
                    }

                }
                return totalSize;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Int64 GetDocumentSizeByGroupIdAndDemandProductionID(tblProduction_Document production_Document)
        {
            List<tblProduction_Document> list = new List<tblProduction_Document>();
            Int64 totalSize = 0;
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    list = (from p in context.tblProduction_Document
                            where p.grup_Id == production_Document.grup_Id && p.Demand_Production_Id == production_Document.Demand_Production_Id && p.Status == 1 && p.Production_type_eV_Id == production_Document.Production_type_eV_Id
                            select p).ToList();

                    foreach (tblProduction_Document item in list)
                    {
                        totalSize = totalSize + (long)item.documentSize;
                    }

                }
                return totalSize;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion


        #region TblComMessage

        public tblComMessage AddComMessage(tblComMessage item)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblComMessages.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeleteComMessage(tblComMessage item)
        {

            try
            {
                tblComMessage oldItem;
                using (var context = new EmsalDBEntities())
                {





                    oldItem = (from p in context.tblComMessages
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.LastUpdatedStatus = item.LastUpdatedStatus;
                        oldItem.updatedDate = item.updatedDate;
                        oldItem.updatedUser = item.updatedUser;
                        context.tblComMessages.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblComMessage UpdateComMessage(tblComMessage item)
        {

            try
            {
                tblComMessage oldItem;
                using (var context = new EmsalDBEntities())
                {
                    oldItem = (from p in context.tblComMessages
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem = item;

                        context.tblComMessages.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblComMessage GetComMessageById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var item = (from p in context.tblComMessages
                                where p.Id == ID && p.Status == 1
                                select p).First();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblComMessage> GetComMessagesyByGroupId(Int64 groupId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var items = (from p in context.tblComMessages
                                 where p.groupID == groupId && p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblComMessage> GetComMessagesyByToUserId(Int64 toUserId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var items = (from p in context.tblComMessages
                                 where p.toUserID == toUserId && p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblComMessage> GetComMessagesyByFromUserId(Int64 fromUserId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var items = (from p in context.tblComMessages
                                 where p.fromUserID == fromUserId && p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public List<tblComMessage> GetComMessagesyByFromUserIDToUserId(Int64 fromUserId, Int64 toUserId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var items = (from p in context.tblComMessages
                                 where p.toUserID == toUserId && p.fromUserID == fromUserId && p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        //ferid
        public List<tblComMessage> GetComMessagesyByToUserIdSortedForDateAsc(Int64 toUserId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var items = (from p in context.tblComMessages
                                 where p.toUserID == toUserId && p.Status == 1
                                 orderby p.createdDate
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblComMessage> GetComMessagesyByToUserIdSortedForDateDes(Int64 toUserId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var items = (from p in context.tblComMessages
                                 where p.toUserID == toUserId && p.Status == 1
                                 orderby p.createdDate descending
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblComMessage> GetComMessagesyByFromUserIdSortedForDateAsc(Int64 fromUserId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var items = (from p in context.tblComMessages
                                 where p.fromUserID == fromUserId && p.Status == 1
                                 orderby p.createdDate
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblComMessage> GetComMessagesyByFromUserIdSortedForDateDes(Int64 fromUserId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var items = (from p in context.tblComMessages
                                 where p.fromUserID == fromUserId && p.Status == 1
                                 orderby p.createdDate descending
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblComMessage> GetNotReadComMessagesByToUserId(Int64 toUserId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var items = (from p in context.tblComMessages
                                 where p.toUserID == toUserId && p.Status == 1 && p.isRead == null
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /////////////////////
        #endregion

        #region tblUserRole
        public tblUserRole AddUserRole(tblUserRole userRole)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblUserRoles.Add(userRole);
                    context.SaveChanges();
                    return userRole;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteUserRole(tblUserRole userRole)
        {

            try
            {
                tblUserRole oldUserRole;
                using (var context = new EmsalDBEntities())
                {

                    oldUserRole = (from p in context.tblUserRoles
                                   where p.Id == userRole.Id && p.Status == 1
                                   select p).FirstOrDefault();

                }

                if (oldUserRole != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldUserRole.Status = 0;

                        context.tblUserRoles.Attach(oldUserRole);
                        context.Entry(oldUserRole).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblUserRole UpdateUserRole(tblUserRole userRole)
        {

            try
            {
                tblUserRole oldUserRole;
                using (var context = new EmsalDBEntities())
                {

                    oldUserRole = (from p in context.tblUserRoles
                                   where p.Id == userRole.Id && p.Status == 1
                                   select p).FirstOrDefault();

                }

                if (oldUserRole != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldUserRole = userRole;

                        context.tblUserRoles.Attach(oldUserRole);
                        context.Entry(oldUserRole).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldUserRole;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblUserRole GetUserRoleById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    var ecatalogs = (from p in context.tblUserRoles
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblUserRole> GetUserRoles()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var pcatalogs = (from p in context.tblUserRoles
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tblUserRole> GetUserRoleByUserId(long userID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    var ecatalogs = (from p in context.tblUserRoles
                                     where p.UserId == userID && p.Status == 1
                                     select p);

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion
        #region tblProductPrice
        public tblProductPrice AddProductPrice(tblProductPrice productPrice)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblProductPrices.Add(productPrice);
                    context.SaveChanges();
                    return productPrice;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteProductPrice(tblProductPrice productPrice)
        {

            try
            {
                tblProductPrice oldItem;
                using (var context = new EmsalDBEntities())
                {

                    oldItem = (from p in context.tblProductPrices
                               where p.Id == productPrice.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;

                        context.tblProductPrices.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblProductPrice UpdateProductPrice(tblProductPrice productPrice)
        {

            try
            {
                tblProductPrice oldproductPrice;
                using (var context = new EmsalDBEntities())
                {
                    oldproductPrice = (from p in context.tblProductPrices
                                       where p.Id == productPrice.Id && p.Status == 1
                                       select p).FirstOrDefault();

                }

                if (oldproductPrice != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldproductPrice = productPrice;

                        context.tblProductPrices.Attach(oldproductPrice);
                        context.Entry(oldproductPrice).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldproductPrice;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblProductPrice GetProductPriceById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productPrice = (from p in context.tblProductPrices
                                        where p.Id == ID && p.Status == 1
                                        select p).First();

                    return productPrice;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProductPrice> GetProductPriceByProductId(Int64 productId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var productPrices = (from p in context.tblProductPrices
                                         where p.productId == productId && p.Status == 1
                                         select p);

                    return productPrices.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProductPrice> GetProductPriceByYearId(Int64 yearId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var productPrices = (from p in context.tblProductPrices
                                         where p.year == yearId && p.Status == 1
                                         select p);

                    return productPrices.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProductPrice> GetProductPriceByYearIdAndProductId(Int64 productID, Int64 year)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productPrices = (from p in context.tblProductPrices
                                         where p.productId == productID && p.Status == 1 && p.year == year && p.Status == 1
                                         select p);

                    return productPrices.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblProductPrice GetProductPriceByYearAndProductIdAndPartOfYear(Int64 productID, Int64 year, Int64 partOfYear)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productPrices = (from p in context.tblProductPrices
                                         where p.productId == productID && p.Status == 1 && p.year == year && p.partOfYear == partOfYear && p.Status == 1
                                         select p).FirstOrDefault();

                    return productPrices;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        public List<tblProductPrice> GetProductPricetransportationeVId()
        {
            throw new NotImplementedException();
        }



        #region tblAuthenticatedPart
        public tblAuthenticatedPart GetAuthenticatedPartById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    var ecatalogs = (from p in context.tblAuthenticatedParts
                                     where p.ID == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblAuthenticatedPart> GetAuthenticatedParts()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var pcatalogs = (from p in context.tblAuthenticatedParts
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tblAuthenticatedPart GetAuthenticatedPartByName(string name)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    var ecatalog = (from p in context.tblAuthenticatedParts
                                    where p.Name == name && p.Status == 1
                                    select p).FirstOrDefault();

                    return ecatalog;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tblPrivilegedRole
        public tblPrivilegedRole GetPrivilegedRoleById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    var ecatalogs = (from p in context.tblPrivilegedRoles
                                     where p.ID == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblPrivilegedRole> GetPrivilegedRoles()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var pcatalogs = (from p in context.tblPrivilegedRoles
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tblPrivilegedRole> GetPrivilegedRolesByAuthenticatedPartId(long authenticatedPartID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {

                    var ecatalogs = (from p in context.tblPrivilegedRoles
                                     where p.AuthenticatedPartID == authenticatedPartID && p.Status == 1
                                     select p);

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeletePrivilegedRole(tblPrivilegedRole privilegedRole)
        {

            try
            {
                tblPrivilegedRole oldprivilegedRole;
                using (var context = new EmsalDBEntities())
                {

                    oldprivilegedRole = (from p in context.tblPrivilegedRoles
                                         where p.ID == privilegedRole.ID && p.Status == 1
                                         select p).FirstOrDefault();

                }

                if (oldprivilegedRole != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldprivilegedRole.Status = 0;

                        context.tblPrivilegedRoles.Attach(oldprivilegedRole);
                        context.Entry(oldprivilegedRole).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblPrivilegedRole AddPrivilegedRole(tblPrivilegedRole privilegedRole)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblPrivilegedRoles.Add(privilegedRole);
                    context.SaveChanges();
                    return privilegedRole;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        #endregion
        #region tblProductProfileImage
        public tblProductProfileImage AddProductProfileImage(tblProductProfileImage productProfileImage)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblProductProfileImages.Add(productProfileImage);
                    context.SaveChanges();
                    return productProfileImage;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteProductProfileImage(tblProductProfileImage productProfileImage)
        {

            try
            {
                tblProductProfileImage oldProductProfileImage;
                using (var context = new EmsalDBEntities())
                {


                    oldProductProfileImage = (from p in context.tblProductProfileImages
                                              where p.Id == productProfileImage.Id && p.Status == 1
                                              select p).FirstOrDefault();

                }

                if (oldProductProfileImage != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldProductProfileImage.Status = 0;

                        context.tblProductProfileImages.Attach(oldProductProfileImage);
                        context.Entry(oldProductProfileImage).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblProductProfileImage UpdateProductProfileImage(tblProductProfileImage productProfileImage)
        {

            try
            {
                tblProductProfileImage oldProductProfileImage;
                using (var context = new EmsalDBEntities())
                {





                    oldProductProfileImage = (from p in context.tblProductProfileImages
                                              where p.Id == productProfileImage.Id && p.Status == 1
                                              select p).FirstOrDefault();

                }

                if (oldProductProfileImage != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldProductProfileImage = productProfileImage;

                        context.tblProductProfileImages.Attach(oldProductProfileImage);
                        context.Entry(oldProductProfileImage).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldProductProfileImage;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblProductProfileImage GetProductProfileImageById(Int64 ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblProductProfileImages
                                     where p.Id == ID && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblProductProfileImage GetProductProfileImageByProductId(Int64 productId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblProductProfileImages
                                     where p.productId == productId && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProductProfileImage> GetProductProfileImages()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblProductProfileImages
                                     where p.Status == 1
                                     select p);

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region tblPRM_ASCBranch
        public tblPRM_ASCBranch AddPRM_ASCBranch(tblPRM_ASCBranch branch)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblPRM_ASCBranch.Add(branch);
                    context.SaveChanges();
                    return branch;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeletePRM_ASCBranch(tblPRM_ASCBranch branch)
        {

            try
            {
                tblPRM_ASCBranch oldPRM_ASCBranch;
                using (var context = new EmsalDBEntities())
                {


                    oldPRM_ASCBranch = (from p in context.tblPRM_ASCBranch
                                        where p.Id == branch.Id && p.Status == 1
                                        select p).FirstOrDefault();

                }

                if (oldPRM_ASCBranch != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldPRM_ASCBranch.Status = 0;

                        context.tblPRM_ASCBranch.Attach(oldPRM_ASCBranch);
                        context.Entry(oldPRM_ASCBranch).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public tblPRM_ASCBranch UpdatePRM_ASCBranch(tblPRM_ASCBranch branch)
        {

            try
            {
                tblPRM_ASCBranch oldPRM_ASCBranch;
                using (var context = new EmsalDBEntities())
                {
                    oldPRM_ASCBranch = (from p in context.tblPRM_ASCBranch
                                        where p.Id == branch.Id && p.Status == 1
                                        select p).FirstOrDefault();

                }

                if (oldPRM_ASCBranch != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldPRM_ASCBranch = branch;

                        context.tblPRM_ASCBranch.Attach(oldPRM_ASCBranch);
                        context.Entry(oldPRM_ASCBranch).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldPRM_ASCBranch;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir record yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblPRM_ASCBranch GetPRM_ASCBranchById(Int64 ID)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var branch = (from p in context.tblPRM_ASCBranch
                                  where p.Id == ID && p.Status == 1
                                  select p).First();
                    return branch;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblPRM_ASCBranch> GetPRM_ASCBranches()
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var branch = (from p in context.tblPRM_ASCBranch
                                  where p.Status == 1
                                  select p);
                    return branch.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tblPRM_ASCBranch GetPRM_ASCBranchByName(string branchName)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var branch = (from p in context.tblPRM_ASCBranch
                                  where p.Name == branchName && p.Status == 1
                                  select p).First();
                    return branch;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion
        #region tblPRM_KTNBranch
        public tblPRM_KTNBranch AddPRM_KTNBranch(tblPRM_KTNBranch branch)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblPRM_KTNBranch.Add(branch);
                    context.SaveChanges();
                    return branch;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeletePRM_KTNBranch(tblPRM_KTNBranch branch)
        {

            try
            {
                tblPRM_KTNBranch oldPRM_KTNBranch;
                using (var context = new EmsalDBEntities())
                {


                    oldPRM_KTNBranch = (from p in context.tblPRM_KTNBranch
                                        where p.Id == branch.Id && p.Status == 1
                                        select p).FirstOrDefault();

                }

                if (oldPRM_KTNBranch != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldPRM_KTNBranch.Status = 0;

                        context.tblPRM_KTNBranch.Attach(oldPRM_KTNBranch);
                        context.Entry(oldPRM_KTNBranch).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public tblPRM_KTNBranch UpdatePRM_KTNBranch(tblPRM_KTNBranch branch)
        {

            try
            {
                tblPRM_KTNBranch oldPRM_KTNBranch;
                using (var context = new EmsalDBEntities())
                {
                    oldPRM_KTNBranch = (from p in context.tblPRM_KTNBranch
                                        where p.Id == branch.Id && p.Status == 1
                                        select p).FirstOrDefault();

                }

                if (oldPRM_KTNBranch != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldPRM_KTNBranch = branch;

                        context.tblPRM_KTNBranch.Attach(oldPRM_KTNBranch);
                        context.Entry(oldPRM_KTNBranch).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldPRM_KTNBranch;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir record yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblPRM_KTNBranch GetPRM_KTNBranchById(Int64 ID)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var branch = (from p in context.tblPRM_KTNBranch
                                  where p.Id == ID && p.Status == 1
                                  select p).First();
                    return branch;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblPRM_KTNBranch> GetPRM_KTNBranches()
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var branch = (from p in context.tblPRM_KTNBranch
                                  where p.Status == 1
                                  select p);
                    return branch.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tblPRM_KTNBranch GetPRM_KTNBranchByName(string branchName)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var branch = (from p in context.tblPRM_KTNBranch
                                  where p.Name == branchName && p.Status == 1
                                  select p).First();
                    return branch;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion
        #region tblBranchResponsibility
        public tblBranchResponsibility AddBranchResponsibility(tblBranchResponsibility branch)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblBranchResponsibilities.Add(branch);
                    context.SaveChanges();
                    return branch;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteBranchResponsibility(tblBranchResponsibility branch)
        {

            try
            {
                tblBranchResponsibility oldBranchResponsibility;
                using (var context = new EmsalDBEntities())
                {


                    oldBranchResponsibility = (from p in context.tblBranchResponsibilities
                                               where p.Id == branch.Id && p.Status == 1
                                               select p).FirstOrDefault();

                }

                if (oldBranchResponsibility != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldBranchResponsibility.Status = 0;

                        context.tblBranchResponsibilities.Attach(oldBranchResponsibility);
                        context.Entry(oldBranchResponsibility).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public tblBranchResponsibility UpdateBranchResponsibility(tblBranchResponsibility branch)
        {

            try
            {
                tblBranchResponsibility oldBranchResponsibility;
                using (var context = new EmsalDBEntities())
                {
                    oldBranchResponsibility = (from p in context.tblBranchResponsibilities
                                               where p.Id == branch.Id && p.Status == 1
                                               select p).FirstOrDefault();

                }

                if (oldBranchResponsibility != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldBranchResponsibility = branch;

                        context.tblBranchResponsibilities.Attach(oldBranchResponsibility);
                        context.Entry(oldBranchResponsibility).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldBranchResponsibility;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir record yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblBranchResponsibility GetBranchResponsibilityById(Int64 ID)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var branch = (from p in context.tblBranchResponsibilities
                                  where p.Id == ID && p.Status == 1
                                  select p).FirstOrDefault();
                    return branch;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblBranchResponsibility> GetBranchResponsibilities()
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var branch = (from p in context.tblBranchResponsibilities
                                  where p.Status == 1
                                  select p);
                    return branch.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<tblBranchResponsibility> GetBranchResponsibilityForBranch(tblBranchResponsibility item)
        {
            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var branchs = (from p in context.tblBranchResponsibilities
                                   where p.Status == 1 && p.branchType_eVId == item.branchType_eVId && p.branchId == item.branchId
                                   select p);
                    return branchs.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion




        #region tblProductionCalendar
        public tblProductionCalendar AddProductionCalendar(tblProductionCalendar item)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblProductionCalendars.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteProductionCalendar(tblProductionCalendar item)
        {

            try
            {
                tblProductionCalendar oldItem;
                using (var context = new EmsalDBEntities())
                {





                    oldItem = (from p in context.tblProductionCalendars
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.LastUpdateStatus = item.LastUpdateStatus;
                        oldItem.updatedDate = item.updatedDate;
                        oldItem.updatedUser = item.updatedUser;
                        context.tblProductionCalendars.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProductionCalendar> GetProductionCalendar()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var productionCalendar = (from p in context.tblProductionCalendars
                                              where p.Status == 1
                                              select p);

                    return productionCalendar.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tblProductionCalendar GetProductionCalendarById(Int64 Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcalendar = (from p in context.tblProductionCalendars
                                     where p.Id == Id && p.Status == 1
                                     select p).FirstOrDefault();

                    return pcalendar;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblProductionCalendar> GetProductionCalendarBymonthseVId(Int64 months_eV_Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcalendar = (from p in context.tblProductionCalendars
                                     where p.months_eV_Id == months_eV_Id && p.Status == 1
                                     select p);

                    return pcalendar.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProductionCalendar> GetProductionCalendarpartOfytypeeVId(Int64 partOfyear_eV_Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcalendar = (from p in context.tblProductionCalendars
                                     where p.partOfyear == partOfyear_eV_Id && p.Status == 1
                                     select p);

                    return pcalendar.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public List<tblProductionCalendar> GetProductionCalendartransportationeVId2(Int64 type_eV_Id, Int64 year_id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcalendar = (from p in context.tblProductionCalendars
                                     where p.transportation_eV_Id == type_eV_Id && p.Status == 1 && p.year == year_id
                                     select p);

                    return pcalendar.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblProductionCalendar UpdateProductionCalendar2(tblProductionCalendar item)
        {

            try
            {
                tblProductionCalendar oldProduct;
                using (var context = new EmsalDBEntities())
                {
                    oldProduct = (from p in context.tblProductionCalendars
                                  where p.Id == item.Id && p.Status == 1
                                  select p).FirstOrDefault();

                }

                if (oldProduct != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        item.createdDate = oldProduct.createdDate;
                        item.createdUser = oldProduct.createdUser;
                        item.Status = oldProduct.Status;

                        oldProduct = item;

                        context.tblProductionCalendars.Attach(oldProduct);
                        context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldProduct;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProductionCalendar> GetProductionCalendarOclock(Int64 oclock)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblProductionCalendars
                                     where p.oclock == oclock && p.Status == 1
                                     select p);

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblProductionCalendar GetProductionCalendarQuantity(decimal quantity)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblProductionCalendars
                                     where p.quantity == quantity && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblProductionCalendar> GetProductionCalendarDay(Int64 day)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcalendar = (from p in context.tblProductionCalendars
                                     where p.day == day && p.Status == 1
                                     select p);

                    return pcalendar.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblProductionCalendar GetProductionCalendarPrice(decimal price)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblProductionCalendars
                                     where p.price == price && p.Status == 1
                                     select p).First();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
   
     public tblProductionCalendar GetProductionCalendarByInstance(tblProductionCalendar item)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblProductionCalendars
                                 where p.Production_type_eV_Id==item.Production_type_eV_Id &&  p.Production_Id==item.Production_Id && p.months_eV_Id==item.months_eV_Id
                                  && p.type_eV_Id==item.type_eV_Id && p.year==item.year && p.Status == 1
                                     select p).FirstOrDefault();

                    return ecatalogs;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //public List<tblProductionCalendar> GetProductionCalendarOfferID(Int64 offer_Id)
        //{

        //    try
        //    {
        //        using (var context = new EmsalDBEntities())
        //        {


        //            var ecatalogs = (from p in context.tblProductionCalendars
        //                             where p.offer_Id == offer_Id && p.Status == 1
        //                             select p);

        //            return ecatalogs.ToList();

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
        public List<tblProductionCalendar> GeProductionCalendarProductionId2(Int64 Production_Id,Int64 Production_type_eV_Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblProductionCalendars
                                     where p.Production_Id == Production_Id && p.Status == 1 && p.Production_type_eV_Id ==Production_type_eV_Id
                                     select p);

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        } 
        public List<tblProductionCalendar> GetProductionCalendarProductiontypeEVId2(Int64 Production_type_eV_Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblProductionCalendars
                                     where p.Production_type_eV_Id == Production_type_eV_Id && p.Status == 1
                                     select p);

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        #endregion
        #region tblComMessageAttachment
        public tblComMessageAttachment AddComMessageAttachment(tblComMessageAttachment item)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblComMessageAttachments.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
      

        public bool DeleteComMessageAttachment(tblComMessageAttachment item)
        {

            try
            {
                tblComMessageAttachment oldItem;
                using (var context = new EmsalDBEntities())
                {





                    oldItem = (from p in context.tblComMessageAttachments
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;
                       
                        oldItem.updatedDate = item.updatedDate;
                        oldItem.updatedUser = item.updatedUser;
                        context.tblComMessageAttachments.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblComMessageAttachment> GetComMessageAttachment()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var comMessageAttachment = (from p in context.tblComMessageAttachments
                                              where p.Status == 1
                                              select p);

                    return comMessageAttachment.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tblComMessageAttachment GetComMessageAttachmentById(Int64 Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var comMessageAttachment = (from p in context.tblComMessageAttachments
                                     where p.Id == Id && p.Status == 1
                                     select p).FirstOrDefault();

                    return comMessageAttachment;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        
        public tblComMessageAttachment UpdateComMessageAttachment(tblComMessageAttachment item)
        {

            try
            {
                tblComMessageAttachment oldProduct;
                using (var context = new EmsalDBEntities())
                {
                    oldProduct = (from p in context.tblComMessageAttachments
                                  where p.Id == item.Id && p.Status == 1
                                  select p).FirstOrDefault();

                }

                if (oldProduct != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        item.createdDate = oldProduct.createdDate;
                        item.createdUser = oldProduct.createdUser;
                        item.Status = oldProduct.Status;

                        oldProduct = item;

                        context.tblComMessageAttachments.Attach(oldProduct);
                        context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldProduct;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblComMessageAttachment> GetByComMessageAttachmentId(Int64 Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var comMessageAttachment = (from p in context.tblComMessageAttachments
                                                where p.Id == Id && p.Status == 1
                                                select p).ToList();

                    return comMessageAttachment;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        #endregion
        #region tblContract
        public tblContract AddContract(tblContract item)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblContracts.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool DeleteContract(tblContract item)
        {

            try
            {
                tblContract oldItem;
                using (var context = new EmsalDBEntities())
                {





                    oldItem = (from p in context.tblContracts
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;

                        oldItem.updatedDate = item.updatedDate;
                        oldItem.updatedUser = item.updatedUser;
                        context.tblContracts.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblContract UpdateContract(tblContract item)
        {

            try
            {
                tblContract oldProduct;
                using (var context = new EmsalDBEntities())
                {
                    oldProduct = (from p in context.tblContracts
                                  where p.Id == item.Id && p.Status == 1
                                  select p).FirstOrDefault();

                }

                if (oldProduct != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        item.createdDate = oldProduct.createdDate;
                        item.createdUser = oldProduct.createdUser;
                        item.Status = oldProduct.Status;

                        oldProduct = item;

                        context.tblContracts.Attach(oldProduct);
                        context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldProduct;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblContract GetContractById(Int64 Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var contract = (from p in context.tblContracts
                                    where p.Id == Id && p.Status == 1
                                    select p).FirstOrDefault();

                    return contract;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblContract> GetContractByAgentUserID(Int64 agentUserID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var contract = (from p in context.tblContracts
                                    where p.AgentUserID == agentUserID && p.Status == 1
                                    select p).ToList();

                    return contract;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblContract> GetContractBySupplierUserID(Int64 supplierUserID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var contract = (from p in context.tblContracts
                                    where p.SupplierUserID == supplierUserID && p.Status == 1
                                    select p).ToList();

                    return contract;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblContract> GetContractBySupplierOrganisationID(Int64 organisationID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var contract = (from p in context.tblContracts
                                    where p.SupplierOrganisationID == organisationID && p.Status == 1
                                    select p).ToList();

                    return contract;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblContract> GetContract()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var comMessageAttachment = (from p in context.tblContracts
                                                where p.Status == 1
                                                select p);

                    return comMessageAttachment.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #endregion

        #region Optimastion 

        public List<AnnouncementDetail> GetAnnouncementDetailsByProductId_OP(Int64 productId,int page,int pageSize)
        {
            Int64 curentDate = DateTime.Now.getInt64ShortDate();


            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var ecatalogs = (from p in context.tblAnnouncements
                                     join pc in context.tblProductCatalogs on p.product_id equals pc.Id
                                     where p.product_id == productId  && p.Status == 1 
                                     && p.startDate <= curentDate && p.endDate >= curentDate
                                     
                                 select
                                 new AnnouncementDetail
                                   {

                                        announcement = p,

                                   }
                                   ).OrderBy(i => i.announcement.Id).Skip((page - 1) * pageSize).Take(pageSize);

                    return ecatalogs.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public  Int64 GetAnnouncementDetailsByProductId_OPC(Int64 productId)
        {
            Int64 curentDate = DateTime.Now.getInt64ShortDate();

            Int64 count = 0;
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    count = (from p in context.tblAnnouncements
                                     join pc in context.tblProductCatalogs on p.product_id equals pc.Id
                                     where p.product_id == productId && p.Status == 1 && p.startDate <= curentDate && p.endDate >= curentDate
                                     select
                                    new AnnouncementDetail
                                    {

                                        announcement = p,

                                    }).Count();

                    return count;
;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<AnnouncementDetail> GetAnnouncementDetails_OP(int page, int pageSize)
        {
            Int64 curentDate = DateTime.Now.getInt64ShortDate();
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var pcatalogs = (from p in context.tblAnnouncements
                                     join pc in context.tblProductCatalogs on p.product_id equals pc.Id
                                     orderby pc.ProductName
                                     where p.Status == 1 && p.startDate <= curentDate && p.endDate >= curentDate

                                     select new AnnouncementDetail
                                     {
                                         announcement = p,



                                     }).OrderBy(i => i.announcement.Id).Skip((page - 1) * pageSize).Take(pageSize); ;

                    return pcatalogs.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Int64 GetAnnouncementDetails_OPC()
        {
            Int64 curentDate = DateTime.Now.getInt64ShortDate();

            Int64 count = 0;
            try
            {
                using (var context = new EmsalDBEntities())
                {


                    count = (from p in context.tblAnnouncements
                             join pc in context.tblProductCatalogs on p.product_id equals pc.Id
                             where  p.Status == 1 && p.startDate <= curentDate && p.endDate >= curentDate
                             select
                            new AnnouncementDetail
                            {

                                announcement = p,

                            }).Count();

                    return count;
                    

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
        #region tblPropertyDetail
        public tblPropertyDetail GetPropertyDetailArea(decimal area)
        {

            try
            {

                using (var context = new EmsalDBEntities())
                {


                    var person = (from p in context.tblPropertyDetails
                                  where p.area == area && p.Status == 1
                                  select p).FirstOrDefault();

                    return person;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblPropertyDetail AddPropertyDetail(tblPropertyDetail detail)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblPropertyDetails.Add(detail);
                    context.SaveChanges();
                    return detail;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeletePropertyDetail(tblPropertyDetail item)
        {

            try
            {
                tblPropertyDetail oldItem;
                using (var context = new EmsalDBEntities())
                {

                    oldItem = (from p in context.tblPropertyDetails
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.LastUpdatedStatus = item.LastUpdatedStatus;
                        oldItem.updatedDate = item.updatedDate;
                        oldItem.updatedUser = item.updatedUser;
                        context.tblPropertyDetails.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblPropertyDetail> GetPropertyDetails()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var details = (from p in context.tblPropertyDetails
                                   where p.Status == 1 && p.Status == 1
                                   select p);

                    return details.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public tblPropertyDetail GetPropertyDetailById(Int64 Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var details = (from p in context.tblPropertyDetails
                                  where p.Id == Id && p.Status == 1
                                  select p).FirstOrDefault();

                    return details;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblPropertyDetail GetPropertyDetailByAddressId(Int64 addressId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var detail = (from p in context.tblPropertyDetails
                                  where p.addressID == addressId && p.Status == 1
                                  select p).FirstOrDefault();

                    return detail;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblPropertyDetail UpdatePropertyDetail(tblPropertyDetail item)
        {
            try
            {
                tblPropertyDetail oldItem;
                using (var context = new EmsalDBEntities())
                {
                    oldItem = (from p in context.tblPropertyDetails
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        item.createdDate = oldItem.createdDate;
                        item.createdUser = oldItem.createdUser;
                        item.Status = oldItem.Status;
                        item.addressID = oldItem.addressID;
                        item.capacity_measuriment_evID = oldItem.capacity_measuriment_evID;
                        oldItem = item;

                        context.tblPropertyDetails.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblPropertyDetail> GetPropertyDetailByProperty_type_ID(Int64 property_type_ID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var contract = (from p in context.tblPropertyDetails
                                    where p.property_type_ID == property_type_ID && p.Status == 1
                                    select p).ToList();

                    return contract;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tblPropertyDetail> GetPropertyDetailByCapacity_measuriment_evID(Int64 capacity_measuriment_evID)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var contract = (from p in context.tblPropertyDetails
                                    where p.capacity_measuriment_evID == capacity_measuriment_evID && p.Status == 1
                                    select p).ToList();

                    return contract;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblPropertyDetail GetPropertyDetailCapacity(decimal capacity)
        {

            try
            {

                using (var context = new EmsalDBEntities())
                {


                    var person = (from p in context.tblPropertyDetails
                                  where p.capacity == capacity && p.Status == 1
                                  select p).FirstOrDefault();

                    return person;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
        #region tblPropertyType
        public tblPropertyType GetPropertyType(string name)
        {

            try
            {

                using (var context = new EmsalDBEntities())
                {


                    var person = (from p in context.tblPropertyTypes
                                  where p.Name == name && p.Status == 1
                                  select p).FirstOrDefault();

                    return person;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tblPropertyType AddPropertyTypes(tblPropertyType detail)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    context.tblPropertyTypes.Add(detail);
                    context.SaveChanges();
                    return detail;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeletePropertyType(tblPropertyType item)
        {

            try
            {
                tblPropertyType oldItem;
                using (var context = new EmsalDBEntities())
                {

                    oldItem = (from p in context.tblPropertyTypes
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.LastUpdatedStatus = item.LastUpdatedStatus;
                        oldItem.updatedDate = item.updatedDate;
                        oldItem.updatedUser = item.updatedUser;
                        context.tblPropertyTypes.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tblPropertyType> GetPropertyTypes()
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var details = (from p in context.tblPropertyTypes
                                   where p.Status == 1 && p.Status == 1
                                   select p);

                    return details.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public tblPropertyType GetPropertyTypeById(Int64 Id)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {


                    var details = (from p in context.tblPropertyTypes
                                   where p.Id == Id && p.Status == 1
                                   select p).FirstOrDefault();

                    return details;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblPropertyType GetPropertyTypeByAddressId(Int64 parentId)
        {

            try
            {
                using (var context = new EmsalDBEntities())
                {
                    var detail = (from p in context.tblPropertyTypes
                                  where p.parent_Id == parentId && p.Status == 1
                                  select p).FirstOrDefault();

                    return detail;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tblPropertyType UpdatePropertyType(tblPropertyType item)
        {
            try
            {
                tblPropertyType oldItem;
                using (var context = new EmsalDBEntities())
                {
                    oldItem = (from p in context.tblPropertyTypes
                               where p.Id == item.Id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new EmsalDBEntities())
                    {
                        item.createdDate = oldItem.createdDate;
                        item.createdUser = oldItem.createdUser;
                        item.Status = oldItem.Status;
                        item.parent_Id = oldItem.parent_Id;
                      
                        oldItem = item;

                        context.tblPropertyTypes.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        
    }
}

