import { CategoryItem } from "./category-item";

export class Category {
  categoryId!: number;
  name!: string;
  categoryItems: Array<CategoryItem> = [];
  totalValue: number | undefined;
}
