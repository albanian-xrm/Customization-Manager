﻿using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbanianXrm.CustomizationManager.Helpers
{
    static class Extensions
    {
        public static void SetAttributeCollection(this EntityMetadata entityMetadata, AttributeMetadata[] attributes)
        {
            //AttributeMetadata is internal set in a sealed class so... just doing this
            typeof(EntityMetadata)
                .GetProperty(nameof(entityMetadata.Attributes))
                .SetValue(entityMetadata, attributes, null);
        }

        public static void SetSecurityPrivilegeCollection(this EntityMetadata entityMetadata, SecurityPrivilegeMetadata[] securityPrivileges)
        {
            typeof(EntityMetadata)
                .GetProperty(nameof(entityMetadata.Privileges))
                .SetValue(entityMetadata, securityPrivileges, null);
        }

        public static void SetManyToManyRelationshipCollection(this EntityMetadata entityMetadata, ManyToManyRelationshipMetadata[] relationships)
        {
            typeof(EntityMetadata)
                .GetProperty(nameof(entityMetadata.ManyToManyRelationships))
                .SetValue(entityMetadata, relationships, null);
        }

        public static void SetOneToManyRelationshipCollection(this EntityMetadata entityMetadata, OneToManyRelationshipMetadata[] relationships)
        {
            typeof(EntityMetadata)
                .GetProperty(nameof(entityMetadata.OneToManyRelationships))
                .SetValue(entityMetadata, relationships, null);
        }

        public static void SetManyToOneRelationshipCollection(this EntityMetadata entityMetadata, OneToManyRelationshipMetadata[] relationships)
        {
            typeof(EntityMetadata)
                .GetProperty(nameof(entityMetadata.ManyToOneRelationships))
                .SetValue(entityMetadata, relationships, null);
        }

        public static void SetAttribute(this EntityMetadata entityMetadata, AttributeMetadata attribute)
        {
            var currentAttributes = entityMetadata.Attributes;
            if (currentAttributes == null)
            {
                currentAttributes = new AttributeMetadata[0];
            }
            var newAttributesList = currentAttributes.Where(a => a.LogicalName != attribute.LogicalName).ToList();
            newAttributesList.Add(attribute);
            var newAttributesArray = newAttributesList.ToArray();

            entityMetadata.GetType().GetProperty("Attributes").SetValue(entityMetadata, newAttributesArray, null);
        }

        public static void SetAttributeCollection(this EntityMetadata entityMetadata, IEnumerable<AttributeMetadata> attributes)
        {
            entityMetadata.GetType().GetProperty("Attributes").SetValue(entityMetadata, attributes.ToList().ToArray(), null);
        }

        public static void SetSealedPropertyValue(this EntityMetadata entityMetadata, string sPropertyName, object value)
        {
            entityMetadata.GetType().GetProperty(sPropertyName).SetValue(entityMetadata, value, null);
        }

        public static void SetSealedPropertyValue(this AttributeMetadata attributeMetadata, string sPropertyName, object value)
        {
            attributeMetadata.GetType().GetProperty(sPropertyName).SetValue(attributeMetadata, value, null);
        }

        public static void SetSealedPropertyValue(this ManyToManyRelationshipMetadata manyToManyRelationshipMetadata, string sPropertyName, object value)
        {
            manyToManyRelationshipMetadata.GetType().GetProperty(sPropertyName).SetValue(manyToManyRelationshipMetadata, value, null);
        }

        public static void SetSealedPropertyValue(this OneToManyRelationshipMetadata oneToManyRelationshipMetadata, string sPropertyName, object value)
        {
            oneToManyRelationshipMetadata.GetType().GetProperty(sPropertyName).SetValue(oneToManyRelationshipMetadata, value, null);
        }
    }
}
