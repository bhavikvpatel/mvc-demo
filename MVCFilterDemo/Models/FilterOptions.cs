using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAppMVC.Models
{
    public class FilterDetail
    {
        public string FilterName { get; set; }
        public int FilterId { get; set; }              
        public FilterOptions FilterOptions { get; set; }
    }
    public class FilterOptions
    {
        public FilterOptions()
        {
            GenerateOptions();
            GenerateAlphabets();
        }                       
        public List<Option> Options { get; set; }
        public List<char> Alphabets { get; set; }
        private void GenerateOptions()
        {
            Options = new List<DemoAppMVC.Models.Option>();
            Options.Add(new Option() { OptionId = 1, OptionName = "A-bike - UK" });
            Options.Add(new Option() { OptionId = 2, OptionName = "Abici - Italy",IsChecked=true});
            Options.Add(new Option() { OptionId = 3, OptionName = "Adler - Germany (defunct)" });
            Options.Add(new Option() { OptionId = 4, OptionName = "AIST - Belarus" });
            Options.Add(new Option() { OptionId = 5, OptionName = "ALAN - Italy" });
            Options.Add(new Option() { OptionId = 6, OptionName = "Alcyon - France (defunct)" });
            Options.Add(new Option() { OptionId = 7, OptionName = "Alldays & Onions - UK (defunct)", IsChecked = true });
            Options.Add(new Option() { OptionId = 8, OptionName = "American Bicycle Company - USA (defunct)" });
            Options.Add(new Option() { OptionId = 9, OptionName = "Baltik vairas - Lithuania" });
            Options.Add(new Option() { OptionId = 10, OptionName = "Bacchetta - USA", IsChecked = true });
            Options.Add(new Option() { OptionId = 11, OptionName = "Barnes Cycle Company - USA (defunct)" });
            Options.Add(new Option() { OptionId = 12, OptionName = "Batavus - Netherlands" });
            Options.Add(new Option() { OptionId = 13, OptionName = "Battaglin - Italy" });
            Options.Add(new Option() { OptionId = 14, OptionName = "Berlin & Racycle Manufacturing Company - Canada (defunct)" });
            Options.Add(new Option() { OptionId = 15, OptionName = "BH - Spain" });
            Options.Add(new Option() { OptionId = 16, OptionName = "Bianchi - Italy" });
            Options.Add(new Option() { OptionId = 17, OptionName = "Bickerton - UK (folding bikes)" });
            Options.Add(new Option() { OptionId = 18, OptionName = "Bike Friday - USA (Green Gear Cycling Co.) (folding bikes)" });
            Options.Add(new Option() { OptionId = 19, OptionName = "Bilenky - USA" });
            Options.Add(new Option() { OptionId = 20, OptionName = "Biomega - Denmark" });
            Options.Add(new Option() { OptionId = 21, OptionName = "Birdy - Germany (folding bikes)" });
            Options.Add(new Option() { OptionId = 22, OptionName = "BMC - Switzerland" });
            Options.Add(new Option() { OptionId = 23, OptionName = "Boardman Bikes - UK" });
            Options.Add(new Option() { OptionId = 24, OptionName = "Bohemian Bicycles - USA" });
            Options.Add(new Option() { OptionId = 25, OptionName = "Calcott Brothers - UK (defunct)" });
            Options.Add(new Option() { OptionId = 26, OptionName = "Calfee Design - USA" });
            Options.Add(new Option() { OptionId = 27, OptionName = "Caloi - Brazil" });
            Options.Add(new Option() { OptionId = 28, OptionName = "Campion Cycle Company - UK" });
            Options.Add(new Option() { OptionId = 29, OptionName = "Cannondale - an American division of Canadian conglomerate Dorel Industries" });
            Options.Add(new Option() { OptionId = 30, OptionName = "Canyon bicycles - Germany" });
            Options.Add(new Option() { OptionId = 31, OptionName = "Catrike - USA (Recumbent trikes)" });
            Options.Add(new Option() { OptionId = 32, OptionName = "CCM - Canada" });
            Options.Add(new Option() { OptionId = 33, OptionName = "Centurion - Japan" });
            Options.Add(new Option() { OptionId = 34, OptionName = "Cervélo - Canada" });
            Options.Add(new Option() { OptionId = 35, OptionName = "Chater-Lea - UK" });
            Options.Add(new Option() { OptionId = 36, OptionName = "Chicago Bicycle Company - USA (defunct)" });
            Options.Add(new Option() { OptionId = 37, OptionName = "CHUMBA - USA" });
            Options.Add(new Option() { OptionId = 38, OptionName = "Cilo - Switzerland" });
            Options.Add(new Option() { OptionId = 39, OptionName = "Cinelli - Italy" });
            Options.Add(new Option() { OptionId = 40, OptionName = "Clark-Kent - USA (defunct)" });
            Options.Add(new Option() { OptionId = 41, OptionName = "Claud Butler - UK" });
            Options.Add(new Option() { OptionId = 42, OptionName = "Dahon - USA / China" });
            Options.Add(new Option() { OptionId = 43, OptionName = "Dawes Cycles - UK" });
            Options.Add(new Option() { OptionId = 44, OptionName = "Defiance Cycle Company - UK (defunct)" });
            Options.Add(new Option() { OptionId = 46, OptionName = "Den Beste Sykkel Better known as DBS - Norway" });
            Options.Add(new Option() { OptionId = 48, OptionName = "De Rosa - Italy" });
            Options.Add(new Option() { OptionId = 49, OptionName = "Cycles Devinci - Canada (not to be confused with daVinci Designs of USA, who make tandems.)" });
            Options.Add(new Option() { OptionId = 50, OptionName = "Eagle Bicycle Manufacturing Company - USA (defunct)" });
            Options.Add(new Option() { OptionId = 51, OptionName = "Eddy Merckx Cycles - Belgium" });
            Options.Add(new Option() { OptionId = 52, OptionName = "Electra Bicycle Company - USA (Owned by Trek Bicycle Company)" });
            Options.Add(new Option() { OptionId = 53, OptionName = "Ellis Briggs - UK" });
            Options.Add(new Option() { OptionId = 54, OptionName = "Ellsworth Handcrafted Bicycles - USA" });
            Options.Add(new Option() { OptionId = 55, OptionName = "Falcon Cycles - UK" });
            Options.Add(new Option() { OptionId = 56, OptionName = "Fat City Cycles - USA (defunct)" });
            Options.Add(new Option() { OptionId = 57, OptionName = "Felt - USA" });
            Options.Add(new Option() { OptionId = 58, OptionName = "Fleetwing -USA (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 59, FilterName = "Flying Pigeon - China" });
            //Filters.Add(new FilterOption() { FilterId = 60, FilterName = "Flying Scot - Scotland" });
            //Filters.Add(new FilterOption() { FilterId = 61, FilterName = "Focus Bikes - Germany. Part of Derby Cycle" });
            //Filters.Add(new FilterOption() { FilterId = 62, FilterName = "Gary Fisher - USA (owned by TREK)" });
            //Filters.Add(new FilterOption() { FilterId = 63, FilterName = "Gazelle - Netherlands" });
            //Filters.Add(new FilterOption() { FilterId = 64, FilterName = "Gendron Bicycles - USA" });
            //Filters.Add(new FilterOption() { FilterId = 65, FilterName = "Genesis - UK" });
            //Filters.Add(new FilterOption() { FilterId = 66, FilterName = "Gepida - Hungary" });
            //Filters.Add(new FilterOption() { FilterId = 67, FilterName = "Ghost Bikes - Germany Made in Taiwan" });
            //Filters.Add(new FilterOption() { FilterId = 68, FilterName = "Giant Manufacturing - Taiwan Manufacturers its own bikes and many other brands" });
            //Filters.Add(new FilterOption() { FilterId = 69, FilterName = "Gimson - Spain (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 70, FilterName = "Gitane - France" });
            //Filters.Add(new FilterOption() { FilterId = 71, FilterName = "Harley-Davidson - USA, 1917-1923." });
            //Filters.Add(new FilterOption() { FilterId = 72, FilterName = "Haro Bikes - USA, owns the Masi brand." });
            //Filters.Add(new FilterOption() { FilterId = 73, FilterName = "Harry Quinn - UK (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 74, FilterName = "Hase bikes - Germany" });
            //Filters.Add(new FilterOption() { FilterId = 75, FilterName = "Heinkel - Germany (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 76, FilterName = "Helkama - Finland" });
            //Filters.Add(new FilterOption() { FilterId = 77, FilterName = "Henley Bicycle Works - USA (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 78, FilterName = "Hercules - UK (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 79, FilterName = "Hercules - Germany" });
            //Filters.Add(new FilterOption() { FilterId = 80, FilterName = "Hero Cycles Ltd - India - owning brands like Hero, Hawk and Roma." });
            //Filters.Add(new FilterOption() { FilterId = 81, FilterName = "Ibis - USA" });
            //Filters.Add(new FilterOption() { FilterId = 82, FilterName = "Ideal Bikes - Greece" });
            //Filters.Add(new FilterOption() { FilterId = 83, FilterName = "Indian - USA (bought by Polaris)" });
            //Filters.Add(new FilterOption() { FilterId = 84, FilterName = "IFA - East Germany (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 85, FilterName = "Independent Fabrication - USA" });
            //Filters.Add(new FilterOption() { FilterId = 86, FilterName = "Iride - Italy" });
            //Filters.Add(new FilterOption() { FilterId = 87, FilterName = "Iron Horse Bicycles - American brand now owned by Dorel Industries of Canada" });
            //Filters.Add(new FilterOption() { FilterId = 88, FilterName = "Magna - USA" });
            //Filters.Add(new FilterOption() { FilterId = 89, FilterName = "Malvern Star - Australia" });
            //Filters.Add(new FilterOption() { FilterId = 90, FilterName = "Marin Bikes - USA" });
            //Filters.Add(new FilterOption() { FilterId = 91, FilterName = "Masi Bicycles - USA" });
            //Filters.Add(new FilterOption() { FilterId = 92, FilterName = "Matchless - UK (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 93, FilterName = "Matra - France" });
            //Filters.Add(new FilterOption() { FilterId = 94, FilterName = "Melon Bicycles - USA" });
            //Filters.Add(new FilterOption() { FilterId = 95, FilterName = "Mercian Cycles - UK" });
            //Filters.Add(new FilterOption() { FilterId = 96, FilterName = "Merida Bikes - Taiwan" });
            //Filters.Add(new FilterOption() { FilterId = 97, FilterName = "Merlin - USA" });
            //Filters.Add(new FilterOption() { FilterId = 98, FilterName = "Merckx - Belgium" });
            //Filters.Add(new FilterOption() { FilterId = 99, FilterName = "Miele bicycles - Canada" });
            //Filters.Add(new FilterOption() { FilterId = 100, FilterName = "Milwaukee Bicycle Co. - USA" });
            //Filters.Add(new FilterOption() { FilterId = 101, FilterName = "Minerva - Belgium (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 102, FilterName = "Olive Wheel Company - USA (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 103, FilterName = "Olmo - Italy" });
            //Filters.Add(new FilterOption() { FilterId = 104, FilterName = "Opel - Germany (no longer makes bicycles)" });
            //Filters.Add(new FilterOption() { FilterId = 105, FilterName = "Orange Mountain Bikes - UK" });
            //Filters.Add(new FilterOption() { FilterId = 106, FilterName = "Orbea - Spain" });
            //Filters.Add(new FilterOption() { FilterId = 107, FilterName = "Órbita - Portugal" });
            //Filters.Add(new FilterOption() { FilterId = 108, FilterName = "Orient Bikes - Greece" });
            //Filters.Add(new FilterOption() { FilterId = 109, FilterName = "Overman Wheel Company - USA (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 110, FilterName = "Salcano (bicycle)- Turkey" });
            //Filters.Add(new FilterOption() { FilterId = 111, FilterName = "Samchuly - Korea" });
            //Filters.Add(new FilterOption() { FilterId = 112, FilterName = "Santa Cruz Bikes - (owed by Pon Industries Europe)" });
            //Filters.Add(new FilterOption() { FilterId = 113, FilterName = "Santana Cycles - USA (only makes tandem bicycles)" });
            //Filters.Add(new FilterOption() { FilterId = 114, FilterName = "Saracen Cycles - UK" });
            //Filters.Add(new FilterOption() { FilterId = 115, FilterName = "Maskinfabriks-aktiebolaget Scania - Sweden" });
            //Filters.Add(new FilterOption() { FilterId = 116, FilterName = "Schwinn Bicycle Company - American brand now owned by Dorel Industries of Canada" });
            //Filters.Add(new FilterOption() { FilterId = 117, FilterName = "SCOTT Sports - Switzerland" });
            //Filters.Add(new FilterOption() { FilterId = 118, FilterName = "Serotta - USA" });
            //Filters.Add(new FilterOption() { FilterId = 119, FilterName = "Seven Cycles - USA" });
            //Filters.Add(new FilterOption() { FilterId = 120, FilterName = "Shelby Cycle Company - USA (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 121, FilterName = "Shimano - Japan" });
            //Filters.Add(new FilterOption() { FilterId = 122, FilterName = "Simpel - Switzerland" });
            //Filters.Add(new FilterOption() { FilterId = 124, FilterName = "Sinclair Research - UK" });
            //Filters.Add(new FilterOption() { FilterId = 125, FilterName = "Singer - UK (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 126, FilterName = "Softride- USA" });
            //Filters.Add(new FilterOption() { FilterId = 127, FilterName = "Sohrab - Pakistan" });
            //Filters.Add(new FilterOption() { FilterId = 128, FilterName = "Solé Bicycle Co. - USA" });
            //Filters.Add(new FilterOption() { FilterId = 129, FilterName = "Solex - France (defunct)" });
            //Filters.Add(new FilterOption() { FilterId = 130, FilterName = "Solifer - Finland" });
        }
        private void GenerateAlphabets()
        {
            Alphabets = new List<char>();
            Alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList(); 
        }     
    }
    public class Option
    {
        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public bool IsChecked { get; set; }
    }
}