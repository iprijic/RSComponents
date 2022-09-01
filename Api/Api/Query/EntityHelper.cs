using Microsoft.OData.ModelBuilder;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.OData.UriParser;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.OData.Edm;
using Microsoft.AspNetCore.OData.Abstracts;
using System.Reflection;
using System.IO;
using System.Linq;

namespace Api.Query
{
    public class EntityHelper
    {
        public static Type GetClrType(ODataPath path, IEdmModel model)
        {
            IEdmCollectionType[] collectionType = new[] { path.FirstSegment.EdmType as IEdmCollectionType, path.LastSegment.EdmType as IEdmCollectionType };
            IEdmEntityType[] entityType = new[] { collectionType[0].ElementType.Definition as IEdmEntityType, collectionType[1].ElementType.Definition as IEdmEntityType };

            if (entityType[0].Equals(entityType[1]) == false)
            {
                throw new NotImplementedException("entityType[0].Equals(entityType[1]) == false");
            }

            return (model.GetAnnotationValue<ClrTypeAnnotation>(entityType[0]) ?? model.GetAnnotationValue<ClrTypeAnnotation>(entityType[1])).ClrType;
        }

        public static PropertyInfo GetPropertyInfo(IEdmProperty property, IEdmModel model)
        {
            ClrPropertyInfoAnnotation clrPropertyAnnotation = model.GetAnnotationValue<ClrPropertyInfoAnnotation>(property);
            if (clrPropertyAnnotation != null)
            {
                return clrPropertyAnnotation.ClrPropertyInfo;
            }

            ClrTypeAnnotation clrTypeAnnotation = model.GetAnnotationValue<ClrTypeAnnotation>(property.DeclaringType);
            PropertyInfo info = clrTypeAnnotation.ClrType.GetProperty(property.Name);

            return info;
        }

        public static IEdmStructuralProperty GetEdmPrimaryKey(ODataPath path)
        {
            IEdmCollectionType[] collectionType = new[] { path.FirstSegment.EdmType as IEdmCollectionType, path.LastSegment.EdmType as IEdmCollectionType };
            IEdmEntityType[] entityType = new[] { collectionType[0].ElementType.Definition as IEdmEntityType, collectionType[1].ElementType.Definition as IEdmEntityType };

            if (entityType[0].Equals(entityType[1]) == false)
            {
                throw new NotImplementedException("entityType[0].Equals(entityType[1]) == false");
            }

            return entityType[0].Key().FirstOrDefault() ?? entityType[1].Key().FirstOrDefault();
        }
    }
}
