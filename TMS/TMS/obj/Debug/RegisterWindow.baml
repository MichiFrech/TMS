   M S B A M L   `   `   `  ����U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   > :TMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TMSTMS 5   	      TMS.RegisterWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   S OMahApps.Metro.IconPacks, Version=2.0.0.31, Culture=neutral, PublicKeyToken=null=	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  5   	   localclr-namespace:TMS  5   	   -   5          � Title$  Task Management Software��5
   	      AccentColorBrush#���   6*   $	��0.5q�6[   $��342.302��5   	   $	��484��6    � 
ResizeMode$ NoResize=�6&    � WindowStartupLocation$ CenterScreen=�6X    � Icon$! _images/projects-folder.ico��6}   .��5      � .+�5   
   �� -   5         ��Name$ lbl_firstname��5      $��15(�6'     	firstname#���  65   $��Center=�6[   $
��Left=�6}   $��	21,12,0,0q�6�   $	��Top=�6�   $��46��6�   $	��100��6�   �� 5   
   -   5         ��Name$ tb_firstname��5      $
��Left=�6(   $��Center=�6C   $��29��6e   $��15(�6q   $��
123,15,0,0q�6   $	��Top=�6�   $	��310��6�   6�   .  (MahApps.Metro.IconPacks.PackIconMaterial  5   
   -   5          Name$ firstname_state_icon��5   1   �  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconMaterialKind, MahApps.Metro.IconPacks, Version=2.0.0.31, Culture=neutral, PublicKeyToken=null]]   Kind$
 None=�6%     name_tooltip � ToolTip# �  6O   $��	0,14,12,0q�6x   $��29��6�   $��29��6�   $��Right=�6�   $	��Top=�6�   |�.  6�   �� 5   
   -   5        $ lbl_lastname��5      $��15(�6&     lastname#���  64   $��Center=�6Y   $
��Left=�6{   $��	21,56,0,0q�6�   $	��Top=�6�   $��46��6�   $	��100��6�   �� 5   
   -   5        $��tb_lastname��5      $
��Left=�6%   $��Center=�6@   $��29��6b   $��15(�6n   $��
123,59,0,0q�6|   $	��Top=�6�   $	��310��6�   6�     5   
   -   5        $ lastname_state_icon��5   1   $
 None=�6%   # �  6N   $��	0,58,12,0q�6w   $��29��6�   $��29��6�   $��Right=�6�   $	��Top=�6�   |�.  6�   �� 5   
   -   5        $ lbl_mail��5      $��15(�6"     mail#���  60   $��Center=�6Q   $
��Left=�6s   $��
21,100,0,0q�6�   $	��Top=�6�   $��46��6�   $	��100��6�   �� 5   
   -	   5        $ tb_mail��5      $
��Left=�6#   $��Center=�6>   $��29��6`   $��15(�6l   $��123,103,0,0q�6z   $	��Top=�6�   $	��310��6�   6�     5   
   -
   5        $ mail_state_icon��5   1   $
 None=�6%     mail_tooltip# �  6J   $��
0,102,12,0q�6s   $��29��6�   $��29��6�   $��Right=�6�   $	��Top=�6�   |�.  6�   �� 5   
   -   5        $ lbl_mail_Copy��5      $��15(�6'     password#���  65   $��Center=�6Z   $
��Left=�6|   $��
21,144,0,0q�6�   $	��Top=�6�   $��46��6�   $	��100��6�   G� 5   
   -   5        	 G�Name$	 tb_pw��5      $
��Left=�6%   $��Center=�6@   $��29��6b   $��15(�6n   $��123,147,0,0q�6|   $	��Top=�6�   $	��310��6�   6�     5   
   -   5        $ password_state_icon��5   1   $
 None=�6%     psw_tooltip# �  6N   $��
0,148,12,0q�6v   $��29��6�   $��29��6�   $��Right=�6�   $	��Top=�6�   |�.  6�   �� 5   
   $��15(�6    	 usage#���  6   $��Center=�6@   $
��Left=�6b   $��
21,186,0,0q�6}   $	��Top=�6�   $��46��6�   $	��100��6�   	� 5   
   -   5        
 	�Name$
 rb_pu��5       d� 	IsChecked$
 TrueY�6%   |�.   	 usage_private#��� 	 6H   $
��Left=�6s   $��123,201,0,0q�6�   $	��Top=�6�   	� 5    
   -   5        $
 rb_bu��5       $ FalseY�6%   |�.   
 usage_business#��� 
 6I   $
��Left=�6t   $��123,237,0,0q�6�   $	��Top=�6�   �� 5"   
   -   5         ��Name$ btn_register��5"   I   +   %MahApps.Metro.Controls.ControlsHelper   ContentCharacterCasing$ Normal=�6     register#���  6_     SquareButtonStyle#���  5#      |�.  $
��Left=�6O   $��335,271,0,0q�6j   $	��Top=�5$      $��98��6)   $��30��64   $��1q�6[   )   #MahApps.Metro.Controls.ProgressRing  5%   
   -   5          Name$ progring��5%          IsActive .  $��437,271,9.8,11q�6C   $��30��6[   $��30��6g   �� 5&   
   -   5        $ lbl_msg��5&      $
��Left=�6!   $��
18,274,0,0q�6<   $	��Top=�5'      $��27��6(   $	��335��64   $
��Bold"�6@   5)      (   "MahApps.Metro.Controls.MetroWindow   LeftWindowCommands 5+      +   %MahApps.Metro.Controls.WindowCommands  5,   
   .@�5-      �� -   5        $ 	gobackbtn��5-      |�.  .��5.        $! ArrowLeftBoldHexagonOutline=�6-   5/      50      52      