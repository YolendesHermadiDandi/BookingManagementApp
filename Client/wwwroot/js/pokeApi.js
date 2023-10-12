$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
}).done((result) => {
    let temp = "";
    $.each(result.results, (key, val) => {
        temp += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td><button type="button" onclick="detail('${val.url}')" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalPoke">Detail</button></td>
                </tr >`;
    });
    $("#tbodyPoke").html(temp);


}).fail((error) => {
    console.log(error);
})

function detail(stringUrl) {
    $.ajax({
        url: stringUrl

    }).done((res) => {
        let temp = `<div class="carousel-item active">
                        <div class="d-flex justify-content-center">
                            <img src="${res.sprites.other.dream_world.front_default}" class="d-block w-50" alt="...">
                        </div>
                    </div>
                    <div class="carousel-item">
                        <div class="d-flex justify-content-center">
                            <img src="${res.sprites.other.home.front_default}" class="d-block w-50" alt="...">
                        </div>
                    </div>
                    <div class="carousel-item">
                        <div class="d-flex justify-content-center">
                            <img src="${res.sprites.other['official-artwork'].front_default}" class="d-block w-50" alt="...">
                        </div>
                    </div>`;

        let types = "";
        $.each(res.types, (key, val) => {
            types += `<h6 class="d-inline text-white ${val.type.name} mx-1">${val.type.name}</h6>`;
        });

        $(".modal-title").html(res.name);
        $(".types").html(types);
        $(".carousel-inner").html(temp);


        //abilities
        let abilities = "";
        $.each(res.abilities, (key, val) => {
            abilities += `<li class="list-group-item">${val.ability.name}</li>`;
        });
        $(".list-group").html(abilities);


        //stats & stats name
        let statsDetail = "";
        let total = 0;
        $.each(res.stats, (key, val) => {
            let emot = "";
            total += val.base_stat;
            switch (val.stat.name) {
                case "hp":
                    emot = "❤";
                    break;
                case "attack":
                    emot = "⚔";
                    break;
                case "defense":
                    emot = "🛡";
                    break;
                case "speed":
                    emot = "💨 wahss";
                //    break;
                //case "special-defense":
                //    emot = "🛡";
                //    break;
                default:
                    break;
            };
            statsDetail += ` <div class="col-sm-5">
                                <h6>${val.stat.name + " " + emot}</h6>
                              </div>
                              <div class="col-sm-7">
                                <div class="progress">
                                    <div class="progress-bar ${val.stat.name}" role="progressbar" style="width: ${val.base_stat}%" aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="${val.base_stat}">${val.base_stat}</div>
                                </div>
                              </div>`;
        });
        //total = `<h6 class="card-title">Total : ${total}</h6>`;
        $(".stats-detail").html(statsDetail + "" + total);
    });
}
