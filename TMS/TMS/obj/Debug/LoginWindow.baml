   M S B A M L   `   `   `  ����U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   > :TMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TMSTMS 5   	      TMS.LoginWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   S OMahApps.Metro.IconPacks, Version=2.0.0.31, Culture=neutral, PublicKeyToken=null=	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  5   	   localclr-namespace:TMS  5   	   -   5          � Title$  Task Management Software��5
   	      AccentColorBrush#���   6F   $	��0.5q�6w   $	��168��6�   $	��484��6�    � 
ResizeMode$ NoResize=�6�    � WindowStartupLocation$ CenterScreen=�6�    � Icon$! _images/projects-folder.ico��6�   .��5      � .+�5   
   �� $��15(�6     mail#���  6   $��Center=�6?   $
��Left=�6a   $��	23,12,0,0q�6|   $	��Top=�6�   $��46��6�   $	��100��6�   �� 5   
   -   5         ��Name$ tb_mail��5      $��Center=�6#   $��29��6E   $��15(�6Q     mail_tooltip � ToolTip# �  6_   $��
123,15,0,0q�6�   $	��Top=�6�   $
��Left=�6�   $	��310��6�   .  (MahApps.Metro.IconPacks.PackIconMaterial  5   
   -   5          Name$ mail_state_icon��5   1   �  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconMaterialKind, MahApps.Metro.IconPacks, Version=2.0.0.31, Culture=neutral, PublicKeyToken=null]]   Kind$
 None=�6%   $��	0,14,12,0q�6J   $��29��6]   $��29��6i   $��Right=�6u   $	��Top=�6�   �� 5   
   $��15(�6     password#���  6   $��Center=�6C   $
��Left=�6e   $��	23,54,0,0q�6�   $	��Top=�6�   $��46��6�   $	��100��6�   G� 5   
   -   5         G�Name$ tb_pw��5      $
��Left=�6%   $��Center=�6@   $��29��6b   $��15(�6n     psw_tooltip# �  6|   $��
123,59,0,0q�6�   $	��Top=�6�   $	��310��6�   6�     5   
   -   5        $ password_state_icon��5   1   $
 None=�6%   $��	0,58,12,0q�6N   $��29��6a   $��29��6m   $��Right=�6y   $	��Top=�6�   �� 5   
   -   5        	 ��Name$	 	btn_login��5      +   %MahApps.Metro.Controls.ControlsHelper
   ContentCharacterCasing$
 Normal=�6$    	 login#���  6\   |�.    SquareButtonStyle#���  6�   $
��Left=�6�   $��
335,98,0,0q�6�   $	��Top=�6�   $��98��6  $��30��6  $��1q�62  �� 5   
   -   5         ��Name$ lbl_msg��5        msg_err#���  6!   $
��Left=�6E   $��
18,102,0,0q�6`   $	��Top=�6t   $	��317��6�   $��27��6�   $
��Bold"�6�   $y�Hidden=�6�   )   #MahApps.Metro.Controls.ProgressRing  5   
   -   5          Name$ progring��5          IsActive .  $��438,98,8.8,9.6q�6C   $��30��6[   $��30��6g   5      (   "MahApps.Metro.Controls.MetroWindow   LeftWindowCommands 5      +   %MahApps.Metro.Controls.WindowCommands  5   
   .@�5      �� -	   5        $	 	GoBackBtn��5      |�.  .��5        $! ArrowLeftBoldHexagonOutline=�6-   5      5       5"      