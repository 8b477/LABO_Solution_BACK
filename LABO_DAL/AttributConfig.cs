
namespace LABO_DAL
{

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class ColumnNameAttribute : Attribute
    {
        public string Name { get; }

        public ColumnNameAttribute(string name)
        {
            Name = name;
        }
    }
}

#region Explication
/*
La classe ColumnNameAttribute est une classe d'attribut personnalisée en C#.

ColumnNameAttribute est un attribut scellé (sealed), ce qui signifie qu'il ne peut pas être utilisé comme classe de base pour d'autres attributs personnalisés. Il peut être appliqué uniquement aux propriétés (ou champs) des classes.

La classe contient un constructeur qui prend un paramètre string name. Ce paramètre est utilisé pour spécifier le nom de la colonne associée à la propriété à laquelle l'attribut est appliqué.

Permet d'ajouter des métadonnées sur les propriétés de leurs classes qui correspondent à des colonnes de base de données, 
en précisant le nom de la colonne correspondante dans la base de données pour chaque propriété.
Cela simplifie l'interaction avec la base de données, en particulier lors de l'utilisation d'un ORM ou de la création de requêtes SQL.

*/
#endregion