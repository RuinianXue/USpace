   M S B A M L   `   `   `  ����D  @UIDisplay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UIDisplay	UIDisplay  5   	       UIDisplay.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:UIDisplay     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	   -   5        $��
HomeWindow��5        � Icon$0  *pack://application:,,,/Resources/Icon0.ico��5	   	    � Title$ USpace��5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ Normal=�6.    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�6O   .��5      ��  �� CornerRadius$ 15��6      PrimaryBackgroundColor#���   6   ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      .+�5      �� $ 8 0 0 8��6     SecondaryBackgroundColor#���  6,   ._�5      � -   5        	 �Name$	 gridMenu��5      $��60��6)   
 � RowDefinitions
 5      �� 5      $��60 �6(   �� 5      $��80 �6(   �� 5      $
��Auto �6(   �� 5      .+�5      �� $��1��6!    �� Orientation$ 
Horizontal=�6.   $	��Top=�6G   .+�5      F�   logo#���  6   $��16 0 0 0q�6=    �� Stretch$
 Fill=�6O     PrimaryBlueColor#���  6^   $��25��6�   $��25��6�   $
��Left=�6�   &   HandyControl.Controls.SimpleText  5          Text$ USpace��6(      
Foreground# �  66      
FontWeight$
 Bold"�6f      FontSize$ 20(�6x   $��Center=�6�      TextAlignment$
 Left=�6�   $
��Left=�6�   $��20 0 0 0q�6�   5!      d� 5$      -   5         d�Name$ btnMenu��5$   #   $��Right=�65   $��0q�6Q   
������ $��Center=�6~     MenuIconStyle#��[ 6�   $��0 0 15 0q�6�    � Triggers 5%      :� 5&       :� RoutedEvent$ ToggleButton.Unchecked��6+   .[�5'   "   �� .��5(   &   �� -   5         ��Name$ HideStackPanel��5(   1   .��5)   *   U�  �� 
TargetName$ gridMenu��6H    �� TargetProperty$ Width�6i   .\�5*   .   9  3System.Windows.Media.Animation.EasingDoubleKeyFrame   O� KeyTime$ 0:0:0��6C    O� Value$	 200Q�6S      EasingFunction 5+   2   /  )System.Windows.Media.Animation.CircleEase  5,   6   7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode$ EaseOut=�6A   5.   /     5/   .   $ 0:0:0.5��6C   $ 60Q�6U    50   2     51   6   $ EaseOut=�6A   53   /   54   +   55   '   56   #   57      :� 58      $ ToggleButton.Checked��6+   .[�59   "   �� .��5:   &   �� -   5        $ ShowStackPanel��5:   1   .��5;   *   U� $ gridMenu��6H   $ Width�6i   .\�5<   .     $ 0:0:0��6C   $ 60Q�6S    5=   2     5>   6   $ EaseOut=�6A   5@   /     5A   .   $ 0:0:0.5��6C   $	 200Q�6U    5B   2     5C   6   $ EaseOut=�6A   5E   /   5F   +   5G   '   5H   #   5I      5K      �� 5L      -   5         ��Name$ 	mainstack��5L   .   $��4��6!   $��0 0 0 70q�6A   .+�5M      	� -   5         	�Name$ 	Dashboard��5M   &    d� 	IsChecked$
 TrueY�69   $��
 Dashboard��6J     MenuRadioButtonStyle#���  6_     home
 � Tag# �  6�     � Cursor$
  Hand��6�   	� 5N      -   5        $ rdAddressbook��5N   &   $�� Address Book��6=   #���  6U     addressbook# �  6�   $
  Hand��6�   	� 5O      -	   5        $ 
rdTodolist��5O   &   $�� To Do List��6:   #���  6P     todo# �  6   $
  Hand��6�   	� 5P      -
   5        $ MarkdownEditor��5P   &   $��	 Markdown��6>   #���  6R    	 	txtEditor# � 	 6�   $
  Hand��6�   5Q      	� 5R      -   5        $ rdLogOut��5R   /   $��5��6"   $�� Log Out��6A   $��0q�6T   
������ #���  6�    
 exit# � 
 6�   $
  Hand��6�   $��Bottom=�6�   $��0 0 0 15q�6  5S      5T      � 5W      -   5        $	 ContentsGrid��5W   #   $��1��6   
 5X      �� 5Y      $��0 �6$   �� 5Z      .+�5]      �� $��0��6   $��Right=�6*   $	��Top=�6F   $��0,5,10,0q�6^   $��30��6p   $ 
Horizontal=�6|   $��RightToLeft=�6�   .+�5^      �� -   5        ! ��Name$! btnClose��5^        IconButtonsStyle#��[ 6/    	 close#���  6Y   $ IsCloseButton��6�   �� 5_      -   5        $! 
btnRestore��5_      $	��5,0q�61   ��5`      �� 5a      " �� 
TargetType"" ��6$   # �� BasedOn## [ 6A   .�5b   "   �� $ �� Property$ � ��  maximize% �� Value#% �  6<   & �� Triggers& 5c   "   �� 5d   &   ' �� Binding' 62   �� ( �� ElementName$
( home��) �� Path$) WindowState�* �� Value* 	Maximized6i   .a�5e   *   �� $ � ��  restore#% �  6D   5f   '   5h      5j      �� 5k      -   5        $! btnMinimize��5k      #���  62     minimize#���  6]   6�   5l      �� 5n      $��1��6   $��4,4,4,4q�6&   ._�5p       � -   5        +  �Name$+ PagesNavigation��5p      ,  � NavigationUIVisibility$, Hidden=�65   
������ 5r      5s      �� 5u      $��1��6   - K� ZIndex$- 10��6&   $��Right=�67   $	��Top=�6S   $
��Auto��6k   $
��Auto��6x   ._�5v      �� $��Hidden=�6#   $��Right=�6H   $
��Auto��6d   $��75��6r   .��5w      �� !  HandyControl.Controls.Growl.   GrowlParent. . $	��Top=�6A   $��0,4,8,4q�6Y   $
��Auto��6j   5x      5y      5z      5|      5}      5~      