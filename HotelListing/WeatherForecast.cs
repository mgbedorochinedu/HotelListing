using System;

namespace HotelListing
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}

////Update Staff User - Service
//public async Task<StaffUserDto> UpdateStaffInfo(int id, StaffUserDto staffUser)
//{
//    try
//    {
//        var result = await _db.StaffUsers.Where(x => x.Id == id).FirstOrDefaultAsync();

//        if (result != null)
//        {
//            result.Fullname = staffUser.Fullname;
//            result.FirstName = staffUser.FirstName;
//            result.LastName = staffUser.LastName;
//            result.JobTitle = staffUser.JobTitle;
//            result.DepartmentName = staffUser.DepartmentName;

//            _db.StaffUsers.Update(result);

//            await _db.SaveChangesAsync();

//            var staffUpdatedResult = _mapper.Map<StaffUserDto>(result);

//            return staffUpdatedResult;

//        }

//        throw new Exception("Staff record not found!");

//    }
//    catch (Exception ex)
//    {
//        throw new Exception("Something went wrong!");
//    }

//}


//...............
//Update Staff User - Controller

//[HttpPost("update/staff-user-info/{id:int}")]
//[ProducesResponseType(typeof(ResponseModel), 200)]
//[ProducesResponseType(typeof(ResponseModel), 401)]
//public async Task<IActionResult> UpdateStaffInfo(int id, [FromBody] StaffUserDto staffUserDto)
//{
//    try
//    {

//        var staffUser = await _adminService.UpdateStaffInfo(id, staffUserDto);

//        return Ok(StandardResponse.Ok("Updated successfully", staffUser));

//    }
//    catch (Exception ex)
//    {
//        return BadRequest(StandardResponse.BadRequest("Error occured", ex.Message)); ;
//    }

//}



//.....................................................................................................................
//Fetch All Staff Loan History - Service

//public async Task<IEnumerable<LoanAppHistoryDto>> GetAllStaffLoanHistory(string username)
//{

//    try
//    {
//        var staffLoans = await _db.LoanApplications.Include(x => x.StaffUser)
//            .Where((u => u.StaffUser.Username == username)).ToListAsync();


//        if (staffLoans.Count <= 0)
//        {
//            throw new Exception("Staff loan not found!");
//        }

//        var loanHistoryList = new List<LoanAppHistoryDto>();

//        foreach (var loanHistory in staffLoans)
//        {
//            var loan = new LoanAppHistoryDto
//            {
//                LoanAmount = loanHistory.LoanAmount,
//                LoanRepayment = loanHistory.LoanRepayment,
//                LoanType = loanHistory.LoanType,
//                MaturityDateOfLoan = loanHistory.MaturityDateOfLoan
//            };

//            loanHistoryList.Add(loan);
//        }

//        return loanHistoryList;
//    }
//    catch (Exception e)
//    {
//        throw new Exception("Something went wrong!");
//    }
//}

//............................ Controller
//[Authorize(AuthenticationSchemes = "Bearer")]
//[HttpGet("fetch-staff-loan-history")]
//[ProducesResponseType(typeof(ResponseModel), 200)]
//[ProducesResponseType(StatusCodes.Status401Unauthorized)]
//public async Task<IActionResult> FetchStaffLoanHistory()
//{
//    var identity = HttpContext.User.Identity as ClaimsIdentity;
//    if (identity == null)
//    {
//        throw new UnauthorizedAccessException("User Unauthorized");
//    }
//    var tokenUser = identity.FindFirst("userName").Value;

//    var staffLoanHistory = await _loan.GetAllStaffLoanHistory(tokenUser);
//    if (staffLoanHistory != null)
//        return Ok(StandardResponse.Ok("Successfully fetch loan history", staffLoanHistory));
//    else
//        return BadRequest(StandardResponse.BadRequest("Loan history not found"));
//}



//..................................................................................................................


//Get staff users either by emailAddress or accountNumber - SERVICE

//public async Task<IEnumerable<StaffUser>> GetStaffUsers(string emailAddress, string accountNumber)
//{
//    try
//    {
//        IQueryable<StaffUser> staffUsersQuery = _db.StaffUsers;
//        if (!string.IsNullOrEmpty(emailAddress))
//        {
//            staffUsersQuery = staffUsersQuery.Where(x => x.Email.ToLower().Trim() == emailAddress.ToLower().Trim());

//        }

//        if (!string.IsNullOrEmpty(accountNumber))
//        {
//            staffUsersQuery = staffUsersQuery.Where(x => x.StaffAccountNumber.Trim() == accountNumber.Trim());
//        }

//        var staffUsers = await staffUsersQuery.ToListAsync();


//        if (staffUsers == null || !staffUsers.Any())
//        {
//            throw new Exception("Staff user not found");
//        }

//        return staffUsers;

//    }
//    catch (Exception ex)
//    {
//        throw new Exception("An error occur");
//    }
//}
//.......................................................
//Get Staff User - CONTROLLER

//[HttpGet("get-staff-users")]
//[ProducesResponseType(typeof(ResponseModel), 200)]
//[ProducesResponseType(typeof(ResponseModel), 401)]
//public async Task<IActionResult> GetStaffUsers([FromQuery] string emailAddress, [FromQuery] string accountNumber)
//{
//    var staffUsers = await _adminApproval.GetStaffUsers(emailAddress, accountNumber);

//    return Ok(StandardResponse.Ok("Fetched successfully", staffUsers));
//}


//...............................................................................................................

//Loan approval/Get Staff loan Approval/Get list of loan status - SERVICE

//public async Task UpdateLoanApprovalStatus(LoanApprovalStatusTrackerModel loanApproval, string username)
//{

//    try
//    {
//        //Get the staff loan to approve
//        var staffLoan = await _db.LoanApplications.FirstOrDefaultAsync(a => a.Id == loanApproval.LoanApplicationId);

//        if (staffLoan != null)
//        {
//            staffLoan.ApprovalStatus = loanApproval.ApprovalStatus;

//            _db.Update(staffLoan);

//            var action = loanApproval.ApprovalStatus.ToString().StartsWith("Approved") ? "Approved" : "Declined";


//            LoanApprovalStatusTracker loanApprovalStatusTracker = new LoanApprovalStatusTracker
//            {
//                Action = $"{action} By {username}",

//                Comment = loanApproval.Comment,
//                ApprovalDate = DateTime.Now,
//                LoanApplicationId = loanApproval.LoanApplicationId,

//            };
//            await _db.loanApprovalStatusTrackers.AddAsync(loanApprovalStatusTracker);
//            await _db.SaveChangesAsync();

//            //TODO: Send email to SuperAdmin if Accepted by Disciplinary/Appraisal Department

//            if (loanApproval.ApprovalStatus == ApprovalStatus.ApprovedByDisciplinary)
//            {
//                //Send an email to Appraise Department
//            }
//            if (loanApproval.ApprovalStatus == ApprovalStatus.ApprovedByAppraisal)
//            {
//                //Send  email to SuperAdmin Department
//            }

//            if (loanApproval.ApprovalStatus == ApprovalStatus.ApprovedBySuperAdmin)
//            {
//                //Undecided
//            }

//            if (loanApproval.ApprovalStatus == ApprovalStatus.DeclinedByDisciplinary || loanApproval.ApprovalStatus == ApprovalStatus.DeclinedByAppraisal || loanApproval.ApprovalStatus == ApprovalStatus.DeclinedBySuperAdmin)
//            {
//                //Send  email to User that the loan has been Declined.
//            }
//            return;
//        }
//        throw new Exception("Loan record not found");

//    }
//    catch (Exception ex)
//    {

//        throw new Exception("Error occur, cannot approve loan");
//    }

//}

////Get list of loan status
//public async Task<IEnumerable<LoanApplication>> GetAllLoanByApprovalStatus(ApprovalStatus approvalStatus)
//{
//    try
//    {
//        var loanApplications = await _db.LoanApplications.Where(x => x.ApprovalStatus == approvalStatus).ToListAsync();

//        if (loanApplications != null)
//        {
//            return loanApplications;
//        }
//        throw new Exception("No record found");
//    }
//    catch (Exception ex)
//    {

//        throw new Exception(ex.Message);
//    }

//}

////Get list of loan record by Id
//public async Task<IEnumerable<LoanApprovalStatusTracker>> GetLoanApprovalRecord(int id)
//{
//    try
//    {
//        var loanRecords = await _db.loanApprovalStatusTrackers.Where(x => x.LoanApplicationId == id).ToListAsync();

//        if (loanRecords != null)
//        {
//            return loanRecords;
//        }
//        throw new Exception("No record found");
//    }
//    catch (Exception ex)
//    {

//        throw new Exception(ex.Message);
//    }
//}

//............................................

//Loan approval/Get Staff loan Approval/Get list of loan status - CONTROLLER

//[HttpPost("update-loan-approval-status")]
//[ProducesResponseType(typeof(ResponseModel), 200)]
//[ProducesResponseType(typeof(ResponseModel), 400)]
//[ProducesResponseType(typeof(ResponseModel), 401)]
//public async Task<IActionResult> UpdateLoanApprovalStatus([FromBody] LoanApprovalStatusTrackerModel trackerModel)
//{
//    if (!ModelState.IsValid)
//    {
//        return BadRequest(StandardResponse.BadRequest("Validation error", ModelState.SelectMany(x => x.Value.Errors)));
//    }
//    try
//    {
//        var identity = HttpContext.User.Identity as ClaimsIdentity;

//        if (identity == null)
//        {
//            throw new UnauthorizedAccessException("User is not authorized");
//        }

//        var approvalName = identity.FindFirst("userName").Value;
//        await _adminApproval.UpdateLoanApprovalStatus(trackerModel, approvalName);

//        return Ok(StandardResponse.Ok("Loan record updated successfully", null));

//    }
//    catch (Exception ex)
//    {

//        return BadRequest(StandardResponse.BadRequest("Error occured", ex.Message));
//    }

//}


//[HttpGet("loan-status")]
//[ProducesResponseType(typeof(ResponseModel), 200)]
//[ProducesResponseType(typeof(ResponseModel), 400)]
//[ProducesResponseType(typeof(ResponseModel), 401)]
//public async Task<IActionResult> GetAllLoanByApprovalStatus([FromQuery] ApprovalStatus status)
//{
//    try
//    {
//        var loans = await _adminApproval.GetAllLoanByApprovalStatus(status);
//        return Ok(StandardResponse.Ok("Loans retrieved successfully", loans));
//    }
//    catch (Exception ex)
//    {
//        return BadRequest(StandardResponse.BadRequest("Error occured", ex.Message));
//    }
//}


//[HttpGet("{id}/records")]
//[ProducesResponseType(typeof(ResponseModel), 200)]
//[ProducesResponseType(typeof(ResponseModel), 400)]
//[ProducesResponseType(typeof(ResponseModel), 401)]
//public async Task<IActionResult> GetLoanApprovalRecord(int id)
//{
//    try
//    {
//        var loans = await _adminApproval.GetLoanApprovalRecord(id);
//        return Ok(StandardResponse.Ok("Loans record retrieved successfully", loans));
//    }
//    catch (Exception ex)
//    {
//        return BadRequest(StandardResponse.BadRequest("Error occured", ex.Message));
//    }
//}


//.......................................................................................................................
////Get staff users either by emailAddress or accountNumber or staffId - SERVICE
///
//Get staff users either by emailAddress or accountNumber
//public async Task<IEnumerable<LoansDto>> GetStaffUser(string emailAddress, string accountNumber, string staffId)
//{
//    try
//    {
//        IQueryable<Loans> staffUsersQuery = _db.Loans;
//        if (!string.IsNullOrEmpty(emailAddress))
//        {
//            staffUsersQuery = staffUsersQuery.Include(x => x.staffUser).Where(x => x.staffUser.Email.ToLower().Trim() == emailAddress.ToLower().Trim());

//        }

//        if (!string.IsNullOrEmpty(accountNumber))
//        {
//            staffUsersQuery = staffUsersQuery.Where(x => x.LoanApplication.Nuban.Trim() == accountNumber.Trim());
//        }

//        if (!string.IsNullOrEmpty(staffId))
//        {
//            staffUsersQuery = staffUsersQuery.Where(x => x.staffUser.StaffId.Trim() == staffId.Trim());
//        }


//        var staffUsers = await staffUsersQuery.ToListAsync();


//        if (staffUsers == null || !staffUsers.Any())
//        {
//            return null;
//        }


//        return _mapper.Map<List<Loans>, List<LoansDto>>(staffUsers);

//    }
//    catch (Exception ex)
//    {
//        throw new Exception("An error occur");
//    }

//}

//.......................................................................................................................


