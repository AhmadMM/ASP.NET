//-----------------------------------Frontend------------------------------//
<style> 
        #ContentPlaceHolder1_ddlGradesForBatch_sl {
       height:30px; 
        }
    </style>
	
	 <div class="col-md-5">
                    
                        <label class="control-label">Grades:</label>
                    <asp:DropDownCheckBoxes ID="ddlGradesForBatch" runat="server"  CssClass="form-control">
                        <Items> 
                        </Items>
                    </asp:DropDownCheckBoxes>
                
                </div>
				
				
			//------------------------Backend-----------------------------------------//	
                    Global.FillDropDownProc(ddlGradesForBatch, "ClassRetrieval", "Name", "ID");
					
					 List<ListItem> selected = ddlGradesForBatch.Items.Cast<ListItem>()
                                                           .Where(li => li.Selected)
                                                           .ToList();
                if (selected.Count == 0)
                {
				//DONOOP
				} else {
                    foreach (var ItemGrade in selected)
                    {
					ItemGrade.Value//ID
					ItemGrade.Text//Name
                        }
						
						}