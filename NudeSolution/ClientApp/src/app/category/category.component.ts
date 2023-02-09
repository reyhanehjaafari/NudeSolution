import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { CategoryItem } from '../model/category-item';
import { CatrgoryResult } from '../model/category-result';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  public catrgoryResult: CatrgoryResult = new CatrgoryResult();
  faTrash = faTrash;
  public categoryItems: any;
  selectedCategoryItem: CategoryItem = new CategoryItem();


  constructor(private http: HttpClient) { }

  ngOnInit(): void {

    this.getAll();

    this.http.get(`${environment.BASE_URL}/getcategories`).subscribe(result => {
      this.categoryItems = result;
    }, error => console.error(error));

  }


  remove(categoryItem: CategoryItem) {
    this.http.delete<CatrgoryResult>(`${environment.BASE_URL}/deleteCategoryItem?categoryItemId=${categoryItem.categoryItemId}`).subscribe(result => {
      this.getAll();
    }, error => console.error(error));
  }

  getAll() {
    this.http.get<CatrgoryResult>(`${environment.BASE_URL}/getAll`).subscribe(result => {
      this.catrgoryResult = result;
    }, error => console.error(error));
  }


  add() {
    this.http.post(`${environment.BASE_URL}/CreateCategoryItem`, this.selectedCategoryItem).subscribe(result => {
      this.selectedCategoryItem = new CategoryItem();
      this.getAll();
    }, error => console.error(error));
  }

}
