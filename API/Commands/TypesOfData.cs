using System.Linq.Expressions;
using EntityFrame.API.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace EntityFrame.API.Commands
{
    public static class TypesOfData
    {
        public static void AddData(EntityFrameContext context)
        {
            string mynamespace = "EntityFrame.API.Models";

            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.Namespace == mynamespace
                select t;


            foreach (var type in types)
            {
                if (type.Name is "TypesOfData" or "Session")
                    continue;
                var dataType = new Models.TypesOfData()
                {
                    DataType = type.Name
                };
                context.Set<Models.TypesOfData>().AddIfNotExists(dataType, x => x.DataType == type.Name);
            }

            context.SaveChanges();
            context.ChangeTracker.Clear();
        }

        public static EntityEntry<T>? AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>>? predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return !exists ? dbSet.Add(entity) : null;
        }

        public static String[] GetDataTypes(EntityFrameContext con)
        {
            var dataTypes = con.TypesOfData.Select(x => x.DataType).ToArray();

            return dataTypes;
        }
    }

}