Module Uff_tec_count



    Public Sub Uff_tec_count1()


        tot_DF_VD_Uff_tec(uff_count) = All_client_bk(Num_ID).Substring(All_client_bk(Num_ID).IndexOf("_DF") + 3, All_client_bk(Num_ID).IndexOf("_PF") - (All_client_bk(Num_ID).IndexOf("_DF") + 3))
        tot_PF_VD_Uff_tec(uff_count) = All_client_bk(Num_ID).Substring(All_client_bk(Num_ID).IndexOf("_PF") + 3, All_client_bk(Num_ID).IndexOf("_PN") - (All_client_bk(Num_ID).IndexOf("_PF") + 3))
        tot_PN_VD_Uff_tec(uff_count) = All_client_bk(Num_ID).Substring(All_client_bk(Num_ID).IndexOf("_PN") + 3, All_client_bk(Num_ID).IndexOf("_ATX") - (All_client_bk(Num_ID).IndexOf("_PN") + 3))
        tot_SF_VD_Uff_tec(uff_count) = All_client_bk(Num_ID).Substring(All_client_bk(Num_ID).IndexOf("_SF") + 3, All_client_bk(Num_ID).IndexOf("_IND") - (All_client_bk(Num_ID).IndexOf("_SF") + 3))
        tot_ATX_VD_Uff_tec(uff_count) = All_client_bk(Num_ID).Substring(All_client_bk(Num_ID).IndexOf("_ATX") + 4, All_client_bk(Num_ID).IndexOf("_SF") - (All_client_bk(Num_ID).IndexOf("_ATX") + 4))
        tot_ind_VD_Uff_tec(uff_count) = All_client_bk(Num_ID).Substring(All_client_bk(Num_ID).IndexOf("_IND") + 4, All_client_bk(Num_ID).IndexOf("_OFF") - (All_client_bk(Num_ID).IndexOf("_IND") + 4))
        tot_off_VD_Uff_tec(uff_count) = All_client_bk(Num_ID).Substring(All_client_bk(Num_ID).IndexOf("_OFF") + 4, All_client_bk(Num_ID).IndexOf("_SEA") - (All_client_bk(Num_ID).IndexOf("_OFF") + 4))
        tot_sea_VD_Uff_tec(uff_count) = All_client_bk(Num_ID).Substring(All_client_bk(Num_ID).IndexOf("_SEA") + 4)
        tot_Uff_tec(uff_count) = tot_DF_VD_Uff_tec(uff_count) + tot_PF_VD_Uff_tec(uff_count) + tot_PN_VD_Uff_tec(uff_count) + tot_SF_VD_Uff_tec(uff_count) + tot_ATX_VD_Uff_tec(uff_count) + tot_ind_VD_Uff_tec(uff_count) + tot_off_VD_Uff_tec(uff_count) + tot_sea_VD_Uff_tec(uff_count)

        If tot_Uff_tec(uff_count) > max_Uff_tec Then

            max_Uff_tec = tot_Uff_tec(uff_count)

        End If

        tot_projects_DF_VD_Uff_tec = tot_projects_DF_VD_Uff_tec + tot_DF_VD_Uff_tec(uff_count)
        tot_projects_PF_VD_Uff_tec = tot_projects_PF_VD_Uff_tec + tot_PF_VD_Uff_tec(uff_count)
        tot_projects_PN_VD_Uff_tec = tot_projects_PN_VD_Uff_tec + tot_PN_VD_Uff_tec(uff_count)
        tot_projects_SF_VD_Uff_tec = tot_projects_SF_VD_Uff_tec + tot_SF_VD_Uff_tec(uff_count)
        tot_projects_ATX_Uff_tec = tot_projects_ATX_Uff_tec + tot_ATX_VD_Uff_tec(uff_count)
        tot_projects_IND_Uff_tec = tot_projects_IND_Uff_tec + tot_ind_VD_Uff_tec(uff_count)
        tot_projects_OFF_Uff_tec = tot_projects_OFF_Uff_tec + tot_off_VD_Uff_tec(uff_count)
        tot_projects_SEA_Uff_tec = tot_projects_SEA_Uff_tec + tot_sea_VD_Uff_tec(uff_count)


        tot_projects_VD_Uff_tec = tot_projects_VD_Uff_tec + tot_Uff_tec(uff_count)

        uff_count = uff_count + 1



    End Sub



End Module
