   M S B A M L   `   `   `  ����D  @UIDisplay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:UIDisplay.ComponentsUIDisplay.Components  5      1clr-namespace:UIDisplay.PagesUIDisplay.Pages  5      %    UIDisplay.Pages.ContactListPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1my"clr-namespace:UIDisplay.Components     5      /localclr-namespace:UIDisplay.Pages     5      -   5          N�Name$  page1��5
       N� Title$ AddressbookPage��5      z�5      T� 5   
    T� X$ 0Q�6    T� Y$ 0Q�6#   .��5      �� -   5         ��Name$ 
mainBorder��5       �� CornerRadius$ 20��6!      SecondaryBlueColorA#���   63   ._�5   
   � -   5         �Name$ AddressbookSite��5       � RowDefinitions 5      �� 5      �� 5      $��50 �6    .+�5      �� -   5         ��Name$	 scr��5      $��20 20 20 10q�6(   $
��Auto=�6=   $��0��6`   .��5      � -   5        	 �Name$	 	wrapPanel��5      .+�5      &    UIDisplay.Components.AddressUnit  $y�	Collapsed=�6%   5      5      �� 5      $��1��6   
 �� Orientation$
 
Horizontal=�6&   $��Right=�6?   .+�5      �� -   5         ��Name$ 
refreshBtn��5      $��刷新��6-     adbButtonsStyle#���  6:     TextPrimaryBlueColorA#���  5      �� 5      -   5        $ insertpersonBtn��5      $��添加联系人��62   #���  6B   
����� �
�������6�   �� 5      -   5        $ deletepersonBtn��5      $��删除联系人��62   #���  6B   ���#F72626 � Cursor$
 Hand��6�   �� 5      -	   5        $ updatepersonBtn��5      $��修改信息��62   #���  6A   ���#1890ff
�������6�   5       5!      5"      5#      