using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace ProstheticsTable
{
    [DefOf]
    public static class ProstheticsTableDefOf
    {
        public static ThingDef TableSyrProsthetics;
        public static ThingDef TableMachining;
        public static ThingDef FabricationBench;
    }

    [StaticConstructorOnStartup]
    public static class RecipeTransfer
    {
        static RecipeTransfer()
        {
            IEnumerable<RecipeDef> prostheticRecipes = from x in DefDatabase<RecipeDef>.AllDefs
                where !x.AllRecipeUsers.EnumerableNullOrEmpty() && x.AllRecipeUsers.Any(ru => ru == ProstheticsTableDefOf.TableMachining || ru == ProstheticsTableDefOf.FabricationBench) && x.products.Any(p => p.thingDef.isTechHediff)
                select x;
            if (!prostheticRecipes.EnumerableNullOrEmpty())
            {
                foreach (RecipeDef recipeDef in prostheticRecipes)
                {
                    if (recipeDef.recipeUsers.NullOrEmpty())
                    {
                        recipeDef.recipeUsers = new List<ThingDef>();
                    }
                    else
                    {
                        recipeDef.recipeUsers.Clear();
                    }
                    recipeDef.recipeUsers.Add(ProstheticsTableDefOf.TableSyrProsthetics);
                    recipeDef.ResolveReferences();
                }
            }
        }
    }

    public class ProstheticsTableSettings : ModSettings
    {
        public override void ExposeData()
        {
            base.ExposeData();
        }
    }

    public class ProstheticsTableMod : Mod
    {
        public static ProstheticsTableSettings settings;

        public ProstheticsTableMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ProstheticsTableSettings>();
        }
    }
}
