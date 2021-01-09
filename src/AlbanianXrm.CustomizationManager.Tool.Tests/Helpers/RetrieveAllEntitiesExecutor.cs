using FakeXrmEasy;
using FakeXrmEasy.Extensions;
using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbanianXrm.CustomizationManager.Helpers
{
    class RetrieveAllEntitiesExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveAllEntitiesRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as RetrieveAllEntitiesRequest;
            if (req.EntityFilters == 0)
            {
                req.EntityFilters = EntityFilters.Default;
            }

            if (req.EntityFilters.HasFlag(EntityFilters.Entity) ||
                req.EntityFilters.HasFlag(EntityFilters.Attributes) ||
                req.EntityFilters.HasFlag(EntityFilters.Privileges) ||
                req.EntityFilters.HasFlag(EntityFilters.Relationships))
            {
                var allEntities = ctx.CreateMetadataQuery().Select(x => x.Copy()).ToArray();
                foreach (var entityMetadata in allEntities)
                {
                    if (!req.EntityFilters.HasFlag(EntityFilters.Attributes))
                    {
                        entityMetadata.SetAttributeCollection(null);
                    }

                    if (!req.EntityFilters.HasFlag(EntityFilters.Privileges))
                    {
                        entityMetadata.SetSecurityPrivilegeCollection(null);
                    }

                    if (!req.EntityFilters.HasFlag(EntityFilters.Relationships))
                    {
                        entityMetadata.SetOneToManyRelationshipCollection(null);
                        entityMetadata.SetManyToOneRelationshipCollection(null);
                        entityMetadata.SetManyToManyRelationshipCollection(null);
                    }
                }

                var response = new RetrieveAllEntitiesResponse()
                {
                    Results = new ParameterCollection
                        {
                            { "EntityMetadata", allEntities }
                        }
                };

                return response;
            }

            throw new Exception("Entity Filter not supported");
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveAllEntitiesRequest);
        }
    }
}
