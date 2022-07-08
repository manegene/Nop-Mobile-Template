using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace habahabamall.Models
{
    /// <summary>
    /// Model for category.
    /// </summary>
    [DataContract]
    [Preserve(AllMembers = true)]
    public class Category
    {
        #region event and functions src
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual string GetCategoryIconFromDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                return null;

            }
            else if (description.Contains("images"))
            {
                int imageindex1 = description.IndexOf("images") + "images".Length;
                int imagelastindex = description.LastIndexOf("alt");

                string categoryimage = description.Substring(imageindex1, imagelastindex - imageindex1).Replace("\\", "").Replace("\"", "");


                return categoryimage;
            }

            return description;
        }
        #endregion
        #region Fields

        private string icon;

        private string description;
        #endregion

        #region Properties

        [DataMember(Name = "id")]
        public int ID { get; set; }

        ///// <summary>
        ///// Gets or sets the property that has been bound with a label in SfExpander header, which displays the main category.
        ///// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        public string CategoryImage => GetCategoryIconFromDescription(Description);


        ///// <summary>
        ///// Gets or sets the property that has been bound with an image, which displays the category.
        ///// </summary>
        [DataMember(Name = "icon")]
        public string CategoryIcon
        {
            get => App.BaseCategoryImageUrl + CategoryImage;
            set
            {
                if (icon == value)
                {
                    return;
                }
                icon = value;
                NotifyPropertyChanged("");
            }

        }

        [DataMember(Name = "subcategories")]
        public List<SubCategory> SubCategories { get; set; }

        public string Description
        {
            get => description; set
            {
                if (description == value)
                {
                    return;
                }
                description = value;
                NotifyPropertyChanged("description");
            }
        }

        #endregion
    }
}