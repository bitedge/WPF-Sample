# DataTemplateSelectorでDataTemplateを切り替える

![](DataTemplateSelectorSample.png)

ListViewのDataTemplateをデータの内容によって切り替える方法です。

NameプロパティとAgeプロパティを持つPersonクラスがあります。

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

XAMLでは、Ageが20未満のときと、Ageが20以上のときのDataTemplateを用意します。

    <Window.Resources>
        <ResourceDictionary>
            <!--  Ageが20未満のときのテンプレート  -->
            <DataTemplate x:Key="under20Template">
                <TextBlock>
                    <Run Text="{Binding Name}" />
                    (<Run Text="{Binding Age}" />
                    )</TextBlock>
            </DataTemplate>
            <!--  Ageが20以上のときのテンプレート  -->
            <DataTemplate x:Key="over20Template">
                <TextBlock Background="Yellow">
                    <Run Text="{Binding Name}" />
                    (<Run Text="{Binding Age}" />
                    )</TextBlock>
            </DataTemplate>
        </ResourceDictionary>
    </Window.Resources>

ListViewのItemTemplateSelectorでは、DataTemplateを切り替えるPersonTemplateSelectorを設定します。

        <ListView>
            <!--  ListViewのデータ  -->
            <ListView.ItemsSource>
                <x:Array Type="{x:Type local:Person}">
                    <local:Person Name="Alice" Age="18" />
                    <local:Person Name="Bob" Age="19" />
                    <local:Person Name="Carol" Age="20" />
                    <local:Person Name="Dave" Age="21" />
                </x:Array>
            </ListView.ItemsSource>
            <!--  使用するDataTemplateを選択するデータテンプレートセレクターの設定  -->
            <ListView.ItemTemplateSelector>
                <local:PersonTemplateSelector
                    Over20Template="{StaticResource over20Template}"
                    Under20Template="{StaticResource under20Template}" />
            </ListView.ItemTemplateSelector>
        </ListView>

PersonTemplateSelectorは、SelectTemplateCore()メソッドでデータの内容によって適切なDataTemplateを返します。

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
