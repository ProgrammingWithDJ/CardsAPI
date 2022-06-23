using CardsAPi.Data;
using CardsAPi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dtos;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext cardsDbContext;
        private readonly IUnitOfWork unitOfWork;


        public CardsController(IUnitOfWork unitOfWork)
        {
          
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            //  var cards =await cardsDbContext.Cards.ToListAsync();

            var cards = await unitOfWork.cardRepository.GetCardsAsync();

            var cardDto = from c in cards
                          select new CardDto()
                          {
                              Id = c.Id,
                              CardholderName = c.CardholderName,
                              CardNumber = c.CardNumber,
                              CVC =c.CVC,
                              ExpiryMonth=c.ExpiryMonth,
                              ExpiryYear =c.ExpiryYear

                          };

            return Ok(cardDto);
        }

        [HttpGet]
        [Route("{Id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid Id)
        {
            // var card = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id==id);
              var card = await unitOfWork.cardRepository.FindCard(Id);

            if(card !=null)
            {
               return Ok(card);
            }

            return NotFound("Card not found");
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] CardDto cardDto)
        {
            cardDto.Id = Guid.NewGuid();

            var card = new Card
            {
                Id = cardDto.Id,
                CardholderName = cardDto.CardholderName,
                CardNumber = cardDto.CardNumber,
                CVC = cardDto.CVC,
                ExpiryMonth = cardDto.ExpiryMonth,
                ExpiryYear = cardDto.ExpiryYear
            };


           unitOfWork.cardRepository.AddCard(card);

            await unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard),new { id = card.Id },card);
        }

        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid Id, [FromBody] CardDto cardDto)
        {

            var existingCards = await unitOfWork.cardRepository.FindCard(Id);

            if (existingCards != null)
            {
                existingCards.CardholderName = cardDto.CardholderName;
                existingCards.CardNumber = cardDto.CardNumber;
                existingCards.CVC = cardDto.CVC;
                existingCards.ExpiryMonth = cardDto.ExpiryMonth;
                existingCards.ExpiryYear = cardDto.ExpiryYear;


                await unitOfWork.SaveChangesAsync();

                return Ok(existingCards);

            }

            return NotFound("Card not found");
        }


        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid Id)
        {

            var existingCard = await unitOfWork.cardRepository.FindCard(Id);

            if (existingCard != null)
            {
                unitOfWork.cardRepository.DeleteCard(Id);
                await unitOfWork.SaveChangesAsync();

                return Ok(existingCard); 
            }

            return NotFound("Card not found");
        }

    }
}
