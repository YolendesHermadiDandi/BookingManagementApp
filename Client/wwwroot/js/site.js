$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
    //success: (result) => {
    //    console.log(result);
    //}
}).done((result) => {
    console.log(result)
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
        $(".modal-title").html(res.name);
    })
}