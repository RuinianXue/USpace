   M S B A M L   `   `   `  ����D  @UIDisplay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:UIDisplay.ComponentsUIDisplay.Components  5      ;"clr-namespace:UIDisplay.ComponentsUIDisplay.Components  5      "    UIDisplay.Pages.TodoListPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:UIDisplay.Components     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      +my"clr-namespace:UIDisplay.Components  5      -   5          N� Title$  TodolistPage��5      z�5      T� 5   
    T� X$ 0Q�6    T� Y$ 0Q�6#   .��5      �� -   5         ��Name$ 
mainBorder��5   1      
BorderClip#��[  6    �� CornerRadius$ 20��6E     SecondaryBlueColorA#���  6W   ._�5   
   � .+�5      #   UIDisplay.Components.TodoList  -   5          Name$ todoList��5       K� ZIndex$ 0��6/   $��32 72 32 112q�6?   �� 5      -   5        $ b1��5      $ 1��6!   $��70��61   $	��Top=�6=   #���  6U    J� Opacity$	 0.8Q�6�   �� 5      -   5         ��Name$ 
titlePanel��5      $ 3��6-   	 �� Orientation$	 Vertical=�6=   $	��Top=�6T   
������ .+�5      � $��0 20 0 0q�6   .+�5      �� $	 
Horizontal=�6!   $
��Left=�6:   .+�5      F�   home#���  6   $��37 0 0 0q�6=   
 �� Stretch$

 Fill=�6O     PrimaryBlueColorA#���  6^   $��28��6�   $��28��6�   &   HandyControl.Controls.SimpleText  5         Text$ 任务��6(   $��16 0 0 0q�62   $��Center=�6D      
Foreground# �  6_      FontSize$ 28(�6�      
FontWeight$
 Bold"�6�   5      5      5      �� 5      -   5        $ addressbookBorder��5      $ 4��60   $ 10��6@   $��	4 70 4 90q�6R     SecondaryGrayColor#���  6e   $y�	Collapsed=�6�   z�5      T� 5      $ 0Q�6)   $ 0Q�6/   ._�5       � -   5         �Name$ addressBookPanel��5       .+�5!      �� -   5         ��Name$	 scr��5!   #   $��0q�60   $
��Auto=�6;   $��0��6^   .��5"      � -	   5         �Name$ 	wrapPanel��5"   $   .+�5#      &    UIDisplay.Components.AddressUnit  $y�Visible=�6-   5$      5%      �� 5&      -
   5         ��Name$ addressbookRefreshBtn��5&      $ 2��6<   $��刷新��6L   $��Right=�6Y   $��Bottom=�6u   $��
0 0 110 10q�6�   )  #HandyControl.Controls.BorderElement   CornerRadius$ 5��6�   $��0q�6�    
 TEAL_C#���  6�     TextIconBrush#���  6   $��42��6-  $��85��69  $��16(�6D  6R  �� 5'      -   5        $ emailListConfirmBtn��5'      $ 1��6:   $��确认��6J   $��Right=�6W   $��Bottom=�6s   $��	0 0 15 10q�6�   $ 5��6�   $��0q�6�     PrimaryBrush#���  6�   #���  6  $��42��60  $��85��6<  $��16(�6G  6U  5(      5)      �� 5+      -   5        $ b2��5+      $ 1��6!   $��90��61   $��Bottom=�6=   #���  6X   $	 0.8Q�6�   �� 5,      $ 3��6   $	 Vertical=�6)   $��Bottom=�6@   .+�5-      �� -   5        $
��Auto��5-      $ 10��6'     SecondaryBackgroundColor#��[ 69   $��
32 8 32 20q�6p   $
 0.75Q�50   &   ._�51      � $��80��6    � ColumnDefinitions 52      �� 53      $��50 �6/   �� 54      �� 55      $
��Auto �6/   .+�57      F� -   5         F�Name$ g0Focus0��57      $��0��61   $y�Visible=�6A    	 add#��� 	 6V   $

 Fill=�6s    
 TextSecondaryColor#��� 
 6�   $��22��6�   $��22��6�   $��Center=�6�   $��Center=�6�   F� 58      -   5        $ g0Focus1��58      $��0��61   $y�	Collapsed=�6A     uncheckedcircle#���  6X   $

 Fill=�6�   #��� 
 6�   $��1��6�   #��� 
 6�   $��22��6�   $��22��6	  $��Center=�6  $��Center=�62  � 5:      -   5        $ g1Focus0��5:      $��1��61   $��Center=�6A   .+�5;        $ 添加任务��6,   $ 15(�68   5<      � 5=      -   5        $ g1Focus1��5=      $��1��61   $��Center=�6A   $y�	Collapsed=�6\   .+�5>      �� $	 Vertical=�6)   .+�5?   "   #  HandyControl.Controls.TextBox  -   5          Name$ todoTaskContentTextBox��5?   -   '  !HandyControl.Controls.InfoElement   Placeholder$ 请输入任务内容��6M   
������ $��0 0 8 0q�6�   6�   �� 5@   "   $	 
Horizontal=�6-   $��0 8 0 0q�6F   .+�5A   &     $ 已选关联人:��64   $��70��6B     5B   &   -   5          Name$ teammateList��5B   4   $	 无��6J   5C   #   5D      5E      �� 5F      -   5        $ 
spFuncArea��5F   %   $��2��69   $	 
Horizontal=�6I   $��0 0 8 0q�6b   $y�	Collapsed=�6s   .+�5G      #  HandyControl.Controls.Divider     Orientation$ Vertical=�6)   $��10��6@   $��60��6K   �� 5H      -   5        $ functionalArea��5H   )   $��2��6A   $��Center=�6Q   $
��Auto��6l   $	 Vertical=�6y   .+�5I   "   �� -   5        $ addEmaillistBtn��5I   )     ButtonPrimary#��[ 6B   $��添加关联人��6i   $	��200��6y   6�   *  $HandyControl.Controls.DateTimePicker  5J   "   -   5          Name$ dateTimePickers��5J   4     DateTimePickerExtend#��[ 6M   (	  "HandyControl.Controls.TitleElement 	  Title$ 提醒日期:��6{    	  TitlePlacement$
 Left=�6�   $��0 4 0 0q�6�   5K      5L      5N      5O      5P      5Q      5S      5T      