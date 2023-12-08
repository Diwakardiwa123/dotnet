import { TestBed } from '@angular/core/testing';
import { ShimmerComponent } from './shimmer.component';
describe('ShimmerComponent', () => {
    let component;
    let fixture;
    beforeEach(() => {
        TestBed.configureTestingModule({
            declarations: [ShimmerComponent]
        });
        fixture = TestBed.createComponent(ShimmerComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=shimmer.component.spec.js.map