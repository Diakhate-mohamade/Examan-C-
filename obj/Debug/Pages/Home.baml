   M S B A M L   `   `   `  ����P  LWPFModernVerticalMenu, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:WPFModernVerticalMenu.PagesWPFModernVerticalMenu.Pages  5      &     WPFModernVerticalMenu.Pages.Home   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:WPFModernVerticalMenu.Pages     5        N� Title$
  Home��5      $	��499��6   $	��821��6!   .��5	      ��  �� CornerRadius$ 5��6      SecundaryBackgroundColor#���   6   ._�5
   
   � $��	0,0,0,-23q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��Gestion des Utilisateurs��6   $��30(�68     PrimaryTextColor#���  6F   $
��Bold"�6v   $��Center=�5      $��Center=�63   $��	0,10,0,20q�6P   �� 5      $��1��6   $��20q�6&   .+�5      �� -   5        $��Charger les utilisateurs��5      $��0,0,0,10q�6^   �� 5      -   5         ��Name$ UtilisateursListBox��5      $	��200��67   $��0,0,0,10q�6D   ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6)   $��5q�6B   .+�5   "   �� $	��100��6A   $��5q�6M   ��6,   �� *Nom+6M   �� 5   "   $	��100��6D   $��5q�6P   ��6,   �� *Prenom+6P   �� 5   "   $��50��6A   $��5q�6L   ��6,   �� *Age+6L   5      5      5      �� 5       $��0,0,0,5q�6   .+�5!      � $��0,0,0,5q�6   .+�5"      �� -   5         ��Name$ 
NomTextBox��5"   "   $	��200��66   $��0,0,0,5q�6B   �� 5#      $	��Nom��6$   $��Center=�6/   $
��Left=�6J   $��5,0,0,0q�6e   
�������6v   5$      � 5%      $��0,0,0,5q�6   .+�5&      �� -   5        $ PrenomTextBox��5&   "   $	��200��69   $��0,0,0,5q�6E   �� 5'      $��Prénom��6$   $��Center=�62   $
��Left=�6M   $��5,0,0,0q�6h   
�������6y   5(      � 5)      $��0,0,0,5q�6   .+�5*      �� -   5        $ 
AgeTextBox��5*   "   $	��200��66   $��0,0,0,5q�6B   �� 5+      $
��Âge��6$   $��Center=�6/   $
��Left=�6J   $��5,0,0,0q�6e   
�������6v   5,      5-      �� 5.      $ 
Horizontal=�6   $��Center=�66   .+�5/      �� -   5        $��Ajouter��5/      $	��100��6P   $��5q�6\   �� 50      -   5        $��Mettre à jour��50      $	��100��6Z   $��5q�6f   �� 51      -   5        $��	Supprimer��51      $	��100��6T   $��5q�6`   52      53      54      55      56      