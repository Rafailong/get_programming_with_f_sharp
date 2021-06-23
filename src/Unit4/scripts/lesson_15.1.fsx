type FootballResult =
    { HomeTeam: string
      AwayTeam: string
      HomeGoals: int
      AwayGoals: int }

let create (ht, hg) (at, ag) =
    { HomeTeam = ht
      AwayTeam = at
      HomeGoals = hg
      AwayGoals = ag }

let results =
    [ create ("Messiville", 1) ("Ronaldo City", 2)
      create ("Messiville", 1) ("Bale Town", 3)
      create ("Bale Town", 3) ("Ronaldo City", 1)
      create ("Bale Town", 2) ("Messiville", 1)
      create ("Ronaldo City", 4) ("Messiville", 2)
      create ("Ronaldo City", 1) ("Bale Town", 2) ]

let f =
    fun acc r ->
        if r.AwayGoals > r.HomeGoals then
            if Map.containsKey r.AwayTeam acc then
                Map.change r.AwayTeam (Option.map (fun n -> n + 1)) acc
            else
                Map.add r.AwayTeam 1 acc
        else
            acc

let teamsThatWonTheMostAwayGames =
    List.fold f Map.empty<string, int> results

Map.iter
    (fun t w -> printfn "%s: %d wins" t w)
    teamsThatWonTheMostAwayGames

// let xs = query {
//   for r in results do
//   where (r.AwayGoals > r.HomeGoals)
//   groupBy r.AwayTeam into g
//   select g
// }
// Seq.iter (fun x -> printfn "%A" x) xs