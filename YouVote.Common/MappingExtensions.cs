using System;
using AutoMapper;

namespace YouVote.Common
{
    public static class MappingExtensions
    {
        public static object Map(this object @this, Type destinationType)
        {
            return Mapper.Map(@this, @this.GetType(), destinationType);
        }

        public static TDestination Map<TDestination>(this object @this)
        {
            if (@this == null)
            {
                return default(TDestination);
            }

            return (TDestination)@this.Map(typeof(TDestination));
        }

        public static TDestination Map<TSource, TDestination>(this TSource @this)
        {
            return Mapper.Map<TSource, TDestination>(@this);
        }
    }
}