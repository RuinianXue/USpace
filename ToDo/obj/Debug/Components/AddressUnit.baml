   M S B A M L   `   `   `  ����D  @UIDisplay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:UIDisplay.ComponentsUIDisplay.Components  5      &     UIDisplay.Components.AddressUnit   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:UIDisplay.Components     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      -   5        z�5
      T� 5   
     T� Y$  0Q�6   .��5      ��  �� CornerRadius$ 10��6   $	��200��6   $	��200��6+   ��5   
   �� 5       �� Color$ #1873e2��6    �� Opacity$ 0Q�6.   ._�5   
   �� -   5         ��Name$ 
mainBorder��5      $ 10��6%      SecondaryBackgroundColor#��[  67    J� Opacity$	 0.7Q�6n   $	��180��6|   $	��180��6�    � Cursor$
 Hand��6�    � Triggers 5      :� 5       :� RoutedEvent$ Border.Loaded��6   .[�5      �� .��5      �� -   5        	 ��Name$	 ShowUnit��5   %   .��5      U� 
 �� 
TargetName$
 
mainBorder��6<    �� TargetProperty$ Height�6_   .\�5   "   9  3System.Windows.Media.Animation.EasingDoubleKeyFrame   O� KeyTime$ 0:0:0��67    O� Value$ 0Q�6G      EasingFunction 5   &   /  )System.Windows.Media.Animation.CircleEase  5   *   7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode$ 	EaseInOut=�65   5   #     5   "   $ 0:0:0.3��67   $	 180Q�6I    5   &     5   *   $ 	EaseInOut=�65   5    #   5!      5"      5#      5$      ._�5&      �  � RowDefinitions 5'      �� 5(      $
��3.5* �6$   �� 5)      �� 5*      �� 5+      .+�5-      �� -   5        $ 	imgBorder��5-        
BorderClip#��[ 6,   $��0��6P   $ 10��6]   $��	10 5 10 4q�6o   ._�5/      �� -   5         ��Name$	 img��5/      $5��/http://s4u6u3ckk.hn-bkt.clouddn.com/default.jpg��6)   $��UniformToFill=�6b   50      �� 52      $��1��6    �� Orientation$ 
Horizontal=�6*   $
��Left=�6C   .+�53      &   HandyControl.Controls.SimpleText     Text$ 姓名:��6$   $��Center=�6/   $��5 0 0 0q�6J     TextPrimaryColor   
Foreground# �  6[     54      -   5          Name$ 	nameLabel��54   ?   $��Center=�6$   $ 
Star-tears��6R   $��5 0 0 0q�6d   # �  6u   55      �� 58      $��2��6   $ 
Horizontal=�6*   $
��Left=�6C   .+�59        $ 电话:��6%   $��Center=�60   $��5 0 0 0q�6K   # �  6\     5:      -   5        $ 
phoneLabel��5:   ?   $��Center=�6$   $ 123456��6S   $��5 0 0 0q�6b   # �  6s   5;      �� 5>      $��3��6   $ 
Horizontal=�6*   $
��Left=�6C   .+�5?        $ 邮箱:��6%   $��Center=�60   $��5 0 0 0q�6K   # �  6\     5@      -   5        $ 
emailLabel��5@   ?   $��Center=�6$   $ nicai@qq.com��6S   $��5 0 0 0q�6h   # �  6y   5A      5C      5D      5E      5F      