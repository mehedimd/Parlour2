using WebMVC.Models.IdentityModels;

namespace WebMVC.Models.UserRole
{
    public class RolePermissionHelper
    {
        #region setup role permission for view
        private int parentId { get; set; } = 1;
        private int childId { get; set; } = 100;

        public IList<RolePermmissionCheck> Items { get; set; }

        public void LoadItems()
        {
            this.Items = new List<RolePermmissionCheck> {

                // Module...
                new RolePermmissionCheck {
                    Id = parentId, Title = "Module", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Admin Module", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Module.AdminModule
                        },
                    }
                },

                // --- Admin Module ---

                // User...
                new RolePermmissionCheck {
                    Id = parentId, Title = "Users", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "List View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Users.ListView
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Users.Create
                        },
                    }
                },

                // User Role...
                new RolePermmissionCheck {
                    Id = (++parentId), Title="User Roles", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "List View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.UserRoles.ListView
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create Or Edit", ParentId = parentId,
                            IsSelected = false, Name = Permissions.UserRoles.CreateOrEdit
                        },
                    }
                },

                // Department...
                new RolePermmissionCheck {
                    Id = (++parentId), Title="Department", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "List View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Departments.ListView
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "List View Academic Department", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Departments.ListViewAcademicDepartment
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Departments.Create
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create Academic Department", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Departments.CreateAcademicDepartment
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Edit", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Departments.Edit
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Edit Academic Department", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Departments.EditAcademicDepartment
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Delete", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Departments.Delete
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Delete Academic Department", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Departments.DeleteAcademicDepartment
                        }
                    }
                },

                // Designations...
                new RolePermmissionCheck {
                    Id = (++parentId), Title="Designations", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "List View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Designations.ListView
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Designations.Create
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Edit", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Designations.Edit
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Delete", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Designations.Delete
                        }
                    }
                },

                
                // --- HRMS Module ---

                // Hr Settings...
                new RolePermmissionCheck {
                    Id = (++parentId), Title="Hr Settings", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Edit", ParentId = parentId,
                            IsSelected = false, Name = Permissions.HrSettings.Edit
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Details View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.HrSettings.DetailsView
                        }
                    }
                },

                // Employees... 
                new RolePermmissionCheck {
                    Id = (++parentId), Title="Employees", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Employees.Create
                        },

                        new RolePermmissionCheck {
                            Id = childId++, Title = "Edit", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Employees.Edit
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "List View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Employees.ListView
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Details View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Employees.DetailsView
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Report View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Employees.ReportView
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Disable", ParentId = parentId,
                            IsSelected = false, Name = Permissions.Employees.Disable
                        },
                    }
                },

                // Employee Attendances...
                new RolePermmissionCheck {
                    Id = (++parentId), Title="Employee Attendance", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create", ParentId = parentId,
                            IsSelected = false, Name = Permissions.EmpAttendances.Create
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Report View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.EmpAttendances.ReportView
                        }
                    }
                },

                // Employee Leave Applications...
                new RolePermmissionCheck {
                    Id = (++parentId), Title="Employee Leave Application", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create", ParentId = parentId,
                            IsSelected = false, Name = Permissions.EmpLeaveApplications.Create
                        },

                        new RolePermmissionCheck {
                            Id = childId++, Title = "Edit", ParentId = parentId,
                            IsSelected = false, Name = Permissions.EmpLeaveApplications.Edit
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Delete", ParentId = parentId,
                            IsSelected = false, Name = Permissions.EmpLeaveApplications.Delete
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "List View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.EmpLeaveApplications.ListView
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Report View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.EmpLeaveApplications.ReportView
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Details View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.EmpLeaveApplications.DetailsView
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Statement View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.EmpLeaveApplications.StatementView
                        }
                    }
                },

                // Leave Type...
                new RolePermmissionCheck {
                    Id = (++parentId), Title="Leave Type", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create", ParentId = parentId,
                            IsSelected = false, Name = Permissions.LeaveTypes.Create
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Edit", ParentId = parentId,
                            IsSelected = false, Name = Permissions.LeaveTypes.Edit
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Delete", ParentId = parentId,
                            IsSelected = false, Name = Permissions.LeaveTypes.Delete
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "List View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.LeaveTypes.ListView
                        }
                    }
                },

                
                // Leave Setup...
                new RolePermmissionCheck {
                    Id = (++parentId), Title="Leave Setup", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create", ParentId = parentId,
                            IsSelected = false, Name = Permissions.LeaveSetups.Create
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Edit", ParentId = parentId,
                            IsSelected = false, Name = Permissions.LeaveSetups.Edit
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Delete", ParentId = parentId,
                            IsSelected = false, Name = Permissions.LeaveSetups.Delete
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "List View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.LeaveSetups.ListView
                        }
                    }
                },

                // Cf Leave...
                new RolePermmissionCheck {
                    Id = (++parentId), Title="CF Leave", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create", ParentId = parentId,
                            IsSelected = false, Name = Permissions.CfLeave.Create
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Edit", ParentId = parentId,
                            IsSelected = false, Name = Permissions.CfLeave.Edit
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Delete", ParentId = parentId,
                            IsSelected = false, Name = Permissions.CfLeave.Delete
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "List View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.CfLeave.ListView
                        }
                    }
                },

                // Set Holidays...
                new RolePermmissionCheck {
                    Id = (++parentId), Title="Set Holidays", IsSelected = false, Children = new List<RolePermmissionCheck> {
                        new RolePermmissionCheck {
                            Id = childId++, Title = "List View", ParentId = parentId,
                            IsSelected = false, Name = Permissions.SetHolidays.ListView
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Create", ParentId = parentId,
                            IsSelected = false, Name = Permissions.SetHolidays.Create
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Edit", ParentId = parentId,
                            IsSelected = false, Name = Permissions.SetHolidays.Edit
                        },
                        new RolePermmissionCheck {
                            Id = childId++, Title = "Delete", ParentId = parentId,
                            IsSelected = false, Name = Permissions.SetHolidays.Delete
                        }
                    }
                },
        };
        }
        #endregion

        #region role permission helper method

        public void LoadRolePermissions(IList<string> permissions)
        {
            if (this.Items == null || !this.Items.Any() || permissions == null || !permissions.Any())
            {
                return;
            }

            foreach (var root in this.Items)
            {
                var isRootSelect = true;
                foreach (var child in root.Children)
                {
                    if (permissions.Any(p => p == child.Name)) child.IsSelected = true;
                    if (!child.IsSelected) isRootSelect = false;
                }
                root.IsSelected = isRootSelect;
            }
        }

        public void ResetRolePermissions()
        {
            if (this.Items == null || !this.Items.Any())
            {
                return;
            }

            foreach (var root in this.Items)
            {
                root.IsSelected = false;
                foreach (var child in root.Children)
                {
                    child.IsSelected = false;
                }
            }
        }

        public IList<string> PreparePermissions()
        {
            var result = new List<string>();

            foreach (var root in this.Items)
            {
                foreach (var child in root.Children)
                {
                    if (child.IsSelected) result.Add(child.Name);
                }
            }

            return result;
        }

        #endregion
    }
}
