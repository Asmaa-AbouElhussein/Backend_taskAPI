using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskAPI.Models;
using TaskAPI.Data;
using TaskAPI.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using TaskAPI.DTO;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly IRegistrationRepository _RegistrationRepository;
        private readonly IMapper _mapper;
        public RegistrationController(IRegistrationRepository RegistrationRepository, IMapper mapper)
        {
            _RegistrationRepository = RegistrationRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getregistration()
        {
            return await _RegistrationRepository.GetAllAsync();
           
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<User>> Getregistername()
        {
            var data = _RegistrationRepository.Getusername();
            return Ok(data);
        }

        [HttpGet("Auth")]
        [Authorize]
        public IActionResult checkAuth()
        {
            var identity = User.Identity as ClaimsIdentity;
            List<Claim> li = identity.Claims.ToList();
            List<string> Udata = new List<string> { li[0].Value, li[1].Value, li[2].Value };

            return Ok(Udata);
        }


        [HttpGet("{id}", Name = "GetUsData")]
        public async Task<ActionResult<User>> Getregistration(int id)
        {
            var user = _RegistrationRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(user);
            }
        }



        [HttpPost]
        public async Task<IActionResult> Postregistration(UserDTO userdto)
        {
            if (userdto.password == userdto.confirmPassword)
            { 
                var user = _mapper.Map<User>(userdto);

            var _user = _RegistrationRepository.PostAsync(user);
                if(_user!=null)
                {
                return Created("GetUsData", user);
                }
                else
                {
                return BadRequest();
                }
            }else
            {
                return BadRequest();
            }
        }


        [HttpPut]
        public IActionResult changepassword(newpasswordDTO dto)
        {
            _RegistrationRepository.putnewpassword(dto);
            try
            {
           
                _RegistrationRepository.SaveChanges();
            }
            catch
            {
                BadRequest();
            }

            return NoContent();
        }



        [HttpPost("UTokin")]
        public IActionResult CreateTokin(UserLogin user)
        {
            var usdata = _RegistrationRepository.userdata(user);
            if (usdata is not null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes("Secret_Key_235K2K1a23"));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var data = new List<Claim>();
                data.Add(new Claim("Email", usdata.email));
                data.Add(new Claim("username", usdata.username));
                if (user.username == "Admin22")
                {
                    data.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else
                {
                    data.Add(new Claim(ClaimTypes.Role, "defaultUser"));

                }
                var token = new JwtSecurityToken(
                claims: data,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
