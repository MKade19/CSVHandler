   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   H DCSVHandler.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:CSVHandler.UICSVHandler.UI 5   	   C&clr-namespace:CSVHandler.UI.ViewModelsCSVHandler.UI.ViewModels 5   	   C&clr-namespace:CSVHandler.UI.ConvertersCSVHandler.UI.Converters 5	   	      CSVHandler.UI.MainWindow   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:CSVHandler.UI     5   	   =
viewModels&clr-namespace:CSVHandler.UI.ViewModels     5   	   =
converters&clr-namespace:CSVHandler.UI.Converters     5	   	      ApplicationTitle  � Title#  �   5   	   $	��800��64   $
��1100��6A    � 	Resources 5      5  /CSVHandler.UI.Converters.BoolToVisibleConverter  BoolToVisibleConverter6  0CSVHandler.UI.Converters.InverseBooleanConverter  InverseBooleanConverter��%:   &	       &	        5   
   6,     5   
   6-   5      .��5      � $��Center=�6   $
��1080��6(    � ColumnDefinitions 5   
   �� 5      $��7* �6   �� 5      $��4* �6   �� 5      $
��349* �6    � RowDefinitions 5   
   �� 5      $��0.05* �6   �� 5      .+�5   
   o� $��40��6   $��Center=�6   $
��Left=�66   $��16(�6Q   $��3��6_   .@�5      m�   ParseCsvMenuItem#���  6=   $��33��6i   $
��Left=�6u    m� Command 6   �� *GoToParserCommand+6u   m� 5        ExportMenuItem#���  6=   $��33��6g   $
��Left=�6s    6   �� *GoToExportCommand+6s   m� 5      -   5         m�Name$ LanguageMenu��5        ChangeLanguageMenuItem#���  6-   $��33��6_   $
��Left=�6k   5       �� 5!   
   -   5         ��Name$ ViewContainer��5!      $��3��6O   $��0,37,0,0q�6c   $��2��6u   ��60   �� *
ActiveView+6u   5"      5#      