using System;
using System.Collections.Generic;
using System.Linq;
using ShardingCore.Core.VirtualRoutes.TableRoutes.RouteTails.Abstractions;
using ShardingCore.Core.VirtualRoutes.TableRoutes.RoutingRuleEngine;
using ShardingCore.Extensions;

namespace ShardingCore.Core.VirtualRoutes.TableRoutes.RouteTails
{
/*
* @Author: xjm
* @Description:
* @Date: Sunday, 22 August 2021 09:59:22
* @Email: 326308290@qq.com
*/
    public class MultiQueryRouteTail:IMultiQueryRouteTail
    {
        private const string RANDOM_MODEL_CACHE_KEY = "RANDOM_MODEL_CACHE_KEY";
        private readonly TableRouteResult _tableRouteResult;
        private readonly string _modelCacheKey;
        private readonly ISet<Type> _entityTypes;

        public MultiQueryRouteTail(TableRouteResult tableRouteResult)
        {
            if (tableRouteResult.ReplaceTables.IsEmpty() || tableRouteResult.ReplaceTables.Count <= 1) throw new ArgumentException("route result replace tables must greater than  1");
            _tableRouteResult = tableRouteResult;
            _modelCacheKey = RANDOM_MODEL_CACHE_KEY+Guid.NewGuid().ToString("n");
            _entityTypes = tableRouteResult.ReplaceTables.Select(o=>o.EntityType).ToHashSet();
        }
        public string GetRouteTailIdentity()
        {
            return _modelCacheKey;
        }

        public bool IsMultiEntityQuery()
        {
            return true;
        }

        public string GetEntityTail(Type entityType)
        {
            return _tableRouteResult.ReplaceTables.Single(o => o.EntityType == entityType).Tail;
        }

        public ISet<Type> GetEntityTypes()
        {
            return _entityTypes;
        }
    }
}