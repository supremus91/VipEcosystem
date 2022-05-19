Imports System.Data.SqlClient
Imports System.Data.OleDb
'Imports Microsoft.Office.Interop.Excel
Imports System.Threading
Imports System
Imports System.IO
Imports System.Text
Imports System.Net
Imports System.Security.AccessControl
Imports System.Net.Mail
Imports System.Runtime.InteropServices
Imports System.IO.Ports
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Net.Sockets
Imports Microsoft.Office.Interop
Imports System.Globalization
Imports System.Windows.Forms.DataVisualization.Charting
Imports DevExpress.XtraReports.UI
Imports System.Collections
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing
Imports MaterialSkin.Animations
Imports MaterialSkin.Controls
Imports MaterialSkin
Imports SavingUserSettings.Properties
Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.Reflection
Imports System.Security.Principal




Module Variables


    Public form_parametri As FormParametri
    Public form_datasheet As Datasheet
    Public form_DatasheetMenu As DatasheetMenu
    Public form_Archivio As ArchivioDS

    Public file_tot_file As String = "Totale_clienti.txt"
    Public file_storico As String = "Storico.txt"
    Public Folder_PC_storage As String = System.IO.Directory.GetCurrentDirectory()
    Public All_client_bk(10000) As String '10000 è il numero di clienti ipotizzato --> se supera il limite ---> aumentare
    Public abilita_grid_click As Integer
    Public abilita_grid_click1 As Integer
    Public numero_ventilatori As Integer = 0
    Public time_tick_del As Integer = 0

    Public client_DF(10000) As String
    Public client_PF(10000) As String
    Public client_PNF(10000) As String
    Public client_ATX(10000) As String
    Public client_SF(10000) As String
    Public client_IND(10000) As String
    Public client_OFF(10000) As String
    Public client_SEA(10000) As String
    Public client_target(10000) As String


    Public ftp As String = "ftp://vipfan.ddns.net/UtentiRegistrati"
    Public ftp1 As String
    Public user As String = "admin"
    Public pass As String = "vip2010$"
    Public utente_sel As Integer = 0
    Public text_UP As String
    Public file_del As String
    Public stato_user As Integer
    Public email_send As String
    Public nome_cliente As String
    Public cognome_cliente As String
    Public Nazione_cliente As String
    Public attivi As Integer
    Public Num_ID As Integer
    Public crown_assign As Integer
    Public crown_max As Integer
    Public file_name_target(10000) As String
    Public file_data_target(10000) As String

    Public file_name_target_order(10000) As String
    Public file_data_target_order(10000) As String


    Public tot_projects_VD As Integer
    Public tot_PF_VD As Integer
    Public tot_DF_VD As Integer
    Public tot_PN_VD As Integer
    Public tot_SF_VD As Integer
    Public tot_ATX_VD As Integer
    Public tot_ind_VD As Integer
    Public tot_off_VD As Integer
    Public tot_sea_VD As Integer



    'Vettore
    Public Utente_selezionato(15) As String
    Public user_err As String
    Public Segnale_DA As Integer

    'indice 0 raccoglie il totale delle progettazioni

    'indice 0 Lorenzo
    'indice 1 Stefano
    'indice 2 Paolo
    'indice 3 Alberto
    'indice 4 Fausto
    'indice 5 Roberto

    Public posizione As Integer
    Public posizione1 As Integer

    Public uff_count As Integer
    Public tot_projects_VD_Uff_tec As Integer

    Public tot_projects_DF_VD_Uff_tec As Integer
    Public tot_projects_PF_VD_Uff_tec As Integer
    Public tot_projects_PN_VD_Uff_tec As Integer
    Public tot_projects_SF_VD_Uff_tec As Integer
    Public tot_projects_ATX_Uff_tec As Integer
    Public tot_projects_IND_Uff_tec As Integer
    Public tot_projects_OFF_Uff_tec As Integer
    Public tot_projects_SEA_Uff_tec As Integer

    Public tot_projects_DF_VD_client As Integer
    Public tot_projects_PF_VD_client As Integer
    Public tot_projects_PN_VD_client As Integer
    Public tot_projects_SF_VD_client As Integer
    Public tot_projects_ATX_client As Integer
    Public tot_projects_IND_client As Integer
    Public tot_projects_OFF_client As Integer
    Public tot_projects_SEA_client As Integer

    Public tot_projects_DF_VD_TOT As Integer
    Public tot_projects_PF_VD_TOT As Integer
    Public tot_projects_PN_VD_TOT As Integer
    Public tot_projects_SF_VD_TOT As Integer
    Public tot_projects_ATX_TOT As Integer
    Public tot_projects_IND_TOT As Integer
    Public tot_projects_OFF_TOT As Integer
    Public tot_projects_SEA_TOT As Integer
    Public tot_projects As Integer


    '(20) indica il numero massimo di utenti ufficio tecnico, per aumentare basta cambiarlo a tutti
    Public tot_PF_VD_Uff_tec(20) As Integer
    Public tot_DF_VD_Uff_tec(20) As Integer
    Public tot_PN_VD_Uff_tec(20) As Integer
    Public tot_SF_VD_Uff_tec(20) As Integer
    Public tot_ATX_VD_Uff_tec(20) As Integer
    Public tot_ind_VD_Uff_tec(20) As Integer
    Public tot_off_VD_Uff_tec(20) As Integer
    Public tot_sea_VD_Uff_tec(20) As Integer
    Public tot_Uff_tec(20) As Integer

    Public max_Uff_tec As Integer

    Public sx As Integer = 0


    'Vettori di salvataggio variabili di statistiche globali

    Public matrix_storico(10000, 17) As String



    Public line_storico(10000) As String

    Public line_storico_cliente_DF(10000) As String
    Public line_storico_cliente_PF(10000) As String
    Public line_storico_cliente_PNF(10000) As String
    Public line_storico_cliente_ATX(10000) As String
    Public line_storico_cliente_SF(10000) As String
    Public line_storico_cliente_IND(10000) As String
    Public line_storico_cliente_OFF(10000) As String
    Public line_storico_cliente_SEA(10000) As String

    Public line_storico_uff_tec_DF(10000) As String
    Public line_storico_uff_tec_PF(10000) As String
    Public line_storico_uff_tec_PNF(10000) As String
    Public line_storico_uff_tec_ATX(10000) As String
    Public line_storico_uff_tec_SF(10000) As String
    Public line_storico_uff_tec_IND(10000) As String
    Public line_storico_uff_tec_OFF(10000) As String
    Public line_storico_uff_tec_SEA(10000) As String

    Public line_storico_iscritti(10000) As String


    '*********************************************************STORICO UFFICIO TECNICO*******************************************************************
    Public Vettore_stat_uffico_tecnico(10, 7) As String '0 Fasuto ,1 Paolo, 2 Stefano, 3 Roberto, 4 Alberto, 5 Lorenzo; ///// 0 Nome, 1 PJaperte, 2 PJ chiuse, 3 VipDesigner, 4 Contributi, 5 Databook, 6 lv
    Public stelle_medie As Double
    Public numero_convalide_attese As Integer


    Public exp_val As Double
    Public exp_tot As Integer
    Public level_val As Integer

    Public exp_PJ_aperte As Integer
    Public exp_PJ_chiuse As Integer
    Public exp_VipDes As Integer
    Public exp_contributi As Integer
    Public exp_DS As Integer


    Public boost_aperte As Integer
    Public lv_aperte As Integer

    Public curse_dev As Integer
    Public lv_curse As Integer

    Public boost_chiuse As Integer
    Public lv_chiuse As Integer

    Public boost_VIP As Integer
    Public lv_VIP As Integer

    Public boost_con As Integer
    Public lv_con As Integer

    Public boost_DS As Integer
    Public lv_DS As Integer

    Public exp_estremi(100, 1) As Integer
    Public estremo_exp As Integer

    Public pre_carica_lv As Integer = 1
    Public livello_user As Integer

    Public hide_var As Integer = 0

    '*****************************************************************************************************************************************************

    Public num_lines As Integer



    'Variabili database condiviso
    'Public provider As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
    'Public dataFile As String = "C:\Users\Lorenzo.VIPAIR\Desktop\VipProject\NewPJ.accdb"
    'Public folders_directory As String = "C:\Users\Lorenzo.VIPAIR\Desktop\VipProject"

    Public provider As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
    Public dataFile As String = "H:\Comune\Applicazioni\VipProject\NewPJ.accdb"
    Public dataFile_RefExcel As String = "H:\Comune\Applicazioni\SW_excell\DataBaseSW.accdb"
    Public folders_directory As String = "H:\Comune\Applicazioni\VipProject"
    Public folders_directoryDS As String = "H:\Comune\Applicazioni\Archivio Datasheet"
    Public modulo_PJ As String = "H:\Passaggio\Paolo\Excel di lavoro\Moduli\Modulo di Progettazione Ventilatori Assiali.xlsm"
    Public File_SW As String = "H:\Comune\Applicazioni\SW_excell\FileSW.xlsx"
    Public constring As String = provider & dataFile
    Public constring_RefExcel As String = provider & dataFile_RefExcel


    Public cliente_new As String
    Public nome_progetto As String
    Public fase_load As Integer
    Public Grid_N_offerta As String
    Public Grid_Descrizione As String
    Public Grid_tipo_richiesta As String
    Public Grid_Cliente As String
    Public Grid_riferimento As String
    Public Grid_richiesta As String
    Public Grid_diametro As String
    Public Grid_Portata As String
    Public Grid_Pressione As String
    Public Grid_assegnata As String
    Public Alimentazione As String

    'Variabili che definiscono le dimensioni dei vettori o matrici --> CERCARE UNA SOLUZIONE PIU SMART (CHE SI AUTODIMESIONI COL DB)
    Public Numero_colonneDBtot As Integer = 250
    Public Numero_righeDBtot As Integer = 10000


    'Connessione ad SQL
    Public connSQL As New SqlConnection("Data Source=VIPWS01\NTS;Initial Catalog=VIP;User ID=sa; pwd=Sipi1Business") 'basically connection string
    Public ds As New DataSet
    Public col As New AutoCompleteStringCollection

    Public new_PJVar As Integer = 0
    Public numero_progetti As Integer
    Public Nome_colonne(Numero_colonneDBtot) As String
    Public Nome_colonneDS(Numero_colonneDBtot) As String
    Public Valore_colonne(Numero_colonneDBtot) As String
    Public Descrizione_colonne(Numero_colonneDBtot) As String
    Public Descrizione_colonneDS(Numero_colonneDBtot) As String
    Public Numero_colonneDB As Integer
    Public Numero_colonneDS As Integer
    Public numero_righeDB As Integer
    Public SchemaDB(Numero_colonneDBtot) As String
    Public Valore_CellaRiga(Numero_colonneDBtot) As String
    Public Valore_CellaRigaDS(Numero_colonneDBtot) As String
    Public Valore_CellaRigaDS_star(Numero_colonneDBtot) As String
    Public valore_DS_star As String

    Public Numero_tabelle As Integer
    Public numero_note_totali As Integer = 100

    Public vettore_elemento_cbx(80) As String
    Public ID_note_vect(Numero_righeDBtot) As String
    Public Data_note_vect(Numero_righeDBtot) As String
    Public User_note_vect(Numero_righeDBtot) As String
    Public Rev_note_vect(Numero_righeDBtot) As String
    Public note_vect(Numero_righeDBtot) As String
    Public all_tables(80, 300) As String
    Public sblocco_take_tabs As Integer = 1
    Public L_vettore As Integer
    Public posizione_grid_note As Integer
    Public N_rev_note As String

    'Variabile che contiene tutto il progetto
    Public tables1 As DataTableCollection
    Public tables2 As DataTableCollection
    Public filtroTab1 As String
    Public mod_filtro As Integer = 0
    Public filterAfterLoad As Integer = 0

    Public prog_rev(Numero_righeDBtot, 4) As String
    Public prog_rev1(Numero_righeDBtot, 4) As String
    Public num_righeDB As Integer
    Public num_chiuse As Integer
    Public num_lav As Integer
    Public num_cod As Integer
    Public num_attesa As Integer
    Public num_codificata As Integer

    Public numeroPJ As Integer
    Public posizione_progetto As Integer


    'aggiornamento database
    Public vettore_aggiornamento(Numero_colonneDBtot) As String
    Public vettore_nomi_aggiornamento(Numero_colonneDBtot) As String
    Public aggiunta_val As Integer

    Public skip_rev_cbx_change As Integer = 0
    Public data_progetto As String
    Public rad_true As Integer = 0
    Public jump_rev As Integer = 1
    Public jump_rev0 As Integer = 0

    Public testRTF As String
    Public testRTF1 As String
    Public testNote1 As String
    Public testRTF2 As String

    Public nome_macchina As String = System.Windows.Forms.SystemInformation.UserName
    Public userNum As Integer
    Public userNameLock As String


    Public time_val As Integer
    Public time_val1 As Integer
    Public blocco_load_imm As Integer = 0
    Public blocca_controllo_vip As Integer = 0

    'sblocco VipDesigner
    Public Set_TipoFan As Integer = 0
    Public Set_Config As Integer = 0
    Public Set_App As Integer = 0
    Public Set_Atex_Safe As Integer = 0
    Public Set_Poli As Integer = 0
    Public Set_client As Integer = 0
    Public Set_TipoRichiesta As Integer = 0
    Public Set_Quantita As Integer = 0
    Public Set_Rif As Integer = 0
    Public Set_Descrizione As Integer = 0
    Public Set_Portata As Integer = 0
    Public Set_Pressione As Integer = 0
    Public Set_DirFlusso As Integer = 0
    Public Set_applicazione As Integer = 0
    Public Set_Min As Integer = 0
    Public Set_Max As Integer = 0
    Public Set_TipoMot As Integer = 0
    Public Set_Alim As Integer = 0
    Public Set_Volt As Integer = 0
    Public Set_Freq As Integer = 0

    Public Codice_ventilatore As String

    Public time222 As Integer = 0

    'variabili di salvataggio progetto
    Public save_session(500) As String

    Public new_project As Integer = 0
    Public mod_rev0 As Integer
    Public on_load As Integer = 0 'processi da eseguire solo all'avvio del programma
    Public primo_avvio As Integer = 1
    Public jump_tipo_richiesta As Integer = 0
    Public jump_tipo_richiesta0 As Integer = 0


    'ricerca valore in tabella --> ricerca_valore_tab
    Public LL1 As Integer
    Public pos_vect1 As Integer
    Public ggg As Integer


    Public OrdineRic As Integer = 0
    Public prezzoPJ As String
    Public fast_PJ As Integer = 0
    Public mod_new_progetto As Integer = 0


    Public Enter_call As Integer = 0



    Public numero_revisioni As Integer
    Public New_rev As Integer
    Public lockPJ As Integer

    Public vettore_revisioni(100) As Integer

    Public check_richiesta As Integer = 0
    Public no_modificaCLrev As Integer = 0

    Public cable_ACT As Integer = 0
    Public cert_ACT As Integer = 0
    Public acc_ACT As Integer = 0


    Public accessories_load As Integer
    Public num_rev_generale As Integer
    Public Timer_accessories As Integer
    Public memo_text_cbx As ComboBox
    Public num_rev As Integer


    'variabili per la modalita' save
    Public check_ReteLatoMotore As Integer = 0
    Public check_ReteLatoVentola As Integer = 0
    Public check_ULCSA As Integer = 0
    Public check_IECEX As Integer = 0
    Public check_EAC As Integer = 0
    Public check_CUTR As Integer = 0
    Public check_NEMA As Integer = 0




    '---------------------------------------MODALITA' DATASHEET---------------------------------------------------------
    Public load_var As Integer
    Public objExcel_BASSA As Object
    Public objExcel_ALTA As Object
    Public name_file_BASSA As String
    Public name_file_ALTA As String
    Public S_D_var As Integer
    Public errore_selezione_file As Integer
    Public num_max_config As Integer = 50
    Public conf_sel(num_max_config) As String
    Public des_sel(num_max_config) As String
    Public part_sel(num_max_config) As String
    Public part_sel1(num_max_config) As String 'utilizzato per tenere tutte le selezioni
    Public num_conf_sel As Integer
    Public descrizione_fan As String
    Public true_eff As Double
    Public data As String
    Public ora As String
    Public directory_exc1 As String
    Public directory_exc2 As String
    Public directory_exc1_completa As String
    Public directory_exc2_completa As String
    Public sel_config(num_max_config) As String
    Public PJ_ref As String
    Public PJ_ref_star As String
    Public blocca_add As Integer
    Public ID_conf As Integer
    Public ID_amb As Integer
    Public cbx_AtexProtezioneDS As String
    Public cbx_AtexCustodiaDS As String
    Public cbx_AtexCategoriaDS As String
    Public cbx_AtexClasseTemperaturaDS As String
    Public Tmax_exc As String
    Public eccezione_stampa As Integer = 1
    Public tb_DS As Guna.UI2.WinForms.Guna2TextBox
    Public traduzione_ventilatore As String
    Public ERP_check_var As Boolean


    Public mod_fan As String ' unificato, atex o vip
    Public Datasheet_print_mode As Integer = 0
    Public fine_stamapa As Integer = 0


    'Tabella bassa Velocita'
    Public portata_Excel_bassa(100) As Integer
    Public Ptot_Excel_bassa(100) As Integer
    Public Pstat_Excel_bassa(100) As Integer
    Public RPM_Excel_bassa(100) As Integer
    Public Power_Excel_bassa(100) As Double
    Public Curr_Excel_bassa(100) As Double
    Public len_bassa As Integer
    Public bassa_find As Integer
    Public numero_fasi_bassa As String
    Public test_numeroDS As String
    Public tipo_testDS As String

    'Tabella alta velocita'
    Public portata_Excel_alta(100) As Integer
    Public Ptot_Excel_alta(100) As Integer
    Public Pstat_Excel_alta(100) As Integer
    Public RPM_Excel_alta(100) As Integer
    Public Power_Excel_alta(100) As Double
    Public Curr_Excel_alta(100) As Double
    Public numero_fasi_alta As String
    Public potenza_installata As String
    Public frame_sel As String

    Public pto1_bassa(7) As Double
    Public pto2_bassa(7) As Double
    Public pto3_bassa(7) As Double
    Public pto1_alta(7) As Double
    Public pto2_alta(7) As Double
    Public pto3_alta(7) As Double


    Public pto_lavoro_bassa(10) As Double
    Public pto_lavoro_alta(10) As Double

    Public len_alta As Integer
    Public alta_find As Integer


    Public Test_numero_bassa As String
    Public Test_numero_alta As String
    Public descrizione_prova As String

    Public freq_bassa_N As String
    Public freq_alta_N As String
    Public tensione_bassa_N As String
    Public tensione_alta_N As String
    Public tensione_bassa_T As String
    Public tensione_alta_T As String


    'ERP 2013 bassa
    Public Max_eff_ERP2013_bassa As Double
    Public target_eff_ERP2013_bassa As Double
    Public pow_ERP2013_bassa As Double
    Public Q_ERP2013_bassa As Double
    Public P_ERP2013_bassa As Double
    Public RPM_ERP2013_bassa As Double
    Public Cat_prova_ERP2013_bassa As String
    Public cat_eff_ERP2013_bassa As String


    'ERP 2015 bassa 
    Public Max_eff_ERP2015_bassa As Double
    Public target_eff_ERP2015_bassa As Double
    Public pow_ERP2015_bassa As Double
    Public Q_ERP2015_bassa As Double
    Public P_ERP2015_bassa As Double
    Public RPM_ERP2015_bassa As Double
    Public Cat_prova_ERP2015_bassa As String
    Public cat_eff_ERP2015_bassa As String


    'ERP 2013 alta
    Public Max_eff_ERP2013_alta As Double
    Public target_eff_ERP2013_alta As Double
    Public pow_ERP2013_alta As Double
    Public Q_ERP2013_alta As Double
    Public P_ERP2013_alta As Double
    Public RPM_ERP2013_alta As Double
    Public Cat_prova_ERP2013_alta As String
    Public cat_eff_ERP2013_alta As String

    'ERP 2015 alta 
    Public Max_eff_ERP2015_alta As Double
    Public target_eff_ERP2015_alta As Double
    Public pow_ERP2015_alta As Double
    Public Q_ERP2015_alta As Double
    Public P_ERP2015_alta As Double
    Public RPM_ERP2015_alta As Double
    Public Cat_prova_ERP2015_alta As String
    Public cat_eff_ERP2015_alta As String

    Public Q1_DS As String
    Public Q2_DS As String
    Public Q3_DS As String
    Public Q4_DS As String
    Public Q5_DS As String
    Public Q6_DS As String

    Public P1_DS As String
    Public P2_DS As String
    Public P3_DS As String
    Public P4_DS As String
    Public P5_DS As String
    Public P6_DS As String

    Public RPM1_DS As String
    Public RPM2_DS As String
    Public RPM3_DS As String
    Public RPM4_DS As String
    Public RPM5_DS As String
    Public RPM6_DS As String

    Public POW1_DS As String
    Public POW2_DS As String
    Public POW3_DS As String
    Public POW4_DS As String
    Public POW5_DS As String
    Public POW6_DS As String

    Public CURR1_DS As String
    Public CURR2_DS As String
    Public CURR3_DS As String
    Public CURR4_DS As String
    Public CURR5_DS As String
    Public CURR6_DS As String

    Public LWA1_DS As String
    Public LWA2_DS As String
    Public LWA3_DS As String
    Public LWA4_DS As String
    Public LWA5_DS As String
    Public LWA6_DS As String

    Public ERP_selezionato As String

    Public I_bassa_DS As String
    Public RPM_bassa_DS As String
    Public pow_bassa_DS As String

    Public I_alta_DS As String
    Public RPM_alta_DS As String
    Public pow_alta_DS As String

    Public Tmin_DS As String
    Public Tmax_DS As String

    Public conn_bassa As String
    Public conn_alta As String

    Public installation_cat As String
    Public Efficiency_category As String
    Public Efficiency_target As String
    Public Efficiency_fan As String
    Public ERP_RPM As String
    Public ERP_pow As String
    Public ERP_curr As String
    Public ERP_Q As String
    Public ERP_P As String

    Public IP_DS As String
    Public Ins_DS As String

    Public Report As New Datasheet1()
    Public printTool As New ReportPrintTool(Report)
    Public print_mode As Integer


    'calcolo parabola
    Public den_parabola As Double
    Public A_parabola As Double
    Public B_parabola As Double
    Public C_parabola As Double


    Public x_bassa(3) As Double
    Public y_bassa(3) As Double



    Public x_alta(3) As Double
    Public y_alta(3) As Double


    Public coeff_bassa(3) As Double
    Public coeff_alta(3) As Double


    Public vect_Bassa_x_chart(10) As Double
    Public vect_Bassa_y_chart(10) As Double

    Public vect_Alta_x_chart(10) As Double
    Public vect_Alta_y_chart(10) As Double

    Public Error_log_ristampa(1000) As String
    Public Count_Mail_Error As Integer
    '------------------------------------------Punti per i grafici Alta e Bassa-----------------------------------------------------------
    Public x_bassa_grafico(3) As Double
    Public y_bassa_grafico(3) As Double

    Public x_alta_grafico(3) As Double
    Public y_alta_grafico(3) As Double
    '-------------------------------------------------------------------------------------------------------------------------------------


    Public vect_DS_state(5) As Integer
    Public vect_DS_state_TOT(5) As Integer
    Public Ds_completi As Integer
    Public PJ_config_star As String

    'Aggioramneto database
    Public tables3 As DataTableCollection
    Public tables4 As DataTableCollection
    Public tables5 As DataTableCollection
    Public vettore_aggiornamentoDS(Numero_colonneDBtot) As String
    Public vettore_nomi_aggiornamentoDS(Numero_colonneDBtot) As String
    Public aggiunta_val_DS As Integer
    Public DS_lista(Numero_righeDBtot, 15) As String
    Public DS_config_lista(Numero_righeDBtot, 4) As String
    Public DS_ambiente_lista(Numero_righeDBtot, 2) As String
    Public DS_totale_dati(Numero_righeDBtot, Numero_colonneDBtot) As String
    Public numero_DS As Integer
    Public numero_DS_conf As Integer
    Public numero_DS_amb As Integer
    Public lista_DS_sel As Integer
    Public lista_DS_sel_name As String
    Public mod_archivio As Integer
    Public Warning_folder_name As String
    Public ERP_ok As Integer

    Public Yes_No_Warning As Integer = 0
    Public Datasheet_New_exc As Integer = -1
    Public Tmax_starA As Integer = 0

    'sezione struttura folder
    Public rigaFolder_selezionata As Integer
    Public NomeFolder_selezionata As String
    Public testo_ricerca As String
    Public lunghezza_testo As Integer
    Public load_end As Integer


    'Ricerca 
    Public Motore_Filtro As DataRowView
    Public Serie_Filtro As DataRowView
    Public Profilo_Filtro As DataRowView
    Public Pale_Filtro As DataRowView
    Public Diametro_Filtro As DataRowView
    Public Calettamento_Filtro As DataRowView
    Public Poli_Filtro As DataRowView
    Public Tensione_Filtro As DataRowView
    Public Frequenza_Filtro As DataRowView
    Public Tmin_Filtro As DataRowView
    Public Tmax_Filtro As DataRowView
    Public Prova_Filtro As String

    'chart coordinates
    Public x_base As Integer = 102
    Public y_base As Integer = 311
    Public x_base_max As Integer = 831
    Public y_base_max As Integer = 15


    Public mod_PJ_DS As Integer = 0
    '----------------------------------------------------------------------------------------------------------------------






    '********************************************GESTIONE DEL PREZZO****************************************************
    Public vettore_aggiornamento_prezzo(Numero_colonneDBtot) As String
    Public vettore_nomi_aggiornamento_prezzo(Numero_colonneDBtot) As String
    Public aggiunta_prezzo_val As Integer

    '*********************************************************************************************************************









    '********************************************GESTIONE DEI DATABASE****************************************************



    '---------------------------------------------PJ_DataBase-------------------------------------------------------------
    Public tables6 As DataTableCollection
    Public numero_PJ_DataBase As Integer
    Public numero_colonne_PJ_DataBase As Integer = 45
    Public PJ_DataBase_lista(Numero_righeDBtot, numero_colonne_PJ_DataBase) As String
    Public PJ_DataBase_val As String
    Public config_pos As Integer
    Public amb_pos As Integer
    Public mot_pos As Integer
    Public tipo_motore_PJ As String
    Public conf_ID_PJ As Integer
    Public puntatore_datagrid As Integer
    Public vettore_nomi_aggiornamentoPJ(Numero_colonneDBtot) As String
    Public vettore_aggiornamentoPJ(Numero_colonneDBtot) As String
    Public Esistenza_confgurazione As Integer
    Public ricerca_ambiente_text As String
    Public ricerca_configurazioni_text As String
    Public grid_config_sel As String
    Public grid_ambient_sel As String
    Public errorePJ As Integer
    Public DB_pasticci_on As Integer = 0

    '---------------------------------------------------------------------------------------------------------------------




    '*********************************************************************************************************************





    '**********************************************Servizio Mailing*********************************************************

    Public vettore_controllo_mail(Numero_righeDBtot, 9) As String ' numeroPJ   dataPJ   rev   owner   stato   dataMail  cliente  attesa  ordine-ricevuto
    Public numero_PJ_mail As Integer
    Public Nome_PJ_mail As String
    Public Data_PJ_mail As String
    Public Rev_PJ_mail As String
    Public Owner_PJ_mail As String
    Public Stato_PJ_mail As String
    Public Data_mail As String
    Public email_owner As String
    Public cliente_mail As String

    Public mail_urgenti(Numero_righeDBtot, 2) As String
    Public PJ_ritardo As Integer


    Public PJ_super_late As String
    Public Giorni_PJ_super_late As Integer
    Public Rev_PJ_super_late As Integer
    Public data_PJ_super_late As String
    Public ordine_ric As String

    '************************************************************************************************************************



    '**********************************************Sviluppo******************************************************************
    Public tables7 As DataTableCollection
    Public Esistenza_sviluppo As Integer
    Public numero_Sviluppo As Integer
    Public N_colonne_sviluppo As Integer = 15
    Public DataBase_sviluppi(Numero_righeDBtot, N_colonne_sviluppo) As String
    Public DataBase_sviluppi_star(Numero_righeDBtot, N_colonne_sviluppo) As String
    Public Nome_colonne_sviluppo(Numero_colonneDBtot) As String
    Public Descrizione_colonne_sviluppo(Numero_colonneDBtot) As String
    Public ID_sviluppo As String
    Public Puntatore_sviluppo As String
    Public vettore_aggiornamentoSV(Numero_colonneDBtot) As String
    Public vettore_nomiSV(Numero_colonneDBtot) As String
    Public vettore_aggiornamentoCAT(Numero_colonneDBtot) As String
    Public vettore_nomiCAT(Numero_colonneDBtot) As String


    Public swSV As String
    Public DescrizioneSV As String
    Public CreazioneSV As String
    Public chiusuraSV As String
    Public giorniSV As String
    Public valutazioneSV As String
    Public ambitoSV As String
    Public nomeSV As String
    Public statoSV As String
    Public urgenzaSV As String
    Public ID_SV As String

    Public durata_sviluppo As Integer
    Public rating_user As String
    Public mod_load_imm As Integer



    Public Sviluppi_chiusi As Integer
    Public Sviluppi_attesa As Integer
    Public Sviluppi_waiting As Integer
    Public Sviluppi_totali As Integer

    Public nome_richiedente As String
    Public spostamento_effettuato As Integer

    Public difficolta_SV As String
    Public NOTE_SV As String
    Public Versione_ref As String
    '************************************************************************************************************************


    '**************************************************SW_interface**********************************************************************
    Public file_exc_name As String
    Public Dir_exc_name As String
    Public directory_SW_excell As String = "H:\Comune\Applicazioni\SW_excell\"
    Public directory_SW_assemblati As String = "H:\Ufficio Tecnico\Modelli Disegno 3D SolidWorks\File comuni\Librerie 3D\Vip Air\VV - Ventilatori\VENTILATORI SERIE "
    Public objExcel_SW As Object

    Public Serie_ventola As String
    Public Profilo_ventola As String
    Public Npale_ventola As String
    Public Serie_motore As String
    Public Config_ventilatore As String
    Public Flusso As String
    Public Diam_SW As String
    Public Angolo_SW As String



    Public Nome_colonne_SW(Numero_colonneDBtot) As String
    Public Numero_colonne_SW As Integer
    Public Valore_CellaRiga_SW(Numero_colonneDBtot) As String
    Public SW_Raggera As String
    Public SW_Code As String
    '************************************************************************************************************************





    '**********************************************VIDEO TUTORIAL***********************************************************
    Public Cartella_Generale_Tutorial As String = "H:\Comune\Applicazioni\Sviluppo Software\VipDesigner\VERSIONI VIPDESIGNER\Versione Ufficio Tecnico\Altro\Gestione Vip Designer\VideoTutorial\Tutorial1"
    Public Video_Name As String = ""

    '***********************************************************************************************************************


    '*********************************************SQL SERVER****************************************************************
    'Public connSQL_archivio As New SqlConnection("Data Source=VIPAIRWS10-0100\SQLEXPRESS;Initial Catalog=MCSS;Integrated Security=True") 'basically connection string
    'Public connSQL_archivio As New SqlConnection("Data Source=VIPWS01\NTS;Initial Catalog=MCSS;User ID=sa; pwd=vip2010") 'basically connection string
    'Public connSQL_archivio As New SqlConnection("Data Source=88.63.111.73;Initial Catalog=MCSS;User ID=andrea; pwd=svizzera2013")
    'Public connSQL_archivio As New SqlConnection("Data Source=127.0.0.1;Initial Catalog=MCSS;User ID=apeuser; pwd=Vip2022")
    Public connSQL_archivio As New SqlConnection("Data Source=80.211.16.23;Initial Catalog=MCSS;User ID=VipAdmin; pwd=Vip2010$")
    '***********************************************************************************************************************
    '******************************************************Catalogo*********************************************************
    Public tables8 As DataTableCollection
    Public numero_Catalogo As Integer
    Public N_colonne_Catalogo As Integer = 15
    Public DataBase_catalogo(Numero_righeDBtot, N_colonne_Catalogo) As String
    Public Nome_colonne_catalogo(Numero_colonneDBtot) As String
    Public Descrizione_colonne_catalogo(Numero_colonneDBtot) As String
    Public Puntatore_catalogo As String
    Public ciclo_stampa_catalogo As Integer
    Public nome_catalogo As String
    '***********************************************************************************************************************



End Module
