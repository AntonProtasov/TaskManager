   M S B A M L   `   `   `  ����F  BTaskManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TaskManagerTaskManager  5   	       TaskManager.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1taskManagerclr-namespace:TaskManager     5   	     � 	Resources  5	       �� Source�� 5
   
   $' !Resources/ResourceDictionary.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��320 �6   .+�5   
   �� $��0��6   $��0��5         TaskListBackground#��[  5      $��Stretch=�5      $
��Left=�5        DefaultFontSize#��[ 5      $��30, 0, 0, 0q�5      $��Center=�5      $��СПИСОК ЗАДАЧ:��5      $
��Auto��5       �� 5"   
   $��0��6   $��1��5#        TaskCreationBackground#��[ 5$      $��Stretch=�5%      $��Center=�5&      #��[ 5'      
�������$��Center=�5)      $$��СОЗДАНИЕ ЗАДАЧИ:��5*          TaskManager.TaskListCotrol  5,   
   $��1��6%   $��0��5-   %   #��[  5.   %   %   TaskManager.TaskCreationControl  50   
   $��1��6*   $��1��51   *   #��[ 52   *   53      54      