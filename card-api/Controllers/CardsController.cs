using AutoMapper;
using card_api.DTOs;
using card_api.Extensions;
using card_api.Models;
using card_api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace card_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IMapper _mapper;
        private CardSerializer _serializer;

        public CardsController(IConfiguration config, IMapper mapper, CardSerializer cardSerializer)
        {
            _config = config;
            _mapper = mapper;
            _serializer = cardSerializer;
        }

        [HttpGet]
        public async Task<ActionResult> GetCards()
        {
            var deserializedCards = await _serializer.DeserializeItemsFromFile();
            var cardsDto = _mapper.Map<ICollection<Card>, ICollection<CardDto>>(deserializedCards);

            return Ok(cardsDto);
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateCard([FromForm] CardDto cardDto, IFormFile image)
        {
            cardDto.Id = Guid.NewGuid().ToString();
            cardDto.Image = await image.ToBytesArray();
            var card = _mapper.Map<CardDto, Card>(cardDto);

            if (await _serializer.SerializeItem(card)) return Ok(card.Id);
            return BadRequest("Error occured while serializing the card");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCard(string id)
        {
            var deserializedCards = (await _serializer.DeserializeItemsFromFile()).ToList();
            var filteredCards = deserializedCards.Where(card => card.Id != id).ToList();
            if (await _serializer.SerializeItemsToFile(filteredCards)) return Ok();
            return BadRequest("Error occured while trying to delete the card");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCard(string id, [FromForm] CardDto cardDto, IFormFile image)
        {
            var deserializedCards = (await _serializer.DeserializeItemsFromFile()).ToList();
            var updateCard = deserializedCards.Find(card => card.Id == id);
            deserializedCards.Remove(updateCard);

            if (image != null) cardDto.Image = await image.ToBytesArray();
            else cardDto.Image = Convert.FromBase64String(updateCard.ImageBase64String);

            updateCard = _mapper.Map<CardDto, Card>(cardDto);

            deserializedCards.Add(updateCard);
            if (await _serializer.SerializeItemsToFile(deserializedCards)) return Ok();
            return BadRequest("Error occured while trying to update the card");
        }
    }
}
