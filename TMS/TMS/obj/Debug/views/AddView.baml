   M S B A M L   `   `   `  ����>  :TMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:TMS.views	TMS.views  5          TMS.views.AddView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:TMS.views     5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      .��5
      �   � ColumnDefinitions  5   
   �� 5      $	��115 �6   �� 5      $��* �6    � 	Resources 5   
   ��%    5      .+�5   
   ��    task#���   6   $��13(�61   $
��Left=�6?   $	��Top=�6Z   $��15,15,15,15q�6r   $��0��6�   �� 5   
   -   5         ��Name$ tb_desc��5      $
��Auto��6<   $
��Left=�6I   $��Center=�6k   $��Stretch=�6�   $	��Top=�6�   $��
0,15,15,15q�6�   $��1��6�   �� 5   
     deadline_task#���  6   $��13(�6:   $
��Left=�6H   $	��Top=�6c   $��15,60,15,15q�6{   $��0��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dp_deadline��5         FirstDayOfWeek$ Monday=�6*   $��
0,60,15,15q�6B   $��Stretch=�6V   $	��Top=�6t   $��1��6�   $
��Auto��6�     date_tooltip*  $MahApps.Metro.Controls.TextBoxHelper   	Watermark# �  6�   �� 5   
     members_task#���  6   $��13(�69   $
��Left=�6G   $	��Top=�6b   $��15,105,15,15q�6z   $��0��6�   �� 5   
   -   5         ��Name$ listView��5      $��Stretch=�6%     select_members# �  6p    �� SelectionMode$ Multiple=�5      $��0,105,15,15q�6,   $	��Top=�6A   $��1q�6Y   $��1��6m   $	��160��6}   
�������6�   5      5      