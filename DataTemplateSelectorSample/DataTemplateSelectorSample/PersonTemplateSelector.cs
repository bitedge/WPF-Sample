using System.Windows;
using System.Windows.Controls;

namespace DataTemplateSelectorSample
{
    public class PersonTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Ageが20未満のときのテンプレート
        /// </summary>
        public DataTemplate Under20Template { get; set; }
        /// <summary>
        /// Ageが20以上のときのテンプレート
        /// </summary>
        public DataTemplate Over20Template { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var person = (Person)item;
            if (person.Age < 20)
                return Under20Template;
            else
                return Over20Template;
        }
    }
}
