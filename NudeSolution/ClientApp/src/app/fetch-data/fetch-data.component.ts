import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {

  public catrgoryResult: CatrgoryResult = new CatrgoryResult();

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

  }
  ngOnInit(): void {

    this.http.get<CatrgoryResult>("https://localhost:7067/getAll").subscribe(result => {

      this.catrgoryResult = result;

    }, error => console.error(error));
  }
}

interface CategoryItem {
  categoryItemId: number,
  name: string,
  value: number,
  categoryId: number,
}

interface Category {
  categoryId: number,
  name: string,
  categoryItems: Array<CategoryItem>;
  totalValue: number
}
class CatrgoryResult {

  categories!: Array<Category>;
  categoriesTotalValue!: number;

}
