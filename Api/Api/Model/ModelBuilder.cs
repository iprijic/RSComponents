using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System;
using System.Reflection;

namespace Api.Model
{
    public static class ModelBuilder
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Packt.Shared.Category>("Categories");
            builder.EntitySet<Packt.Shared.Product>("Products");
            builder.EntitySet<Packt.Shared.Supplier>("Suppliers");
            return builder.GetEdmModel();
        }
        public static IEdmModel GetEdmModel(Type typeDbContext)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            foreach(PropertyInfo propModel in typeDbContext.GetProperties())
            {
                if((propModel.PropertyType).IsGenericType && propModel.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) && propModel.PropertyType.GetGenericArguments().Length == 1)
                {
                    Type typePropModel = propModel.PropertyType.GetGenericArguments()[0];
                    Boolean isKey = false;

                    foreach(PropertyInfo prop in typePropModel.GetProperties())
                    {
                        foreach(CustomAttributeData attrib in prop.CustomAttributes)
                        {
                            if(attrib.AttributeType == typeof(System.ComponentModel.DataAnnotations.KeyAttribute))
                            {
                                isKey = true;
                                break;
                            }
                        }

                        //if (typePropModel.Name.Equals("Category"))
                        //{

                        //}

                        if (isKey)
                        {
                            EntityTypeConfiguration entityTypeConf = builder.AddEntityType(typePropModel);
                            builder.AddEntitySet(typePropModel.Name, entityTypeConf);
                            break;
                        }
                    }                   
                }
            }

            return builder.GetEdmModel();
        }

    }
}
