   M S B A M L   `   `   `  ����U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      fCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.MetroMahApps.Metro.Controls.Dialogs  5      > :TMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:TMS.views	TMS.views 5
      K GApex, Version=1.5.0.0, Culture=neutral, PublicKeyToken=98d06957926c086d;)clr-namespace:Apex.Controls;assembly=ApexApex.Controls 5         TMS.views.ProjectView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      VDialogCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.Metro     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      S OMahApps.Metro.IconPacks, Version=2.0.0.31, Culture=neutral, PublicKeyToken=null=	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  5	      #localclr-namespace:TMS.views  5
      4apex)clr-namespace:Apex.Controls;assembly=Apex  5      -   5        8   2MahApps.Metro.Controls.Dialogs.DialogParticipation    Register  5      �� .��5        Apex.Controls.ApexGrid     Rows$ *,Auto,Auto��6      Columns$ *,Auto��6'   $��Stretch=�68   .+�5   
   �� $��2��6   $��0��6   $��2��6.   $��50��6B   ���	#FF484848._�5      ]� $��Stretch=�6   $��Stretch=�66   ��.   � 	Resources 5       �� 
TargetType   MetroButtonStyle �� BasedOn �� Property  
WhiteBrush �� Value��%�   '��       7   �� 5      " ��6   8   .�5      ��  � ��# �  67   �� 5       � ��$ RIght=�6;   5      5      .+�5      �� -   5         ��Name$ btn_del��5   $   $��40��6   $��40��65   
�������  delete_task_tooltip	 � ToolTip#	 �  6T     DeleteFigure#��[ 6�   {�.  6�   �� 5      -   5        $ btn_save��5   $   $��40��6   $��40��66   
�������  save_task_tooltip#	 �  6U     
SaveFigure#��[ 6�   {�.  6�   �� 5      -   5        $ btn_add��5   $   $��40��6   
�������  add_task_tooltip#	 �  6H     	NewFigure#��[ 6�   {�.  6�   �� 5      -   5        
 ��Name$
 	lbl_error��5      $��Right=�6+   
���  ��$��Bottom=�6S    	 error#���  6n   $y�Hidden=�6�   �� 5      -   5        $ 
btn_filter��5      $��40��6F   $��40��6Q   $��5q�6]   $��Center=�6h   $
��Left=�6�   
������� 	 filter_task_tooltip#	 � 	 5    6    
 FilterFigure#��[
 6f   {�.  6�   �� 5!      -   5        $ btn_refresh��5!      $��40��6.   
�������  refresh_tooltip#	 �  6L   $
��Left=�6x     RefreshFigure#��[ 6�   {�.  6�   5#      5$      � 5&   
    � RowDefinitions 5'      �� 5(      $��90 �6    �� 5)      $��* �6    .+�5+      �� $��0��6   ���	#FF4848486%   �� 5,      -   5         ��Name$ dropdown_allprojects��5,      $��0��65   $��22(�6{    �� MaxDropDownHeight$	 201��6�   $
��Left=�6�   $��Stretch=�6�   $	��270��6�   $��
20,20,0,20q�6�   �� 5-      -	   5        $
 proj_due��5-      $��0��6&   $�� ��63   $��25(�6>   $
��Left=�6L   $��Center=�6g   $��300,15,0,15q�6�   $
��Bold"�6�   )   #MahApps.Metro.Controls.ToggleSwitch  5.      -
   5          Name$ notific��5.   $   {�.  $��0��6G     notification#���  6T   $	��Top=�6|   $��Right=�6�      OffLabel$	 OFF��6�      OnLabel$ ON��6�   
���   �$��
0,15,240,0q�6�   �� 5/      -   5        $ 	btn_print��5/      {�.   MetroCircleButtonStyle#���  6e   $��0��6�   $��Right=�6�   $��Center=�6�   $��66��6�   $��66��6�   $��0,15,165,15q�6	  ��6(   /  )MahApps.Metro.IconPacks.MaterialExtension  �  �MahApps.Metro.IconPacks.PackIconExtension`2[[MahApps.Metro.IconPacks.PackIconMaterial, MahApps.Metro.IconPacks, Version=2.0.0.31, Culture=neutral, PublicKeyToken=null],[MahApps.Metro.IconPacks.PackIconMaterialKind, MahApps.Metro.IconPacks, Version=2.0.0.31, Culture=neutral, PublicKeyToken=null]]   Kind$ Printer=�6	  �� 50      -   5        $ 
btn_invite��50      {�.  #���  6e   $��0��6�   $��Right=�6�   $��Center=�6�   $��66��6�   $��66��6  $��
0,15,90,15q�6  ��6)   -  'MahApps.Metro.IconPacks.ModernExtension  �  �MahApps.Metro.IconPacks.PackIconExtension`2[[MahApps.Metro.IconPacks.PackIconModern, MahApps.Metro.IconPacks, Version=2.0.0.31, Culture=neutral, PublicKeyToken=null],[MahApps.Metro.IconPacks.PackIconModernKind, MahApps.Metro.IconPacks, Version=2.0.0.31, Culture=neutral, PublicKeyToken=null]]   Kind$ UserAdd=�6  �� 51      -   5        $ btn_history��51      {�.  #���  6h   $��0��6�   $��Right=�6�   $��Center=�6�   $��66��6�   $��66��6  $��
0,15,15,15q�6  ��6*     $ History=�6  &   System.Windows.Controls.DataGrid  53      -   5          Name$ dgrid��53      $��0��6&   $��5,5,0,0q�66   $��Stretch=�6G   $��1��6e   $��Stretch=�54         CanUserAddRows .     CanUserResizeColumns .     CanUserSortColumns .  $��14(�55         AutoGenerateColumns .  ��6%   �� *
AllTasks+   Columns 56      4	  .System.Windows.Controls.DataGridTemplateColumn	  57       	  CellTemplate 58      �� 59      .R�5:   "   �� -   5         d� 	IsChecked 5:   +   �� *Done+ �� UpdateSourceTrigger$ PropertyChanged=�6k   5;      5=      0
  *System.Windows.Controls.DataGridTextColumn
  5?      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 500 6)     	task_grid   Header# �  6N      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Name+6s   
  5@      $	 170 6)     deadline# �  6R    .  65   �� *
Deadline+6v   
  5A      $	 340 6)     assignees_task# �  6S    .  65   �� *	Assignees+6}   5C      5D      ]� 5F   
   $��0��6   $��1��6!   $��2��61   .+�5G      �� -   5          ��Name$  gb_add��5G   0   $	��420��6   $	��320��6#     add_task_gp#���  6@   $	��Top=�5I      ! � DataContext! 5H      �� *SelectedTransaction+" �� NotifyOnSourceUpdated" . # �� Mode$# OneWay=�5J      �� 5K      -   5        $  gb_settings��5K        settings_task_gp#���  6,   .��5L      �� $	��Top=�6   $
��Auto=�65   5M      5N      5O      5P      5Q      