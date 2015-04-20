using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;


namespace EventHandlingSystem.Database
{
    public class MemberDB
    {
        private static readonly ATEntities Context = Database.Context;

        // GET
        private static IEnumerable<members> GetAllNotDeletedMembers()
        {
            return Context.members.Where(e => !e.IsDeleted);
        }

        public static List<members> GetAllMembersByAssociationId(associations asso)
        {
            return GetAllNotDeletedMembers().Where(m => m.Associations_Id.Equals(asso.Id)).ToList();
        }

        public static members GetMemberById(int id)
        {
            return GetAllNotDeletedMembers().SingleOrDefault(m => m.Id.Equals(id));
        }

        public static List<members> GetAllContactsInAssociationByAssoId(int id)
        {
            return GetAllNotDeletedMembers().Where(m => m.Associations_Id.Equals(id) && m.IsContact).ToList();
        }

        // UPDATE
        public static int UpdateMember(members member)
        {
            members memberToUpdate = GetMemberById(member.Id);

            memberToUpdate.FirstName = member.FirstName;
            memberToUpdate.SurName = member.SurName;
            memberToUpdate.Email = member.Email;
            memberToUpdate.Phone = member.Phone;
            memberToUpdate.IsContact = member.IsContact;
            memberToUpdate.Associations_Id = member.Associations_Id;

            int affectedRows;

            try
            {
                affectedRows = Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                    // Rollback changes

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                return affectedRows = 0;
            }
            Context.Entry(memberToUpdate).Reload();
            return affectedRows;
        }

        //ADD
        public static bool Addmember(members member)
        {
            Context.members.Add(member);
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                return false;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry
                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages
                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }

                    // Rollback changes
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                return false;
            }
            return true;
        }

        // DELETE
        public static int DeleteMemberById(int id)
        {
            members memberToDelete = GetMemberById(id);

            if (memberToDelete != null)
                memberToDelete.IsDeleted = true;

            int affectedRows;

            try
            {
                affectedRows = Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                    // Rollback changes

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                return affectedRows = 0;
            }
            return affectedRows;
        }
    }
}