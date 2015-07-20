function buildimpactanalysis(strategyList) { 
            //StringBuilder html = new StringBuilder();
            
            var html = '';

            html += "<div id='float-header'>";
            html += "<table border='0'>";
            html += "<tr>";
            html += "<td class='strategy-table-header width-100 type-column'>Type</td>";
            html += "<td class='strategy-table-header width-100'>Agenda</td>";
            html += "<td><table border='0' width='100%'><tr><td class='strategy-table-header width-150'>Business Policy</td><td class='strategy-table-header width-100'>Business Rule</td><td class='strategy-table-header width-100'>Process</td><td class='strategy-table-header width-150' style='text-indent:5px;'>Application</td><td class='strategy-table-header width-200' style='text-indent:5px;'>Sub-Process</td><td class='strategy-table-header width-250' style='text-indent:5px;'>Module</td></tr></table></td>";
            html += "</tr>";
            html += "</table>";
            html += "</div>";

            html += "<div id='ia-holder'>";
            html += "<table border=0 class='strategy-table'>";

            if (strategyList.length > 0)
            {
                var ctr0 = 0;
                $.each(strategyList, function(key, item) {
					
					var policyhtml = '';
					
                    policyhtml += "<table border=0 class='strategy-table business-policy-table'>";
                    var ctr1 = 0;
					$.each(item.Policies, function(key1, item1) {
						var css = '';
                        policyhtml += "<tr>";
                        policyhtml += "<td class='" + css + " width-150'>" + item1.BusinessPolicyName + "</td>";

                        ctr1++;
                        policyhtml += "<td>";
                        policyhtml += "<table border=0 class='strategy-table rule-table'>";

                        var ctr2 = 0;
						$.each(item1.Rules, function(key2, item2) {
							var css2 = '';
                            policyhtml += "<tr>";
                            policyhtml += "<td class='"+ css2 +" width-100'>" + item2.BusinessRuleName + "</td>";

                            ctr2++;
                            policyhtml += "<td>";

                            policyhtml += "<table border=0 class='strategy-table process-table'>";
                            var ctr3 = 0;
							$.each(item2.Processes, function(key3, item3) {
								var css3 = '';
                                policyhtml += "<tr>";
                                policyhtml += "<td class='" + css3 + " width-100'>" + item3.ProcessName + "</td>";
                                ctr3++;

                                if (item3.Application.length == 1)
                                {
                                    policyhtml += "<td class='width-150'>" + item3.Application[0].ApplicationName + "</td>";
                                }
                                else
                                {
                                    policyhtml += "<td class='width-150'>";
                                    
                                    var ctr4 = 0;
									$.each(item3.Application, function(key4, item4) {
                                        policyhtml += "<span class='width-150'>"+ item4.ApplicationName +"</span><br />";
                                        ctr4++;
                                    });
                                    
                                    policyhtml += "</td>";
                                }

                                    policyhtml += "<td>";
                                    policyhtml += "<table class='strategy-table sp-table' border=0 width='100%'>";
                                    var ctr5 = 0;
									$.each(item3.SubProcesses, function(key5, item5) {
										var css4 = '';
                                        policyhtml += "<tr>";
                                        policyhtml += "<td class='" +  css4 + " width-200'>"+ item5.SubProcessName +"</td>";
                                        ctr5++;

                                            policyhtml += "<td>";

                                            policyhtml += "<table class='strategy-table module-table' border=0 width='100%'>";
                                            var ctr6 = 0;
											$.each(item5.Modules, function(key6, item6) {
												var css5 = '';
                                                policyhtml += "<tr>";
                                                policyhtml += "<td class='"+ css5 +" width-300'>"+ item6.ModuleName +"</td>";
                                                policyhtml += "</tr>";
                                                ctr6++;
                                            });
                                            policyhtml += "</table>";
                                            policyhtml += "</td>";
                                        
                                        policyhtml += "</tr>";
                                    });
                                    policyhtml += "</table>";

                                    policyhtml += "</td>";


                                policyhtml += "</tr>";
                            });
                            policyhtml += "</table>";
                            policyhtml += "</td>";
                            policyhtml += "</tr>";
                        });
                        policyhtml += "</table>";
                        policyhtml += "</td>";
                        policyhtml += "</tr>";
                    });
                    policyhtml += "</table>";
                    var css0 = '';
                    html += "<tr><td class='"+ css0 +" width-100 type-column'>" + item.Type + "</td><td class='" + css0 + " width-100 agenda-column'>"+item.Agenda+"</td><td>"+policyhtml+"</td></tr>";
                });
            }
            else
            {
                html += "<tr><td colspan='4'>No Data Found!<td></tr>";
            }
            html += "</table>";
            html += "</div>";

            return html;
}